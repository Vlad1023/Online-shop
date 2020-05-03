
import axios from 'axios';
import router from '@/router/index.js';
const baseURl = "http://localhost:57236";
const apiClient = axios.create({
    baseURL: baseURl,
    withCredentials: true,
    headers:{
        Accept: "application/json",
        'Content-Type': "application/json"
    }
});

function onError(err){
    if (err.response && err.response.status === 401){
        router.push('login');
        return;
    }
    alert('API ERROR: ' + err.message);
}

export default {
    isLogged(onLoad){
        apiClient.get('User/isLogged')
            .then(resp => onLoad(resp.data))
            .catch(err => onError(err));
    },
    isLoggedForce(onLoad){
        apiClient.get('User/isLoggedForce')
            .then(() => {if(typeof onLoad !== "undefined")onLoad()})
            .catch(err => onError(err));
    },
    login(user,onLoad,onErr){
        apiClient.post('User/Login', user )
            .then(resp => onLoad(resp))
            .catch(err => onErr(err.response.data));
    },
    logOut(){
        apiClient.get('User/logOut')
            .catch(err => onError(err));
    },
    register(user,onLoad,onErr){
        apiClient.post('User/Register', user )
        .then(resp => onLoad(resp))
        .catch(err => onErr(err.response.data));
    },   
    market(onLoad,component){
        fetch(`${baseURl}/HomeApi/${component}`).then(function (response) {
            response.json().then(function (parsedJson) {
                onLoad(parsedJson);
            })
        });
    },
    addItemToCart(item){
        apiClient.post('UserCart/Create',item)
        .then(resp => {resp})
        .catch(err => onError(err));
    },
    MoveToPurchase(Id){
        apiClient.put('UserCart/Purchase',Id)
        .catch(err => onError(err));
    },
    getCurrentUserItems(onLoad){
        apiClient.get('UserCart/GetUserItems')
            .then((resp) => onLoad(resp))
            .catch(err => onError(err));
    }
}