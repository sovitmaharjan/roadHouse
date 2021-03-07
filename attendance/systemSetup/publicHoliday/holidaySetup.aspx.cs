using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.systemSetup.publicHoliday {
    public partial class holidaySetup : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Holiday Setup";
            pageNamePlace2.Text = "Holiday Setup";
            if (!IsPostBack) {
                string table = "Tbl_Org_Holiday";
                List<string> field = new List<string>();
                field.Add("*");
                Dictionary<string, object> condition = new Dictionary<string, object>();
                DataTable dtTableData = attendanceObject.getTableData(field, table, condition);
                string tableBodyRow = "";
                int i = 1;
                foreach (DataRow value in dtTableData.Rows) {
                    tableBodyRow += "<tr>";
                    tableBodyRow += "<td>" + i + "</td>";
                    tableBodyRow += "<td>" + value["HOLIDAY_DATE"] + "</td>";
                    tableBodyRow += "<td>" + value["HOLIDAY_NAME"] + "</td>";
                    tableBodyRow += "<td>" + value["HOLIDAY_QTY"] + "</td>";
                    if (Convert.ToInt32(value["holidayType"]) == 1) {
                        tableBodyRow += "<td>Standard</td>";
                    } else if (Convert.ToInt32(value["holidayType"]) == 2) {
                        tableBodyRow += "<td>Specific</td>";
                    } else {
                        tableBodyRow += "<td>Unofficial</td>";
                    }
                    if (Convert.ToInt32(value["sta"]) == 0) {
                        tableBodyRow += "<td>Inactive </td>";
                    } else {
                        tableBodyRow += "<td>Active </td>";
                    }
                    tableBodyRow += "<td><div class='button-list'><button type='button' id='" + value["HOLIDAY_ID"] + "' class='btn btn-warning w-xs waves-effect waves-light btn-xs edit' data-toggle='modal' data-target='#addContent'><i class='mdi mdi-pencil'></i> Edit</button></div></td>";
                    tableBodyRow += "</tr>";
                    i++;
                }
                tableBody.Text = tableBodyRow;
            }
        }

        protected void saveClick(object sender, EventArgs e) {
            //string table = "Tbl_Org_Holiday";
            //Dictionary<string, object> data = new Dictionary<string, object>();
            //data.Add("BRANCH_NAME", branchName.Value);
            //data.Add("BRANCH_CODE", branchCode.Value);
            //if(statusYes.Checked) {
            //    data.Add("sta", "1");
            //}
            //if(statusNo.Checked) {
            //    data.Add("sta", "0");
            //}
            //if(string.IsNullOrEmpty(id.Value)) {
            //    data.Add("BRANCH_ID", Convert.ToInt32(attendanceObject.queryFunction("select max(HOLIDAY_ID)+1 from Tbl_Org_Holiday").Rows[0][0]));
            //    attendanceObject.insertTableData(table, data);
            //} else {
            //    Dictionary<string, object> condition = new Dictionary<string, object>();
            //    condition.Add("HOLIDAY_ID", id.Value);
            //    attendanceObject.updateTableData(table, data, condition);
            //}
            //Response.Redirect(baseUrl + "holidaySetup");
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getData(int id) {
            List<string> field = new List<string>();
            field.Add("*");
            string table = "Tbl_Org_Holiday";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("HOLIDAY_ID", id);
            DataTable dtTableDataById = attendanceObject.getTableData(field, table, condition);

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach(DataRow dataTableRow in dtTableDataById.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach(DataColumn tableColumn in dtTableDataById.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }
    }
}