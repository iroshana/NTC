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
        complainNo: '',
        complainList: [],
        complainMgt: {
            id: '',
            description: '',
            Category: []
        },
        deMeritRecordList: [],
        deMeritMgt: { id: '', memberDeMerit: [] },
        demeritNo: '',
        noticeList: []

    },
    methods: {
        getAllMemeberNotice: function (userId) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Notice/GetMemberNotice', {
                params: {
                    memberId: userId
                }
            }).then(function (response) {
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
        },
        addComplain: function () {
            $(location).attr('href', webURL + 'DriverConductor/AddCompian?memberId=' + this.memeber.id);
        },
        sendMsg: function () {
            msgAlert.isSuccess = true;
            msgAlert.alertMessage = 'Message Send Succesfully.'
            msgAlert.showModal();
        },
        noticeModalShow: function () {
            NoticeModal.noticeVm = {
                id: '0',
                Content: '',
                NoticeCode: '',
                Type: '',
                memberId: getUrlParameter("memberId")
            };
            $('#noticeModal').modal('show');
        },
        getMemberDetails: function (id) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Member/GetMember', {
                params: {
                    id: id
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
        },
        getAllComplainList: function (userId) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Complain/GetComplainNo', {
                params: {
                    userId: userId
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.complainList = response.body.complainNo;
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
        getSelectedComplain: function (complainNo) {
            if (complainNo != "") {
                var userId = getUrlParameter("memberId");
                $('#spinner').css("display", "block");
                this.$http.get(apiURL + 'api/Complain/GetComplainByNo', {
                    params: {
                        complainNo: complainNo,
                        userId: userId
                    }
                }).then(function (response) {
                    if (response.body.messageCode.code == 1) {
                        this.complainMgt = response.body.complain;
                    } else {
                        this.complainMgt = {
                            id: '',
                            description: '',
                            Category: []
                        };
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
        addPoint: function () {
            $(location).attr('href', webURL + 'DriverConductor/AddPoints?memberId=' + this.memeber.id);
        },
        getAllDeMeritDropDown: function (userId) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/GetDemeritByMemberId', {
                params: {
                    memberId: userId
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.deMeritRecordList = response.body.demerit;
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
        getSelectedDeMerit: function (deMeritNo) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/GetDemeritByNo', {
                params: {
                    deMeritNo: deMeritNo
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.deMeritMgt = response.body.demerit;
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
        this.getAllComplainList(getUrlParameter("memberId"));
        this.getAllDeMeritDropDown(getUrlParameter("memberId"));
        this.getAllMemeberNotice(getUrlParameter("memberId"));
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
                memberId: getUrlParameter("memberId"),
                MemberNotices: {
                    memberId: '',
                    IsSent: false,
                    IsOpened: false
                }
            };
            this.submit = false;
        },
        saveNotice: function () {
            this.submit = true;
            console.log(this.noticeVm);
            if (this.noticeVm.Content) {
                this.submit = false;
                $('#spinner').css("display", "block");
                this.$http.post(apiURL + 'api/Notice/AddNotice', this.noticeVm).then(function (response) {
                    if (response.body.messageCode.code == 1) {
                        MemberDetails.noticeList.push(this.noticeVm);
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
        this.noticeVm.memberId = getUrlParameter("memberId");
    }
});