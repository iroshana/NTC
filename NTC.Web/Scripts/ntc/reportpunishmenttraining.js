
var Report = new Vue({
    el: '#punishmentReport',
    data: {
        fromDate: '',
        toDate:'',
        dataList: []
    },
    methods: {
        getReportData: function () {
            var search = {
                colorCodeId: '3',
                fromDate: this.fromDate,
                toDate: this.toDate,
                typeId: '0',
                order: 'ASC'
            };

            $('#spinner').css("display", "block");
            this.$http.post(apiURL + 'api/Report/GetDemeritReports', search).then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.dataList = response.body.roleList;
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
            $('#datatable-punishment').DataTable({
                "data": this.dataList,
                "bPaginate": true,
                "bFilter": true,
                "bInfo": true,
                "bDestroy": true,
                "rowId": 'id',
                "aoColumns": [
                    {
                        "data": "fullName", sWidth: "15%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "ntcNo", sWidth: "10%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "description", sWidth: "30%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    },
                    {
                        "data": "inqueryDate", sWidth: "10%", bSortable: true, "render": function (data, type, row, meta) {
                            return '<a data-view="view" data-dataId="' + row.id + '">' + ((data != null) ? data : '<center>-</center>') + '</a>';
                        }
                    }
                ],
                "order": [[0, "desc"]]
            });
            var table = $('#datatable-punishment').DataTable();
        },
        exportReport: function () {

        }

    },
    mounted() {
        $('#fromDate').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $('#toDate').datepicker({
            dateFormat: 'yy-mm-dd',
            showOtherMonths: true,
            selectOtherMonths: true,
            numberOfMonths: 1
        });

        $("#fromDate").datepicker("setDate", new Date());
        $("#toDate").datepicker("setDate", new Date());

        $('#fromDate').on('change', function () {
            this.fromDate = $('#fromDate').val();
            $(this).datepicker('hide');
        });

        $('#toDate').on('change', function () {
            this.toDate = $('#toDate').val();
            $(this).datepicker('hide');
        });

        this.getReportData();
    }
});