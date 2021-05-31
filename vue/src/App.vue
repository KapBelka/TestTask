<template>
  <div>
    <Navmenu @change="changepath"></Navmenu>
    <Storage v-if="path=='#storages'"></Storage>
    <Products v-else-if="path=='#products'"></Products>
    <Transfers v-else-if="path=='#transfers'"></Transfers>
  </div>
</template>
 
<script>
import Navmenu from './Navmenu.vue'
import Storage from './Storage.vue'
import Products from './Products.vue'
import Transfers from './Transfers.vue'
export default {
  components: { Navmenu, Storage, Products, Transfers },
  name: 'app',
  data () {
    return {
      path: location.hash
    }
  },
  methods: {
      changepath: function(path) {
          this.path = path
      }
  }
}
</script>
 
<style>
body {
  margin: 0;
  padding: 0;
}
.page {
  margin: 10px;
}
</style>