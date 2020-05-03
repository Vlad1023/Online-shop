<template>
  <section id="Wrap">
        <section class="TopMain">
            <div class="Center">
                <div class="TopMainWrap">
                    <div class="Info">
                        <div><h3 class="InfoTxt">О нас</h3></div>
                        <div><h3 class="InfoTxt">Доставка</h3></div>
                        <div><h3 class="InfoTxt">Оплата</h3></div>
                    </div>
                    <div class="Personal">
                        <i class="fa fa-male" style="display: inline-block; margin-right: 30px; font-size: xx-large;"></i>
                        <h3 style="display: inline-block; margin-right: 30px;" class="InfoName">{{name}}</h3>
                         <router-link to="/personal"><button><h3 class="InfoTxt" style="display: inline-block;">Личный кабинет</h3></button></router-link>
                    </div>
                </div>
            </div>
        </section>
        <section class="TopMiddle">
            <div class="Center">
                <div class="TopMiddleWrap">
                    <div class="Top">
                        <img :src="images.logo" width="70px" height="90px">
                        <div class="InputField" style="width: 20%; display: flex; justify-content: space-between;">
                            <input v-model="toSearch" type="text" rows="1" placeholder="Например: гипсокартон стеневой">
                            <i @click="findItem()" class="fas fa-cloud" style=" padding-top: 5%; font-size: 100%;"></i>
                        </div>
                        <div class="Phone">
                            <div><p>(099)-000-777-1</p></div>
                            <div><p>Заказать звонок</p></div>
                        </div>
                        <div class="Icons">
                            <i class="fas fa-shopping-cart" style="font-size: 200%;"></i>
                            <i class="fas fa-heart" style="font-size: 200%;"></i>
                            <i class="fas fa-balance-scale" style="font-size: 200%;"></i>
                        </div>
                        <div class="Button">
                           <button v-if="name" @click="logOut" style="border-radius: 10px;background-color: rgb(247,158,120);"><p style="font-size: 100%;">Выйти из аккаунта</p></button>
                           <button v-else @click="logIn" style="border-radius: 10px;background-color: rgb(247,158,120);"><p  style="font-size: 100%;">Войти в аккаут</p></button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <marketComp/>
        <div class="Center">
            <section class="CenterPictures" style="width: 100%;padding-top: 5%;">
                <div style="width: 60%; height: 300px;"><img src="@/assets/img/Shock.jpg"></div>
                <div style="width: 40%; height: 300px; display:flex; flex-direction: column; justify-content: space-between;">
                    <div style="width: 100%; height: 50%;"><img src="@/assets/img/Knauf.jpg"></div>
                    <div style="width: 100%; height: 50%;"><img src="@/assets/img/Brick.jpg"></div>
                </div>
            </section>
            <section class="Propose" style="padding-top: 5%; width: 100%; display: flex; justify-content: space-between;">
                <div style="width: 20%;">
                    <i class="fas fa-ambulance" style="font-size: 150%;"></i>
                    <div><h4>Бесплатная доставка</h4></div>
                    <div><p>При заказе от 8000 грн, бесплатная доставка по всему Харькове</p></div>
                </div>
                <div style="width: 20%;">
                    <i class="fas fa-ambulance" style="font-size: 150%;"></i>
                    <div><h4>Бесплатная доставка</h4></div>
                    <div><p>При заказе от 8000 грн, бесплатная доставка по всему Харькове</p></div>
                </div>
                <div style="width: 20%;">
                    <i class="fas fa-ambulance" style="font-size: 150%;"></i>
                    <div><h4>Бесплатная доставка</h4></div>
                    <div><p>При заказе от 8000 грн, бесплатная доставка по всему Харькове</p></div>
                </div>
                <div style="width: 20%;">
                    <i class="fas fa-ambulance" style="font-size: 150%;"></i>
                    <div><h4>Бесплатная доставка</h4></div>
                    <div><p>При заказе от 8000 грн, бесплатная доставка по всему Харькове</p></div>
                </div>
            </section>
        </div>
    </section>
</template>

<script>
import api from '@/API/api.js'
import Vue from 'vue'
import marketComp from '@/components/marketComp.vue'
export default {
 created(){
api.isLogged((name) => this.name = name);
},
data() {
        return {
            images: {
                brick: require('@/assets/img/Brick.jpg'),
                knauf: require('@/assets/img/Knauf.jpg'),
                logo: require('@/assets/img/logo.jpg'),
                shock: require('@/assets/img/Shock.jpg'),
            },
            name : null,
            toSearch: null,
            itemArID: 0
        }
    },
  components:{
    marketComp
  },
  methods: {
      logOut(){
          api.logOut();
          this.name = null;
          Vue.prototype.$isLogged = false;
      },
      logIn(){
          this.$router.push('/login');
      },
      findItem(){
          if(this.toSearch){
            let ifFound = api.market( (info) => this.checkIfContains(info,'market1'),'market1');
            if(!ifFound) ifFound = api.market( (info) => this.checkIfContains(info,'market2'),'market2');
            if(!ifFound) ifFound = api.market( (info) => this.checkIfContains(info,'market3'),'market3');
          }
      },
      checkIfContains(info,toPush){
         let ObjArray = info;
         this.itemArID = 0;
         for (const item of ObjArray) {
            if(item['Name'].includes(this.toSearch)){
                if(!this.$route.path.includes(toPush)){
                    this.$router.push({ name : toPush, params: { itemArId :  this.itemArID  }});
                }
                else{
                    this.$eventBus.$emit(`${toPush} + 'Find'`, this.itemArID);
                }
                return true;
            }
            this.itemArID++;
        }
        return false;
      }
  }
}
</script>
<style scoped>
body {
    margin: 0;
    text-align: center;
}

#Wrap {
    margin: 0;
    padding: 0;
    width: 100%;
}

.TopMain {
    background-color: rgb(97,90,87);
}
img{
max-width: 100%;
max-height: 100%;
}
.TopMain .Center .TopMainWrap {
display: flex;
flex-direction: row;
justify-content: space-between;
}

.TopMain .Center .TopMainWrap .Info {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: 50%;
    flex-wrap: wrap;
    align-items: center;
}

.InfoTxt {
    color: rgb(158,155,152);
    font-size: 80%;
    white-space: nowrap;
}
.InfoName {
    color: green;
    font-size: 1rem;
    white-space: nowrap;
}

.TopMainWrap .Personal {
    vertical-align: center;
    align-items: center;
    display: flex;
    justify-content: space-between;
}

.Phone {
    width: 15%;
    display: flex;
    flex-direction: column;
}

.TopMiddle {
    background-color: rgb(239,239,239);
    width: 100%;
}

input[type = "text"] {
    padding: 5%;
    width: 110%;
    border-radius: 30px;
}

.TopMiddle .TopMiddleWrap {
    padding-top: 1%;
    padding-bottom: 1%;
}

.TopMiddle .TopMiddleWrap .Top {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    flex-wrap: wrap;
}

.Icons {
    width: 9%;
    display: flex;
    justify-content: space-between;
    flex-flow: row wrap;
}

.Center .Down {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    flex-wrap: wrap;
    align-items: baseline;
}

.Center .CenterPictures {
    display: flex;
    flex-flow: row nowrap;
    justify-content: center;
}

.Center {
    padding: 0 10%;
}
</style>
