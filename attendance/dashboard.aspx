<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="attendance.dashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="page-title-box">
                <h4 class="page-title">
                    <asp:Literal ID="pageNamePlace1" runat="server"></asp:Literal></h4>
                <ol class="breadcrumb p-0 m-0">
                    <li>
                        <%=this.projectName%>
                    </li>
                    <li class="active">
                        <asp:Literal ID="pageNamePlace2" runat="server"></asp:Literal>
                    </li>
                </ol>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- end row -->

    <div class="row">
        <div class="col-lg-3 col-md-6">
            <div class="card-box widget-box-two widget-two-info">
                <i class="mdi mdi-source-branch widget-two-icon"></i>
                <div class="wigdet-two-content text-white">
                    <p class="m-0 text-uppercase font-600 font-secondary text-overflow" title="Statistics">Branch</p>
                    <h2 class="text-white"><span data-plugin="counterup">
                        <asp:Literal ID="branch" runat="server"></asp:Literal>
                    </span></h2>
                    <p class="m-0">&nbsp;</p>
                </div>
            </div>
        </div>
        <!-- end col -->
        <div class="col-lg-3 col-md-6">
            <div class="card-box widget-box-two widget-two-primary">
                <i class="mdi mdi-chart-areaspline widget-two-icon"></i>
                <div class="wigdet-two-content text-white">
                    <p class="m-0 text-uppercase font-600 font-secondary text-overflow" title="User This Month">Department</p>
                    <h2 class="text-white"><span data-plugin="counterup">
                        <asp:Literal ID="department" runat="server"></asp:Literal>
                    </span></h2>
                    <p class="m-0">&nbsp;</p>
                </div>
            </div>
        </div>
        <!-- end col -->
        <div class="col-lg-3 col-md-6">
            <div class="card-box widget-box-two widget-two-danger">
                <i class="mdi mdi-gender-female widget-two-icon"></i>
                <div class="wigdet-two-content text-white">
                    <p class="m-0 text-uppercase font-600 font-secondary text-overflow" title="Statistics">Employee (Female)</p>
                    <h2 class="text-white"><span data-plugin="counterup">
                        <asp:Literal ID="female" runat="server"></asp:Literal>
                    </span></h2>
                    <p class="m-0">&nbsp;</p>
                </div>
            </div>
        </div>
        <!-- end col -->
        <div class="col-lg-3 col-md-6">
            <div class="card-box widget-box-two widget-two-success">
                <i class="mdi mdi-gender-male widget-two-icon"></i>
                <div class="wigdet-two-content text-white">
                    <p class="m-0 text-uppercase font-600 font-secondary text-overflow" title="User Today">Employee (Male)</p>
                    <h2 class="text-white"><span data-plugin="counterup">
                        <asp:Literal ID="male" runat="server"></asp:Literal>
                    </span></h2>
                    <p class="m-0">&nbsp;</p>
                </div>
            </div>
        </div>
        <!-- end col -->

    </div>
    <!-- end row -->

    <div class="row">
        <div class="col-xs-12">
            <div class="card-box">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="demo-box">
                            <h4 class="header-title m-t-0">Bar Chart</h4>
                            <p class="text-muted font-13 m-b-10">
                                &nbsp;
                            </p>
                            <canvas id="bar" height="300"></canvas>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="demo-box">
                            <h4 class="header-title m-t-0">Pie Chart</h4>
                            <p class="text-muted font-13 m-b-30">
                                &nbsp;
                            </p>
                            <div id="pie-chart">
                                <div id="pie-chart-container" class="flot-chart" style="height: 260px;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end row -->
            </div>
        </div>
        <!-- end col-->
    </div>
    <!-- end row -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script>
        $(document).ready(function () {
            var data;
            $.ajax({
                method: 'post',
                url: 'dashboard.aspx/pieChartData',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (data) {
                    chart(data.d);
                }
            })
        })

        function chart(chartData) {
            !function ($) {
                "use strict";

                var FlotChart = function () {
                    this.$body = $("body")
                    this.$realData = []
                };

                //creates Pie Chart
                FlotChart.prototype.createPieGraph = function (selector, labels, datas, colors) {
                    var data = [{
                        label: labels[0],
                        data: datas[0]
                    }, {
                        label: labels[1],
                        data: datas[1]
                    }, {
                        label: labels[2],
                        data: datas[2]
                    }, {
                        label: labels[3],
                        data: datas[3]
                    }, {
                        label: labels[4],
                        data: datas[4]
                    }, {
                        label: labels[5],
                        data: datas[5]
                    }, {
                        label: labels[6],
                        data: datas[6]
                    }, {
                        label: labels[7],
                        data: datas[7]
                    }, {
                        label: labels[8],
                        data: datas[8]
                    }, {
                        label: labels[9],
                        data: datas[9]
                    }, {
                        label: labels[10],
                        data: datas[10]
                    }, {
                        label: labels[11],
                        data: datas[11]
                    }, {
                        label: labels[12],
                        data: datas[12]
                    }, {
                        label: labels[13],
                        data: datas[13]
                    }, {
                        label: labels[15],
                        data: datas[15]
                    }, {
                        label: labels[16],
                        data: datas[16]
                    }, {
                        label: labels[17],
                        data: datas[17]
                    }, {
                        label: labels[18],
                        data: datas[18]
                    }, {
                        label: labels[19],
                        data: datas[19]
                    }, {
                        label: labels[20],
                        data: datas[20]
                    }];
                    var options = {
                        series: {
                            pie: {
                                show: true
                            }
                        },
                        legend: {
                            show: true
                        },
                        grid: {
                            hoverable: true,
                            clickable: true
                        },
                        colors: colors,
                        tooltip: true,
                        tooltipOpts: {
                            content: "%s, %p.0%"
                        }
                    };

                    $.plot($(selector), data, options);
                },

                //initializing various charts and components
                FlotChart.prototype.init = function () {
                    //Pie graph data
                    var pielabels = chartData[0];
                    var datas = chartData[1];
                    var colors = ['#188ae2', '#4bd396', "#f5707a", "#f9c851", "#d697c6", "#ec521e", "#fb8349", "#757766", "#0fcd55", "#d45524", "#2f0c09", "#9d1e88", "#839abe", "#fc6816", "#447169", "#edf124", "#54f9c0", "#00447e", "#36ce73"];
                    this.createPieGraph("#pie-chart #pie-chart-container", pielabels, datas, colors);
                },

                //init flotchart
                $.FlotChart = new FlotChart, $.FlotChart.Constructor =
                FlotChart

            }(window.jQuery),

            //initializing flotchart
            function ($) {
                "use strict";
                $.FlotChart.init()
            }(window.jQuery);
   

            !function ($) {
                "use strict";

                var ChartJs = function () { };

                ChartJs.prototype.respChart = function (selector, type, data, options) {
                    // get selector by context
                    var ctx = selector.get(0).getContext("2d");
                    // pointing parent container to make chart js inherit its width
                    var container = $(selector).parent();

                    // enable resizing matter
                    $(window).resize(generateChart);

                    // this function produce the responsive Chart JS
                    function generateChart() {
                        // make chart width fit with its container
                        var ww = selector.attr('width', $(container).width());
                        switch (type) {
                            case 'Bar':
                                new Chart(ctx, { type: 'bar', data: data, options: options });
                                break;
                        }
                        // Initiate new chart or Redraw

                    };
                    // run function - render chart at first load
                    generateChart();
                },
                //init
                ChartJs.prototype.init = function () {

                    //barchart
                    var barChart = {
                        labels: chartData[0],
                        datasets: [
                            {
                                label: "Branch",
                                backgroundColor: "#188ae2",
                                borderColor: "#188ae2",
                                borderWidth: 1,
                                hoverBackgroundColor: "#188ae2",
                                hoverBorderColor: "#7fc1fc",
                                data: chartData[1]
                            }
                        ]
                    };
                    this.respChart($("#bar"), 'Bar', barChart);
                },
                $.ChartJs = new ChartJs, $.ChartJs.Constructor = ChartJs

            }(window.jQuery),

            //initializing
            function ($) {
                "use strict";
                $.ChartJs.init()
            }(window.jQuery);
        }

    </script>

</asp:Content>