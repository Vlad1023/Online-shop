<template>
<div id = "Wrap">
    <div class = "elem"><userBought v-bind:items="userBought" /></div>
    <div class = "elem"><userCart v-bind:items="userCart" /></div>
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
    width: 30%;
    min-width: 100px;
}
</style>