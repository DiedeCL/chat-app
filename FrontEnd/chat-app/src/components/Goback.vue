<template>
  <v-btn large depressed color="primary" @click="goOnePageBack">⇦</v-btn>
</template>

<script>
export default {
    name: "GoBack",
    methods:{
        goOnePageBack(){
            window.history.back();
        }
    }

}
</script>

<style>

</style>