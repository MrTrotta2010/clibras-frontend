<template>
  <div id="app">
    <div class="content">
      <div class="header-div">
        <span class="clibras-title">C-Libras</span>
      </div>
      <camera />
      <div class="footer-div">
        <span class="clibras-title">...</span>
      </div>
    </div>
  </div>
</template>

<script>
import camera from '@/components/Camera'

export default {
  name: 'App',
  components: {
    camera
  },
  data() {
    return {
      videoActive: false
    }
  }
}
</script>

<style scoped>
.clibras-title {
  font-size: 40px;
  font-weight: 500;
}

.content {
  position: absolute;
  left: 50%;
  top: 50%;
  -webkit-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
  width: 80%;
  background-color: gainsboro;
  border-radius: 20px;
  text-align: center;
  align-content: center;
}

.header-div {
  padding-top: 10px;
  padding-bottom: 10px;
  border-radius: 20px 20px 0 0;
  background-color: rgb(136, 201, 255);
}

.footer-div {
  padding-top: 10px;
  padding-bottom: 10px;
  border-radius: 0px 0px 20px 20px;
  background-color: rgb(136, 201, 255);
}
</style>
