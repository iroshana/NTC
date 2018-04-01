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
        }
    },
    mounted() {

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
                Notice.noticeList.push(this.noticeVm);
                $('#noticeModal').modal('hide');
            }
        }
    }
});