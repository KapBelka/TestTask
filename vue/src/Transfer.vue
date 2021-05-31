<template>
    <tbody class="bordered">
        <tr>
            <td>{{transferobj.id}}</td>
            <td>{{transferobj.date}}</td>
            <td>{{transferobj.fromstorage.name}}</td>
            <td>{{transferobj.tostorage.name}}</td>
            <td><button @click="deletetransfer">Удалить</button></td>
        </tr>
        <tr>
            <th></th>
            <th>Номер</th>
            <th>Название</th>
            <th>Количество</th>
        </tr>
        <TransferProduct v-for="product in transferobj.products" :key="product.id" v-bind:productobj="product"></TransferProduct>
    </tbody>
</template>

<script>
import TransferProduct from './TransferProduct.vue';
export default {
  components: { TransferProduct },
  name: 'transfer',
  props: ["transferobj", "index"],
  methods: {
      deletetransfer: function(){
          this.$emit('delete', this.index);
      }
  }
}
</script>
 
<style>
.bordered {
    border: 2px solid black;
}
</style>