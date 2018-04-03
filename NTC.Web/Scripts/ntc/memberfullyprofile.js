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
    el:'#fullyDetails',
    data: {
        memeber: {
            id: '',
            nicNo: '',
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
        },
        complainList: [],
        complainMgt: {
            complain:{}
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
        submit: false
    },
    methods: {
        saveNotice: function () {
            this.submit = true;
            if (this.noticeVm.note) {
                this.submit = false;
                MemberDetails.noticeList.push(this.noticeVm);
                $('#noticeModal').modal('hide');
            }
        }
    }
});