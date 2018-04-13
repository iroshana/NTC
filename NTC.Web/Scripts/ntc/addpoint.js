

var Point = new Vue({
    el: '#point',
    data: {
        pointVm:{
            id: '0',
            deMeritNo: '',
            member: { id: '', fullName: '', licenceNo :''},
            route: { id: '', routeNo :'', from:'',to:''},
            inqueryDate: '',
            officer: { id: '', name :''},
            bus: {id:'', busNo:''},
            memberDeMerit: []

        },
        demeritList: []
    },
    methods: {
        getDemeritNo: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/GetLastDemeritNo').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.pointVm.deMeritNo = response.body.complainNo;
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
        this.getDemeritNo();
    }
});