<template>
    <div class="menu">
        <div class="d-flex text-center">
          <h5>Книги по категориям</h5>
        </div>
        <div class="menu-item">
          <a href="index.html">Языки программирования</a>
        </div>
        <div class="menu-item">
          <a href="index.html">Архитектура и паттерны</a>
        </div>
        <div class="menu-item">
          <a href="index.html">Data science</a>
        </div>
        <div class="menu-item">
          <a href="index.html">Алгоритмы и структуры данных</a>
        </div>
        <div class="menu-item">
          <a href="index.html">IoT и IIoT</a>
        </div>
      </div>
</template>

<script>
export default {
    name: 'SideMenu'
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