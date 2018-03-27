
var Report = new Vue({
    el: '#advisingReport',
    data: {
        fromDate: '',
        toDate:''
    },
    methods: {

    },
    mounted() {
        $('#fromDate').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $('#toDate').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $('#fromDate').on('change', function () {
            this.fromDate = $('#fromDate').val();
            $(this).datepicker('hide');
        });

        $('#toDate').on('change', function () {
            this.toDate = $('#toDate').val();
            $(this).datepicker('hide');
        });
    }
});