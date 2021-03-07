<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="companyInfo.aspx.cs" Inherits="attendance.systemSetup.companyInfo" %>
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
            <div class="panel panel-color panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title"><span class="text-danger">* </span>Denotes Mandatory Fields </h3>
                </div>
                <div class="panel-body">
                    <h4 class="m-t-0 header-title"></h4>
                    <p class="text-muted m-b-30 font-13">
                    </p>
                    <form class="form-horizontal" runat="server">
                        <input type="hidden" id="id" runat="server">
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Comapany Name
                                <span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="name" class="form-control" runat="server" required="required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Address
                                <span class="text-danger">*</span>
                            </label>
                            <div class="col-md-4">
                                <input type="text" id="address" class="form-control" runat="server" required="required" />
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-4">
                                <input type="text" id="address2" class="form-control" runat="server" required="required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Telephone
                                <span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="telephone" class="form-control" runat="server" required="required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Fax
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="fax" class="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Email
                                <span class="text-danger">*</span>
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="email" class="form-control" runat="server" required="required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Website
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="website" class="form-control" runat="server" />
                            </div>
                        </div>
                        <br /><br />
                        <div class="form-group row">
                            <div class="col-sm-9 col-sm-offset-2">
                                <div class="button-list">
                                    <asp:Button ID="saveButton" text="Save" CssClass="btn btn-success btn-bordered w-md btn-bordered col-md-1" OnClick="saveClick" runat="server" />
                                    <a class="btn btn-danger btn-bordered waves-effect w-md waves-light col-md-1" href="<%=this.baseUrl%>companyInfo">Cancel</a>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- end row -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="footer" runat="server">
    <script>
        $(document).ready(function () {
            dataTableFunction(function () {
                $('.edit').on('click', function () {
                    var id = $(this).attr('id');
                    $.ajax({
                        method: 'post',
                        url: '<%=this.baseUrl%>systemSetup/branch.aspx/getData',
                        data: '{ "name" : ' + id + ', "address" : ' + $('#<%=address.ClientID%>').val() + ', "address2" : ' + $('#<%=address2.ClientID%>').val() + ', "telephone" : ' + $('#<%=telephone.ClientID%>').val() + ', "fax" : ' + $('#<%=fax.ClientID%>').val() + ', "email" : ' + $('#<%=email.ClientID%>').val() + ', "website" : ' + $('#<%=website.ClientID%>').val() + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (result) {
                            result = result.d[0];
                        }
                    })
                })
            });

            $('#add').on('click', function () {
                $('#<%=id.ClientID%>').val('');
                $('#<%=branchName.ClientID%>').val('');
                $('#<%=branchCode.ClientID%>').val('');
                $('#<%=statusYes.ClientID%>').prop('checked', true);
                $('#<%=statusNo.ClientID%>').prop('checked', false);
            })

            $('#clear').on('click', function () {
                $('#<%=branchName.ClientID%>').val('');
                $('#<%=branchCode.ClientID%>').val('');
                $('#<%=statusYes.ClientID%>').prop('checked', true);
                $('#<%=statusNo.ClientID%>').prop('checked', false);
            })
        })
    </script>
</asp:Content>