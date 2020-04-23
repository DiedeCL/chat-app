<template>
  <v-card
    max-width="450"
    class="mx-auto"
  >
  <v-list>
    <v-list-item
    v-model="Conversations"
        v-for="conversation in Conversations"
        :key="conversation.title"
        @click="handeledClick"
      >
        <v-list-item-content>
          <v-list-item-title v-text="conversation"></v-list-item-title>
        </v-list-item-content>

        <v-list-item-icon>
          <v-icon :color="'deep-purple accent-4'">chat_bubble</v-icon>
        </v-list-item-icon>
      </v-list-item>
  </v-list>
  </v-card>
</template>

<script>
export default {
    name: "ConversationComponent",
    data: ()=>{
      return {
                Conversations:[]
            }
    },
    methods:{
      fetchConverstations(){
        let url = 'https://localhost:5001/api/Conversation/GetAllConversationsOfAUser';
        let body = {"email":"balou@gmail.com"}   
        fetch(url, body)

      },
      beforeMount() {
            this.fetchFood("maincourse");
        }
    },

}
</script>

<style>

</style>