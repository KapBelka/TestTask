<template>
  <div>
  <div id="form">
      <select v-model="currentstorage">
          <option v-for="storage in storages" :key="storage.id" v-bind:value="storage">{{storage.name}}</option>
      </select><br>
      <hr>
      <div>
        <select v-model="productid">
            <option v-for="(product, index) in productsforselect" :key="index" v-bind:value="index">{{product.name}}</option>
        </select>
        <input type="number" v-model.number="count" required="true" min=1 placeholder="Введите количество" />
        <button @click="addproduct">Добавить продукт</button>
      </div>
      <table v-if="products.length">
          <tr>
            <th>Номер</th>
            <th>Название</th>
            <th>количество</th>
          </tr>
          <StorageProduct v-for="product in products" :key="product.id" v-bind:productobj="product"></StorageProduct>
      </table>
      <div id="addbutton">
      <button @click="addtransfer">Добавить данные о перевозке</button>
      <button @click="resetproducts">Сбросить</button>
      </div>
      {{ error }}
  </div>
  </div>
</template>
 
<script>
import axios from 'axios'
import StorageProduct from './StorageProduct.vue'
export default {
  components: { StorageProduct },
  name: 'addtransferform',
  props: ["storagesobj", "currentstorageobj"],
  data () {
    return {
        productsforselect: [],
        storages: this.storagesobj.slice(),
        products: [],
        error: "",
        currentstorage: "",
        productid: 0,
        fromstorageid: "",
        tostorageid: "",
        count: "",
    }
  },
  watch: {
    currentstorageobj: function () {
      this.storages = this.storagesobj.slice();
      this.storages.splice(this.storages.indexOf(this.currentstorageobj), 1);
      this.currentstorage = this.storages[0];
    }
  },
  methods: {
      loadproducts: function(){
          var th = this;
          axios.get('https://localhost:5001/api/products').then((response) => {
             th.productsforselect = response.data;
          })
      },
      addproduct: function(){
          if (this.count) {
          for (var p of this.products) {
            if (p.id == this.productsforselect[parseInt(this.productid)].id) {
              p.count += this.count;
              return;
            }
          }
          this.products.push({ id: this.productsforselect[parseInt(this.productid)].id, name: this.productsforselect[parseInt(this.productid)].name, count: this.count });
          }
      },
      resetproducts: function(){
        this.products = [];
        this.error = "";
        this.count = "";
      },
      addtransfer: function(){
          this.error = "";
          var th = this;
          axios.post('https://localhost:5001/api/transfers', { fromstorageid: this.currentstorageobj.id, tostorageid: this.currentstorage.id, products: this.products}).then((response) => {
              if (response.data.error == null) {
                th.products = [];
                th.currentstorage = th.storages[0];
                th.productid = 0;
                th.count = "";
                this.$emit('updatestorage');
              }
              else {
                this.error = response.data.error;
              }
          })
      }
  },
  created: function() {
      this.loadproducts();
      this.storages.splice(this.storages.indexOf(this.currentstorageobj), 1);
      this.currentstorage = this.storages[0];
  }
}
</script>
 
<style>
#form {
  display: inline-block;
  border: 1px solid black;
  margin-bottom: 4px;
  padding: 2px;
}
</style>