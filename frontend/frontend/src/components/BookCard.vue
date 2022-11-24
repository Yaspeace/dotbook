<!--TODO: накинуть стили, разобраться с роутами-->
<template>
    <div class="book-card">
          <div class="book-card-sub">
            <!--<img src="@/assets/covers/cover1.png"/>-->
            <img :src="this.$FilesHost + book.image.name" />
            <div>
                <h3>{{book.name}}</h3>
                <p>Автор(ы): {{book.author}}</p>
                <p>Год выпуска: {{book.publishYear}}</p>
                <p>Издание: {{book.publish.name}}</p>
                <p>Выложил: {{book.uploadUser.name}}</p>
            </div>
          </div>
          <div>
              <a  class="btn btn-outline-dark" href="#">В избранное</a>
              <a target="_blank" rel="noopener noreferrer" class="btn btn-dark ml-3" :href="this.$FilesHost + book.pdf.name"><b>Читать!</b></a>
          </div>
        </div>
</template>

<script>
export default {
 name: 'BookCard',
 props: ["book"]
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