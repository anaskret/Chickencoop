import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl('http://localhost:5100/tictactoehub')
      .configureLogging(LogLevel.Information)
      .build()
 
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