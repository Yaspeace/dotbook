<template>
  <div class="relative">
    <div :class="'cover' + (covered ? '' : ' invisible')"></div>
    <NavMenu 
      v-on:search-changed="searchChanged" 
      v-on:find="findBooks" 
      v-on:find-favorites="findFavorites"
      v-on:home="reset"
      :isFavorites="query == '/Book/favorites'" 
      ref="navMenu" 
    />
    <div class="wrapper">
      <SideMenu 
        v-on:theme-click="themeClicked" 
        :ThemeId="curTheme" />
      <div class="cards">
        <BookCard :key="book.id" v-for="book in books" :book="book" />
        <div style="text-align: center;"><a v-show="page > 1" v-on:click="changePage(-1)">&lt;</a>&nbsp;{{page}}&nbsp;<a v-show="page < totalPages" v-on:click="changePage(1)">&gt;</a></div>
      </div>
    </div>
  </div>
</template>

<script>
import NavMenu from '@/components/NavMenu.vue'
import SideMenu from '@/components/SideMenu.vue'
import HelloWorld from '@/components/HelloWorld.vue'
import BookCard from '@/components/BookCard.vue'

export default {
  name: 'HomeView',
  data() {
    return {
      books: [],
      search: "",
      curTheme: 0,
      page: 1,
      totalPages: 1,
      booksPerPage: 10,
      query: "/Book",
      covered: true,
      parameters: {
        params: {
          take: 10,
          skip: 0,
          search: ""
        }
      }
    }
  },
  components: {
    HelloWorld,
    BookCard,
    SideMenu,
    NavMenu
  },
  created() {
    this.$http.get(this.query, this.parameters)
      .then((responce) => this.books = responce.data);
    this.covered = false;
  },
  methods: {
    searchChanged(newSearch) {
      this.search = newSearch;
    },
    getBooks() {
      this.$http.get(this.query, this.parameters)
        .then((responce) => {
          this.books = responce.data;
        })
        .catch((error) => {
          if(error.response.status == 401) {
            this.$router.push({ name: 'login', params: { register: 'false' } });
          }
        });
    },
    changePage(delta) {
      this.page += delta;
      var skip = this.booksPerPage * (this.page - 1);
      this.lastParameters.params.skip = skip;
      this.$http.get(this.query, this.parameters)
          .then((responce) => {
            this.books = responce.data;
          })
          .catch((error) => {
            if(error.response.status == 401) {
              this.$router.push({ name: 'login', params: { register: 'false' } });
            }
          });
    },
    recount() {
      var booksCount = 1;
      this.$http.get(this.query + '/count', this.parameters)
        .then((responce) => booksCount = responce.data)
        .catch((error) => {
            if(error.response.status == 401) {
              this.$router.push({ name: 'login', params: { register: 'false' } });
            }
          });
      this.totalPages = Math.ceil(booksCount / this.booksPerPage);
    },
    themeClicked(id) {
      this.page = 1;
      if(id == 0 || id == this.curTheme) {
        this.curTheme = 0;
        this.query = '/Book';
        this.parameters = {
          params: {
            take: this.booksPerPage,
            skip: 0,
            search: this.search
          }
        };
      } else {
        this.curTheme = id;
        this.query = '/Book/bythemes';
        this.parameters = {
          params: {
            themeId: id,
            take: this.booksPerPage,
            skip: 0,
            search: this.search
          }
        };
      }
      this.recount();
      this.getBooks();
    },
    findBooks() {
      this.page = 1;
      this.parameters.params.search = this.search;
      this.recount();
      this.getBooks();
    },
    reset() {
      this.page = 1;
      this.$refs.navMenu.reset();
      this.curTheme = 0;
      this.query = "/Book";
      this.parameters = {
        params: {
          take: 10,
          skip: 0,
          search: ""
        }
      };
      this.recount();
      this.getBooks();
    },
    findFavorites() {
      this.page = 1;
      this.curTheme = 0;
      this.query = '/Book/favorites';
      this.parameters = {
        params: {
          take: this.booksPerPage,
          skip: 0,
          search: this.search
        }
      };
      this.recount();
      this.getBooks();
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
.wrapper {
    display: flex;
    justify-content: flex-start;
    width: 100%;
    padding: 0px 20px 0px 20px;
  }

.cards {
  display: flex;
  flex-direction: column;
  width: 100%;
}
</style>
