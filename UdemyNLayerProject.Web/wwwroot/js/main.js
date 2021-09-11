var app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loading: false,
        categories: [],
        categoryModel: {
            id: 0,
            name: ""
        },
        objectIndex: 0
    },
    mounted() {
        this.getCategories();
    },
    methods: {

       
        getCategories() {
            this.loading = true;
            axios.get('https://localhost:44391/api/categories')
                .then(res => {
                    this.categories = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        deleteCategory(data, index) {
            axios.delete('https://localhost:44391/api/categories/' + data.id)
                .then(res => {
                    this.categories.splice(index, 1);

                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createCategory() {
            this.loading = true;
            axios.post("https://localhost:44391/api/categories", this.categoryModel)
                .then(res => {
                    console.log(res.data);
                    this.categories.push(res.data);
                }).catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        updateCategory() {
            this.loading = true;
            axios.put('https://localhost:44391/api/categories', this.categoryModel)
                .then(res => {
                    this.categories.splice(this.objectIndex, 1, this.categoryModel);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        editCategory(data, index) {
            this.objectIndex = index;
            this.categoryModel = this.categories.filter(i => i.id == data.id)[0];
            this.editing = true;
        },
        newCategory() {
            this.editing = true;
            this.categoryModel.id = 0;
            this.categoryModel.name = "";
        },
        cancel() {
            this.editing = false;
        },
    },


});


