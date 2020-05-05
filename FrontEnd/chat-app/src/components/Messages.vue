<template>
  <v-card max-width="450" class="mx-auto">
    <v-list three-line>
      <template v-for="message in messages">
        <v-list-item :key="message.MessageId">
          <v-list-item-content>
            <v-list-item-title v-html="message.emailOfSender"></v-list-item-title>
            <v-container v-if="message.dataIsTrusted">
              <v-list-item-subtitle v-html="message.content" id="green"></v-list-item-subtitle>
            </v-container>
            <v-container v-else>
              <v-list-item-subtitle v-html="message.content" id="red"></v-list-item-subtitle>
            </v-container>
          </v-list-item-content>
        </v-list-item>
      </template>
    </v-list>
  </v-card>
</template>

<script>
export default {
  name: "Messages",
  data() {
    return {
      messages: []
    };
  },
  created() {
    var myHeaders = new Headers();
    let token = localStorage.getItem("token");
    let email = localStorage.getItem("email");
    let ReceiverEmail = sessionStorage.getItem("conversationReceiverEmail");
    let conversationId = sessionStorage.getItem("conversationId");

    myHeaders.append("Authorization", "bearer " + token);
    myHeaders.append("Content-Type", "application/json");

    var data = JSON.stringify({
      ConversationID: parseInt(conversationId),
      ReceiversEmail: ReceiverEmail,
      SendersEmail: email
    });

    var requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: data,
      redirect: "follow"
    };

    let url = "https://localhost:5001/api/Conversation/GetConversationById";

    fetch(url, requestOptions)
      .then(response => response.json())
      .then(result => {
        console.log(result)
        for (let index = 0; index < result.length; index++) {
          const element = result[index];
          this.addMessage(element);
        }
      })
      .catch(error => console.log("error", error));
  },
  methods: {
    addMessage(message) {
      this.messages.push(message);
    }
  }
};
</script>

<style scoped>
#green {
  color: green;
}
#red {
  color: red;
}
</style>