<template>
    <div class="menu">
        <div class="d-flex text-center">
          <h5>Книги по категориям</h5>
        </div>
        <div v-for="theme in themes" :key="theme.id" v-on:click="themeClick(theme.id)" :class="'menu-item' + (ThemeId == theme.id ? ' chosen' : '')">
          <a href="#">{{theme.name}}</a>
        </div>
      </div>
</template>

<script>
export default {
    name: 'SideMenu',
    props: ['ThemeId'],
    data() {
      return {
        themes: []
      }
    },
    created() {
      this.$http.get('/Theme')
        .then((responce) => this.themes = responce.data);
    },
    methods: {
      themeClick: function(id) {
        this.$emit('theme-click', id);
      }
    }
}
</script>

<style scoped>
.menu {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  margin-right: 50px;
}

.menu-item {
  border: 1px solid gray;
  height: 50px;
  text-align: center;
  cursor: pointer;
  transition: 0.5s;
  background: white;
  color: black;
}
.chosen {
  background: black;
  color: white;
}
.menu-item:hover {
  background: black;
  color: white;
}

.menu-item a {
  text-decoration: none;
  font-style: normal;
  color: inherit;
  margin-bottom: 5px;
}
</style>