<template>
  <div>
    <NavMenu />
    <div class="wrapper">
      <SideMenu :books="this.books" :search="this.search" />
      <div class="cards">
        <BookCard :key="book.id" v-for="book in this.books" :book="book" />
        <!--<BookCard name="Паттерны ООП" 
          authors="Э. Гамма, Р. Хелм, Р. Джонсон, Дж. Влиссидес"
          year="2020"
          publish="ООО Издательство Питер"/>
          <BookCard name="Паттерны ООП" 
          authors="Э. Гамма, Р. Хелм, Р. Джонсон, Дж. Влиссидес"
          year="2020"
          publish="ООО Издательство Питер"/>
          <BookCard name="Паттерны ООП" 
          authors="Э. Гамма, Р. Хелм, Р. Джонсон, Дж. Влиссидес"
          year="2020"
          publish="ООО Издательство Питер"/>-->
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
      search: String
    }
  },
  components: {
    HelloWorld,
    BookCard,
    SideMenu,
    NavMenu
  },
  created() {
    this.$http.get('/Book', {params: { Take: 10 }})
      .then((responce) => this.books = responce.data);
  }
}
</script>

<style scoped>
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
