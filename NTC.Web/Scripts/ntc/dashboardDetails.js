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
            redNoticeMembers: '0'

        }
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