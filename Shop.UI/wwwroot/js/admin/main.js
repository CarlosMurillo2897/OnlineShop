var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        objectIndex: 0,
        valueCurrency: "₡ 1.99",
        productModel: {
            id: 0,
            name: "Product Name",
            description: "Product Description",
            value: "1.99",
        },
        products: [],
    },
    methods: {
        getProduct(id) {
            this.loading = true;
            axios.get('/Admin/product/' + id)
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        updateProduct() {
            this.loading = true;
            axios.put('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                    this.productModel.id = 0;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        editProduct(product, index) {
            this.objectIndex = index;
            this.productModel = {
                id: product.id,
                name: product.name,
                description: product.description,
                value: product.value
            }
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/Admin/products/' + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        priceChanged() {
            let newValue = this.valueCurrency.replace(/[^0-9\.]/g, '');
            let hasDecimals = newValue.endsWith('.');

            let regex = RegExp(/\./g);
            if (newValue.match(regex)?.length > 1) {
                let counter = 0;
                let placeHolder = '<holder>';
                newValue = newValue.replace(
                    regex, (_) => counter++ ? '' : placeHolder 
                ).replace(
                   placeHolder, '.'
                );
            }

            if (newValue === '') {
                newValue = '0';
            }
            
            this.productModel.value = newValue;
            this.valueCurrency = '₡ '
                + new Intl.NumberFormat(
                    'en-US',
                    { maximumFractionDigits: 2 }
                ).format(newValue)
                + (hasDecimals ? '.' : '');
        }
    }
});