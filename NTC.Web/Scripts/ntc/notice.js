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

var Notice = new Vue({
    el:'#notice',
    data: {
        noticeList: [],        
    
    },
    methods: {
        sendMsg: function (note) {

        $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Notice/SentNotice', {
                params: {
                    noticeId: note.ID
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.getAllGenaralNotice();
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = 'Message Send Succesfully.'
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
        },

        sendMemberMsg: function (note) {

            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Notice/SentMemberNotice', {
                params: {
                    noticeId: note.ID
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.getAllGenaralNotice();
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = 'Message Send Succesfully.'
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
        },
        getAllGenaralNotice: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Notice/GetAllNotices').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.noticeList = response.body.notice;
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
        this.getAllGenaralNotice();
    }
});

var NoticeModal = new Vue({
    el: '#noticeModal',
    data: {
        noticeVm: {
            id: '0',
            Content: '',
            NoticeCode: '',
            Type: '',
            memberId: ''
        },
        submit: false
    },
    methods: {
        clearData: function () {
            this.noticeVm = {
                id: '0',
                Content: '',
                NoticeCode: '',
                Type: '',
                memberId: '0'                
            };
            this.submit = false;
        },
        saveNotice: function () {
            this.submit = true;
            if (this.noticeVm.Content) {
                this.submit = false;
                $('#spinner').css("display", "block");
                this.$http.post(apiURL + 'api/Notice/AddNotice', this.noticeVm).then(function (response) {
                    if (response.body.messageCode.code == 1) {
                        //MemberDetails.noticeList.push(this.noticeVm);
                        Notice.getAllGenaralNotice();
                        $('#noticeModal').modal('hide');
                        msgAlert.isSuccess = true;
                        msgAlert.alertMessage = "Notice Save Successfully.";
                        msgAlert.showModal();
                        this.clearData();
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
    },
    mounted() {
        
    }
});