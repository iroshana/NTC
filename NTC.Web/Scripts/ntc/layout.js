

var Layout = new Vue({
    el: '#menu',
    data: {
        role: ''
    },
    methods:{

    },
    mounted() {
        this.role = localStorage.getItem('role');
    }
 });