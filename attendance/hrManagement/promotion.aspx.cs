using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.hrManagement {
    public partial class promotion : System.Web.UI.Page {
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
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT DEG_ID, DEG_NAME FROM Tbl_Org_Desg ORDER BY DEG_NAME ASC");
            designation.DataSource = dtDepartment;
            designation.DataTextField = "DEG_NAME";
            designation.DataValueField = "DEG_ID";
            designation.DataBind();
            designation.Items.Insert(0, new ListItem("Select Designation", ""));

            DataTable dtResult = attendanceObject.queryFunction("SELECT * FROM view_emp_promotion_details ORDER BY TDate DESC ");
            string tableBodyRow = "";
            foreach (DataRow value in dtResult.Rows) {
                tableBodyRow += "<tr>";
                tableBodyRow += "<td>" + value["TDate"] + "</td>";
                tableBodyRow += "<td>" + value["emp_Fullname"] + " (" + value["Emp_Id"] + ")</td>";
                tableBodyRow += "<td>" + value["Promotion_Title"] + "</td>";
                tableBodyRow += "<td>" + value["DEG_NAME"] + "</td>";
                if (Convert.ToInt32(value["IsCurrent"]) == 0) {
                    tableBodyRow += "<td>No</td>";
                } else {
                    tableBodyRow += "<td>Yes</td>";
                }
                tableBodyRow += "<td>" + value["Expr2"] + "</td>";
                tableBodyRow += "</tr>";
            }
            tableBody.Text = tableBodyRow;

             if (!IsPostBack) {
                if (!string.IsNullOrEmpty(Request.Params["employeeId"])) {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Emp_id", Request.Params["employeeId"]);
                    data.Add("TDate", Request.Params["startDate"]);
                    data.Add("Promotion_title", Request.Params["description"]);
                    data.Add("IsCurrent", Request.Params["isCurrent"]);
                    data.Add("desg_id", Request.Params["designation"]);
                    data.Add("P_Deg_Id", Request.Params["previousDesignationId"]);
                    int i = attendanceObject.insertTableData("Tbl_Emp_Promotion_Detail", data);
                    if (i == 1) {
                        data.Clear();
                        data.Add("DEG_ID", Request.Params["previousDesignationId"]);
                        Dictionary<string, object> condition = new Dictionary<string, object>();
                        condition.Add("Emp_id", Request.Params["designation"]);
                        attendanceObject.updateTableData("Tbl_emp_off_info", data, condition);
                    }
                }
            }
        }

        protected void saveClick(object sender, EventArgs e) {
            string isCur;
            if (latestTransfer.Checked) {
                isCur = "1";
            } else {
                isCur = "0";
            }
            Response.Redirect(baseUrl + "promotion?startDate=" + startDate.Value + "&employeeId=" + employeeId.Value + "&previousDesignationId=" + currentDesignationId.Value + "&designation=" + designation.SelectedValue + "&isCurrent=" + isCur + "&description=" + description.Value);
        }
    }
}