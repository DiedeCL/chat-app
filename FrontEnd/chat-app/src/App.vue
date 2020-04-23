<template>
  <v-app>
    <v-app-bar
      app
      flat
      color="primary"
      dark
      elevate-on-scroll
      
    >
        <v-toolbar-title> <v-btn text to="/" >Chat App</v-btn></v-toolbar-title>
        
        
    </v-app-bar>
     <v-content>
    <v-container fluid>

      <router-view></router-view>
    </v-container>
  </v-content>

  <v-footer app>
    Copright groep GHI_PQR 18
  </v-footer>
  </v-app>
</template>

<script>

export default {
  name: 'App',

  components: {
  },

  data: () => ({
    
  }),
};
</script>
<style>
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}
</style>
