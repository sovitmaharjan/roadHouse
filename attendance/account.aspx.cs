using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance {
    public partial class account : System.Web.UI.Page {
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
                DataTable dtTableData = attendanceObject.queryFunction("select * from TBL_UserList inner join Tbl_Comp_Branch on TBL_UserList.BRANCH_ID = Tbl_Comp_Branch.BRANCH_ID where UserTypeId = 2");
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

                string table = "Tbl_Comp_Branch";
                List<string> field = new List<string>();
                field.Add("*");
                Dictionary<string, object> condition = new Dictionary<string, object>();
                DataTable dtTableData2 = attendanceObject.getTableData(field, table, condition);
                branch.DataSource = dtTableData2;
                branch.DataTextField = "BRANCH_NAME";
                branch.DataValueField = "BRANCH_ID";
                branch.DataBind();
            }
        }

        protected void saveClick(object sender, EventArgs e) {
            string table = "TBL_UserList";
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("LoginName", loginName.Value);
            data.Add("Password", password.Value);
            data.Add("FullName", fullName.Value);
            data.Add("UserTypeId", "2");
            if (statusYes.Checked) {
                data.Add("AccStatus", "1");
            }
            if (statusNo.Checked) {
                data.Add("AccStatus", "0");
            }
            data.Add("Branch_ID", branch.SelectedItem.Value);
            if (string.IsNullOrEmpty(id.Value)) {
                attendanceObject.insertTableData(table, data);
            } else {
                Dictionary<string, object> condition = new Dictionary<string, object>();
                condition.Add("UserId", id.Value);
                attendanceObject.updateTableData(table, data, condition);
            }
            Response.Redirect(baseUrl + "account");
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getData(int id) {
            List<string> field = new List<string>();
            field.Add("*");
            string table = "TBL_UserList";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("UserId", id);
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