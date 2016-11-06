<template>
  <div class="outer">
    <div class="middle">
      <div class="inner">
        <div id="app">
          <h1>A Website</h1>
          <div class="form-group">
              <input
                type="text"
                class="form-control"
                placeholder="Enter your username"
                v-model="credentials.username"
              >
            </div>
            <div class="form-group">
              <input
                type="password"
                class="form-control"
                placeholder="Enter your password"
                v-model="credentials.password"
              >        
            </div>    
            <button class="btn btn-primary" @click="submit(credentials)">Login</button>          
        </div>
      </div>
    </div>
  </div>
</template>

<script>
const API_URL = 'http://localhost:5000/'
const LOGIN_URL = API_URL + 'connect/token'
import jquery from "jquery";
export default {  
  name: 'app',
  data() {
      return {        
        credentials: {
          username: '',
          password: ''
        },
        error: ''
      }
    },
  methods: {
      submit(credentials) {          
        var settings = {
          "url": "http://localhost:5000/connect/token",
          "method": "POST",
          "headers": {
            "content-type": "application/x-www-form-urlencoded",
          },
          "data": {
            "username": credentials.username,
            "password": credentials.password,
            "grant_type": "password"
          }
        }

        jquery.ajax(settings).done(function (response) {
          console.log(response);
        });
      }
    }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;  
}
.outer {
    display: table;
    position: absolute;
    height: 100%;
    width: 100%;
}

.middle {
    display: table-cell;
    vertical-align: middle;
}

.inner {
    margin-left: auto;
    margin-right: auto; 
    width: 25%;
    
}
</style>
