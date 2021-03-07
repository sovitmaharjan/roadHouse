<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="securityGuard.aspx.cs" Inherits="attendance.report.securityGuard" %>
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
                    <li>
                        Report
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
                <div style="text-align: center;">
                    <p class="text-muted font-13">
                        <asp:Literal ID="heading" runat="server"></asp:Literal>
                    </p>
                </div>
                <div class="form-group">
                    <button type="button" id="load" class="btn btn-success w-xs waves-effect waves-light" data-toggle="modal" data-target="#loadContent">
                        <i class="mdi mdi-plus"></i> Load Content
                    </button>
                    
                </div>
                <table id="" class="table table-striped table-bordered table-colored table-info">
                    <thead>
						<tr style="font-size: 16px;">
							<th colspan="2" style="text-align: center"></th>
							<th colspan="5" style="text-align: center">Shift 1</th>
							<th colspan="5" style="text-align: center">Shift 2</th>
							<th colspan="2" style="text-align: center"></th>
						</tr>
						<tr>
							<th> Date (Day)</th>
							<th> Worked Hour </th>
							<th> In Time </th>
							<th> Out Date </th>
							<th> Out Time </th>
							<th> Remarks </th>
							<th> Difference </th>
							<th> In Time </th>
							<th> Out Date </th>
							<th> Out Time </th>
							<th> Remarks </th>
							<th> Difference </th>
							<th> OT </th>
							<th> Status </th>
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

    <div id="loadContent" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="custom-width-modalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width:65%;">
            <div class="modal-content">
                <form id="form" class="form-horizontal" runat="server">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Denotes Mandatory Fields (<span class="text-danger">*</span>)</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Start Date
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <input autocomplete="off" type="text" id="startDate" name="startDate" class="form-control startDate" placeholder="English Date" required="required" runat="server" />
                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <input autocomplete="off" type="text" id="startNepaliDate" name="startNepaliDate" class="form-control startNepaliDate" placeholder="Nepali Date" required="required" runat="server" />
                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                End Date
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <input autocomplete="off" type="text" id="endDate" name="endDate" class="form-control endDate" placeholder="English Date" required="required" runat="server" />
                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <input autocomplete="off" type="text" id="endNepaliDate" name="endNepaliDate" class="form-control endNepaliDate" placeholder="Nepali Date" required="required" runat="server" />
                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Branch
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="branch" CssClass="branch form-control" runat="server" required="required"></asp:DropDownList>
                            </div>
                            <label class="col-md-2 control-label">
                                Branch Id
                            </label>
                            <div class="col-md-1">
                                <input type="text" id="branchId" class="branchId form-control onlyNumber" required="required" readonly="readonly" runat="server"/>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Department
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="department" CssClass="department form-control" runat="server" required="required"></asp:DropDownList>
                            </div>
                            <label class="col-md-2 control-label">
                                Department Id
                            </label>
                            <div class="col-md-1">
                                <input type="text" id="departmentId" class="departmentId form-control onlyNumber" required="required" readonly="readonly" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Employee
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="employee" CssClass="employee form-control" runat="server" ></asp:DropDownList>
                            </div>
                            <label class="col-md-2 control-label">
                                Employee Id
                            </label>
                            <div class="col-md-1">
                                <input type="text" id="employeeId" class="employeeId form-control onlyNumber" required="required" runat="server"/>
                            </div>
                            <div class="col-md-2">
                                <div class="checkbox">
                                    <input type="checkbox" id="allEmployee" class="allEmployee" value="0" runat="server" />
                                    <label for="allEmployee">All Employee</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="clear" type="button" class="btn btn-default waves-effect">Clear</button>
                        <asp:Button ID="loadButton" text="Load" CssClass="btn btn-success" OnClick="loadClick" runat="server" />
                    </div>
                </form>
            </div>
        </div>
    </div><!-- /.modal -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="footer" runat="server">
    <script>
        $(document).ready(function () {
            //$('#load').click();
        })
    </script>
</asp:Content>