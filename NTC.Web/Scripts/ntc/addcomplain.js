

var AddComplain = new Vue({
    el: '#complain',
    data:{
        complainVm:{
            id:'',
            bus:{id:'', busNo:'', route:{id:'',routeNo:'',from:'',to:''}},
            place:'',
            time:'',
            method:'',
            complainCode:'',
            description:'',
            userId:'',
            evidenceId:'',
            employeeId:'',
            isEvidenceHave:false,
            isInqueryParticipation:false,
            Category:[],
        },
        categoryList:[]
    },
    methods:{
        getAllCategory: function(){
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Complain/GetAllCategorties').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.categoryList = response.body.categories;
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
        //this.getAllCategory();
    
    }
});