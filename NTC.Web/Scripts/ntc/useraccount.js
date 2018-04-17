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

var Account = new Vue({
    el:'#memeberAcc',
    data: {
        roleList: [],
        userView: {
            id: '0',
            userName: '',
            email: '',
            nic: '',
            telNo: '',
            password: '',
            roleId: ''

        },
        submitted: false

    },
    methods: {
        clearData: function () {
            this.userView = {
                id: '0',
                userName: '',
                email: '',
                nic: '',
                telNo: '',
                password: '',
                roleId: ''

            };
            this.submitted = false;
        },
        getAllRols: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/User/GetRoles').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.roleList = response.body.roleList;
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
        createAccount: function () {
            this.submitted = true;
            if (this.userView.userName && this.userView.password) {
                this.submitted = false;
                $('#spinner').css("display", "block");
                this.$http.post(apiURL + 'api/User/RegisterUser', this.userView).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.clearData();
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = "Member Account Create Successfully";
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
    },
    mounted() {
        this.getAllRols();
    }
});