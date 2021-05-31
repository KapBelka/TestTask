<template>
    <tr>
        <td></td>
        <td>{{productobj.id}}</td>
        <td>{{productobj.name}}</td>
        <td>{{productobj.count}}</td>
    </tr>
</template>

<script>
export default {
  name: 'storageproduct',
  props: ["productobj"]
}
</script>
 
<style>
</style>