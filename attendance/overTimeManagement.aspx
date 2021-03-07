<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="overTimeManagement.aspx.cs" Inherits="attendance.overTimeManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="page-title-box">
                <h4 class="page-title">
                    <asp:Literal ID="pageNamePlace1" runat="server"></asp:Literal>
                </h4>
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
                    <button type="button" id="load" class="btn btn-success w-xs waves-effect waves-light" data-toggle="modal" data-target="#loadContent">
                        <i class="mdi mdi-plus"></i> Load
                    </button>
                </div>
                <table id="datatable" class="table table-striped  table-colored table-info">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Date</th>
                            <th>In Time</th>
                            <th>Out Date</th>
                            <th>Out Time</th>
                            <th>Work Hour 1</th>
                            <th>In Time</th>
                            <th>Out Date</th>
                            <th>Out Time</th>
                            <th>Work Hour 2</th>
                            <th>Total Work Hrs</th>
                            <th>Total OT</th>
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
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Employee Id<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="employeeId" type="text" class="form-control onlyNumber" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Employee Name<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="employeeName" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Designation<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="designation" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Department<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="department" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Branch<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="branch" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Start Date
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <input autocomplete="off" type="text" id="startEnglishDate" name="startEnglishDate" class="form-control englishDate" placeholder="English Date" required="required" runat="server" />
                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <input autocomplete="off" type="text" id="startNepaliDate" name="startNepaliDate" class="form-control nepaliDate" placeholder="Nepali Date" runat="server" />
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
                                    <input autocomplete="off" type="text" id="endEnglishDate" name="endEnglishDate" class="form-control englishDate" placeholder="English Date" required="required" runat="server" />
                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <input autocomplete="off" type="text" id="endNepaliDate" name="endNepaliDate" class="form-control nepaliDate" placeholder="Nepali Date" runat="server" />
                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Remarks<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="remarks" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Approved By<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="approvedBy" CssClass="form-control" runat="server"></asp:DropDownList>
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
            $('#load').click();
            dataTableFunction();
            var timer;
            $('#<%=employeeId.ClientID%>').keyup(function () {
                var id = $(this).val();
                clearTimeout(timer);
                timer = setTimeout(function () {
                    console.log(id);
                    $.ajax({
                        method: 'post',
                        url: '<%=this.baseUrl%>overTimeManagement.aspx/getEmployeeData',
                        data: '{ "id": ' + id + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (result) {
                            <%--result = result.d[0];
                            $('#<%=employeeName.ClientID%>').val(result['emp_Fullname']);
                            $('#<%=designation.ClientID%>').val(result['DEG_NAME']);
                            $('#<%=department.ClientID%>').val(result['DEPT_NAME']);
                            $('#<%=branch.ClientID%>').val(result['BRANCH_NAME']);--%>
                        }
                    })
                }, 500);
            })

            <%--$('#load, #clear').on('click', function () {
                $('#<%=employeeId.ClientID%>').val('');
                $('#<%=employeeName.ClientID%>').val('');
                $('#<%=designation.ClientID%>').val('');
                $('#<%=department.ClientID%>').val('');
                $('#<%=branch.ClientID%>').val('');
                $('#<%=startEnglishDate.ClientID%>').val('');
                $('#<%=startNepaliDate.ClientID%>').val('');
                $('#<%=endEnglishDate.ClientID%>').val('');
                $('#<%=endNepaliDate.ClientID%>').val('');
                $('#<%=remarks.ClientID%>').val('');
                $('#<%=approvedBy.ClientID%> option:eq(0)').prop('selected', true);
            })--%>

        })
    </script>
</asp:Content>