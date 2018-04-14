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

var Panaltie = new Vue({
    el:'#panalties',
    data:{
        details: {
            driver: {
                adPannel: [],
                finePay: [],
                punish: [],
                cancel: []

            },
            conductor: {
                adPannel: [],
                finePay: [],
                punish: [],
                cancel: []
            }
        }
    },
    methods:{
        loadDetails: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/GetPanelties').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.details = response.body.merits;
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
    mounted(){
        this.loadDetails();
    }
});


var NoticeModal = new Vue({
    el: '#paneltiesModal',
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
                msgAlert.isSuccess = true;
                msgAlert.alertMessage = 'Message Send Succesfully.'
                msgAlert.showModal();
                $('#paneltiesModal').modal('hide');
            }
        }
    }
});