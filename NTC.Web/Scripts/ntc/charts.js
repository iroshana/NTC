
var Charts = new Vue({
    el: '#chart',
    data: {
        chart: {
            adPannel: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            cancel: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            punish: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            finePay: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        }
    },
    methods: {
        getAllChartDetails: function () {
            $('#spinner').css("display", "block");
            this.$http.get(apiURL + 'api/DeMerit/ChartData').then(function (response) {
                if (response.body.messageCode.code == 1) {
                    this.chart = response.body.chart;
                    this.bindData();
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
        bindData: function () {
            if ($('#advising').length) {
                var ctx = document.getElementById("advising");
                var advisingChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ["January", "February", "March", "April", "May", "June", "July", "Augest", "September", "Octomber", "November", "December"],
                        datasets: [{
                            label: "Advising",
                            backgroundColor: "rgba(38, 185, 154, 0.31)",
                            borderColor: "rgba(38, 185, 154, 0.7)",
                            pointBorderColor: "rgba(38, 185, 154, 0.7)",
                            pointBackgroundColor: "rgba(38, 185, 154, 0.7)",
                            pointHoverBackgroundColor: "#fff",
                            pointHoverBorderColor: "rgba(220,220,220,1)",
                            pointBorderWidth: 1,
                            data: Charts.chart.adPannel
                        }]
                    },
                });
            }

            if ($('#fine').length) {
                var ctx = document.getElementById("fine");
                var fineChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ["January", "February", "March", "April", "May", "June", "July", "Augest", "September", "Octomber", "November", "December"],
                        datasets: [{
                            label: "Fine",
                            backgroundColor: "rgba(3, 88, 106, 0.3)",
                            borderColor: "rgba(3, 88, 106, 0.70)",
                            pointBorderColor: "rgba(3, 88, 106, 0.70)",
                            pointBackgroundColor: "rgba(3, 88, 106, 0.70)",
                            pointHoverBackgroundColor: "#fff",
                            pointHoverBorderColor: "rgba(151,187,205,1)",
                            pointBorderWidth: 1,
                            data: Charts.chart.finePay
                        }]
                    },
                });
            }

            if ($('#punishment').length) {
                var ctx = document.getElementById("punishment");
                var fineChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ["January", "February", "March", "April", "May", "June", "July", "Augest", "September", "Octomber", "November", "December"],
                        datasets: [{
                            label: "Punishment",
                            backgroundColor: "rgba(38, 185, 154, 0.31)",
                            borderColor: "rgba(38, 185, 154, 0.7)",
                            pointBorderColor: "rgba(38, 185, 154, 0.7)",
                            pointBackgroundColor: "rgba(38, 185, 154, 0.7)",
                            pointHoverBackgroundColor: "#fff",
                            pointHoverBorderColor: "rgba(220,220,220,1)",
                            pointBorderWidth: 1,
                            data: Charts.chart.punish
                        }]
                    },
                });
            }

            if ($('#cancelation').length) {
                var ctx = document.getElementById("cancelation");
                var fineChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ["January", "February", "March", "April", "May", "June", "July", "Augest", "September", "Octomber", "November", "December"],
                        datasets: [{
                            label: "Cancelation",
                            backgroundColor: "rgba(3, 88, 106, 0.3)",
                            borderColor: "rgba(3, 88, 106, 0.70)",
                            pointBorderColor: "rgba(3, 88, 106, 0.70)",
                            pointBackgroundColor: "rgba(3, 88, 106, 0.70)",
                            pointHoverBackgroundColor: "#fff",
                            pointHoverBorderColor: "rgba(151,187,205,1)",
                            pointBorderWidth: 1,
                            data: Charts.chart.cancel
                        }]
                    },
                });
            }
        }
    },
    mounted() {
        this.getAllChartDetails();
    }
});