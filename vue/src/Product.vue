<template>
    <tr>
        <td>{{productobj.id}}</td>
        <td>{{productobj.name}}</td>
    </tr>
</template>

<script>
export default {
  name: 'product',
  props: ["productobj"]
}
</script>
 
<style>
</style>