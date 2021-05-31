<template>
    <div class="page">
        <div id=storagemenu>
        <select v-model="currentstorage" @change="showstoragedata">
          <option selected disabled value="0">Выберите склад</option>
          <option v-for="storage in storages" :key="storage.id" v-bind:value="storage">{{storage.name}}</option>
        </select>
        <input type="date" @change="showstoragedata" v-model="date" />
        </div>
        <AddTransferForm @updatestorage="showstoragedata" v-if="storagefound" v-bind:storagesobj="storages" v-bind:currentstorageobj="currentstorage"></AddTransferForm>
        <p v-if="error">{{error}}</p>
        <p v-else-if="loading">Идёт загрузка...</p>
        <table v-else-if="products.length">
          <tr>
            <th>Номер</th>
            <th>Название</th>
            <th>количество</th>
          </tr>
          <StorageProduct v-for="product in products" :key="product.id" v-bind:productobj="product"></StorageProduct>
        </table>
    </div>
</template>

<script>
import axios from 'axios';
import StorageProduct from './StorageProduct.vue';
import AddTransferForm from './AddTransferForm.vue';
export default {
  components: { StorageProduct, AddTransferForm },
  name: 'storage',
  data () {
    return {
      storages: [],
      currentstorage: 0,
      date: "",
      products: [],
      error: '',
      loading: false,
      storagefound: false
    }
  },
  methods: {
      loadstorages: function() {
          axios.get('https://localhost:5001/api/storages').then((response) => {
                this.storages = response.data;
              })
      },
      showstoragedata: function() {
          if (!this.date) this.error = "Укажите дату!"
          else if (new Date(this.date) > new Date()) this.error = "Некоректная дата!"
          else {
              this.error = '';
              this.loading = true;
              var th = this;
              axios.get('https://localhost:5001/api/storages/'+this.currentstorage.id, {params: {datetime: this.date}}).then((response) => {
                if (response.data.error == null) {
                    th.loading = false;
                    th.storagefound = true;
                    th.products = response.data.products;
                }
                else {
                    th.error = response.data.error;
                }
              })
          }
      }
  },
  created: function(){
    this.date = new Date();
    this.date = this.date.getFullYear()+"-"+String(this.date.getMonth() + 1).padStart(2, "0")+"-"+String(this.date.getDate()).padStart(2, "0");
    this.loadstorages();
  }
}
</script>
 
<style>
table, th, td{
  border: 1px solid black;
  border-collapse: collapse;
}
#storagemenu {
  margin-bottom: 4px;
}
</style>