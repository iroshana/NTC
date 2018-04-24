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

var AddComplain = new Vue({
    el: '#complain',
    data: {
        complainVm: {
            id: '0',
            memberId: '',
            bus: { id: '', busNo: '', route: { id: '1', routeNo: '', from: '', to: '' }},            
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
            file: {},
            filePath:'',
            route: '',
            evidence: {
                fileName: '',
                evidenceNo: '',
                extension: '',
                filePath: ''
            },
            status:'Unresolve'
        },
        categoryList: [],
        isResultShow: false
    },
    methods: {
        getBusDetails: function (busno) {            
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Complain/SearchBus', {
                params: {
                    busNo: busno
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {                                        
                    this.complainVm.bus = JSON.parse(JSON.stringify(response.body.bus));
                    this.complainVm.route = this.complainVm.bus.route;
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
        getAllCategory: function () {
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
        },
        removeElement: function (array, element) {
            return array.filter(e => e !== element);
        },
        addComplain: function () {
            this.complainVm.Category = this.categoryList;

            if (this.complainVm.isEvidenceHave) {
                var formData = new FormData();
                formData.append('UploadedImage', this.complainVm.file);
                formData.append('nic', this.complainVm.complainNo);
                formData.append('uploadedFileName', "");
                formData.append('fileExtension', '.png');
                formData.append('imageFolder', "Evidence");  
                
                this.$http.post(apiURL + '/api/DocumentUpload/MediaUpload', formData).then(function (response) {
                            if (response.body.messageCode.code == 1) {
                                this.complainVm.evidence.filePath = response.body.filesData[0].filePath;
                                this.complainSave();
                            }
                            else {

                            }
                        }); 
            } else {
                this.complainSave();
            }           
        },
        complainSave: function(){
             $('#spinner').css("display", "block");
            this.$http.post(apiURL + 'api/Complain/AddComplain', this.complainVm).then(function (response) {
                if (response.body.messageCode.code == 1) {                    
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = "Complain Save Successfully. Thank You";
                    msgAlert.showModal();
                    var memberId = getUrlParameter("memberId");
                    if (memberId != 'undefined') {
                        $(location).attr('href', webURL + 'DriverConductor/MemeberFullProfile?memberId=' + memberId);
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
                if (response.statusText == "Unauthorized") {
                    $(location).attr('href', webURL + 'Account/Login');
                }
            });        
        },
        getComplainNo: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Complain/GetLastComplainNo').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.complainVm.complainNo = response.body.complainNo;
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
        $('#dateComplain').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1,
            maxDate: '0'
        });

        $('#dateComplain').on('change', function () {
            AddComplain.complainVm.complainDate = $('#dateComplain').val();
            $(this).datepicker('hide');
        });

        this.complainVm.memberId = getUrlParameter("memberId");
        if (this.complainVm.memberId == 'undefined') {
            this.complainVm.memberId = 0;
            console.log(this.complainVm.memberId);
        }
        this.getAllCategory();
        this.getComplainNo();



        $(document).ready(function () {
            var $uploadCrop;
            $uploadCrop = $('#item-img').croppie({
                viewport: {
                    width: 150,
                    height: 150,
                    type: 'squre'
                },
                boundary: { width: 250, height: 250 },
            });

            $('#evidence').on('change', function () {
                $("#item-img-result").empty();
                AddComplain.isResultShow = false;

                if (this.files && this.files[0]) {
                    var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
                    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) != -1) {
                        var reader = new FileReader();

                        reader.onload = function (e) {
                            $('.upload-demo').addClass('ready');
                            $uploadCrop.croppie('bind', {
                                url: e.target.result
                            }).then(function () {
                                console.log('jQuery bind complete');
                            });
                        }
                        reader.readAsDataURL(this.files[0]);
                    }
                    else {

                    }
                }
                else {

                }
            });
            $('#cropImage').on('click', function (ev) {
                $uploadCrop.croppie('result', {
                    type: 'blob',
                    size: { width: 150, height: 150 },
                    format: 'jpeg'
                }).then(function (resp) {
                    AddComplain.complainVm.file = resp;
                    $uploadCrop.croppie('result', {
                        type: 'canvas',
                        size: 'viewport'
                    }).then(function (resp) {
                        AddComplain.isResultShow = true;
                        var html;
                        if (resp) {
                            html = '<img src="' + resp + '" />';
                            $("#item-img-result").empty();
                            $("#item-img-result").append(html);
                            $uploadCrop.croppie('bind', {
                                url: ''
                            })
                        }
                    });
                });
            });

        });
    }
});