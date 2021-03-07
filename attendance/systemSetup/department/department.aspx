<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="department.aspx.cs" Inherits="attendance.systemSetup.department.department" %>
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
                            <th>Department Parent</th>
                            <th>Department Name</th>
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
                                Department/Section<span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input id="departmentSectionName" type="text" class="form-control" runat="server" autocomplete="off" required="required">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Group Under
                            </label>
                            <div class="checkbox col-md-1">
                                <input id="groupUnder" type="checkbox" runat="server">
                                <label for="groupUnder"></label>
                            </div>
                            <div class="col-md-8 departmentDropDownList" hidden="hidden">
                                <asp:DropDownList ID="departmentList" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-md-2 control-label">
                                Create Default Section
                            </label>
                            <div class="checkbox col-md-1">
                                <input id="createDefaultSection" type="checkbox" runat="server">
                                <label for="createDefaultSection"></label>
                            </div>
                            <div class="col-md-8 sectionInput" hidden="hidden">
                                <input id="section" type="text" class="form-control" runat="server" autocomplete="off" hidden="hidden">
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
                        url: '<%=this.baseUrl%>systemSetup/department/department.aspx/getData',
                        data: '{ "id": ' + id + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (result) {
                            result = result.d[0];
                            $('#<%=id.ClientID%>').val(result['DEPT_NAME']);
                            $('#<%=departmentSectionName.ClientID%>').val(result['DEPT_NAME']);
                        }
                    })
                })
            });

            $('#<%=groupUnder.ClientID%>').on('click', function () {
                if ($('#<%=groupUnder.ClientID%>').is(':checked')) {
                    $('.departmentDropDownList').prop('hidden', false);
                    $('#<%=createDefaultSection.ClientID%>').prop('disabled', true);
                    $('#<%=groupUnder.ClientID%>').prop('required', true);
                } else {
                    $('.departmentDropDownList').prop('hidden', true);
                    $('#<%=createDefaultSection.ClientID%>').prop('disabled', false);
                    $('#<%=groupUnder.ClientID%>').prop('required', false);
                }
            })

            $('#<%=createDefaultSection.ClientID%>').on('click', function () {
                if ($('#<%=createDefaultSection.ClientID%>').is(':checked')) {
                    $('.sectionInput').prop('hidden', false);
                    $('#<%=groupUnder.ClientID%>').prop('disabled', true);
                    $('#<%=section.ClientID%>').val($('#<%=departmentSectionName.ClientID%>').val() + ' sec');
                    $('#<%=departmentSectionName.ClientID%>').on('keyup', function () {
                        $('#<%=section.ClientID%>').val($('#<%=departmentSectionName.ClientID%>').val() + ' sec');
                    })
                    $('#<%=section.ClientID%>').prop('required', true);
                } else {
                    $('.sectionInput').prop('hidden', true);
                    $('#<%=groupUnder.ClientID%>').prop('disabled', false);
                    $('#<%=section.ClientID%>').val('');
                    $('#<%=section.ClientID%>').prop('required', false);
                }
            })

            $('#add').on('click', function () {
                $('#<%=id.ClientID%>').val('');
                $('#<%=departmentSectionName.ClientID%>').val('');
                $('#<%=departmentSectionName.ClientID%>').val('');
                $('#<%=statusYes.ClientID%>').prop('checked', true);
                $('#<%=statusNo.ClientID%>').prop('checked', false);
            })

            $('#clear').on('click', function () {
                $('#<%=departmentSectionName.ClientID%>').val('');
                $('#<%=departmentSectionName.ClientID%>').val('');
                $('#<%=statusYes.ClientID%>').prop('checked', true);
                $('#<%=statusNo.ClientID%>').prop('checked', false);
            })
        })
    </script>
</asp:Content>