<template>
  <v-card max-width="450" class="mx-auto">
    <v-list three-line>
      <template v-for="file in files">
        <v-list-item :key="file.fileId">
          <v-list-item-content>
            <a @click="getFile(file.fileId)">{{file.fileName}}</a>
          </v-list-item-content>
        </v-list-item>
      </template>
    </v-list>
  </v-card>
</template>

<script>
export default {
  name: "Files",
  data() {
    return {
      files: []
    };
  },
  created() {
    var myHeaders = new Headers();
    let token = localStorage.getItem("token");
    myHeaders.append("Authorization", "bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    let conversationId = sessionStorage.getItem("conversationId");
    var data = JSON.stringify({
      ConversationID: parseInt(conversationId)
    });

    var requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: data,
      redirect: "follow"
    };
    fetch(
      "https://localhost:5001/api/Conversation/GetAllFilesOfCOnversation",
      requestOptions
    )
      .then(response => response.json())
      .then(result => {
        for (let index = 0; index < result.length; index++) {
          const element = result[index];
          console.log(element);
          this.addFile(element);
        }
      })
      .catch(error => console.log("error", error));
  },
  methods: {
    addFile(file) {
      this.files.push(file);
    },
    getFile(id) {
      console.log(id);
     /* var myHeaders = new Headers();
      let token = localStorage.getItem("token");
      myHeaders.append("Authorization", "bearer " + token);
      myHeaders.append("Content-Type", "application/json");
      
      var data = JSON.stringify({
        id: id
      });

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
       // body: data,
        redirect: "follow"
      };
      */
      let url = "https://localhost:5001/api/Conversation/GetFile?id=" + id;
      window.open(url, "_blank", "")
           /* fetch(
        "https://localhost:5001/api/Conversation/GetFile?" ,
        requestOptions
      )
        .then(response => response.json())
        .then((result) =>  window.downloads.download(result) )
        .catch(error => console.log("error", error));*/
    }
  }
};
</script>

<style scoped>
a {
}
</style>