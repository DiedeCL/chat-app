<template>
  <v-container>
      <GoBack/>
      <Files/>
  </v-container>
</template>

<script>
import GoBack from "@/components/Goback.vue";
import Files from "@/components/Files.vue";
export default {
    name: "FilesView",
    components: {
        GoBack,
        Files
    }

}
</script>

<style>

</style>