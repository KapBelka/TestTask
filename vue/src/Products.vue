<template>
    <div class="page">
        <button @click="update">Обновить</button>
        <p v-if="loading">Идёт загрузка...</p>
        <table v-else-if="products.length">
          <tr>
            <th>Номер</th>
            <th>Название</th>
          </tr>
        <Product v-for="product in products" :key="product.id" v-bind:productobj="product"></Product>
        </table>
    </div>
</template>

<script>
import axios from 'axios'
import Product from './Product.vue'
export default {
  components: { Product },
  name: 'products',
  data () {
    return {
      products: [],
      loading: false,
    }
  },
  methods: {
      update: function() {
          this.loading = true;
          var th = this;
          axios.get('https://localhost:5001/api/products').then((response) => {
             this.loading = false;
             th.products = response.data;
          })
      }
  },
  created: function(){
      this.update();
  }
}
</script>
 
<style>
</style>