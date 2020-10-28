import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl('http://localhost:5001/tictactoehub')
      .configureLogging(LogLevel.Information)
      .build()  

      const tictactoeHub = new Vue() 

      Vue.prototype.$tictactoeHub = tictactoeHub

      connection.on('OpponentTurnEnd', (x, y, player) => {
        tictactoeHub.$emit('opponent-turn-end', {x, y, player})
      })

      tictactoeHub.turnChange = (x, y, player) =>{
        return startedPromise
        .then(() => connection.invoke('TurnChange', x, y, player))
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