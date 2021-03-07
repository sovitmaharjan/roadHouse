using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance {
    public partial class overTimeManagement : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Account";
            pageNamePlace2.Text = "Account";
            if (!IsPostBack) {
                if (string.IsNullOrEmpty(Request.Params["employeeId"]) == false) {
                    DataTable dtTableData = attendanceObject.queryFunction("EXECUTE proc_Attn_Mon_General_OTGen '" + Request.Params["employeeId"] + "', '" + Request.Params["startDate"] + "', '" + Request.Params["endDate"] + "', '0' ");
                    string tableBodyRow = "";
                    int i = 1;
                    foreach (DataRow value in dtTableData.Rows) {
                        tableBodyRow += "<tr>";
                        tableBodyRow += "<td>" + i + "</td>";
                        tableBodyRow += "<td>" + value["LoginName"] + "</td>";
                        tableBodyRow += "<td>" + value["FullName"] + "</td>";
                        tableBodyRow += "<td>" + value["BRANCH_NAME"] + "</td>";
                        if (Convert.ToInt32(value["AccStatus"]) == 0) {
                            tableBodyRow += "<td>Inactive</td>";
                        } else {
                            tableBodyRow += "<td>Active</td>";
                        }
                        tableBodyRow += "<td><div class='button-list'><button type='button' id='" + value["UserId"] + "' class='btn btn-warning w-xs waves-effect waves-light btn-xs edit' data-toggle='modal' data-target='#addContent'><i class='mdi mdi-pencil'></i> Edit</button></div></td>";
                        tableBodyRow += "</tr>";
                        i++;
                    }
                    tableBody.Text = tableBodyRow;
                }

                string table = "view_emp_info";
                List<string> field = new List<string>();
                field.Add("*");
                Dictionary<string, object> condition = new Dictionary<string, object>();
                condition.Add("STATUS_ID", "1");
                DataTable dtTableData2 = attendanceObject.getTableData(field, table, condition);
                approvedBy.DataSource = dtTableData2;
                approvedBy.DataTextField = "emp_Fullname";
                approvedBy.DataValueField = "EMP_ID";
                approvedBy.DataBind();
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            Response.Redirect(baseUrl + "overTimeManagement?employeeId = " + employeeId.Value + "startDate = " + startEnglishDate.Value + "endDate = " + endEnglishDate.Value + "employeeName = " + employeeName.Value + "designation = " + designation.Value + "department = " + department.Value + "branch = " + branch.Value + "remarks = " + remarks.Value + "approvedBy = " + approvedBy.SelectedItem.Value);
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getEmployeeData(int id) {
            List<string> field = new List<string>();
            field.Add("EMP_ID");
            field.Add("emp_Fullname");
            field.Add("DEG_NAME");
            field.Add("DEPT_NAME");
            field.Add("BRANCH_NAME");
            string table = "view_emp_info";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("EMP_ID", id);
            DataTable dtTableDataById = attendanceObject.getTableData(field, table, condition);

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtTableDataById.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtTableDataById.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }
    }
}