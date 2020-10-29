import { HubConnectionBuilder, HttpTransportType, LogLevel } from '@aspnet/signalr'

export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/tictactoehub', {
        skipNegotiation:true,
        transport: HttpTransportType.WebSockets
      })
      .configureLogging(LogLevel.Information)
      .build()  

      const tictactoeHub = new Vue() 

      Vue.prototype.$tictactoeHub = tictactoeHub

      connection.on('TurnChange', (x, y, player) => {
        tictactoeHub.$emit('opponent-turn-end', {x, y, player})
      })

      connection.on('Victory', (player) => {
        tictactoeHub.$emit('who-won', {player})
      })

      tictactoeHub.turnChange = (x, y, player) =>{
        return startedPromise
        .then(() => connection.invoke('TurnChange', x, y, player))
        .catch(console.error)
      }

      tictactoeHub.victory = (player) =>{
        return startedPromise
        .then(() => connection.invoke('Victory', player))
        .catch(console.error)
      }

      let startedPromise = null
      function start () {
        startedPromise = connection.start().catch(err => {
          console.error('Failed to connect with hub', err)
          return new Promise((resolve, reject) => 
          setTimeout(() => start().then(resolve).catch(reject), 20000))
        })
        return startedPromise
      }
      connection.onclose(() => start())
      
      start()
  }
}