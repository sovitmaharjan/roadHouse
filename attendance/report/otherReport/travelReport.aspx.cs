using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report.otherReport {
    public partial class travelReport : System.Web.UI.Page {
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
                if (!string.IsNullOrEmpty(Request.Params["startDate"])) {
                    DataTable dtEmployeeInfo = attendanceObject.queryFunction("SELECT DISTINCT(DEPT_NAME), BRANCH_NAME FROM view_emp_info WHERE DEPT_ID = '" + Request.Params["departmentId"] + "' AND BRANCH_ID = '" + Request.Params["branchId"] + "'");
                    heading.Text = "<b>" + Request.Params["startDate"] + " <span style='color: #797979;'>-to-</span> " + Request.Params["endDate"] + "</b><br/><b>Branch: " + dtEmployeeInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: " + dtEmployeeInfo.Rows[0]["DEPT_NAME"] + "</b>";

                    startDate.Value = Request.Params["startDate"];
                    endDate.Value = Request.Params["endDate"];
                    branch.SelectedValue = Request.Params["branchId"];
                    branchId.Value = Request.Params["branchId"];
                    department.SelectedValue = Request.Params["departmentId"];
                    departmentId.Value = Request.Params["departmentId"];

                    Dictionary<string, object> procedureData = new Dictionary<string, object>();
                    procedureData.Add("@FDATE", Request.Params["startDate"]);
                    procedureData.Add("@LDATE", Request.Params["endDate"]);
                    procedureData.Add("@EMPCODE", 0);
                    procedureData.Add("@BRANCH_ID", Request.Params["branchId"]);
                    procedureData.Add("@DEPT_ID", Request.Params["departmentId"]);
                    DataTable dtResult = attendanceObject.procedure("proc_MnOfficeOut", procedureData);
                    string tableBodyRow = "";
                    int i = 1;
                    foreach (DataRow value in dtResult.Rows) {
                        tableBodyRow += "<tr>";
                        tableBodyRow += "<td>" + i + "</td>";
                        tableBodyRow += "<td>" + value["BRANCH_NAME"] + "</td>";
                        tableBodyRow += "<td>" + value["DEPT_NAME"] + "</td>";
                        tableBodyRow += "<td>" + value["EMP_ID"] + "</td>";
                        tableBodyRow += "<td>" + value["FULLNAME"] + "</td>";
                        tableBodyRow += "<td>" + value["DEG_NAME"] + "</td>";
                        tableBodyRow += "<td>" + value["STATION"] + "</td>";
                        tableBodyRow += "<td>" + value["DAYONE"].ToString().Split(' ')[0] + "</td>";
                        tableBodyRow += "<td>" + value["DAYTWO"].ToString().Split(' ')[0] + "</td>";
                        tableBodyRow += "<td>" + value["Days"] + "</td>";
                        tableBodyRow += "<td>" + value["PURPOSE"] + "</td>";
                        tableBodyRow += "</tr>";
                        i++;
                    }
                    tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            Response.Redirect(baseUrl + "travelReport?startDate=" + startDate.Value + "&endDate=" + endDate.Value + "&branchId=" + branchId.Value + "&departmentId=" + departmentId.Value);
        }
    }
}