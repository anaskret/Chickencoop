import { HubConnectionBuilder, HttpTransportType, LogLevel } from '@aspnet/signalr'

export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/lobbyhub', {
        skipNegotiation:true,
        transport: HttpTransportType.WebSockets
      })
      .configureLogging(LogLevel.Information)
      .build() 

      const lobbyHub = new Vue() 

      Vue.prototype.$lobbyHub = lobbyHub

      
    
      lobbyHub.joinLobby = (id) =>{
        return startedPromise
        .then(() => connection.invoke('JoinLobby', id))
        .catch(console.error)
      }
      
      lobbyHub.leaveLobby = (id) =>{
        return startedPromise
        .then(() => connection.invoke('LeaveLobby', id))
        .catch(console.error)
      }

      lobbyHub.lobbyChange = () =>{
        return startedPromise
        .then(() => connection.invoke('LobbyChange'))
        .catch(console.error)
      }
      
     connection.on('LobbyChange', () => {
        lobbyHub.$emit('lobby-change')
     })
    
      connection.on('NewPlayer', (playerId, lobbyId) => {
        lobbyHub.$emit('new-player', {playerId, lobbyId})
      })

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