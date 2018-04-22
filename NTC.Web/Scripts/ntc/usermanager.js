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

var Login = new Vue({
    el: '#loginForm',
    data: {
        userModel: {
            userName: '',
            password: ''
        },
        submitted: false
    },
    methods: {
        login: function () {
            this.submitted = true;
            if (this.userModel.userName && this.userModel.password) {
                this.submitted = true;
                $('#spinner').css("display", "block");
                this.$http.post(apiURL + 'api/User/Login', this.userModel).then(function (response) {
                    if (response.body.messageCode.code == 1) {
                        console.log(response.body.userRole);
                        localStorage.removeItem('role');
                        localStorage.setItem('role', response.body.userRole);
                        if (response.body.userRole == 'ADMIN') {
                            window.location.replace(webURL + 'Dashboard/AdminDashboard');
                        } else if(response.body.userRole == "OFFICER") {
                            window.location.replace(webURL + 'DriverConductor/List');
                        } else {
                            window.location.replace(webURL + 'DriverConductor/MemeberFullProfile?memberId=' + response.body.memberId);
                        }
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
                    console.log(response);
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