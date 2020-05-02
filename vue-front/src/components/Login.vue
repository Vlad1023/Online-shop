
<template>
    <form @submit="checkForm" class="form">
        <div class="validation"></div>
        <h1 class="form_title">Вход на сайт</h1>
          <router-link class = "form_title" to="/registration">Регистрация</router-link>
        <div class="form_group">
            <label class="form_label">Введите логин</label>
            <input required v-model="Login" class="form_input" placeholder="Login" type="text"/>
        </div>
        <div class="form_group">
            <label class="form_label">Введите пароль</label>
            <input required v-model="Password" class="form_input" type = "password" placeholder="Password"/>
        </div>
        <div v-if="errorMes" id = "Error"><p>{{errorMes}}</p></div>
        <button type="submit" class="form_button">Вход</button>
        <button @click="Continue" class="form_button"><h1 id = "h1_cont" class="form_title">Продолжить</h1></button>

    </form>
</template>
<script>
    import api from '@/API/api.js';
export default {
    data: function () {
    return {
      errorMes: null,
      Login: null,
      Password: null
    }
    },
    methods: {
    checkForm: function (e) { 
        api.login({Login: this.Login,Password: this.Password},
            () => this.$router.push({name : 'main'}),
                (mes) => {this.errorMes = mes; this.ifExist = true});
        e.preventDefault(); 
    },
    Continue(){
        this.$router.push({name : 'main'});
    }
}
}
</script>
<style scoped>
body{
    display: flex;
    justify-content: center;
    align-items: center;
    height: 90vh;
}
#but_cont{
   text-align: center;
   vertical-align: center;
}
#h1_cont{
    margin: 0;
    padding: 0;
}
.form{
    width: 100%;
    padding: 5%;
    border-radius: 18px;
    font-family: sans-serif;
    letter-spacing: 1px;
    background: #EFF3F6;
    box-sizing: border-box;
}
#Error{
    text-align: left;
    margin-top: 50px;
}
#Error p{
    font-size: 2rem;
    color: red;
}
ul {
    list-style-type: none;
    border: 2px solid blue;
    padding: 1%;
    background-color: lightblue;
}
li {
    color: red;
    font-size: 3rem;
    margin: 0;
    background-color: lightyellow;
}
.form_input,
.form_button{
    font-family: sans-serif;
    letter-spacing: 1px;
    font-size: 2rem;
}

.form_title{
    text-align: center;
    margin: 0;
    margin-bottom: 32px;
    font-weight: normal;
}

.form_group{
    position: relative;
    margin-top: 100px;
}

.form_label{
    position: absolute;
    top: 0;
    left: 0;
    z-index: -1;
    color: #9e9e9e;
    transition: 0.3s;
}

.form_input{
    width: 100%;
    padding: 0 0 10px 0;
    border: none;
    border-bottom: 1px solid #e0e0e0;
    background-color: transparent;
    outline: none;
    transition: 0.3s;
}

.form_input:focus{
    border-bottom: 1px solid #1a738a;

}

.form_button{
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    color: white;
    background-color: #0071f0;
    cursor: pointer;
    transition: 0.3s;
    margin-top: 20px;
}

.form_button:focus:not(.disabled-button),
.form_button:hover:not(.disabled-button){
    background-color: rgba(0, 113, 240, 0.7);
}

.form_input:focus ~ .form_label,
.form_input:not(:placeholder-shown) ~.form_label {
    top:-18px;
    font-size: 12px;
    color: #e0e0e0;
}

.form_button{
    margin-left: auto;
    margin-right: auto;
    display: block;
}

.disabled-button{
    cursor: initial;
    opacity: 0.3;
}

.input-req{
    margin-bottom: 2px;
    margin-top: 10px;
}
</style>