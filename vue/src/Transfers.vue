<template>
    <div class="page">
        <button @click="update">Обновить</button>
        <p v-if="loading">Идёт загрузка...</p>
        <table class="table-primary" v-else-if="transfers.length">
          <tr>
            <th>Номер</th>
            <th>Дата</th>
            <th>Откуда</th>
            <th>Куда</th>
          </tr>
        <Transfer v-for="(transfer, index) in transfers" :key="index" v-bind:transferobj="transfer" v-bind:index="index" @delete="deletetransfer"></Transfer>
        </table>
    </div>
</template>

<script>
import axios from 'axios'
import Transfer from './Transfer.vue'
export default {
  components: { Transfer },
  name: 'transfers',
  data () {
    return {
      transfers: [],
      loading: false
    }
  },
  methods: {
      update: function() {
          this.loading = true;
          var th = this;
          axios.get('https://localhost:5001/api/transfers').then((response) => {
             this.loading = false;
             th.transfers = response.data;
          })
      },
      deletetransfer: function(index) {
          axios.delete('https://localhost:5001/api/transfers/'+this.transfers[index].id)
          this.transfers.splice(index, 1);
      }
  },
  created: function(){
      this.update();
  }
}
</script>
 
<style>

</style>