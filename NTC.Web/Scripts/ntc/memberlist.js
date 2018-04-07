

var MemberList = new Vue({
    el:'#list',
    data:{
        memberList: []
    },
    methods: {
        getAllMembers: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/Member/GetAllMembers').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.memberList = response.body.members;
                    this.bindDatatable();
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
        bindDatatable: function () {
            $('#datatable-member').DataTable({
                "data": this.memberList,
                "bPaginate": false,
                "bFilter": false,
                "bInfo": false,
                "bDestroy": true,
                "rowId": 'id',
                "aoColumns": [                    
                    {
                        "data": "type", sWidth: "10%", bSortable: false, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "nic", sWidth: "15%", bSortable: false, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "fullName", sWidth: "20%", bSortable: false, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "cetificateNo", sWidth: "20%", bSortable: false, "render": function (data, type, row, meta) {
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
            var table = $('#datatable-member').DataTable();
        }
    },
    mounted() {
        this.getAllMembers();

        $('#datatable-member').on('click', '#btnView', function () {
                var table = $('#datatable-member').DataTable();
                var tr = $(this).closest('tr');
                var row = table.row(tr);
                var rowData = table.row(tr).data();

                $(location).attr('href', webURL + 'DriverConductor/MemeberFullProfile?memberId=' + rowData.id);
            });
    }
});