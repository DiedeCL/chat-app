<template>
    <div>
        <v-row justify="center">
            <v-dialog v-model="dialog" persistent max-width="600px">
                <template v-slot:activator="{ on }">
                    <v-btn color="primary" dark v-on="on">Create Conversation</v-btn>
                </template>
                <v-card>
                    <v-card-title>
                    <span class="headline">Create Conversation</span>
                    </v-card-title>
                    <v-card-text>
                    <v-container>
                        <v-row>
                        
                        <v-col cols="12" sm="6">
                            <v-autocomplete
                            :items="users"
                            label="Users"
                            v-model="selectedEmail"
                            ></v-autocomplete>
                        </v-col>
                        </v-row>
                    </v-container>
                    </v-card-text>
                    <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="dialog = false">Close</v-btn>
                    <v-btn color="blue darken-1" text @click="creatNewConversation">Create</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-row>
       <ConversationComponent/>     
    </div>
</template>

<script>
import ConversationComponent from '@/components/ConversationComponent.vue';
export default {
    name: 'Conversations',
    data: () => ({
      dialog: false,
      users: [],
      selectedEmail: "",
      conversations: []
    }),
    created() {
        var myHeaders = new Headers();
        let token = localStorage.getItem('token');
        let currentUsersEmailAdress = localStorage.getItem('email');

        myHeaders.append("Authorization", "bearer " + token);

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow',
            
            };

        fetch("https://localhost:5001/api/users/getallusers", requestOptions)
        .then(response => response.json())
        .then(result => {
            let counter =0;
            for (let index = 0; index < result.length; index++) {
                let email = result[index].email;
                if(email != currentUsersEmailAdress)
                {
                     this.users[counter] = email;
                     counter++;
                }
                   
            }
            
        })
        .catch(error => console.log('error', error));
    },
    components:{
        ConversationComponent
    },
    methods:{
        creatNewConversation(){
            let receiverEmail = this.selectedEmail;
            let token = localStorage.getItem('token');
            let senderEmail = localStorage.getItem('email');
            let myHeaders = new Headers();
            myHeaders.append("Authorization", "bearer " + token);
            myHeaders.append("Content-Type", "application/json");

            let raw = JSON.stringify({"ReceiverEmail":receiverEmail,"SenderEmail":senderEmail});

            let requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
            };

            fetch("https://localhost:5001/api/Conversation/CreatedConversation", requestOptions)
            .then(response => response.text())
            .then(result => console.log("ok: " + result))
            .catch(error => console.log('error', error));
            this.dialog = false
        },
        getAllConversationsOfUser(){
            var myHeaders = new Headers();
            let token = localStorage.getItem('token');
            let email = localStorage.getItem('email');
            myHeaders.append("Authorization", "bearer " + token);
            myHeaders.append("Content-Type", "application/json");

            var raw = JSON.stringify({"email": email});

            var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
            };

            fetch("https://localhost:5001/api/Conversation/GetAllConversationsOfAUser", requestOptions)
            .then(response => response.text())
            .then(result => {
                for (let index = 0; index < array.length; index++) {
                    const element = array[index];
                    
                }
            })
            .catch(error => console.log('error', error));
                    }
    }
}
</script>

<style>

</style>