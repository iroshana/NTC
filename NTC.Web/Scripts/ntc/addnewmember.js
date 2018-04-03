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
            type:'',
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
        submitted: false,
        isResultShow: true
    },
    methods: {
        validate: function () {
            this.submitted = true;            
            if (this.memeber.nicNo && this.memeber.dob && this.memeber.fullName && this.memeber.permanetAddress) {
                this.submitted = false;
                var formData = new FormData();
                formData.append('UploadedImage', this.memeber.image);                
                formData.append('nic', this.memeber.nicNo);
                formData.append('uploadedFileName', "");
                formData.append('fileExtension', '.png');
                //this.submitMemeber();
            }
        },
        submitMemeber: function () {
            $('#spinner').css("display", "block");
            this.$http.post(apiURL + '', this.memeber).then(function (response) {
                if (response.body.messageCode.code == 1) {                    
                    msgAlert.isSuccess = true;
                    msgAlert.alertMessage = "Member Save Successfully.";
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
            this.memeber.dob = $('#dob').val();
            $(this).datepicker('hide');
        });

        $('#dateIssued').on('change', function () {
            this.memeber.dateIssued = $('#dateIssued').val();
            $(this).datepicker('hide');
        });

        $('#dateValidity').on('change', function () {
            this.memeber.dateValidity = $('#dateValidity').val();
            $(this).datepicker('hide');
        });

        $('#dateJoined').on('change', function () {
            this.memeber.dateJoin = $('#dateJoined').val();
            $(this).datepicker('hide');
        });


        $('#fullName').on('change', function () {
            var initials = "";
            var x = $('#fullName').val().split(' ');
            var a = x.length;

            for (var i = 0; i < a - 1; i++) {
                initials += x[i].charAt(0).toUpperCase() + " ";
            }
            //$('#initial').val(initials + ' ' + x[a - 1].toString());
            this.memeber.nameWithInitial = initials + ' ' + x[a - 1].toString();
        });

        //$(document).ready(function(){
        //    var $uploadCrop;
        //    $uploadCrop = $('#item-img').croppie({
        //        viewport: {
        //            width: 150,
        //            height: 150,
        //            type: 'squre'
        //        },
        //        boundary: { width: 250, height: 250 },                
        //    });
        
        //    $('#upload').on('change', function () {
        //        $("#item-img-result").empty();
        //        Memeber.isResultShow = false;
                
        //        if (this.files && this.files[0]) {
        //            var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        //            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) != -1) {
        //                var reader = new FileReader();

        //                reader.onload = function (e) {
        //                    $('.upload-demo').addClass('ready');
        //                    $uploadCrop.croppie('bind', {
        //                        url: e.target.result
        //                    }).then(function () {
        //                        console.log('jQuery bind complete');
        //                    });
        //                }
        //                reader.readAsDataURL(this.files[0]);
        //            }
        //            else {
                        
        //            }
        //        }
        //        else {
                    
        //        }
        //    });
        //    $('#cropImage').on('click', function (ev) {
        //        $uploadCrop.croppie('result', {
        //            type: 'blob',
        //            size: { width: 150, height: 150 },
        //            format: 'jpeg'
        //        }).then(function (resp) {
        //            Memeber.memeber.image = resp;
        //            $uploadCrop.croppie('result', {
        //                type: 'canvas',
        //                size: 'viewport'
        //            }).then(function (resp) {
        //                Memeber.isResultShow = true;
        //                var html;
        //                if (resp) {
        //                    html = '<img src="' + resp + '" />';
        //                    $("#item-img-result").empty();
        //                    $("#item-img-result").append(html);
        //                    $uploadCrop.croppie('bind', {
        //                        url: ''
        //                    })
        //                }
        //            });
        //        });
        //    });
           
        //});
    }
});