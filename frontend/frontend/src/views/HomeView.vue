<template>
  <div>
    <NavMenu />
    <div class="wrapper">
      <SideMenu />
      <BookCard name="Паттерны ООП" 
        authors="Э. Гамма, Р. Хелм, Р. Джонсон, Дж. Влиссидес"
        year="2020"
        publish="ООО Издательство Питер"/>
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
  components: {
    HelloWorld,
    BookCard,
    SideMenu,
    NavMenu
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
</style>
