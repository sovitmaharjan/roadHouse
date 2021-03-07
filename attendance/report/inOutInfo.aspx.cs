using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report {
    public partial class inOutInfo : System.Web.UI.Page {
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

            if (!IsPostBack) {
                if (!string.IsNullOrEmpty(Request.Params["startDate"])) {
                    heading.Text = "<b>" + Request.Params["startDate"] + " <span style='color: #797979;'>-to-</span> " + Request.Params["endDate"] + "</b><br/>";
                    startDate.Value = Request.Params["startDate"];
                    endDate.Value = Request.Params["endDate"];
                    if (Request.Params["branchId"] == "0") {
                        allBranch.Checked = true;
                        branch.Attributes.Remove("required");
                        branchId.Value = "";
                    } else {
                        branch.SelectedValue = Request.Params["branchId"];
                        branchId.Value = Request.Params["branchId"];
                    }

                    Dictionary<string, object> procedureData = new Dictionary<string, object>();
                    procedureData.Add("@branch_id", Request.Params["branchId"]);
                    procedureData.Add("@StartDate", Request.Params["startDate"]);
                    procedureData.Add("@EndDate", Request.Params["endDate"]);
                    string sql;
                    if (Request.Params["branchId"] == "0") {
                        sql = "SELECT * FROM TBL_EMP_INOUT ORDER BY BRANCH_ID";
                    } else {
                        sql = "SELECT * FROM TBL_EMP_INOUT ORDER BY EMP_ID";
                    }
                    DataTable dtResult = attendanceObject.procedureAndQuery("proc_emp_list_Null", procedureData, sql);
                    string tableBodyRow = "";
                    string temp_data = "";
                    foreach (DataRow value in dtResult.Rows) {
                        if (temp_data != value["BRANCH_NAME"].ToString()) {
                            tableBodyRow += "<tr><td colspan='7' style='text-align: center;'><b>Branch: " + value["BRANCH_NAME"] + "</b></td></tr>";
                        }
                        temp_data = value["BRANCH_NAME"].ToString();
                        tableBodyRow += "<tr>";
                        tableBodyRow += "<td>" + value["TDATE"] + "</td>";
                        tableBodyRow += "<td>" + value["EMP_ID"] + "</td>";
                        tableBodyRow += "<td>" + value["EMP_FULLNAME"] + "</td>";
                        tableBodyRow += "<td>" + value["InTime"] + "</td>";
                        if (string.IsNullOrEmpty(value["InTime"].ToString()) == false) {
                            tableBodyRow += "<td style='color: red'>Forgot</td>";
                        } else {
                            tableBodyRow += "<td>" + value["InTime"] + "</td>";
                        }
                        tableBodyRow += "<td>" + value["InTime1"] + "</td>";
                        if (string.IsNullOrEmpty(value["InTime1"].ToString()) == false) {
                            tableBodyRow += "<td style='color: red'>Forgot</td>";
                        } else {
                            tableBodyRow += "<td>" + value["InTime1"] + "</td>";
                        }
                        tableBodyRow += "</tr>";
                    }
                    tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            string branch;
            if (allBranch.Checked) {
                branch = "0";
            } else {
                branch = branchId.Value;
            }
            Response.Redirect(baseUrl + "inOutInfo?startDate=" + startDate.Value + "&endDate=" + endDate.Value + "&branchId=" + branch);
        }
    }
}