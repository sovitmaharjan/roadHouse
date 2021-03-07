using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report.attendanceReport {
    public partial class overTimeSummary : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Over Time Summary";
            pageNamePlace2.Text = "Over Time Summary";
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
                    DataTable dtEmployeeInfo = attendanceObject.queryFunction("SELECT DISTINCT(DEPT_NAME), BRANCH_NAME FROM view_emp_info WHERE DEPT_ID = '" + Request.Params["departmentId"] + "'");
                    heading.Text = "<b>" + Request.Params["startDate"] + " <span style='color: #797979;'>-to-</span> " + Request.Params["endDate"] + "</b><br/><b>Branch: " + dtEmployeeInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: " + dtEmployeeInfo.Rows[0]["DEPT_NAME"] + "</b>";

                    startDate.Value = Request.Params["startDate"];
                    endDate.Value = Request.Params["endDate"];
                    branch.SelectedValue = Request.Params["branchId"];
                    department.SelectedValue = Request.Params["departmentId"];
                    branchId.Value = Request.Params["branchId"];
                    departmentId.Value = Request.Params["departmentId"];

                    //DataTable dtquickAttendance = attendanceObject.quickAttendance(Convert.ToInt32(Request.Params["employeeId"]), Convert.ToDateTime(Request.Params["startDate"]), Convert.ToDateTime(Request.Params["endDate"]));
                    //string tableBodyRow = "";
                    //foreach (DataRow value in dtquickAttendance.Rows) {
                    //    tableBodyRow += "<tr>";
                    //    tableBodyRow += "</tr>";
                    //}
                    //tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            Response.Redirect(baseUrl + "quickAttendance?startDate=" + startDate.Value + "&endDate=" + endDate.Value + "&branchId=" + branchId.Value + "&departmentId=" + departmentId.Value);
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getDeptById(int id) {
            List<string> field = new List<string>();
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT DISTINCT(DEPT_ID), DEPT_NAME FROM view_Emp_Info WHERE BRANCH_ID = '" + id + "'");

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtDepartment.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtDepartment.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }
    }
}