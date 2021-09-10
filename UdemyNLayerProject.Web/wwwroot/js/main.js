
var app = new Vue({
    el: '#app',
    data: {
        categories: []
    },
    mounted() {
        this.getCategories();
    },
    methods: {

        getCategories() {

            axios.get('https://localhost:44391/api/categories')
                .then(res => {
                    console.log(res);
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
            console.log(data, index);
            axios.delete('https://localhost:44391/api/categories/'+ data.id)
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
    },


});


