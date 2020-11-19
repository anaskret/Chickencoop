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
    

     lobbyHub.newPlayer = (lobbyId, playerId) =>{
      return startedPromise
      .then(() => connection.invoke('NewPlayer', lobbyId, playerId))
      .catch(console.error)
      }

      connection.on('NewPlayer', (lobbyId, playerId) => {
        lobbyHub.$emit('new-player', lobbyId, playerId)
      })

      lobbyHub.hostChange = (lobbyId) =>{
        return startedPromise
        .then(() => connection.invoke('HostChange', lobbyId))
        .catch(console.error)
        }
  
        connection.on('HostChange', (lobbyId) => {
          lobbyHub.$emit('host-change', lobbyId)
        })

        lobbyHub.newGame = (lobbyId) =>{
          return startedPromise
          .then(() => connection.invoke('NewGame', lobbyId))
          .catch(console.error)
          }
    
        connection.on('NewGame', (lobbyId) => {
          lobbyHub.$emit('new-game', lobbyId)
        })

        lobbyHub.opponentLeft = (lobbyId) =>{
          return startedPromise
          .then(() => connection.invoke('OpponentLeft', lobbyId))
          .catch(console.error)
          }
    
        connection.on('OpponentLeft', (lobbyId) => {
          lobbyHub.$emit('opponent-left', lobbyId)
        })

        lobbyHub.gameAccepted = (lobbyId) =>{
          return startedPromise
          .then(() => connection.invoke('GameAccepted', lobbyId))
          .catch(console.error)
          }
    
        connection.on('GameAccepted', (lobbyId) => {
          lobbyHub.$emit('game-accepted', lobbyId)
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