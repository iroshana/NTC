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

var MemberList = new Vue({
    el: '#list',
    data: {
        memberListDriver: [],
        memberListConductor: []
    },
    methods: {
        getBestMember: function (date, type, isMonth) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Member/GetAllBestMembers', {
                params: {
                    date: date,
                    type: type,
                    isMonth: isMonth
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.memberListDriver = response.body.memberListDriver;
                    this.memberListConductor = response.body.memberListConductor;
                    this.bindDatatableDriver();
                    this.bindDatatableConductor();
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
        getRedNoticeMember: function (colorCode, fromdate, todate, type, point) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Member/GetAllRetnoticeMembers', {
                params: {
                    colorCode: colorCode,
                    fromdate: fromdate,
                    todate: todate,
                    type: type,
                    point: point
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.memberListDriver = response.body.memberListDriver;
                    this.memberListConductor = response.body.memberListConductor;
                    this.bindDatatableDriver();
                    this.bindDatatableConductor();
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
        getAllMembers: function (colorCode, fromdate, todate, type, point) {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Member/GetAllMembers', {
                params: {
                    colorCode: colorCode,
                    fromdate: fromdate,
                    todate: todate,
                    type: type,
                    point: point
                }
            }).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.memberListDriver = response.body.memberListDriver;
                    this.memberListConductor = response.body.memberListConductor;
                    this.bindDatatableDriver();
                    this.bindDatatableConductor();
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
        bindDatatableDriver: function () {
            $('#datatable-memberDriver').DataTable({
                "data": this.memberListDriver,
                "bPaginate": true,
                "bFilter": true,
                "bInfo": true,
                "bDestroy": true,
                "rowId": 'id',
                "aoColumns": [                    
                    {
                        "data": "nic", sWidth: "15%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "ntcNo", sWidth: "15%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "fullName", sWidth: "20%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "trainingCertificateNo", sWidth: "20%", bSortable: false, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "trainingCenter", sWidth: "20%", bSortable: false, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": null, "bSortable": false, sWidth: "10%",
                        "mRender": function (o) {
                            var viewBtn = '<button id="btnView" class="btn btn-info btn-icon" data-toggle="tooltip" data-placement="top" title="View Member"><div><i class="fa fa-check-circle-o"></i></div></a>';

                            return viewBtn;
                        }
                    }
                ],
                "order": [[0, "desc"]]
            });
            var table = $('#datatable-memberDriver').DataTable();
        },
        bindDatatableConductor: function () {
            $('#datatable-memberConductor').DataTable({
                "data": this.memberListConductor,
                "bPaginate": true,
                "bFilter": true,
                "bInfo": true,
                "bDestroy": true,
                "rowId": 'id',
                "aoColumns": [                    
                    {
                        "data": "nic", sWidth: "15%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "ntcNo", sWidth: "15%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "fullName", sWidth: "20%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "trainingCertificateNo", sWidth: "20%", bSortable: false, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "trainingCenter", sWidth: "20%", bSortable: false, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": null, "bSortable": false, sWidth: "10%",
                        "mRender": function (o) {
                            var viewBtn = '<button id="btnView" class="btn btn-info btn-icon" data-toggle="tooltip" data-placement="top" title="View Member"><div><i class="fa fa-check-circle-o"></i></div></a>';

                            return viewBtn;
                        }
                    }
                ],
                "order": [[0, "desc"]]
            });
            var table = $('#datatable-memberConductor').DataTable();
        }
    },
    mounted() {       

        var isDashBoard = getUrlParameter("isDash");
        if (isDashBoard) {
            if (getUrlParameter("memberType") == "1") {
                if (getUrlParameter("month")) {
                    var toDate = getUrlParameter("date");                 
                    this.getBestMember(toDate, 1, true);
                } else if (getUrlParameter("isRedNotice")) {
                    this.getRedNoticeMember(0, "", "", 1, 2);
                } else if (getUrlParameter("year")) {
                    var toDate = getUrlParameter("date");
                    this.getBestMember(toDate, 1, true);
                }
            } else {
                if(getUrlParameter("isRedNotice")){
                    this.getRedNoticeMember(0, "", "", 2, 2);
                } else if (getUrlParameter("month")) {
                    var toDate = getUrlParameter("date");
                    this.getBestMember(toDate, 2, true);
                } else if (getUrlParameter("year")) {
                    var toDate = getUrlParameter("date");
                    this.getBestMember(toDate, 2, true);
                }
            }
        } else {
            this.getAllMembers(0, "", "", 0, 0);
        }


        $('#datatable-memberDriver').on('click', '#btnView', function () {
            var table = $('#datatable-memberDriver').DataTable();
            var tr = $(this).closest('tr');
            var row = table.row(tr);
            var rowData = table.row(tr).data();

            $(location).attr('href', webURL + 'DriverConductor/MemeberFullProfile?memberId=' + rowData.id);
        });

        $('#datatable-memberConductor').on('click', '#btnView', function () {
            var table = $('#datatable-memberConductor').DataTable();
            var tr = $(this).closest('tr');
            var row = table.row(tr);
            var rowData = table.row(tr).data();

            $(location).attr('href', webURL + 'DriverConductor/MemeberFullProfile?memberId=' + rowData.id);
        });
    }
});