<!--TODO: накинуть стили, разобраться с роутами-->
<template>
    <div class="book-card">
          <div class="book-card-sub">
            <img src="@/assets/covers/cover1.png"/>
            <div>
                <h3>{{name}}</h3>
                <p>Автор(ы): {{authors}}</p>
                <p>Год выпуска: {{year}}</p>
                <p>Издание: {{publish}}</p>
            </div>
          </div>
          <div>
              <a class="btn btn-outline-dark" href="index.html">В избранное</a>
              <a class="btn btn-dark ml-3" href="index.html"><b>Читать!</b></a>
          </div>
        </div>
</template>

<script>
export default {
 name: 'BookCard',
 props: {
    name: String,
    authors: String,
    year: String,
    publish: String
 }   
}
</script>

<style scoped>
.book-card {
  border: 1px solid gray;
  border-radius: 10px;
  box-shadow: 2px 2px 5px rgb(165, 165, 165);
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  padding: 15px;
  width: 100%;
  margin-bottom: 30px;
}

.book-card-sub {
  display: flex;
  justify-content: flex-start;
  margin-bottom: 15px;
}

.book-card-sub img {
  margin-right: 15px;
  height: 200px;
  border: 5px solid gray;
}
</style>