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
                advisingPanelList: [],
                finePaymentList: [],
                punishmentList: [],
                cancelationList: []

            },
            conductor: {
                advisingPanelList: [],
                finePaymentList: [],
                punishmentList: [],
                cancelationList: []
            }
        }
    },
    methods:{
        loadDetails: function () {

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