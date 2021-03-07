<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="leave.aspx.cs" Inherits="attendance.systemSetup.leave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
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
        <div class="col-lg-12">
            <div class="card-box table-responsive">
                <div class="form-group">
                    <button type="button" id="add" class="btn btn-success w-xs waves-effect waves-light" data-toggle="modal" data-target="#addContent">
                        <i class="mdi mdi-plus"></i> Add Content
                    </button>
                </div>
                <table id="datatable" class="table table-striped  table-colored table-info">
                    <thead>
                        <tr>
                            <th>S. No.</th>
                            <th>Leave Name</th>
                            <th>Leave Type</th>
                            <th>Leave Day</th>
                            <th>Max Accumulation Days</th>
                            <th>Cashable</th>
                            <th>Max Days at a Time</th>
                            <th>Service Period A Year</th>
                            <th>Other</th>
                            <th>Status</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="tableBody" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <!-- end row -->

    <div id="addContent" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="custom-width-modalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width:65%;">
            <div class="modal-content">
                <form id="form" class="form-horizontal" runat="server">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Denotes Mandatory Fields (<span class="text-danger">*</span>)</h4>
                    </div>
                    <div class="modal-body">
                        <input type="hidden" id="id" runat="server">
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Leave Name<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="leaveName" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Leave Type<span class="text-danger">* </span>
                            </label>
                            <div class="col-md-10">
                                <div class="radio">
                                    <input type="radio" name="leaveType" id="expireYearly" value="Expire Yearly" runat="server" checked />
                                    <label for="expireYearly">
                                        Expire Yearly 
                                    </label>
                                </div>
                                <div class="radio">
                                    <input type="radio" name="leaveType" id="accumulative" value="Accumulative" runat="server" />
                                    <label for="accumulative">
                                        Accumulative 
                                    </label>
                                </div>
                                <div class="radio">
                                    <input type="radio" name="leaveType" id="servicePeriod" value="Service Period" runat="server" />
                                    <label for="servicePeriod">
                                        Service Period 
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Cashable 
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-10">
                                <div class="radio">
                                    <input type="radio" name="cashable" id="cashableYes" value="yes" runat="server" checked />
                                    <label for="cashableYes">
                                        Yes 
                                    </label>
                                </div>
                                <div class="radio">
                                    <input type="radio" name="cashable" id="cashableNo" value="no" runat="server" />
                                    <label for="cashableNo">
                                        No 
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Day (Annually)<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="dayAnnually" type="text" class="form-control onlyNumber" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Max Days at a Time<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="maxDaysAtTime" type="text" class="form-control onlyNumber" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row toHide" hidden="hidden">
                            <label class="col-md-2 control-label">
                                Max Accumulation Day
                            </label>
                            <div class="col-md-9">
                                <input id="maxAccumulationDay" type="text" class="form-control onlyNumber" runat="server" autocomplete="off">
                            </div>
                        </div>
                        <div class="form-group row toHide" hidden="hidden">
                            <label class="col-md-2 control-label">
                                Service Period in a Year
                            </label>
                            <div class="col-md-9">
                                <input id="servicePeriodInAYear" type="text" class="form-control onlyNumber" runat="server" autocomplete="off">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Other<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <div class="radio">
                                    <input type="radio" name="other" id="monthlyEarning" value="yes" runat="server" checked />
                                    <label for="monthlyEarning">
                                        Monthly Earning 
                                    </label>
                                </div>
                                <div class="radio">
                                    <input type="radio" name="other" id="mustExhaustAllTheLeave" value="no" runat="server" />
                                    <label for="mustExhaustAllTheLeave">
                                        Must Exhaust All The Leave 
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Status 
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-10">
                                <div class="radio">
                                    <input type="radio" name="status" id="statusYes" value="yes" runat="server" checked />
                                    <label for="statusYes">
                                        Yes 
                                    </label>
                                </div>
                                <div class="radio">
                                    <input type="radio" name="status" id="statusNo" value="no" runat="server" />
                                    <label for="statusNo">
                                        No 
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="clear" type="button" class="btn btn-default waves-effect">Clear</button>
                        <asp:Button ID="saveButton" text="Save" CssClass="btn btn-success" OnClick="saveClick" runat="server" />
                    </div>
                </form>
            </div>
        </div>
    </div><!-- /.modal -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="footer" runat="server">
    <script>
        $(document).ready(function () {
            dataTableFunction(function () {
                $('.edit').on('click', function () {
                    var id = $(this).attr('id');
                    $.ajax({
                        method: 'post',
                        url: '<%=this.baseUrl%>systemSetup/leave.aspx/getData',
                        data: '{ "id": ' + id + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (result) {
                            result = result.d[0];
                            console.log(result);
                            $('#<%=id.ClientID%>').val(result['LEAVE_ID']);
                            $('#<%=leaveName.ClientID%>').val(result['LEAVE_NAME']);
                            if (result['LEAVE_TYPE'] == 3) {
                                $('#<%=expireYearly.ClientID%>').prop('checked', false);
                                $('#<%=accumulative.ClientID%>').prop('checked', false);
                                $('#<%=servicePeriod.ClientID%>').prop('checked', true);
                            } else if (result['LEAVE_TYPE'] == 2) {
                                $('#<%=expireYearly.ClientID%>').prop('checked', false);
                                $('#<%=accumulative.ClientID%>').prop('checked', true);
                                $('#<%=servicePeriod.ClientID%>').prop('checked', false);
                            } else {
                                $('#<%=expireYearly.ClientID%>').prop('checked', true);
                                $('#<%=accumulative.ClientID%>').prop('checked', false);
                                $('#<%=servicePeriod.ClientID%>').prop('checked', false);
                            }
                            if (result['ISCashable'] == 1) {
                                $('#<%=cashableYes.ClientID%>').prop('checked', true);
                                $('#<%=cashableNo.ClientID%>').prop('checked', false);
                            } else {
                                $('#<%=cashableYes.ClientID%>').prop('checked', false);
                                $('#<%=cashableNo.ClientID%>').prop('checked', true);
                            }
                            $('#<%=dayAnnually.ClientID%>').val(result['LEAVE_DAYS']);
                            $('#<%=maxDaysAtTime.ClientID%>').val(result['MAX_DAYS_AT_A_TIME']);
                            if (result['LEAVE_TYPE'] == 2 || result['LEAVE_TYPE'] == 3) {
                                $('.toHide').prop('hidden', false   );
                                $('#<%=maxAccumulationDay.ClientID%>').val(result['LEAVE_MAX']);
                                $('#<%=servicePeriodInAYear.ClientID%>').val(result['service_period']);
                            }
                            if (result['others'] == 1) {
                                $('#<%=monthlyEarning.ClientID%>').prop('checked', false);
                                $('#<%=mustExhaustAllTheLeave.ClientID%>').prop('checked', true);
                            } else {
                                $('#<%=monthlyEarning.ClientID%>').prop('checked', true);
                                $('#<%=mustExhaustAllTheLeave.ClientID%>').prop('checked', false);
                            }
                            if (result['sta'] == 1) {
                                $('#<%=statusYes.ClientID%>').prop('checked', true);
                                $('#<%=statusNo.ClientID%>').prop('checked', false);
                            } else {
                                $('#<%=statusYes.ClientID%>').prop('checked', false);
                                $('#<%=statusNo.ClientID%>').prop('checked', true);
                            }
                        }
                    })
                })
            });

            $('#<%=expireYearly.ClientID%>').on('click', function () {
                if ($('#<%=expireYearly.ClientID%>').is(':checked')) {
                    $('.toHide').prop('hidden', true);
                    $('#<%=maxAccumulationDay.ClientID%>').val('');
                    $('#<%=servicePeriodInAYear.ClientID%>').val('');
                }
            })

            $('#<%=accumulative.ClientID%>, #<%=servicePeriod.ClientID%>').on('click', function () {
                if ($('#<%=accumulative.ClientID%>').is(':checked') || $('#<%=servicePeriod.ClientID%>').is(':checked')) {
                    $('.toHide').prop('hidden', false);
                    $('#<%=maxAccumulationDay.ClientID%>').val('');
                    $('#<%=servicePeriodInAYear.ClientID%>').val('');
                }
            })

            $('#add').on('click', function () {
                $('#<%=id.ClientID%>').val('');
                $('#<%=leaveName.ClientID%>').val('');
                $('#<%=leaveName.ClientID%>').val('');
                $('#<%=statusYes.ClientID%>').prop('checked', true);
                $('#<%=statusNo.ClientID%>').prop('checked', false);
            })

            $('#clear').on('click', function () {
                $('#<%=leaveName.ClientID%>').val('');
                $('#<%=leaveName.ClientID%>').val('');
                $('#<%=statusYes.ClientID%>').prop('checked', true);
                $('#<%=statusNo.ClientID%>').prop('checked', false);
            })
        })
    </script>
</asp:Content>