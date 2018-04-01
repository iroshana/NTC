

var DashBoard = new Vue({
    el: '#dash',
    data: {
        wighet: {
            driverCount:'0'
        }
    },
    methods: {

    },
    mounted() {
        $('#driverMonth').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $('#driverYear').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $('#conMonth').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $('#conYear').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });
    }
});