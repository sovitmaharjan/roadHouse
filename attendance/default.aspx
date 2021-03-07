<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="attendance._default" %>

<!DOCTYPE html>

<html>
<head>
    <!-- App favicon -->
    <link rel="shortcut icon" href="<%=this.baseUrl%>assets/images/favicon.ico" />

    <!-- App title -->
    <title><%=this.projectName%></title>

    <!-- App css -->
    <link href="<%=this.baseUrl%>assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="<%=this.baseUrl%>assets/css/core.css" rel="stylesheet" type="text/css" />
    <link href="<%=this.baseUrl%>assets/css/components.css" rel="stylesheet" type="text/css" />
    <link href="<%=this.baseUrl%>assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="<%=this.baseUrl%>assets/css/pages.css" rel="stylesheet" type="text/css" />
    <link href="<%=this.baseUrl%>assets/css/menu.css" rel="stylesheet" type="text/css" />
    <link href="<%=this.baseUrl%>assets/css/responsive.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shiv and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->

    <script src="<%=this.baseUrl%>assets/js/modernizr.min.js"></script>

</head>


<body>

    <!-- Loader -->
    <div id="preloader">
        <div id="status">
            <div class="spinner">
                <div class="spinner-wrapper">
                    <div class="rotator">
                        <div class="inner-spin"></div>
                        <div class="inner-spin"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- HOME -->
    <section>
        <div class="container-alt">
            <div class="row">
                <div class="col-sm-12">

                    <div class="wrapper-page">

                        <div class="m-t-40 account-pages">
                            <div class="text-center account-logo-box">
                                <h2 class="text-uppercase">
                                    <a href="index.html" class="text-success">
                                        <span>
                                            <img src="<%=this.baseUrl%>assets/images/sm.png" alt="" height="36" />
                                        </span>
                                    </a>
                                </h2>
                                <!--<h4 class="text-uppercase font-bold m-b-0">Sign In</h4>-->
                            </div>
                            <div class="account-content">
                                <form class="form-horizontal" runat="server">

                                    <asp:Literal ID="message" runat="server"></asp:Literal>

                                    <div class="form-group ">
                                        <div class="col-xs-12">
                                            <input class="form-control" id="userId" type="text" placeholder="UserId" runat="server" required="required" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <input class="form-control" id="password" type="password" placeholder="Password" runat="server" required="required" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <asp:DropDownList ID="userType" CssClass="form-control" runat="server" required="required">
                                                <asp:ListItem Text="Select" Value="" disabled="disabled" Selected="Selected"></asp:ListItem>
                                                <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Supervisor" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="User" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <!--<div class="form-group">
                                        <div class="col-xs-12">
                                            <div class="checkbox checkbox-success">
                                                <input id="checkbox-signup" type="checkbox" checked="">
                                                <label for="checkbox-signup">
                                                    Remember me
                                                </label>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group text-center m-t-30">
                                        <div class="col-sm-12">
                                            <a href="page-recoverpw.html" class="text-muted"><i class="fa fa-lock m-r-5"></i> Forgot your password?</a>
                                        </div>
                                    </div>-->

                                    <div class="form-group account-btn text-center m-t-10">
                                        <div class="col-xs-12">
                                            <asp:Button CssClass="btn w-md btn-bordered btn-danger waves-light" runat="server" Text="Log In" OnClick="loginClick" />
                                            <%--<button class="btn w-md btn-bordered btn-danger waves-effect waves-light" type="submit" onserverclick="loginClick" runat="server">Log In</button>--%>
                                        </div>
                                    </div>

                                </form>

                                <div class="clearfix"></div>

                            </div>
                        </div>
                        <!-- end card-box-->


                        <%--<div class="row m-t-50">
                            <div class="col-sm-12 text-center">
                                <p class="text-muted">Don't have an account? <a href="<%=this.baseUrl%>signup.aspx" class="text-primary m-l-5"><b>Sign Up</b></a></p>
                            </div>
                        </div>--%>
                    </div>
                    <!-- end wrapper -->

                </div>
            </div>
        </div>
    </section>
    <!-- END HOME -->

    <script>
        var resizefunc = [];
    </script>

    <!-- jQuery  -->
    <script src="<%=this.baseUrl%>assets/js/jquery.min.js"></script>
    <script src="<%=this.baseUrl%>assets/js/bootstrap.min.js"></script>
    <script src="<%=this.baseUrl%>assets/js/detect.js"></script>
    <script src="<%=this.baseUrl%>assets/js/fastclick.js"></script>
    <script src="<%=this.baseUrl%>assets/js/jquery.blockUI.js"></script>
    <script src="<%=this.baseUrl%>assets/js/waves.js"></script>
    <script src="<%=this.baseUrl%>assets/js/jquery.slimscroll.js"></script>
    <script src="<%=this.baseUrl%>assets/js/jquery.scrollTo.min.js"></script>

    <!-- App js -->
    <script src="<%=this.baseUrl%>assets/js/jquery.core.js"></script>
    <script src="<%=this.baseUrl%>assets/js/jquery.app.js"></script>

    <asp:Literal ID="timeScript" runat="server"></asp:Literal>
</body>
<script>
    //Restricts input for each element in the set of matched elements to the given inputFilter
    (function ($) {
        $.fn.inputFilter = function (inputFilter) {
            return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
                if (inputFilter(this.value)) {
                    this.oldValue = this.value;
                    this.oldSelectionStart = this.selectionStart;
                    this.oldSelectionEnd = this.selectionEnd;
                } else if (this.hasOwnProperty("oldValue")) {
                    this.value = this.oldValue;
                    this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                }
            });
        };
    }(jQuery));
    //Install input filters
    $(".onlyNumber").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
</script>
</html>
