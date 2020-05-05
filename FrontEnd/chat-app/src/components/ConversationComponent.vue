<template>
  <v-card max-width="450" class="mx-auto">
    <v-list >
      <v-list-item-group v-model="Conversation" color="indigo">
        <v-list-item
          v-for="conversation in Conversations"
          :key="conversation.conversationId"
          @click.stop="clickOnConversation(conversation.conversationId, conversation.email)"
        >
          <v-list-item-content>
            <v-list-item-title v-text="conversation.email"></v-list-item-title>
          </v-list-item-content>

          <v-list-item-icon>
            <v-icon :color="'deep-purple accent-4'">V</v-icon>
          </v-list-item-icon>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-card>
</template>

<script>
export default {
  name: "ConversationComponent",
  data: () => {
    return {
      Conversations: [],
      Conversation: ""
    };
  },
  created() {
    var myHeaders = new Headers();
    let token = localStorage.getItem("token");
    let email = localStorage.getItem("email");
    myHeaders.append("Authorization", "bearer " + token);
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({ email: email });

    var requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: raw,
      redirect: "follow"
    };

    let url =
      "https://localhost:5001/api/Conversation/GetAllConversationsOfAUser";

    fetch(url, requestOptions)
      .then(response => response.json())
      .then(result => {
        for (let index = 0; index < result.length; index++) {
          const element = result[index];
          this.fetchConverstationInformation(element.conversationId);
        }
      });
  },
  methods: {
    fetchConverstationInformation(conversationId) {
      var myHeaders = new Headers();
      let token = localStorage.getItem("token");
      let email = localStorage.getItem("email");

      myHeaders.append("Authorization", "bearer " + token);
      myHeaders.append("Content-Type", "application/json");

      var data = JSON.stringify({
        ConversationID: conversationId,
        EmailOfCurrentUser: email
      });

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: data,
        redirect: "follow"
      };

      let url =
        "https://localhost:5001/api/Conversation/GetConversationInformationById";

      fetch(url, requestOptions)
        .then(response => response.json())
        .then(result => {
          this.Conversations.push(result);
        });
    },
    clickOnConversation(conversationId, email) {
      sessionStorage.setItem("conversationId", conversationId)
      sessionStorage.setItem("conversationReceiverEmail", email)
      this.$router.push('/conversation')
    }
  }
};
</script>

<style>
</style>