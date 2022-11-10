<template>
    <div class="navmenu">
        <a href="index.html"><img class="navmenu-main-image" src="@/assets/dotbook.png"/></a>
        <form class="form-inline navmenu-item-2">
          <input class="form-control mr-sm-2 navmenu-search-inp" type="search" placeholder="Найти книги..." aria-label="Search">
          <button class="btn btn-outline-dark my-2 my-sm-0" type="submit">Найти!</button>
        </form>
        <a href="index.html"><img class="navmenu-item" src="@/assets/liked.png"/></a>
        <a href="login.html"><img class="navmenu-item" src="@/assets/login.png"/></a>
    </div>
</template>

<script>
export default {
    name: 'NavMenu'
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
</style>