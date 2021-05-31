<template>
  <div class="navmenu">
    <button @click="goto('#storages')">Склады</button>
    <button @click="goto('#products')">Товары</button>
    <button @click="goto('#transfers')">Перевозки</button>
  </div>
</template>
 
<script>
export default {
  name: 'navmenu',
  data () {
    return {
    }
  },
  methods: {
      goto: function(str) {
          location.hash = str;
          this.$emit('change', str);
      }
  }
}
</script>
 
<style>
.navmenu {
  background-color: lightgray;
  padding: 10px;
}
</style>