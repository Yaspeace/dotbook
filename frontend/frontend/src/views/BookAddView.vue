<template>
    <div class="d-flex flex-row justify-content-center">
        <div class="d-flex flex-column justify-content-center wrapper">
            <form @submit.prevent="addBook">
                <b-form-file
                    v-model="image"
                    :state="Boolean(image)"
                    placeholder="Выберите изображение обложки или перетащите её сюда..."
                    drop-placeholder="Перетащите для прикрепления..."
                    accept=".jpg, .png"
                ></b-form-file>
                <b-form-file
                    v-model="pdf"
                    :state="Boolean(pdf)"
                    placeholder="Выберите книгу в формате PDF или перетащите её сюда..."
                    drop-placeholder="Перетащите для прикрепления..."
                    accept=".pdf"
                ></b-form-file>
                <input v-model="name" type="text" placeholder="Название книги" name="Name" />
                <input v-model="author" type="text" placeholder="Автор(-ы)" name="Author" />
                <input v-model="year" type="text" placeholder="Год публикации" name="PublishYear" />
                <model-list-select
                :list="publishes"
                v-model="chosenPublishId"
                option-value="id"
                :custom-text="getText"
                placeholder="Издательство..."></model-list-select>
                <input type="submit" value="Добавить книгу!" />
            </form>
        </div>
    </div>
</template>

<script>
import { ModelListSelect } from 'vue-search-select'

export default {
  name: 'BookAddView',
  data() {
    return {
        publishes: [],
        chosenPublishId: null,
        image: null,
        pdf: null,
        author: "",
        year: "",
        name: ""
    }
  },
  created() {
    this.$http.get('/Publish')
        .then((responce) => this.publishes = responce.data)
        .catch((error) => {
            console.log(error);
            this.publishes = [];
        });
  },
  methods: {
    getText (item) {
        return `${item.name}`;
    },
    addBook() {
        var data = new FormData();
        data.append('name', this.name);
        data.append('author', this.author);
        data.append('publishYear', this.year);
        data.append('PublishId', this.chosenPublishId);
        data.append('FileImg', this.image);
        data.append('FilePdf', this.pdf);
        this.$http.post('/Book', data)
            .then(() => this.$router.push({ name: 'home' }));
    }
  },
  components: {
      ModelListSelect
    }
}
</script>

<style scoped>
.wrapper {
    text-align: center;
    width: 40%;
    height: 90vh;
}
form {
    border: 1px solid gray;
    border-radius: 12px;
    padding: 40px;
    height: 50%;

    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
}
input {
    width: 100%;
    border: 1px solid gray;
    border-radius: 4px;
    padding: 5px;
    text-align: center;
}
select {
    text-align: center;
}
</style>
