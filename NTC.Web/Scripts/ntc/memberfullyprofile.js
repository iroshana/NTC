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

var MemberDetails = new Vue({
    el: '#fullyDetails',
    data: {
        memeber: {
            id: '',
            nic: '',
            dob: '',
            fullName: '',
            nameWithInitial: '',
            permanetAddress: '',
            currentAddress: '',
            cetificateNo: '',
            trainingCenter: '',
            licenceNo: '',
            dateIssued: '',
            dateValidity: '',
            educationQuali: '',
            dateJoin: '',
            image: {},
            imagePath: "",
            type: ''
        },
        complainList: [],
        complainMgt: {
            complain: {}
        },
        deMeritRecordList: [],
        demeritNo: '',
        noticeList: []
    },
    methods: {
        sendMsg: function () {
            msgAlert.isSuccess = true;
            msgAlert.alertMessage = 'Message Send Succesfully.'
            msgAlert.showModal();
        },
        noticeModalShow: function () {
            NoticeModal.noticeVm = {
                id: '',
                note: ''
            }
            $('#noticeModal').modal('show');
        },
        getMemberDetails: function (id) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Member/GetMember', {
                params: {
                    id:  id
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.memeber = response.body.member;
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
        this.getMemberDetails(getUrlParameter("memberId"));
        //this.getMemberDetails(7);
    }
});

var NoticeModal = new Vue({
    el: '#noticeModal',
    data: {
        noticeVm: {
            id: '',
            note: ''

        },
        submit: false
    },
    methods: {
        saveNotice: function () {
            this.submit = true;
            if (this.noticeVm.note) {
                this.submit = false;
                $('#spinner').css("display", "block");
                this.$http.post(apiURL + 'api//', this.noticeVm).then(function (response) {
                    if (response.body.messageCode.code == 1) {
                        MemberDetails.noticeList.push(this.noticeVm);
                        $('#noticeModal').modal('hide');
                        msgAlert.isSuccess = true;
                        msgAlert.alertMessage = "Notice Save Successfully.";
                        msgAlert.showModal();
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
        }
    }
});