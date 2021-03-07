using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report.employeeInfo {
    public partial class employeeStatusReport : System.Web.UI.Page {
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

            if (!IsPostBack) {
                if (!string.IsNullOrEmpty(Request.Params["branchId"])) {
                    string status;
                    if (Request.Params["status"].ToString() == "0") {
                        status = "Retired";
                    } else if (Request.Params["status"].ToString() == "1") {
                        status = "Working";
                    } else if (Request.Params["status"].ToString() == "2") {
                        status = "Suspension";
                    } else if (Request.Params["status"].ToString() == "3") {
                        status = "Discharged";
                    } else if (Request.Params["status"].ToString() == "4") {
                        status = "Dismissed";
                    } else {
                        status = "Resigned";
                    }
                    DataTable dtHeaderInfo;
                    if (Request.Params["branchId"] == "0" && Request.Params["departmentId"] == "0") {
                        heading.Text = "<b>Branch: All</b><br /><b>Department: All</b><br /><b>Status: " + status + "</b>";
                    } else if (Request.Params["branchId"] != "0" && Request.Params["departmentId"] == "0") {
                        dtHeaderInfo = attendanceObject.queryFunction("SELECT BRANCH_NAME FROM Tbl_Comp_Branch WHERE BRANCH_ID = '" + Request.Params["branchId"] + "'");
                        heading.Text = "<b>Branch: " + dtHeaderInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: All</b><br /><b>Status: " + status + "</b>";
                    } else {
                        dtHeaderInfo = attendanceObject.queryFunction("SELECT BRANCH_NAME FROM Tbl_Comp_Branch WHERE BRANCH_ID = '" + Request.Params["branchId"] + "'");
                        heading.Text = "<b>Branch: " + dtHeaderInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: " + dtHeaderInfo.Rows[0]["DEPT_NAME"] + "</b><br /><b>Status: " + status + "</b>";
                    }

                    if (Request.Params["branchId"] == "0") {
                        allBranch.Checked = true;
                        branch.Attributes.Remove("required");
                        branchId.Attributes.Remove("required");
                        branchId.Value = "";
                    } else {
                        branch.SelectedValue = Request.Params["branchId"];
                        branchId.Value = Request.Params["branchId"];
                    }
                    if (Request.Params["departmentId"] == "0") {
                        allDepartment.Checked = true;
                        department.Attributes.Remove("required");
                        departmentId.Attributes.Remove("required");
                        departmentId.Value = "";
                    } else {
                        department.SelectedValue = Request.Params["departmentId"];
                        departmentId.Value = Request.Params["departmentId"];
                    }

                    string query;
                    if (Request.Params["branchId"] == "0" && Request.Params["departmentId"] == "0") {
                        query= "SELECT * FROM view_emp_info WHERE STATUS_ID = '" + Request.Params["status"] + "'";
                    } else if (Request.Params["branchId"] != "0" && Request.Params["departmentId"] == "0") {
                        query = "SELECT * FROM view_emp_info WHERE BRANCH_ID = '" + Request.Params["branchId"] + "' AND STATUS_ID = '" + Request.Params["status"] + "'";
                    } else {
                        query = "SELECT * FROM view_emp_info WHERE BRANCH_ID = '" + Request.Params["branchId"] + "' AND DEPT_ID = '" + Request.Params["departmentId"] + "' AND STATUS_ID = '" + Request.Params["status"] + "'";
                    }
                    DataTable dtResult = attendanceObject.queryFunction(query);
                    string tableBodyRow = "";
                    foreach (DataRow value in dtResult.Rows) {
                        tableBodyRow += "<tr>";
                        tableBodyRow += "<td>" + value["EMP_ID"] + "</td>";
                        tableBodyRow += "<td>" + value["emp_Fullname"] + "</td>";
                        tableBodyRow += "<td>" + value["DEG_NAME"] + "</td>";
                        tableBodyRow += "<td>" + value["DEPT_NAME"] + "</td>";
                        tableBodyRow += "<td>" + value["GRADE_TYPE"] + "</td>";
                        tableBodyRow += "<td>" + value["EMP_MOBILE"] + "</td>";
                        tableBodyRow += "<td>" + value["EMP_TADD"] + ", " + value["EMP_TSTREET"] + ", " + value["EMP_TDISTRICT"] + ", " + value["EMP_TZONE"] + "</td>";
                        if (value["EMP_GENDER"].ToString() == "M") {
                            tableBodyRow += "<td>Male</td>";
                        } else {
                            tableBodyRow += "<td>Female</td>";
                        }
                        if (Convert.ToInt32(value["STATUS_ID"]) == 0) {
                            tableBodyRow += "<td>Retired</td>";
                        } else if (Convert.ToInt32(value["STATUS_ID"]) == 1) {
                            tableBodyRow += "<td>Working</td>";
                        } else if (Convert.ToInt32(value["STATUS_ID"]) == 2) {
                            tableBodyRow += "<td>Suspension</td>";
                        } else if (Convert.ToInt32(value["STATUS_ID"]) == 3) {
                            tableBodyRow += "<td>Discharged</td>";
                        } else if (Convert.ToInt32(value["STATUS_ID"]) == 4) {
                            tableBodyRow += "<td>Dismissed</td>";
                        } else {
                            tableBodyRow += "<td>Resigned</td>";
                        }
                        if (value["EMP_MARITALSTATUS"].ToString() == "S") {
                            tableBodyRow += "<td>Single</td>";
                        } else if (value["EMP_MARITALSTATUS"].ToString() == "M") {
                            tableBodyRow += "<td>Married</td>";
                        } else {
                            tableBodyRow += "<td>Divorce</td>";
                        }
                        tableBodyRow += "<td>" + value["EMP_PEMAIL"] + "</td>";
                        tableBodyRow += "</tr>";
                    }
                    tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            string branch, department;
            if (allBranch.Checked) {
                branch = "0";
            } else {
                branch = branchId.Value;
            }
            if (allDepartment.Checked) {
                department = "0";
            } else {
                department = branchId.Value;
            }
            Response.Redirect(baseUrl + "employeeStatusReport?branchId=" + branch + "&departmentId=" + department + "&status=" + status.SelectedValue);
        }
    }
}