<template>
  <v-card class="mx-auto" max-width="800" outlined raised>
    <Messages />
    <v-container class="container">
      <v-text-field v-model="contentOfMessage" outlined></v-text-field>
      <v-btn color="primary" left @click="sendMessage">Send Message</v-btn>
    </v-container>
  </v-card>
</template>

<script>
import Messages from "@/components/Messages.vue";
export default {
  name: "Conversation",
  data() {
    return {
      contentOfMessage: ""
    };
  },
  components: {
    Messages
  },
  methods: {
    sendMessage() {
      let myHeaders = new Headers();
      let token = localStorage.getItem("token");
      let email = localStorage.getItem("email");
      let ReceiverEmail = sessionStorage.getItem("conversationReceiverEmail");
    let conversationId = sessionStorage.getItem("conversationId");
      myHeaders.append("Authorization", "bearer " + token);
      myHeaders.append("Content-Type", "application/json");

      var raw = JSON.stringify({
        ReceiverEmail: ReceiverEmail,
        SenderEmail: email,
        Message: this.contentOfMessage,
        ConversationId: parseInt(conversationId)
      });

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow"
      };

      fetch(
        "https://localhost:5001/api/Conversation/SendMessage",
        requestOptions
      )
        .then(response => response.text())
        .then(() => location.reload())
        .catch(error => console.log("error", error));
    }
  }
};
</script>

<style scoped>
.container {
  margin-top: 20px;
}
</style>