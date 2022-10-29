<!--TODO: накинуть стили, разобраться с роутами-->
<template>
    <div class="book-card">
          <div class="book-card-sub">
            <img src=""/>
            <div class="book-card-content">
                <h3>{{name}}</h3>
                <p>Автор(ы): {{authors}}</p>
                <p>Год выпуска: {{year}}</p>
                <p>Издание: {{publish}}</p>
            </div>
          </div>
          <div class="book-card-footer">
              <a class="btn btn-outline-dark" href="index.html">В избранное</a>
              <a class="btn btn-dark" href="index.html"><b>Читать!</b></a>
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

</style>