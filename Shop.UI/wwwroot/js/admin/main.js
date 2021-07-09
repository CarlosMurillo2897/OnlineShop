var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        objectIndex: 0,
        valueCurrency: '₡ 1.99',
        languageFormat: 'en-US',
        productModel: {
            id: 0,
            name: 'Product Name',
            description: 'Product Description',
            value: '1.99',
        },
        products: [],
    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProduct(id) {
            this.loading = true;
            axios.get('/Admin/products/' + id)
                .then(res => {
                    console.log(res);
                    this.productModel = res.data;
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
        editProduct(id, index) {
            this.objectIndex = index;
            this.getProduct(id);

            this.valueCurrency = this.productModel.value;
            this.priceChanged();
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
            // Create common variables.
            let whiteSpace = '';
            let amountSymbol = '.';
            let decimalSymbol = this.languageFormat === 'en-US' ? amountSymbol : ',';

            // Replace with blanks everything but numbers and dots. i.e.: RegEx = /[^0-9\.]/g
            let newValue = this.valueCurrency.replace(
                RegExp(`[^0-9\\${decimalSymbol}]`, 'g'), 
                whiteSpace
            );

            // Check if amount has decimals and attach a decimalSymbol once input is converted.
            let hasDecimals = newValue.endsWith(decimalSymbol);

            // Check if there's more than one dot i.e.: 192.168.100.1
            let regex = RegExp(`\\${decimalSymbol}`, 'g');
            if (newValue.match(regex)?.length > 1) {
                // Replace all matches, but keep first one and others replace by a white space.
                let counter = 0;
                newValue = newValue.replace(
                    regex, (_) => counter++ ? whiteSpace : decimalSymbol
                );
            }

            // In case validations or input set newValue as empty assign '0' as value.
            newValue === whiteSpace ? newValue = '0' : null;

            // In case decimalSymbol is a '.' amount will be splitted by ',' and the other way around.
            decimalSymbol !== amountSymbol ? newValue = newValue.replace(decimalSymbol, amountSymbol) : null;

            // Concatenate ₡ symbol + formatted amount + decimals if has.
            this.valueCurrency = this.formatCurrency(newValue) + (hasDecimals ? decimalSymbol : whiteSpace);
            this.productModel.value = newValue;
        },
        formatCurrency(value) {
            return '₡ '
                + new Intl.NumberFormat(
                    this.languageFormat,
                    { maximumFractionDigits: 2 }
                ).format(value);
        }
    }
});