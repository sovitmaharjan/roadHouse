using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.hrManagement {
    public partial class transfer : System.Web.UI.Page {
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
            bra.DataSource = dtBranch;
            bra.DataTextField = "BRANCH_NAME";
            bra.DataValueField = "BRANCH_ID";
            bra.DataBind();
            bra.Items.Insert(0, new ListItem("Select Branch", ""));
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT DEPT_ID, DEPT_NAME FROM Tbl_Org_Dept WHERE LEVEL = 2 ORDER BY DEPT_NAME ASC ");
            sec.DataSource = dtDepartment;
            sec.DataTextField = "DEPT_NAME";
            sec.DataValueField = "DEPT_ID";
            sec.DataBind();
            sec.Items.Insert(0, new ListItem("Select Section", ""));
            DataTable dtEmployee = attendanceObject.queryFunction("SELECT EMP_ID, emp_Fullname FROM view_Emp_Info ORDER BY emp_Fullname ASC");
            employee.DataSource = dtEmployee;
            employee.DataTextField = "emp_Fullname";
            employee.DataValueField = "EMP_ID";
            employee.DataBind();
            employee.Items.Insert(0, new ListItem("Select Employee", ""));

            DataTable dtResult = attendanceObject.queryFunction("SELECT * FROM view_emp_transfer_details ORDER BY TDATE DESC ");
            string tableBodyRow = "";
            int i = 1;
            foreach (DataRow value in dtResult.Rows) {
                tableBodyRow += "<tr>";
                tableBodyRow += "<td>" + i + "</td>";
                tableBodyRow += "<td>" + value["TDATE"] + "</td>";
                tableBodyRow += "<td>" + value["emp_Fullname"] + " (" + value["Emp_Id"] + ")</td>";
                tableBodyRow += "<td>" + value["branch_name_from"] + "</td>";
                tableBodyRow += "<td>" + value["dept_name_from"] + "</td>";
                tableBodyRow += "<td>" + value["trans_desc"] + "</td>";
                tableBodyRow += "<td>" + value["branch_name_to"] + "</td>";
                tableBodyRow += "<td>" + value["dept_name_to"] + "</td>";
                if (Convert.ToInt32(value["iscurrent"]) == 0) {
                    tableBodyRow += "<td>No</td>";
                } else {
                    tableBodyRow += "<td>Yes</td>";
                }
                tableBodyRow += "</tr>";
                i++;
            }
            tableBody.Text = tableBodyRow;

            if (!IsPostBack) {
                if (!string.IsNullOrEmpty(Request.Params["employeeId"])) {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("tDate", Request.Params["employeeId"]);
                    data.Add("Trans_desc", Request.Params["description"]);
                    data.Add("Iscurrent", Request.Params["isCurrent"]);
                    data.Add("Eid", Request.Params["employeeId"]);
                    data.Add("Branch_ID_To", Request.Params["designation"]);
                    data.Add("DEPT_ID", 0);
                    data.Add("Section_ID_To", Request.Params["previousDesignationId"]);
                    data.Add("Function_ID_To", 0);
                    data.Add("Branch_id_from", Request.Params["previousDesignationId"]);
                    data.Add("Section_id_From", Request.Params["previousDesignationId"]);
                    data.Add("FNC_ID_From", 0);
                    int result = attendanceObject.insertTableData("Proc_transfer_Info", data);
                    if (result == 1) {
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
            Response.Redirect(baseUrl + "promotion?startDate=" + startDate.Value + "&employeeId=" + employeeId.Value + "&previousDesignationId=" + currentDesignationId.Value + "&branch=" + bra.SelectedValue + "&section=" + sec.SelectedValue + "&isCurrent=" + isCur + "&description=" + description.Value);
        }
    }
}