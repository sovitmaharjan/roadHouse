<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="account.aspx.cs" Inherits="attendance.account" %>
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
                            <th>Login Name</th>
                            <th>Full Name</th>
                            <th>Branch</th>
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
                                Full Name<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="fullName" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Login Name<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="loginName" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Password<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="password" type="password" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Branch<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="branch" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Status 
                                <span class="text-danger">* </span>
                            </label>
                            <div class="col-md-9">
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
                        url: '<%=this.baseUrl%>account.aspx/getData',
                        data: '{ "id": ' + id + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (result) {
                            result = result.d[0];
                            $('#<%=id.ClientID%>').val(result['UserId']);
                            $('#<%=fullName.ClientID%>').val(result['FullName']);
                            $('#<%=loginName.ClientID%>').val(result['LoginName']);
                            $('#<%=password.ClientID%>').val(result['Password']);
                            $('#<%=branch.ClientID%> option').each(function () {
                                $(this).prop('selected', false);
                            });
                            $('#<%=branch.ClientID%> option').each(function () {
                                if ($(this).val() == result['Branch_ID']) {
                                    $(this).prop('selected', true);
                                }
                            });
                            if (result['AccStatus'] == 1) {
                                $('#<%=statusYes.ClientID%>').prop('checked', true);
                                $('#<%=statusNo.ClientID%>').prop('checked', false);
                            } else {
                                $('#<%=statusNo.ClientID%>').prop('checked', true);
                                $('#<%=statusYes.ClientID%>').prop('checked', false);
                            }
                        }
                    })
                })
            });

            $('#add').on('click', function () {
                $('#<%=id.ClientID%>').val('');
                $('#<%=fullName.ClientID%>').val('');
                $('#<%=loginName.ClientID%>').val('');
                $('#<%=password.ClientID%>').val('');
                $('#<%=branch.ClientID%> option:eq(0)').prop('selected', true);
                $('#<%=statusYes.ClientID%>').prop('checked', true);
                $('#<%=statusNo.ClientID%>').prop('checked', false);
            })

            $('#clear').on('click', function () {
                $('#<%=fullName.ClientID%>').val('');
                $('#<%=loginName.ClientID%>').val('');
                $('#<%=password.ClientID%>').val('');
                $('#<%=branch.ClientID%> option:eq(0)').prop('selected', true);
                $('#<%=statusYes.ClientID%>').prop('checked', true);
                $('#<%=statusNo.ClientID%>').prop('checked', false);
            })
        })
    </script>
</asp:Content>