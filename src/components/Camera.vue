<template>
    <div class="camera">
        <!-- <video v-if="cameraIsOpen" autoplay class="feed"></video> -->
        <button @click="cameraIsOpen = !cameraIsOpen">Fechar câmera</button>
        <button @click="sendMessage('Opa, toma aí a minha mensagem!')">Enviar mensagem</button>
    </div>
</template>

<script>
export default {
  name: 'Camera',
  data() {
    return {
      cameraIsOpen: true,
      connection: null,
      serverURL: `ws://127.0.0.1:8000/live`,
      webcamFrame: 'Olar!!'
    }
  },
  computed: {
    
  },
  created() {
    },
  beforeMount() {
    this.connectToServer()
    //this.init()
    this.interval = setInterval(() => this.sendNewFrame(), 1000);
  },
  methods: {
    init() {
      if ('mediaDevices' in navigator && 'getUserMedia' in navigator.mediaDevices) {
        navigator.mediaDevices.getUserMedia({ video: 'video' })
          .then(stream => {
            const videoPlayer = document.querySelector('video')
            videoPlayer.srcObject = stream
            videoPlayer.play()
          })
      }
    },
    connectToServer() {
      console.log('Estabelecendo conexão com o servidor')
      this.connection = new WebSocket(this.serverURL)
      this.connection.onopen = (event) => {
        console.log(event)
        console.log('Successfuly connected to the server!')
      }
      this.connection.onmessage = (event) => {
        console.log(event)
      }
    },
    getConstraints() {
      return {
        video: {
          width: {
            min: 640,
            ideal: 1280,
            max: 1920
          },
          height: {
            min: 360,
            ideal: 720,
            max: 1080
          }
        }
      }
    },
    closeCamera() {

    },
    sendNewFrame() {
      console.log(this.connection)
      this.connection.send(this.webcamFrame)
    }
  }
}
</script>

<style scoped>
.camera {
  align-content: center;
  box-sizing: content-box;
}

.feed {
  display: block;
  margin: 0 auto;
  max-height: 720px !important;
  background-color: #202020;
  box-shadow: 6px 6px 12px 0px rgba(0, 0, 0, 0.25);
}
</style>