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
            type: '',
            isNotification1: false,
            isNotification2: false,
            isNotification3: false,
            notification1:'',
            notification2:'',
            notification3:''
        },
        complainNo: '',
        complainList: [],
        complainMgt: {
            id: '',
            description: '',
            Category: []
        },
        isComplainShow: false,
        complainsCategory: [],
        deMeritRecordList: [],
        deMeritMgt: { id: '', memberDeMerit: [] },
        demeritNo: '',
        noticeList: [],
        complainVm: {
            id: '0',
            bus: { id: '', busNo: '', route: { id: '1', routeNo: '', from: '', to: '' } },
            place: '',
            complainNo: '',
            method: '',
            complainDate: '',
            description: '',
            userId: '',
            evidenceId: '',
            employeeId: '',
            isEvidenceHave: false,
            isInqueryParticipation: false,
            Category: [],
            complainerName: '',
            complainerAddress: '',
            telNo: '',
            file: '',
            route: '',
            status:''
        },
        categoryList: [],
        role: '',
        imageURL: '',
        statusComplain: 'Unresolve'
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
        sendMsg: function (note) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Notice/SentNotice', {
                params: {
                    noticeId: note.ID
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.getAllMemeberNotice(getUrlParameter("memberId"));
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
                        this.complainVm = response.body.complain;
                        this.isComplainShow = false;
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
            if (deMeritNo == 0) {
                this.getOverallDeMerit();
            } else {
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
        getOverallDeMerit: function () {
            var memberId = getUrlParameter("memberId");
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/GetDemeritSummery', {
                params: {
                    memberId: memberId
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.deMeritMgt.memberDeMerit = response.body.merits;
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
        getComplainDetails: function () {
            var memberId = getUrlParameter("memberId");
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Complain/GetAllComplainsByMember', {
                params: {
                    memberId: memberId
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.complainsCategory = response.body.complainsCategory;
                    this.isComplainShow = true;
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
        toggleComplainStatus: function () {
            this.isComplainShow = true;
            this.complainNo = "";
        },
        complainAction: function (status) {
            $('#spinner').css("display", "block");
            this.$http.post(apiURL + 'api/Complain/ChangeComplainStatus?complainId=' + MemberDetails.complainVm.id + '&status=' + status).then(function (response) {
                if (response.body.messageCode.code == 1) {                    
                        $(location).attr('href', webURL + 'DriverConductor/AddPoints?memberId=' + this.memeber.id);                                      
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
        complainActionAdminChange: function (status) {
            $('#spinner').css("display", "block");
            this.$http.post(apiURL + 'api/Complain/ChangeComplainStatus?complainId=' + MemberDetails.complainVm.id + '&status=' + status).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = "Successfully Status Change";
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
    },
    mounted() {
        this.role = localStorage.getItem('role');

        this.getMemberDetails(getUrlParameter("memberId"));
        this.getAllComplainList(getUrlParameter("memberId"));
        this.getAllDeMeritDropDown(getUrlParameter("memberId"));
        this.getAllMemeberNotice(getUrlParameter("memberId"));
        this.getOverallDeMerit();
        this.getComplainDetails();

        this.imageURL = imageURL;
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
                        MemberDetails.getAllMemeberNotice(getUrlParameter("memberId"));
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