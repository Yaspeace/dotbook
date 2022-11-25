<template>
<div class="relative">
    <div :class="'cover' + (covered ? '' : ' invisible')"></div>
    <div class="d-flex flex-row justify-content-center">
        <div class="d-flex flex-column justify-content-center text-center inner-wrapper">
            <h1 v-if="getBool(register)">Регистрация</h1>
            <h1 v-else>Вход</h1>
            <div class="login">
                <form class="login-form" @submit.prevent="loginOrRegister">
                    <input v-model="login" class="login-input" type="text" :placeholder="(getBool(register) ? 'Придумайте л' : 'Л') + 'огин'"/><br>
                    <input v-model="password" class="login-input" type="password" :placeholder="(getBool(register) ? 'Придумайте п' : 'П') + 'ароль'"/><br>
                    <input v-model="passwordCheck" v-if="getBool(register)" class="login-input" type="password" placeholder="Повторите пароль"/><br>
                    <input class="btn btn-dark login-btn" type="submit" :value="getBool(register) ? 'Зарегистрироваться!' : 'Войти!'"/>
                </form>
                <p v-if="!getBool(register)" class="mt-3">
                    Ещё не зарегистрированы?
                    <router-link :to="{ name: 'login', params: { register: 'true' } }">
                        Регистрация
                    </router-link>
                </p>
            </div>
        </div>
    </div>
</div>    
</template>

<script>
export default {
    name: 'LoginView',
    props: {
        register: String
    },
    data() {
        return {
            login: "",
            password: "",
            passwordCheck: "",
            covered: false
        }
    },
    methods: {
        getBool(value) {
            return value == 'true';
        },
        logIn() {
            this.covered = true;
            this.$http.post('/Account/login', null, {
                params: {
                    login: this.login,
                    password: this.password
                }
            })
            .then(() => {
                this.covered = false;
                this.$router.push({ name: 'home' });
            })
            .catch(() => this.covered = false);
        },
        registerUser() {
            if(this.password == this.passwordCheck) {
                this.covered = true;
                this.$http.post('/Account/register', null, {
                    params: {
                        login: this.login,
                        password: this.password
                    }
                })
                .then(() => {
                    this.covered = false;
                    this.$router.push({ name: 'home' });
                })
                .catch(() => this.covered = false);
            }
        },
        loginOrRegister() {
            if(this.getBool(this.register)) {
                this.registerUser();
            } else {
                this.logIn();
            }
        }
    }
}


</script>

<style scoped>
.invisible {
    display: none;
}
.relative {
    position: relative;
}
.cover {
    background: rgba(0, 0, 0, 0.8);
    position: absolute;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    z-index: 1000;
}

.login {
  height: 90vh;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.login-form {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 100%;
  align-items: center;
}

.inner-wrapper {
  width: 35%;
}

.login input {
  text-align: center;
  height: 40px;
  border-radius: 15px;
  width: 100%;
  max-width: 300px;
}

.login-input {
  border: 1px solid black;
}
</style>