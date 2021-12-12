<template>
    <div class="camera">
        <video v-if="cameraIsOpen" autoplay class="feed"></video>
        <canvas v-if="cameraIsOpen" class="d-none" id="canvas" />
        <img v-else src="@/assets/no-video.png" class="no-video-img">
        <br>
        <button v-if="webcam" @click="toggleWebcam">{{ buttonTitle }}</button>
        <button v-else>{{ buttonTitle }}</button>
    </div>
</template>

<script>
export default {
  name: 'Camera',
  data() {
    return {
      cameraIsOpen: true,
      webcam: null,
      connection: null,
      wsReady: false,
      targetFPS: 30,
      serverURL: `ws://127.0.0.1:8000/live`,
    }
  },
  computed: {
    buttonTitle() {
      return this.cameraIsOpen ? 'Fechar câmera' : 'Abrir câmera'
    }
  },
  created() {
    this.connectToServer()
    this.interval = setInterval(() => this.sendNewFrame(), 1000 / this.targetFPS);
  },
  beforeMount() {
    this.openWebcam()
  },
  methods: {
    openWebcam() {
      if ('mediaDevices' in navigator && 'getUserMedia' in navigator.mediaDevices) {
        navigator.mediaDevices.getUserMedia(this.getConstraints())
        .then(stream => {
          this.webcam = document.querySelector('video')
          this.webcam.srcObject = stream
          this.webcam.play()
          this.cameraIsOpen = true
        })
      }
    },
    closeWebcam() {
      this.webcam.srcObject.getTracks().forEach(track => { track.stop() });
    },
    toggleWebcam() {
      if (this.cameraIsOpen) this.closeWebcam()
      else this.openWebcam()
      this.cameraIsOpen = !this.cameraIsOpen
    },
    connectToServer() {
      console.log('Estabelecendo conexão com o servidor')
      this.connection = new WebSocket(this.serverURL)
      this.connection.onopen = (event) => {
        this.wsReady = true
        console.log(event)
        console.log('Successfuly connected to the server!')
      }
      this.connection.onmessage = (event) => {
        console.log(event)
      }
      this.connection.onerror = function (error) {
        this.wsReady = false;
        console.log('[ERROR]', error)
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
    sendNewFrame() {
      if (this.wsReady && this.cameraIsOpen) {
        const payload = this.getVideoFrame()
        this.connection.send(payload)
      }
    },
    getVideoFrame() {
      const ratio = (window.innerHeight < window.innerWidth) ? 16 / 9 : 9 / 16 
      const picture = document.querySelector('canvas')
      picture.width = (window.innerWidth < 1280) ? window.innerWidth : 1280
      picture.height = picture.width / ratio

      const context = picture.getContext('2d')
      context.drawImage(document.querySelector('video'), 0, 0, picture.width, picture.height)
      const dataUrl = picture.toDataURL('image/png')
      return dataUrl
    }
  }
}
</script>

<style scoped>
.camera {
  align-content: center;
  box-sizing: content-box;
}

.no-video-img {
  height: 480px;
}

.feed {
  display: block;
  margin: 0 auto;
  max-height: 480px !important;
  background-color: #202020;
  box-shadow: 6px 6px 12px 0px rgba(0, 0, 0, 0.25);
}
</style>