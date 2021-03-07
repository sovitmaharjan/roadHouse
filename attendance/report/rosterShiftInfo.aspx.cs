using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report {
    public partial class rosterShiftInfo : System.Web.UI.Page {
        static attendance attendanceObject = new attendance();

        public string baseUrl {
            get {
                return attendanceObject.baseUrl();
            }
        }

        public string projectName {
            get {
                return attendanceObject.projectName();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            pageNamePlace1.Text = "Quick Attendance";
            pageNamePlace2.Text = "Quick Attendance";
            DataTable dtBranch = attendanceObject.queryFunction("SELECT BRANCH_ID, BRANCH_NAME FROM Tbl_Comp_Branch ORDER BY BRANCH_NAME ASC");
            branch.DataSource = dtBranch;
            branch.DataTextField = "BRANCH_NAME";
            branch.DataValueField = "BRANCH_ID";
            branch.DataBind();
            branch.Items.Insert(0, new ListItem("Select Branch", ""));
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT DEPT_ID, DEPT_NAME FROM Tbl_Org_Dept WHERE LEVEL = 1 ORDER BY DEPT_NAME ASC");
            department.DataSource = dtDepartment;
            department.DataTextField = "DEPT_NAME";
            department.DataValueField = "DEPT_ID";
            department.DataBind();
            department.Items.Insert(0, new ListItem("Select Department", ""));
            DataTable dtEmployee = attendanceObject.queryFunction("SELECT EMP_ID, emp_Fullname FROM view_Emp_Info ORDER BY emp_Fullname ASC");
            employee.DataSource = dtEmployee;
            employee.DataTextField = "emp_Fullname";
            employee.DataValueField = "EMP_ID";
            employee.DataBind();
            employee.Items.Insert(0, new ListItem("Select Employee", ""));

            if (!IsPostBack) {
                if (!string.IsNullOrEmpty(Request.Params["startDate"])) {
                    DataTable dtEmployeeInfo = attendanceObject.queryFunction("SELECT emp_Fullname, BRANCH_NAME, DEPT_NAME FROM view_emp_info WHERE EMP_ID = '" + Request.Params["employeeId"] + "'");
                    heading.Text = "<b>" + Request.Params["startDate"] + " <span style='color: #797979;'>-to-</span> " + Request.Params["endDate"] + "</b><br/><b><span style='font-size: 14px; color: #797979;'>Employee: " + dtEmployeeInfo.Rows[0]["emp_fullName"] + " (" + Request.Params["employeeId"] + ")</span></b><br/><b>Branch: " + dtEmployeeInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: " + dtEmployeeInfo.Rows[0]["DEPT_NAME"] + "</b>";

                    startDate.Value = Request.Params["startDate"];
                    endDate.Value = Request.Params["endDate"];
                    branch.SelectedValue = Request.Params["branchId"];
                    branchId.Value = Request.Params["branchId"];
                    department.SelectedValue = Request.Params["departmentId"];
                    departmentId.Value = Request.Params["departmentId"];
                    employee.SelectedValue = Request.Params["employeeId"];
                    employeeId.Value = Request.Params["employeeId"];

                    Dictionary<string, object> procedureData = new Dictionary<string, object>();
                    procedureData.Add("@startdate", Convert.ToDateTime(Request.Params["startDate"]));
                    procedureData.Add("@tilldate", Convert.ToDateTime(Request.Params["endDate"]));
                    procedureData.Add("@empid", Convert.ToInt32(Request.Params["employeeId"]));
                    procedureData.Add("@flag", 0);
                    DataTable dtResult = attendanceObject.procedureAndQuery("proc_rptRosterShiftInfo", procedureData, "SELECT * FROM tbl_rptRosterShift ");
                    string tableBodyRow = "";
					string tempName = "";
                    foreach (DataRow value in dtResult.Rows) {
		                if(tempName != value["EMP_FULLNAME"].ToString()){
                            tableBodyRow += "<tr><td colspan='7' style='text-align: center;'>Employee Name: " + value["EMP_FULLNAME"] + " (" + value["EMP_ID"] + ")</td></tr>";
						}
						tempName = value["EMP_FULLNAME"].ToString();
						tableBodyRow += "<tr>";
						if(value["Remark"].ToString() == "Weekned") {
                            tableBodyRow += "<td>" + value["date"] + "<br>(" + value["day"] + ")</td><td colspan='5' style='text-align: center;background-color: #ff8080;color:Black;font-size: 18px;'>Weekend</td>";
						} else {
		                    tableBodyRow += "<td>" + value["date"] + "<br>(" + value["day"] + ")</td>";
						    tableBodyRow += "<td>" + value["in_start"] + "</td>";
						    tableBodyRow += "<td>" + value["out_start"] + "</td>";
						    tableBodyRow += "<td>" + value["workhour"] + "</td>";
						    tableBodyRow += "<td>" + value["group_name"] + "</td>";
							if(value["Remark"].ToString() == "Working") {
								tableBodyRow += "<td style='text-align: center; color: #33cc33; font-size: 14px'>Working</td>";
						    }
                        tableBodyRow += "</tr>";
					    }
                    }
                    tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            Response.Redirect(baseUrl + "rosterShiftInfo?startDate=" + startDate.Value + "&endDate=" + endDate.Value + "&branchId=" + branchId.Value + "&departmentId=" + departmentId.Value + "&employeeId=" + employeeId.Value);
        }
    }
}