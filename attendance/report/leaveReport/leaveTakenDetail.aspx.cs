using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report.leaveReport {
    public partial class leaveTakenDetail1 : System.Web.UI.Page {
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
                if (!string.IsNullOrEmpty(Request.Params["branchId"])) {
                    DataTable dtHeaderInfo;
                    string branchInfo, departmentInfo, employeeInfo;
                    if (Request.Params["branchId"] == "0") {
                        branchInfo = "ALL";
                    } else {
                        dtHeaderInfo = attendanceObject.queryFunction("SELECT BRANCH_NAME FROM tbl_comp_branch WHERE BRANCH_ID = '" + Request.Params["branchId"] + "'");
                        branchInfo = dtHeaderInfo.Rows[0]["BRANCH_NAME"].ToString();
                    }
                    if (Request.Params["departmentId"] == "0") {
                        departmentInfo = "ALL";
                    } else {
                        dtHeaderInfo = attendanceObject.queryFunction("SELECT DEPT_NAME FROM Tbl_Org_Dept WHERE DEPT_ID = '" + Request.Params["departmentId"] + "'");
                        departmentInfo = dtHeaderInfo.Rows[0]["DEPT_NAME"].ToString();
                    }
                    if (Request.Params["employeeId"] == "0") {
                        employeeInfo = "ALL";
                    } else {
                        dtHeaderInfo = attendanceObject.queryFunction("SELECT emp_Fullname FROM view_emp_info WHERE EMP_ID = '" + Request.Params["employeeId"] + "'");
                        employeeInfo = dtHeaderInfo.Rows[0]["emp_Fullname"].ToString() + " (" + Request.Params["employeeId"] + ")";
                    }
                    heading.Text = "<b>Branch: " + branchInfo + "</b><br /><b>Department: " + departmentInfo + "</b><br /><b><span style='font-size: 14px; color: #797979;'>Employee: " + employeeInfo + "</span></b><br/>";

                    startDate.Value = Request.Params["startDate"];
                    endDate.Value = Request.Params["endDate"];
                    branch.SelectedValue = Request.Params["branchId"];
                    department.SelectedValue = Request.Params["departmentId"];
                    employee.SelectedValue = Request.Params["employeeId"];
                    if (Request.Params["branchId"] == "0") {
                        allBranch.Checked = true;
                    } else {
                        branchId.Value = Request.Params["branchId"];
                    }
                    if (Request.Params["departmentId"] == "0") {
                        allDepartment.Checked = true;
                    } else {
                        departmentId.Value = Request.Params["departmentId"];
                    }
                    if (Request.Params["employeeId"] == "0") {
                        allEmployee.Checked = true;
                    } else {
                        employeeId.Value = Request.Params["employeeId"];
                    }

                    Dictionary<string, object> procedureData = new Dictionary<string, object>();
                    procedureData.Add("@sdate", Request.Params["startDate"]);
                    procedureData.Add("@edate   ", Request.Params["endDate"]);
                    procedureData.Add("@EMP_ID", Request.Params["employeeId"]);
                    procedureData.Add("@LEAVE_ID", Request.Params["leaveId"]);
                    procedureData.Add("@BRANCH_ID", Request.Params["branchId"]);
                    procedureData.Add("@DEPT_ID", Request.Params["departmentId"]);
                    DataTable dtResult = attendanceObject.procedure("Proc_leave_monthly_report", procedureData);
                    string tableBodyRow = "";
                    string temp_emp = "";
                    int clength = dtResult.Rows.Count;
                    foreach (DataRow value in dtResult.Rows) {
                        if (temp_emp != value["EMP_FULLNAME"].ToString()) {
                            tableBodyRow += "<tr>";
                            tableBodyRow += "<td style='text-align: center; color: #438EB9;' colspan='2'>Employee Name: " + value["EMP_FULLNAME"] + "</td>";
                            tableBodyRow += "<td style='text-align: center; color: #438EB9;' colspan='2'>Branch Name: " + value["BRANCH_NAME"] + "</td>";
                            tableBodyRow += "<td style='text-align: center; color: #438EB9;' colspan='2'>Department Name: " + value["DEPT_NAME"] + "</td>";
                            tableBodyRow += "</tr>";
                        }
                        temp_emp = value["EMP_FULLNAME"].ToString();
                        tableBodyRow += "<tr>";
                        string[] a = value["LEAVE_DATE"].ToString().Split(' ');
                        tableBodyRow += "<td>" + a[0] + "</td>";
                        tableBodyRow += "<td>" + value["LEAVE_NAME"] + "</td>";
                        tableBodyRow += "<td>" + value["DAYS"] + "</td>";
                        tableBodyRow += "<td>" + value["ApprovedBy"] + "</td>";
                        tableBodyRow += "<td>" + value["REMARKS"] + "</td>";
                        tableBodyRow += "</tr>";
                    }
                    tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            string emp, bra, dept;
            if (allBranch.Checked) {
                bra = "0";
            } else {
                bra = branchId.Value.ToString();
            }
            if (allDepartment.Checked) {
                dept = "0";
            } else {
                dept = departmentId.Value.ToString();
            }
            if (allEmployee.Checked) {
                emp = "0";
            } else {
                emp = employeeId.Value.ToString();
            }
            Response.Redirect(baseUrl + "leaveTakenDetail?startDate=" + startDate.Value + "&endDate=" + endDate.Value + "&branchId=" + bra + "&departmentId=" + dept + "&employeeId=" + emp);
        }
    }
}