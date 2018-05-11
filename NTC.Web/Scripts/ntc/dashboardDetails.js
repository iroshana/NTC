var msgAlert = new Vue({
    el: "#alertModal",
    data: {
        isSuccess: true,
        alertMessage: "",
        modalShown: false,
        header: ''
    },
    methods: {
        showModal: function () {
            this.header = this.isSuccess ? "Success!" : "Error!";
            $("#alertModal").modal('show');
        }
    }
});

var DashBoard = new Vue({
    el: '#dash',
    data: {
        wighet: {
            driverCount: '-',
            highestConductorComplain: '0',
            highestDriverComplain: '0',
            highestConductorPoints: '0',
            highestDriverPoints: '0',
            redNoticeConductors: '0',
            redNoticeDrivers: '0',
            redNoticeMembers: '0',
            bestConductorsofMonth: '0',
            bestConductorsofYear: '0',
            bestdriversofMonth: '0',
            bestdriversofYear: '0'

        },
        driverMonth: '',
        driverYear: '',
        conductorMonth: '',
        conductorYear:''
    },
    methods: {
        getDashboardData: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DashBoard/GetDashboardCounts').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.wighet = response.body.dashboard;
                } else {
                    msgAlert.isSuccess = false;
                    msgAlert.alertMessage = response.body.messageCode.message;
                    msgAlert.showModal();
                }
                $('#spinner').css("display", "none");
            }).catch(function (response) {
                msgAlert.isSuccess = false;
                msgAlert.alertMessage = response.statusText;
                msgAlert.showModal();
                $('#spinner').css("display", "none");
                if (response.statusText == "Unauthorized") {
                    $(location).attr('href', webURL + 'Account/Login');
                }
            });
        },
        bestDriverMonth: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=1&isDash=true&month=true&date=' + DashBoard.driverMonth);
        },
        bestDriverYear: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=1&isDash=true&year=true&date=' + DashBoard.driverYear);
        },
        bestConductorMonth: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=2&isDash=true&month=true&date=' + DashBoard.conductorMonth);
        },
        bestConductorYear: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=2&isDash=true&year=true&date=' + DashBoard.conductorYear);
        },
        redNoticeDriver: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=1&isDash=true&isRedNotice=true');
        },
        redNoticeConductor: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=2&isDash=true&isRedNotice=true');
        },
        highestNoofComplainDriver: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=1&isDash=true&isNoOfComplain=true');
        },
        highestNoofComplainConductor: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=2&isDash=true&isNoOfComplain=true');
        },
        highestNoOfPointDriver: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=1&isDash=true&isNoOfPoints=true');
        },
        highestNoOfPointConductor: function () {
            $(location).attr('href', webURL + 'DriverConductor/List?memberType=2&isDash=true&isNoOfPoints=true');
        }
    },
    mounted() {
        this.getDashboardData();

        $('#driverMonth').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $("#driverMonth").datepicker("setDate", new Date());

        $('#driverYear').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $("#driverYear").datepicker("setDate", new Date());

        $('#conMonth').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $("#conMonth").datepicker("setDate", new Date());

        $('#conYear').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $("#conYear").datepicker("setDate", new Date());

        this.driverMonth = $('#driverMonth').val();
        this.driverYear = $('#driverYear').val();
        this.conductorMonth = $('#conMonth').val();
        this.conductorYear = $('#conYear').val();

        $('#driverMonth').on('change', function () {
            DashBoard.driverMonth = $('#driverMonth').val();
            $(this).datepicker('hide');
        });

        $('#driverYear').on('change', function () {
            DashBoard.driverYear = $('#driverYear').val();
            $(this).datepicker('hide');
        });

        $('#conMonth').on('change', function () {
            DashBoard.conductorMonth = $('#conMonth').val();
            $(this).datepicker('hide');
        });

        $('#conYear').on('change', function () {
            DashBoard.conductorYear = $('#conYear').val();
            $(this).datepicker('hide');
        });
    }
});