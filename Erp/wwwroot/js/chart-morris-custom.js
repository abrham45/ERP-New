'use strict';

const { data } = [];

var completed = 0;
var pending = 0;
var notstarted = 0;
var newArray = [];
var RiskArray = [];

$(document).ready(function () {

    $.ajax({
        url: "https://localhost:44307/api/GetTaskDue/",
        type: "get",
        contentType: "json",
        success: function (result, status, xhr) {

            $.each(result, function (index, value) {
                newArray.push({ y: value.taskName, a: value.duration, b: value.taskCost, c: value.taskProgress });
            });


            barChart(newArray)
        },
        error: function (xhr, status, error) {
            console.log(xhr)
        }
    });


    $.ajax({
        url: "https://localhost:44307/api/GetTaskRisk/",
        type: "get",
        contentType: "json",
        success: function (result, status, xhr) {

            $.each(result, function (index, value) {
                RiskArray.push({ y: value.asset, a: value.totalAssetTypeRisk });
            });


            RiskChart(RiskArray)
        },
        error: function (xhr, status, error) {
            console.log(xhr)
        }
    });

    $.ajax({
        url: "https://localhost:44307/api/GetTasks/",
        type: "get",
        contentType: "json",
        success: function (result, status, xhr) {
            notstarted = result.notStarted;
            completed = result.completed;
            pending = result.pending;

        },
        error: function (xhr, status, error) {
            console.log(xhr)
        }
    });

    function barChart(data) {
        Morris.Bar({
            element: 'morris-bar-chart',
            data: data,
            xkey: 'y',
            barSizeRatio: 0.70,
            barGap: 3,
            resize: true,
            responsive: true,
            ykeys: ['a', 'b', 'c'],
            labels: ['Duration', 'Cost', 'Progress'],
            barColors: ["0-#1de9b6-#1dc4e9", "0-#899FD4-#A389D4", "#04a9f5"]
        });
    }

    function RiskChart(data) {
        Morris.Bar({
            element: 'morris-bar-Risk-chart',
            data: data,
            xkey: 'y',
            barSizeRatio: 0.70,
            barGap: 3,
            resize: true,
            responsive: true,
            ykeys: ['a'],
            labels: ['Risk-Level'],
            barColors: ["0-#1de9b6-#1dc4e9"]
        });
    }
   
    Morris.Donut({
        element: 'morris-donut-chart',

        data:
            [{

                value: pending,
                label: 'Pending'
            },
            {
                value: notstarted,
                label: 'Not Started'
            },
            {
                value: completed,
                label: 'Completed'
            },

            ],

        colors: [
            '#1de9b6',
            '#A389D4',
            '#04a9f5',
            '#1dc4e9',
        ],
        resize: true,
        formatter: function (x) {
            return "Count : " + x
        }
    });

  


/*
    $.ajax({
        url: "https://localhost:44307/api/GetTaskHigh/",
        type: "get",
        contentType: "json",
        success: function (result, status, xhr) {
            high = result.high;
            low = result.low;
            medium = result.medium;

        },
        error: function (xhr, status, error) {
            console.log(xhr)
        }
    });

    Morris.Donut({
        element: 'morris-donut-Risk-chart',

        data:
            [{

                value: low,
                label: 'Low'
            },
            {
                value: high,
                label: 'High'
            },
            {
                value: medium,
                label: 'Medium'
            },

            ],

        colors: [
            '#1de9b6',
            '#A389D4',
            '#04a9f5',
            '#1dc4e9',
        ],
        resize: true,
        formatter: function (x) {
            return "Count : " + x
        }
    });
*/
   

  
   
});
