import {authService} from '../services';
import  router from '../router/index';

const user = JSON.parse(localStorage.getItem('user'));
const state = user
    ? { status: { loggedIn: true }, user }
    : { status: {}, user: null };

function StoreEmailInLocalStorage(email) {
    localStorage.setItem("email", email);

}

const actions = {
  login({commit}, {email, password}) {
      commit('loginRequest', {email});
      authService.login(email, password)
          .then(
              user => {
                  commit('loginSuccess', user);
                  StoreEmailInLocalStorage(email);
                  router.push('/conversations');
              },
              error => {
                  commit('loginFailure', error);
              } 
          );
  },
  signup({commit}, {email, password, username}){
    commit('signupSucces')
     authService.signup(email, password, username).then((data) => {
        if (data == 200){

            router.push('/login')
        }else{
            alert('User exists')
        }
    })
      
        
      
  },
    logout({commit}) {
        authService.logout();
      commit('logout');
    }
};

const mutations = {
    signupSucces(state){
        state.state = {signingUp: true}
    },

    loginRequest(state, user) {
        state.status = {loggingIn: true};
        state.user = user;
    },
    loginSuccess(state, user) {
        state.status = {loggedIn: true};
        state.user = user;
    },
    loginFailure(state) {
        state.status = {};
        state.user = user;
    },
    logout(state) {
        state.status = {};
        state.user = null;
    }
}

export const authentication = {
    namespaced: true,
    state,
    actions,
    mutations
}