import {
  HubConnectionBuilder,
  HttpTransportType,
  LogLevel
} from "@aspnet/signalr";

export default {
  install(Vue) {
    //configuring connection with the lobby hub
    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:5001/tictactoehub", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .configureLogging(LogLevel.Information)
      .build();

    const tictactoeHub = new Vue();

    Vue.prototype.$tictactoeHub = tictactoeHub;

    //use of hub methods
    connection.on("TurnChange", (x, y, player, lobbyId) => {
      tictactoeHub.$emit("opponent-turn-end", { x, y, player, lobbyId });
    });

    connection.on("Victory", (player, lobbyId) => {
      tictactoeHub.$emit("who-won", { player, lobbyId });
    });

    tictactoeHub.turnChange = (x, y, player, lobbyId) => {
      return startedPromise
        .then(() => connection.invoke("TurnChange", x, y, player, lobbyId))
        .catch(console.error);
    };

    tictactoeHub.victory = (player, lobbyId) => {
      return startedPromise
        .then(() => connection.invoke("Victory", player, lobbyId))
        .catch(console.error);
    };

    //try reconeccting if connection failed
    let startedPromise = null;
    function start() {
      startedPromise = connection.start().catch(err => {
        console.error("Failed to connect with hub", err);
        return new Promise((resolve, reject) =>
          setTimeout(
            () =>
              start()
                .then(resolve)
                .catch(reject),
            20000
          )
        );
      });
      return startedPromise;
    }
    connection.onclose(() => start());

    start();
  }
};
