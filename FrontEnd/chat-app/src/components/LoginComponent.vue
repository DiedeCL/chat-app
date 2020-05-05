<template>
  <v-form v-model="valid" ref="form"
      class="pa-4 pt-6">
    <v-container>
      <v-row>
             
       
        <v-col
          cols="12"
          md="4"
        >
          <v-text-field
            v-model="email"
            :rules="emailRules"
            label="E-mail"
            required
          ></v-text-field>
        </v-col>
      </v-row>
       <v-col
          cols="12"
          md="4"
        >
          <v-text-field
            v-model="password"
            :rules="passwordRules"
            
            label="Password"
            required
            type="password"
          ></v-text-field>
        </v-col>

      <v-row>
        <v-btn
        :disabled="!valid"
        :loading="isLoading"
        class="white--text"
        color="deep-purple accent-4"
        depressed
        @click="handleSubmit"
        >
        LogIn
        </v-btn>
      </v-row>
    </v-container>
  </v-form>
</template>

<script>
    import {mapState, mapActions} from 'vuex'

export default {
  name: "LoginComponent",
 data: () => ({
   
      valid: false,
      isLoading: false,
      password: '',
      passwordRules:[
          v => !!v || 'Password is required',
          v => v.length >= 8 || 'Password must be motre than 8 characters'
      ],
      email: '',
      emailRules: [
        v => !!v || 'E-mail is required',
        v => /.+@.+/.test(v) || 'E-mail must be valid',
      ],
      
    }),
     computed: {
            ...mapState('authentication', ['status']),
     },
    methods:{
       ...mapActions('authentication', ['login', 'logout']),
            handleSubmit() {
                this.submitted = true;
                const {email, password} = this;
                if(email && password) {
                  console.log("ok")
                    this.login({email, password});
                }
            },
    }
    
}
</script>

<style>

</style>