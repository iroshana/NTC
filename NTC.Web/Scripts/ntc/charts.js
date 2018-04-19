
var Charts = new Vue({
    el:'#chart',
    data: {

    },
    methods: {

    },
    mounted() {
        $(document).ready(function () {
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
                            data: [31, 74, 6, 39, 20, 85, 7,0 ,0, 0, 0, 0]
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
                            data: [31, 74, 6, 39, 20, 85, 7, 0, 0, 0, 0, 0]
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
                            data: [31, 74, 6, 39, 20, 85, 7, 0, 0, 0, 0, 0]
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
                            data: [31, 74, 6, 39, 20, 85, 7, 0, 0, 0, 0, 0]
                        }]
                    },
                });
            }


        });

        

        
    }
});