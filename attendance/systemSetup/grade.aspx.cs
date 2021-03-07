using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.systemSetup {
    public partial class grade : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Grade Setup";
            pageNamePlace2.Text = "Grade Setup";
            if (!IsPostBack) {
                string table = "Tbl_Org_Grade";
                List<string> field = new List<string>();
                field.Add("*");
                Dictionary<string, object> condition = new Dictionary<string, object>();
                DataTable dtTableData = attendanceObject.getTableData(field, table, condition);
                string tableBodyRow = "";
                int i = 1;
                foreach (DataRow value in dtTableData.Rows) {
                    tableBodyRow += "<tr>";
                    tableBodyRow += "<td>" + i + "</td>";
                    tableBodyRow += "<td>" + value["GRADE_NAME"] + "</td>";
                    tableBodyRow += "<td>" + value["GRADE_TYPE"] + "</td>";
                    if (Convert.ToInt32(value["sta"]) == 0) {
                        tableBodyRow += "<td>Inactive </td>";
                    } else {
                        tableBodyRow += "<td>Active </td>";
                    }
                    tableBodyRow += "<td><div class='button-list'><button type='button' id='" + value["GRADE_ID"] + "' class='btn btn-warning w-xs waves-effect waves-light btn-xs edit' data-toggle='modal' data-target='#addContent'><i class='mdi mdi-pencil'></i> Edit</button></div></td>";
                    tableBodyRow += "</tr>";
                    i++;
                }
                tableBody.Text = tableBodyRow;
            }
        }

        protected void saveClick(object sender, EventArgs e) {
            string table = "Tbl_Org_Grade";
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("GRADE_NAME", gradeName.Value);
            data.Add("GRADE_TYPE", gradeType.Value);
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
                condition.Add("GRADE_ID", id.Value);
                attendanceObject.updateTableData(table, data, condition);
            }
            Response.Redirect(baseUrl + "grade");
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getData(int id) {
            List<string> field = new List<string>();
            field.Add("*");
            string table = "Tbl_Org_Grade";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("GRADE_ID", id);
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