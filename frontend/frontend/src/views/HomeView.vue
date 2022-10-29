<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    <BookCard name="Паттерны ООП" 
      authors="Э. Гамма, Р. Хелм, Р. Джонсон, Дж. Влиссидес"
      year="2020"
      publish="ООО Издательство Питер"/>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import BookCard from '@/components/BookCard.vue'

export default {
  name: 'HomeView',
  components: {
    HelloWorld,
    BookCard
  }
}
</script>
