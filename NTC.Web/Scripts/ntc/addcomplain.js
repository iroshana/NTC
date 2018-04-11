﻿var msgAlert = new Vue({
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

var AddComplain = new Vue({
    el: '#complain',
    data: {
        complainVm: {
            id: '0',
            bus: { id: '', busNo: '', route: { id: '1', routeNo: '', from: '', to: '' }},            
            place: '',
            method: '',
            complainDate: '',
            description: '',
            userId: '',
            evidenceId: '',
            employeeId: '',
            isEvidenceHave: false,
            isInqueryParticipation: false,
            Category: [],
            complainerName: '',
            complainerAddress: '',
            telNo: '',
            file:''
        },
        categoryList: []
    },
    methods: {
        getBusDetails: function (busno) {            
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Complain/SearchBus', {
                params: {
                    busNo: busno
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {                                        
                    this.complainVm.bus = JSON.parse(JSON.stringify(response.body.bus));                   
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
        getAllCategory: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Complain/GetAllCategorties').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.categoryList = response.body.categories;
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
        removeElement: function (array, element) {
            return array.filter(e => e !== element);
        },
        addComplain: function () {
            this.complainVm.Category = this.categoryList;

            $('#spinner').css("display", "block");
            this.$http.post(apiURL + 'api/Complain/AddComplain', this.complainVm).then(function (response) {
                if (response.body.messageCode.code == 1) {                    
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = "Complain Save Successfully.";
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
        $('#dateComplain').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1,
            maxDate: '0'
        });

        $('#dateComplain').on('change', function () {
            AddComplain.complainVm.complainDate = $('#dateComplain').val();
            $(this).datepicker('hide');
        });

        this.complainVm.userId = getUrlParameter("memberId");
        this.getAllCategory();
    }
});