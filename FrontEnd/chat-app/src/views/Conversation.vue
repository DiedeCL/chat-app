<template>
  <v-container>
    <GoBack />
    <v-card class="mx-auto" max-width="800" outlined raised>
      <v-btn to="/Files">Files</v-btn>
      <Messages />
      <v-container class="container">
        <v-text-field v-model="contentOfMessage" outlined placeholder="Send message"></v-text-field>
        <v-btn color="primary" left @click="sendMessage">Send Message</v-btn>
        <form method="post" enctype="multipart/form-data">
          <input type="file" name="file" id="file" />
          <button type="button" @click="UploadFile">Upload</button>
        </form>
      </v-container>
    </v-card>
  </v-container>
</template>

<script>
import Messages from "@/components/Messages.vue";
import GoBack from "@/components/Goback.vue";
export default {
  name: "Conversation",
  data() {
    return {
      contentOfMessage: ""
    };
  },
  components: {
    Messages,
    GoBack
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
    },
    UploadFile(e) {
      e.preventDefault();
      const files = document.querySelector("[type=file]").files;
      const formData = new FormData();
      formData.append("file", files[0]);
      

      let myHeaders = new Headers();
      let token = localStorage.getItem("token");

      myHeaders.append("Authorization", "bearer " + token);

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: formData,
        redirect: "follow"
      };

      fetch(
        "https://localhost:5001/api/Conversation/UploadFile",
        requestOptions
      )
        .then(response => response.text())
        .then(result => {
          
          this.storeFile(result)})
        .catch(error => console.log("error", error));
    },
    storeFile(filePath) {
      let myHeaders = new Headers();
      let token = localStorage.getItem("token");
      let conversationId = sessionStorage.getItem("conversationId");

      myHeaders.append("Authorization", "bearer " + token);
      myHeaders.append("Content-Type", "application/json");
      
     
      var data = JSON.stringify({
        ConversationId: parseInt(conversationId),
        filePath: filePath
      });
      
      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: data,
        redirect: "follow"
      };
      fetch("https://localhost:5001/api/Conversation/StoreFile", requestOptions)
        .then(() => this.sendFile())
        .catch(error => console.log("error", error));
    },
    sendFile() {
      let email = localStorage.getItem("email");
      let ReceiverEmail = sessionStorage.getItem("conversationReceiverEmail");
      let conversationId = sessionStorage.getItem("conversationId");
      let message = email + " heeft een bestand gestuurd";
      var raw = JSON.stringify({
        ReceiverEmail: ReceiverEmail,
        SenderEmail: email,
        Message: message,
        ConversationId: parseInt(conversationId)
      });
      let myHeaders = new Headers();
      let token = localStorage.getItem("token");
      myHeaders.append("Content-Type", "application/json");

      myHeaders.append("Authorization", "bearer " + token);

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow"
      };
      fetch("https://localhost:5001/api/Conversation/SendFile", requestOptions)
        .then(response => response.text())
        .then(() => location.reload()
        )
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