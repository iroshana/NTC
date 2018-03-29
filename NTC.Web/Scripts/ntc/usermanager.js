
var Login = new Vue({
    el: '#loginForm',
    data: {
        userModel: {
            userName: '',
            password:''
        },
        submitted: false
    },
    methods: {
        login: function () {
            this.submitted = true;
            if (this.userModel.userName && this.userModel.password) {
                this.submitted = true;
            }
        }
    },
    mounted() {

    }
});