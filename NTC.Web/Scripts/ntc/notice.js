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
        sendMsg: function () {
            msgAlert.isSuccess = true;
            msgAlert.alertMessage = 'Message Send Succesfully.'
            msgAlert.showModal();
        },
        getAllGenaralNotice: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api//').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.noticeList = response.body.noticeList;
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
        //this.getAllGenaralNotice();
    }
});

var NoticeModal = new Vue({
    el: '#noticeModal',
    data: {
        noticeVm: {
            id: '',
            note: ''

        },
        submit:false
    },
    methods: {
        saveNotice: function() {
            this.submit = true;
            if (this.noticeVm.note) {
                this.submit = false;
                 $('#spinner').css("display", "block");
                this.$http.post(apiURL + 'api//', this.noticeVm).then(function (response) {
                    if (response.body.messageCode.code == 1) {
                        Notice.noticeList.push(this.noticeVm);
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