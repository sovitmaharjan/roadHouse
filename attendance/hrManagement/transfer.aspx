<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="transfer.aspx.cs" Inherits="attendance.hrManagement.transfer" %>
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
                    <li>Report</li>
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
                    <button type="button" id="add" class="btn btn-success w-xs waves-effect waves-light" data-toggle="modal" data-target="#addContent">
                        <i class="mdi mdi-plus"></i>Add Content
                    </button>

                </div>
                <table id="datatable" class="table table-striped table-bordered table-colored table-info">
                    <thead>
                        <tr>
                            <th>S. No.</th>
                            <th>Date</th>
							<th>Employee </th>
							<th>Branch From</th>
							<th>Department From </th>
							<th>Transfer Details </th>
							<th>Branch To</th>
							<th>Department To </th>
							<th>Is Current</th>
                        </tr>
                    </thead>
                    <tbody class="tbody">
                        <asp:Literal ID="tableBody" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <!-- end row -->

    <div id="addContent" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="custom-width-modalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 65%;">
            <div class="modal-content">
                <form id="form" class="form-horizontal" runat="server">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Denotes Mandatory Fields (<span class="text-danger">*</span>)</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Date
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
                                Employee
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="employee" CssClass="emp form-control" runat="server"></asp:DropDownList>
                            </div>
                            <label class="col-md-2 control-label">
                                Employee Id
                            </label>
                            <div class="col-md-2">
                                <input type="text" id="employeeId" class="empId form-control onlyNumber" required="required" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="panel panel-color panel-info">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Current Info</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Grade
                                            </label>
                                            <div class="col-md-8">
                                                <input type="text" id="currentGrade" class="currentGrade form-control" required="required" readonly="readonly" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Designation
                                            </label>
                                            <div class="col-md-8">
                                                <input type="text" id="currentDesignation" class="currentDesignation form-control" required="required" readonly="readonly" runat="server" />
                                                <input type="hidden" id="currentDesignationId" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Section
                                            </label>
                                            <div class="col-md-8">
                                                <input type="text" id="currentSection" class="currentSection form-control" required="required" readonly="readonly" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Branch
                                            </label>
                                            <div class="col-md-8">
                                                <input type="text" id="currentBranch" class="currentBranch form-control" required="required" readonly="readonly" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="panel panel-color panel-info">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Transfer Info</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Branch
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="bra" CssClass="bra form-control" runat="server" required="required"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Section
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="sec" CssClass="sec form-control" runat="server" required="required"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Latest Transfer
                            </label>
	                        <div class="col-md-2">
                                <div class="checkbox">
                                    <input type="checkbox" id="latestTransfer" class="latestTransfer" value="0" runat="server" />
                                    <label for="latestTransfer"></label>
                                </div>
                            </div>
	                    </div>
                        <div class="form-group">
	                        <label class="col-md-2 control-label">
                                Transfer Detail
                                <span class="text-danger">* </span>
	                        </label>
	                        <div class="col-md-10">
	                            <textarea id="description" class="description form-control" runat="server"></textarea>
	                        </div>
	                    </div>
                    </div>
                    <div class="modal-footer">
                        <button id="clear" type="button" class="btn btn-default waves-effect">Clear</button>
                        <asp:Button ID="saveButton" Text="Save" CssClass="btn btn-success" OnClick="saveClick" runat="server" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    <!-- /.modal -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="footer" runat="server">
    <script>
        $(document).ready(function () {
            dataTableFunction();
            function get(id) {
                $.ajax({
                    method: 'post',
                    url: baseUrl + '/function.aspx/getDataByEmpId',
                    data: '{ "id" : ' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: false,
                    success: function (result) {
                        result = result.d;
                        if (result.length > 0) {
                            result = result[0];
                            $('.currentGrade').val(result['GRADE_NAME']);
                            $('.currentDesignation').val(result['DEG_NAME']);
                            $('#<%=currentDesignationId.ClientID%>').val(result['DEG_ID']);
                            $('.currentSection').val(result['section_name']);
                            $('.currentBranch').val(result['BRANCH_NAME']);
                        }
                    }
                });
            }

            $('.emp').on('change', function () {
                var id = $(this).val();
                $('.empId').val(id);
                get(id);
            })

            var timer;
            $('.empId').keyup(function () {
                clearTimeout(timer);
                timer = setTimeout(function () {
                    var id = $('.empId').val();
                    $('.emp').val(id);
                    get(id);
                }, 1000);
            })

            $('.bra').on('change', function () {
                var id = $(this).val();
                $('.sec').val('');
                $.ajax({
                    method: 'post',
                    url: baseUrl + '/function.aspx/getSectionByBranchId',
                    data: '{ "id" : ' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: false,
                    success: function (result) {
                        result = result.d;
                        console.log(result);
                        if (result.length > 0) {
                            $('.sec').append('<option value="">Select Section</option>');
                            result.forEach(function (e, i) {
                                $('.sec').append('<option value="' + e.sect_id + '">' + e.sect_name + '</option>');
                            });
                        } else {
                            $('.sec').val('');
                        }
                    }
                });
            })
        })
    </script>
</asp:Content>