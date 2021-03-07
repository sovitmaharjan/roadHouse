using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report.otherReport {
    public partial class promotionReport : System.Web.UI.Page {
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
            DataTable dtEmployee = attendanceObject.queryFunction("SELECT EMP_ID, emp_Fullname FROM view_Emp_Info ORDER BY emp_Fullname ASC");
            employee.DataSource = dtEmployee;
            employee.DataTextField = "emp_Fullname";
            employee.DataValueField = "EMP_ID";
            employee.DataBind();
            employee.Items.Insert(0, new ListItem("Select Employee", ""));

            if (!IsPostBack) {
                if (!string.IsNullOrEmpty(Request.Params["startDate"])) {
                    if (Request.Params["employeeId"] == "0") {
                        DataTable dtHeaderInfo = attendanceObject.queryFunction("SELECT DISTINCT(DEPT_NAME), BRANCH_NAME FROM view_emp_info WHERE DEPT_ID = '" + Request.Params["departmentId"] + "' AND BRANCH_ID = '" + Request.Params["branchId"] + "'");
                        heading.Text = "<b>" + Request.Params["startDate"] + " On Wards<br/><b>Branch: All</b><br /><b>Department: All</b>";
                    } else {
                        DataTable dtHeaderInfo = attendanceObject.queryFunction("SELECT emp_Fullname, BRANCH_NAME, DEPT_NAME FROM view_emp_info WHERE EMP_ID = '" + Request.Params["employeeId"] + "'");
                        heading.Text = "<b>" + Request.Params["startDate"] + " On Wards</b><br/><b><span style='font-size: 14px; color: #797979;'>Employee: " + dtHeaderInfo.Rows[0]["emp_fullName"] + " (" + Request.Params["employeeId"] + ")</span></b><br/><b>Branch: " + dtHeaderInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: " + dtHeaderInfo.Rows[0]["DEPT_NAME"] + "</b>";
                    }

                    startDate.Value = Request.Params["startDate"];
                    employee.SelectedValue = Request.Params["employeeId"];
                    if (Request.Params["employeeId"] == "0") {
                        allEmployee.Checked = true;
                    } else {
                        employeeId.Value = Request.Params["employeeId"];
                    }

                    Dictionary<string, object> procedureData = new Dictionary<string, object>();
                    procedureData.Add("@Emp_id", Request.Params["employeeId"]);
                    procedureData.Add("@date", Request.Params["startDate"]);
                    DataTable dtResult = attendanceObject.procedure("sp_Promotion", procedureData);
                    string tableBodyRow = "";
                    foreach (DataRow value in dtResult.Rows) {
                        tableBodyRow += "<tr>";
                        tableBodyRow += "<td>" + value["Promotion_id"].ToString().Split(' ')[0] + "</td>";
                        tableBodyRow += "<td>" + Convert.ToDateTime(value["TDate"].ToString().Split(' ')[0]).ToString("yyyy-MM-dd") + "</td>";
                        tableBodyRow += "<td>" + value["BRANCH_NAME"] + "</td>";
                        tableBodyRow += "<td>" + value["DEPT_NAME"] + "</td>";
                        tableBodyRow += "<td>" + value["Emp_Id"] + "</td>";
                        tableBodyRow += "<td>" + value["emp_Fullname"] + "</td>";
                        tableBodyRow += "<td>" + value["Deg_old"] + "</td>";
                        tableBodyRow += "<td>" + value["Deg_new"] + "</td>";
                        tableBodyRow += "<td>" + value["Promotion_Title"] + "</td>";
                        tableBodyRow += "</tr>";
                    }
                    tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            string emp;
            if (allEmployee.Checked) {
                emp = "0";
            } else {
                emp = employeeId.Value.ToString();
            }
            Response.Redirect(baseUrl + "promotionReport?startDate=" + startDate.Value + "&endDate=" + "&employeeId=" + emp);
        }
    }
}