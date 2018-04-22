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

var Memeber = new Vue({
    el:'#newMemeber',
    data: {
        memeber: {
            id: '',
            typeId: '',
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
        },
        submitted: false,
        isResultShow: true,
        memeberTypeList: []
    },
    methods: {
        clearData: function(){
            this.memeber = {
                id: '',
                typeId: '',
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
            };
            this.submitted = false;
            this.isResultShow = true;
        
        },
        validate: function () {
            this.submitted = true;            
            if (this.memeber.nic && this.memeber.dob && this.memeber.fullName && this.memeber.permanetAddress) {
                this.submitted = false;
                if (this.memeber.image != null) {
                    var formData = new FormData();
                    formData.append('UploadedImage', this.memeber.image);                
                    formData.append('nic', this.memeber.nic);
                    formData.append('uploadedFileName', "");
                    formData.append('fileExtension', '.png');
                    formData.append('imageFolder', "profilePictures");


                    this.$http.post(apiURL + '/api/DocumentUpload/MediaUpload', formData).then(function (response) {
                            if (response.body.messageCode.code == 1) {
                                this.memeber.imagePath = response.body.filesData[0].filePath;
                                this.submitMemeber();
                            }
                            else {

                            }
                        });                       
                         
                }
                
            }
        },
        submitMemeber: function () {
            $('#spinner').css("display", "block");
            this.$http.post(apiURL + 'api/Member/AddMember', this.memeber).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.clearData();
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = "Member Save Successfully. NTC No = " + response.body.ntcNo;
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
        getMemberTypeList: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Member/GetAllMemberTypes').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.memeberTypeList = response.body.types;
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
        this.getMemberTypeList();

        $('#dob').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1,
            maxDate: '0'
        });

        $('#dateIssued').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1            
        });

        $('#dateValidity').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $('#dateJoined').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $('#dob').on('change', function () {
            Memeber.memeber.dob = $('#dob').val();
            $(this).datepicker('hide');
        });

        $('#dateIssued').on('change', function () {
            Memeber.memeber.dateIssued = $('#dateIssued').val();
            $(this).datepicker('hide');
        });

        $('#dateValidity').on('change', function () {
            Memeber.memeber.dateValidity = $('#dateValidity').val();
            $(this).datepicker('hide');
        });

        $('#dateJoined').on('change', function () {
            Memeber.memeber.dateJoin = $('#dateJoined').val();
            $(this).datepicker('hide');
        });


        $('#fullName').on('change', function () {
            var initials = "";
            var x = $('#fullName').val().split(' ');
            var a = x.length;

            for (var i = 0; i < a - 1; i++) {
                initials += x[i].charAt(0).toUpperCase() + " ";
            }
            Memeber.memeber.nameWithInitial = initials + ' ' + x[a - 1].toString();
        });

        $(document).ready(function(){
            var $uploadCrop;
            $uploadCrop = $('#item-img').croppie({
                viewport: {
                    width: 150,
                    height: 150,
                    type: 'squre'
                },
                boundary: { width: 250, height: 250 },                
            });
        
            $('#upload').on('change', function () {
                $("#item-img-result").empty();
                Memeber.isResultShow = false;
                
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
                    Memeber.memeber.image = resp;
                    $uploadCrop.croppie('result', {
                        type: 'canvas',
                        size: 'viewport'
                    }).then(function (resp) {
                        Memeber.isResultShow = true;
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