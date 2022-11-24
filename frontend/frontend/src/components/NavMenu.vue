<template>
    <div class="navmenu">
        <router-link to="/"><img class="navmenu-main-image" src="@/assets/dotbook.png"/></router-link>
        <form class="form-inline navmenu-item-2">
          <input class="form-control mr-sm-2 navmenu-search-inp" type="search" placeholder="Найти книги..." aria-label="Search">
          <button class="btn btn-outline-dark my-2 my-sm-0" type="submit">Найти!</button>
        </form>
        <a href="index.html"><img class="navmenu-item" src="@/assets/liked.png"/></a>
        <router-link v-if="!authorized" :to="{ name: 'login', params: { register: 'false' } }"><img class="navmenu-item" src="@/assets/login.png"/></router-link>
        <div v-else class="pop-parent">
          <a v-on:click="popVisible = !popVisible" href="#"><img class="navmenu-item" src="@/assets/login_auth.png"/></a>
          <ul :class="'pop' + (popVisible ? ' pop-visible' : '')">
            <li><router-link to="/bookadd">Добавить книгу</router-link></li>
            <li v-on:click="logout">Выйти</li>
          </ul>
        </div>
    </div>
</template>

<script>
export default {
    name: 'NavMenu',
    data() {
      return {
        authorized: false,
        popVisible: false
      }
    },
    created() {
      this.$http.get('/Account/authorized')
        .then((responce) => this.authorized = responce.data);
    },
    methods: {
      logout() {
        this.$http.post('/Account/logout')
          .then(() => this.authorized = false);
      }
    }
}
</script>

<style scoped>
.navmenu {
  border: 1px solid gray;
  box-shadow: 2px 2px 5px rgb(165,165,165);
  display: flex;
  justify-content: space-around;
  align-items: center;
  margin-bottom: 15px;
}

.navmenu-main-image {
    width: 150px;
}

.navmenu-item {
  width: 100px;
}

.navmenu-item-2 {
  border: 1px solid gray;
  border-radius: 10px;
  padding: 5px;
  width: 50%;
  display: flex;
  justify-content: flex-start;
}

.search-sub {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.navmenu-search-inp {
  border: none;
  width: calc(100% - 85px);
}

.navmenu-search-img {
  width: 50px;
}

/*Стили для выпадающей менюшки*/
ul {
  list-style: none; 
  margin: 0;
  padding: 0;
}
.pop-parent {
  position: relative;
}
.pop {
  position: absolute;
  top: 100%;
  left: 0;
  /*width: 100%;*/
  width: fit-content;
  z-index: 10;
  display: none;
  background: white;
  border: 1px solid black;
  border-radius: 10px;
}
.pop li {
  text-align: center;
  cursor: pointer;
  border-bottom: 1px solid black;
  transition: 0.5s;
}
.pop li:last-child {
  border-bottom: none;
  border-bottom-left-radius: 10px;
  border-bottom-right-radius: 10px;
}
.pop li:first-child {
  border-top-left-radius: 10px;
  border-top-right-radius: 10px;
}
.pop li:hover {
  background-color: black;
  color: white;
}
.pop li * {
  color: inherit;
}
.pop li a {
  text-decoration: none;
  white-space: nowrap;
}
.pop-visible {
  display: block;
}
</style>