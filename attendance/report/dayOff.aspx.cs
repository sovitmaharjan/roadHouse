using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report {
    public partial class dayOff : System.Web.UI.Page {
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
                    DataTable dtResult = attendanceObject.procedure("proc_DayOff", procedureData);
                    string tableBodyRow = "";
                    string temp_data = "";
                    foreach (DataRow value in dtResult.Rows) {
                        if(temp_data != value["BRANCH_NAME"].ToString()){
                            tableBodyRow += "<tr><td colspan='7' style='text-align: center;'><b>Branch: " + value["BRANCH_NAME"] +"</b></td></tr>";
                        }
                        temp_data = value["BRANCH_NAME"].ToString();
                        tableBodyRow += "<tr>";
                            if (Convert.ToInt32(value["days"]) == 1) {
                                tableBodyRow += "<td style='color: red'>" + value["OffDay"] + "</td>";
                            } else if (Convert.ToInt32(value["days"]) == 2) {
                                tableBodyRow += "<td style='color: green'>" + value["OffDay"] + "</td>";
                            } else if (Convert.ToInt32(value["days"]) == 3) {
                                tableBodyRow += "<td style='color: blue'>" + value["OffDay"] + "</td>";
                            } else if (Convert.ToInt32(value["days"]) == 4) {
                                tableBodyRow += "<td style='color: #ba4a45'>" + value["OffDay"] + "</td>";
                            } else if (Convert.ToInt32(value["days"]) == 5) {
                                tableBodyRow += "<td style='color: #babb45'>" + value["OffDay"] + "</td>";
                            } else if (Convert.ToInt32(value["days"]) == 6) {
                                tableBodyRow += "<td style='color: #3abe45'>" + value["OffDay"] + "</td>";
                            } else {
                                tableBodyRow += "<td style='color: magenta'>" + value["OffDay"] + "</td>";
                            }
                            tableBodyRow += "<td>" + value["EMP_ID"] + "</td>";
                            tableBodyRow += "<td>" + value["emp_Fullname"] + "</td>";
                            tableBodyRow += "<td>" + value["DEPT_NAME"] + "</td>";
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
            Response.Redirect(baseUrl + "dayOff?startDate=" + startDate.Value + "&endDate=" + endDate.Value + "&branchId=" + branch);
        }
    }
}