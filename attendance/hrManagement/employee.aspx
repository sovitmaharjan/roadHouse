<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="employee.aspx.cs" Inherits="attendance.hrManagement.employee" %>
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
                    <button type="button" id="load" class="btn btn-success w-xs waves-effect waves-light" data-toggle="modal" data-target="#loadContent">
                        <i class="mdi mdi-plus"></i> Load Content
                    </button>
                    
                </div>
                <table id="" class="table table-striped table-bordered table-colored table-info">
                    <thead>
						<tr>
							<th> Date<br>(Day) </th>
							<th> In Start </th>
							<th> Out Start </th>
							<th> WorkHour </th>
							<th> Group Name </th>
							<th> Remark </th>
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
        <div class="modal-dialog" style="width:90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Denotes Mandatory Fields (<span class="text-danger">*</span>)</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
						<div class="col-sm-12">
							<form id="wizard-validation-form" action="#" class="form-horizontal">
                                <div>
                                    <h3>Employee Information</h3>
                                    <section>
                                        <hr />
                                        <h4 class="m-t-0 header-title"><b>Basic Information</b></h4>
                                        <hr />
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Employee Id
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-2">
                                                <input class="form-control" id="employeeId" name="employeeId" type="text" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Employee Image</label>
                                            <div class="col-lg-10">
                                                <input id="password2" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Full Name
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-1">
                                                <input id="title" type="text" class="form-control">
                                            </div>
                                            <div class="col-lg-3">
                                                <input id="firstName" type="text" class="form-control" placeholder="First Name" />
                                            </div>
                                            <div class="col-lg-3">
                                                <input id="middleName" type="text" class="form-control" placeholder="Middle Name" />
                                            </div>
                                            <div class="col-lg-3">
                                                <input id="lastName" type="text" class="form-control" placeholder="Last Name" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Gender
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4 radio">
                                                <div class="col-sm-4">
                                                    <input type="radio" name="gender" id="male" value="male" checked="">
                                                    <label for="male">
                                                        Male
                                                    </label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <input type="radio" name="gender" id="female" value="female">
                                                    <label for="female">
                                                        Female
                                                    </label>
                                                </div>
                                            </div>
                                            <label class="col-lg-2 control-label">
                                                Relationship
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4 radio">
                                                <div class="col-sm-4">
                                                    <input type="radio" name="relationship" id="single" value="female" checked="">
                                                    <label for="single">
                                                        Single
                                                    </label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <input type="radio" name="relationship" id="married" value="female">
                                                    <label for="married">
                                                        Married
                                                    </label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <input type="radio" name="relationship" id="divorced" value="female">
                                                    <label for="divorced">
                                                        Divorced
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-md-2 control-label">
                                                Start Date
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <input autocomplete="off" type="text" id="startDate" name="startDate" class="form-control startDate" placeholder="English Date" />
                                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <input autocomplete="off" type="text" id="startNepaliDate" name="startNepaliDate" class="form-control startNepaliDate" placeholder="Nepali Date" />
                                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                                </div>
                                            </div>
                                            <label class="col-md-2 control-label">
                                                Start Date
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <input autocomplete="off" type="text" id="Text1" name="startDate" class="form-control startDate" placeholder="English Date" />
                                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <input autocomplete="off" type="text" id="Text2" name="startNepaliDate" class="form-control startNepaliDate" placeholder="Nepali Date" />
                                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Mobile
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="mobile" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">
                                                Telephone
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="telephone" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Email</label>
                                            <div class="col-lg-4">
                                                <input id="email" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">
                                                User Id
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="userId" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Citizenship No.</label>
                                            <div class="col-lg-4">
                                                <input id="citizenshipNumber" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">
                                                Password
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="password" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Temporary Address</label>
                                            <div class="col-lg-4">
                                                <input id="city" type="text" class="form-control" placeholder="City" />
                                            </div>
                                            <div class="col-lg-3">
                                                <input id="mdVdc" type="text" class="form-control" placeholder="MP/VDC" />
                                            </div>
                                            <div class="col-lg-3">
                                                <input id="zone" type="text" class="form-control" placeholder="Zone" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Permanent Address
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="pCity" type="text" class="form-control" placeholder="City" />
                                            </div>
                                            <div class="col-lg-3">
                                                <input id="pMdVdc" type="text" class="form-control" placeholder="MP/VDC" />
                                            </div>
                                            <div class="col-lg-3">
                                                <input id="pZone" type="text" class="form-control" placeholder="Zone" />
                                            </div>
                                        </div>
                                        <hr />
                                        <h3 class="m-t-0 header-title"><b>Health Information</b></h3>
                                        <hr />
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Blood Group</label>
                                            <div class="col-lg-4">
                                                <input id="bloodGroup" type="text" class="form-control" />
                                            </div>
                                            <div class="col-lg-1"></div>
                                            <div class="col-lg-4 checkbox">
                                                <input type="checkbox" id="allergy" value="0" runat="server" />
                                                <label for="allergy">Allergy</label>
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Medical Condiion</label>
                                            <div class="col-lg-10   ">
                                                <input class="form-control" id="medicalCondition" type="text" placeholder="Any Medical condiion like Epilepsy, Migaraine, etc" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Doctor</label>
                                            <div class="col-lg-4">
                                                <input id="doctor" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">Contact</label>
                                            <div class="col-lg-4">
                                                <input id="doctorContact" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Doctor</label>
                                            <div class="col-lg-4">
                                                <input id="doctor2" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">Contact</label>
                                            <div class="col-lg-4">
                                                <input id="doctorContact2" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <hr />
                                        <h3 class="m-t-0 header-title"><b>Emergenct Contact Person Information</b></h3>
                                        <hr />
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Name</label>
                                            <div class="col-lg-4">
                                                <input id="eName" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">Contact</label>
                                            <div class="col-lg-4">
                                                <input id="eRelation" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">Address</label>
                                            <div class="col-lg-4">
                                                <input id="eAddress" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">Phone</label>
                                            <div class="col-lg-4">
                                                <input id="ePhone" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <hr />
                                        <h3 class="m-t-0 header-title"><b>Official Information</b></h3>
                                        <hr />
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Branch
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="branch" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">
                                                Section
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="section" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Designation
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="designation" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">
                                                Status
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="status" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Grade
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="grade" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">
                                                Type
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-4">
                                                <input id="type" type="text" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">PF No.</label>
                                            <div class="col-lg-2">
                                                <input id="pfno" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">EPAN</label>
                                            <div class="col-lg-2">
                                                <input id="epan" type="text" class="form-control" />
                                            </div>
                                            <label class="col-lg-2 control-label">CIT No.</label>
                                            <div class="col-lg-2">
                                                <input id="citno" type="text" class="form-control" />
                                            </div>
                                        </div>  
                                    </section>
                                    <h3>Education / Achievement</h3>
                                    <section>
                                        <hr />
                                        <h4 class="m-t-0 header-title"><b>Education History</b></h4>
                                        <hr />
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Education
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-5">
                                                <input class="form-control" id="education1" name="employeeId" type="text" />
                                            </div>
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <input autocomplete="off" type="text" id="Text3" name="startDate" class="form-control startDate" placeholder="English Date" />
                                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <input autocomplete="off" type="text" id="Text4" name="startNepaliDate" class="form-control startNepaliDate" placeholder="Nepali Date" />
                                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                                </div>
                                            </div>
                                            <div class="button-list col-md-1">
                                                <button class="btn btn-icon waves-effect waves-light btn-success"> <i class="fa fa-plus"></i> </button>
                                            </div>
                                        </div>
                                        <hr />
                                        <h4 class="m-t-0 header-title"><b>Training History</b></h4>
                                        <hr />
                                        <div class="form-group clearfix">
                                            <label class="col-lg-2 control-label">
                                                Training
                                                <span class="text-danger">* </span>
                                            </label>
                                            <div class="col-lg-5">
                                                <input class="form-control" id="training1" name="employeeId" type="text" />
                                            </div>
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <input autocomplete="off" type="text" id="Text65" name="startDate" class="form-control startDate" placeholder="English Date" />
                                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <input autocomplete="off" type="text" id="3456" name="startNepaliDate" class="form-control startNepaliDate" placeholder="Nepali Date" />
                                                    <span class="input-group-addon bg-custom b-0"><i class="mdi mdi-calendar text-white"></i></span>
                                                </div>
                                            </div>
                                            <div class="button-list col-md-1">
                                                <button class="btn btn-icon waves-effect waves-light btn-success"> <i class="fa fa-plus"></i> </button>
                                            </div>
                                        </div>

                                    </section>
                                    <h3>Leave Management</h3>
                                    <section>
                                        <div class="form-group clearfix">
                                            <div class="col-lg-12">
                                                <ul class="list-unstyled w-list">
	                                                <li><b>First Name :</b> Jonathan </li>
	                                                <li><b>Last Name :</b> Smith </li>
	                                                <li><b>Emial:</b> jonathan@smith.com</li>
	                                                <li><b>Address:</b> 123 Your City, Cityname. </li>
	                                            </ul>
                                            </div>
                                        </div>
                                    </section>
                                    <h3>Work Flow Management</h3>
                                    <section>
                                        <div class="form-group clearfix">
                                            <div class="col-lg-12">
                                                <input id="acceptTerms-2" name="acceptTerms2" type="checkbox" class="required">
                                                <label for="acceptTerms-2">I agree with the Terms and Conditions.</label>
                                            </div>
                                        </div>

                                    </section>
                                </div>
                            </form>
						</div>
					</div>
                    <!-- End row -->
                </div>
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