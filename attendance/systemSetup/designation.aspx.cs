using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.systemSetup {
    public partial class designation : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Designation Setup";
            pageNamePlace2.Text = "Designation Setup";
            if (!IsPostBack) {
                string table = "Tbl_Org_Desg";
                List<string> field = new List<string>();
                field.Add("*");
                Dictionary<string, object> condition = new Dictionary<string, object>();
                DataTable dtTableData = attendanceObject.getTableData(field, table, condition);
                string tableBodyRow = "";
                int i = 1;
                foreach (DataRow value in dtTableData.Rows) {
                    tableBodyRow += "<tr>";
                    tableBodyRow += "<td>" + i + "</td>";
                    tableBodyRow += "<td>" + value["DEG_PARENT"] + "</td>";
                    tableBodyRow += "<td>" + value["DEG_NAME"] + "</td>";
                    if (Convert.ToInt32(value["sta"]) == 0) {
                        tableBodyRow += "<td>Inactive </td>";
                    } else {
                        tableBodyRow += "<td>Active </td>";
                    }
                    tableBodyRow += "<td><div class='button-list'><button type='button' id='" + value["DEG_ID"] + "' class='btn btn-warning w-xs waves-effect waves-light btn-xs edit' data-toggle='modal' data-target='#addContent'><i class='mdi mdi-pencil'></i> Edit</button></div></td>";
                    tableBodyRow += "</tr>";
                    i++;
                }
                tableBody.Text = tableBodyRow;
            }
        }

        protected void saveClick(object sender, EventArgs e) {
            string table = "Tbl_Org_Desg";
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("DEG_PARENT", designationParent.Value);
            data.Add("DEG_NAME", designationName.Value);
            if (statusYes.Checked) {
                data.Add("sta", "1");
            }
            if (statusNo.Checked) {
                data.Add("sta", "0");
            }
            if (string.IsNullOrEmpty(id.Value)) {
                attendanceObject.insertTableData(table, data);
            } else {
                Dictionary<string, object> condition = new Dictionary<string, object>();
                condition.Add("DEG_ID", id.Value);
                attendanceObject.updateTableData(table, data, condition);
            }
            Response.Redirect(baseUrl + "designation");
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getData(int id) {
            List<string> field = new List<string>();
            field.Add("*");
            string table = "Tbl_Org_Desg";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("DEG_ID", id);
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