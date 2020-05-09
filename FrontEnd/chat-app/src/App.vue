<template>
  <v-app>
    <v-app-bar app flat color="primary" dark elevate-on-scroll>
      <v-toolbar-title>
        <v-container v-if="LogedInNeeded">
          <v-btn text to="/">Chat App</v-btn>
        </v-container>
        <div id="wrapperOfEmail" v-else>
        <v-btn  text to="/conversations">Chat App</v-btn>
        <p id="email">{{email}} </p>
        </div>
      </v-toolbar-title>
    </v-app-bar>
    <v-content>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-content>

    <v-footer app>Copright groep GHI_PQR 18</v-footer>
  </v-app>
</template>

<script>
export default {
  name: "App",

  components: {},

  data: () => ({
    LogedInNeeded: true,
    email:''
  }),
  updated() {
    // Fired every second
    this.LogedInNeeded = !localStorage.getItem("token");
    this.email = localStorage.getItem('email')
  }
};
</script>
<style>
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}
#email{
  float: right;
}
#wrapperOfEmail{
  width: 94vw;
}
</style>
