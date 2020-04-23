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
            v-model="username"
            :rules="nameRules"
            
            label="Username"
            required
            type="text"
          ></v-text-field>
        </v-col>

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
      <v-row>
        <v-btn
        :disabled="!valid"
        :loading="isLoading"
        class="white--text"
        color="deep-purple accent-4"
        depressed
        @click="handleSubmit"
        >
        SignUp
        </v-btn>
      </v-row>
    </v-container>
  </v-form>
</template>

<script>
    import {mapState, mapActions} from 'vuex'

export default {
name: "SingUpComponent",
 data: () => ({
      valid: false,
      isLoading: false,
      username: '',
      nameRules:[
        v => !!v || 'Username is required',
        v => v.length <= 10 || 'Username must be less than 10 characters'

      ],
      password: '',
      passwordRules:[
        v => !!v || 'Password is required',
          v => v.match(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$/) ||
          'Password must contain an upper case letter, a numeric character, and a special character',
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
       ...mapActions('authentication', ['login', 'logout', 'signup' ]),
            handleSubmit() {
                const {username, password, email} = this;
                console.log(this)
                if(username && password && email) {
                  console.log('Component ' + username,password, email )

                    this.signup({email, password, username});
                }
            },
    }
    
}
</script>

<style>

</style>