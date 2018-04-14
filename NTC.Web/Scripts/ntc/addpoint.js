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

var Point = new Vue({
    el: '#point',
    data: {
        pointVm:{
            id: '0',
            deMeritNo: '',
            member: { id: '', fullName: '', licenceNo :''},
            route: { id: '', routeNo :'', from:'',to:''},
            inqueryDate: '',
            officer: { id: '', name :''},
            bus: {id:'', busNo:''},
            memberDeMerit: []

        },
        demeritList: []
    },
    methods: {
        getDemeritNo: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/GetLastDemeritNo').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.pointVm.deMeritNo = response.body.complainNo;
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
        getBusDetail: function (busno) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Complain/SearchBus', {
                params: {
                    busNo: busno
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {                                        
                    this.pointVm.bus = JSON.parse(JSON.stringify(response.body.bus));
                    this.pointVm.route = this.pointVm.bus.route;
                } else {
                    this.pointVm.bus = { id: '', busNo: '' };
                    this.pointVm.route = { id: '', routeNo :'', from:'',to:''};
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
        getAllDemerits: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/GetAllMerits').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.demeritList = JSON.parse(JSON.stringify(response.body.merits));
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
        getOfficerDetails: function (name) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/GetOfficer', {
                params: {
                    name: name
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.pointVm.officer = JSON.parse(JSON.stringify(response.body.officer));
                } else {
                    this.pointVm.officer = { id: '', name: '' };
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
        addPoints: function () {
            this.pointVm.memberDeMerit = this.demeritList;

            $('#spinner').css("display", "block");
            this.$http.post(apiURL + 'api/DeMerit/AddDeMerit', this.pointVm).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = "De-Merit Save Successfully.";
                    msgAlert.showModal();
                    $(location).attr('href', webURL + 'DriverConductor/MemeberFullProfile?memberId=' + getUrlParameter("memberId"));
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

        $('#dateInquery').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1,
            maxDate: '0'
        });

        $('#dateInquery').on('change', function () {
            Point.pointVm.inqueryDate = $('#dateInquery').val();
            $(this).datepicker('hide');
        });
        this.pointVm.member.id = getUrlParameter("memberId");
        this.getDemeritNo();
        this.getAllDemerits();
    }
});