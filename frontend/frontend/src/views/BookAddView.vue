<template>
    <div class="relative">
        <div :class="'cover' + (covered ? '' : 'invisible')"></div>
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
                    <input class="text-input" v-model="name" type="text" placeholder="Название книги" name="Name" />
                    <input class="text-input" v-model="author" type="text" placeholder="Автор(-ы)" name="Author" />
                    <input class="text-input" v-model="year" type="text" placeholder="Год публикации" name="PublishYear" />
                    <div class="themes-wrapper">
                        <div style="margin-right: 50px;" v-for="theme in themes" :key="theme.id">
                            <input :value="theme.id" type="checkbox" v-model="bookThemes" :id="'checkbox-'+theme.id.toString()" />
                            <label :for="'checkbox-'+theme.id.toString()">{{theme.name}}</label>
                        </div>
                    </div>
                    <model-list-select
                        :list="publishes"
                        v-model="chosenPublishId"
                        option-value="id"
                        :custom-text="getText"
                        placeholder="Издательство...">
                    </model-list-select>
                    <input type="submit" value="Добавить книгу!" />
                </form>
            </div>
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
        themes: [],
        bookThemes: [],
        chosenPublishId: null,
        image: null,
        pdf: null,
        author: "",
        year: "",
        name: "",
        covered: true
    }
  },
  created() {
    this.$http.get('/Publish')
        .then((responce) => this.publishes = responce.data)
        .catch((error) => {
            console.log(error);
            this.publishes = [];
        });
    this.$http.get('/Theme')
        .then((responce) => this.themes = responce.data);
    this.covered = false;
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
        data.append('Themes[]', this.bookThemes);
        this.covered = true;
        this.$http.post('/Book', data)
            .then(() => {
                this.covered = false;
                this.$router.push({ name: 'home' });
            })
            .catch((error) => {
                this.covered = false;
                if(error.response.status == 401) {
                    this.$router.push({ name: 'login', params: { register: 'false' } });
                }
            });
    }
  },
  components: {
      ModelListSelect
    }
}
</script>

<style scoped>
.invisible {
    display: none;
}
.relative {
    position: relative;
}
.cover {
    background: rgba(0, 0, 0, 0.8);
    position: absolute;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    z-index: 1000;
}
.wrapper {
    text-align: center;
    width: 40%;
    height: 90vh;
}
form {
    border: 1px solid gray;
    border-radius: 12px;
    padding: 40px;
    /*height: 50%;*/

    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
}
.text-input {
    width: 100%;
    border: 1px solid gray;
    border-radius: 4px;
    padding: 5px;
    text-align: center;
}
select {
    text-align: center;
}
.themes-wrapper {
    display: flex;
    flex-wrap: wrap;
    flex-direction: row;
}
</style>
