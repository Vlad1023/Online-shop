<template>
<div id = "Wrap">
    <div class = "elWrap">
    <div class = "elName"><p>Купленные товары</p></div>
    <div class = "elem"><userBought v-bind:items="userBought" /></div>
    </div>
    <router-link class = "vue-nav" to="/"><div class = "elName"><p>Вернуться на главную</p></div></router-link>
    <div class = "elWrap">
    <div class = "elName"><p>Корзина товаров</p></div>
    <div class = "elem"><userCart v-bind:items="userCart" /></div>
    </div>
</div>
</template>
<script>
import api from '@/API/api.js';
import userBought from './userBought.vue';
import userCart from './userCart.vue';
export default {
     data: function () {
    return {
      items: Array,
      userBought: Array,
      userCart: Array
    }
    },
    beforeCreate(){
        api.isLoggedForce();
    },
    created(){
        api.getCurrentUserItems((info) => this.setProps(info));  
    },
    components:{
    userBought,
    userCart
    },
    methods: {
        setProps(info){
            
            this.items = JSON.parse(info.data);
            if(this.items){      
            this.userBought = this.items.filter(item => item.IsBought);
            this.userCart = this.items.filter(item => !item.IsBought);
            }
        }
    }
}   
</script>
<style scoped>
#Wrap{
box-sizing: border-box;
min-width: 1315px;
margin: 0;
padding: 3%;
width: 100%;
height: 98vh;
background-color: #AAAAAA;
display: flex;
flex-direction: row;
justify-content: space-between;
}
.elem{
    height: 100%;
    width: 100%;
}
.elName p{
    font-size: 2rem;
}
.elWrap{
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 90%;
    width: 30%;
    min-width: 100px;
}
.vue-nav  {
box-sizing: border-box;
font-size: 1.5rem;
color:white;
font-weight: 100;
letter-spacing: 2px;
text-decoration: none;
background:rgba(0,0,0,0.2);
display: inline-block;
width: 30%;
height: 100px;
text-align: center;
transition:all 0.5s;
color: green;
}
</style>