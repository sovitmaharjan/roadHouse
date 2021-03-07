using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.systemSetup {
    public partial class leave : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Leave Setup";
            pageNamePlace2.Text = "Leave Setup";
            if (!IsPostBack) {
                string table = "Tbl_Org_Leave_info";
                List<string> field = new List<string>();
                field.Add("*");
                Dictionary<string, object> condition = new Dictionary<string, object>();
                DataTable dtTableData = attendanceObject.getTableData(field, table, condition);
                string tableBodyRow = "";
                int i = 1;
                foreach (DataRow value in dtTableData.Rows) {
                    tableBodyRow += "<tr>";
                    tableBodyRow += "<td>" + i + "</td>";
                    tableBodyRow += "<td>" + value["LEAVE_NAME"] + "</td>";
                    if (Convert.ToInt32(value["LEAVE_TYPE"]) == 1) {
                        tableBodyRow += "<td>Expire Yearly </td>";
                    } else if (Convert.ToInt32(value["LEAVE_TYPE"]) == 2) {
                        tableBodyRow += "<td>Accumulative</td>";
                    } else {
                        tableBodyRow += "<td>Service Period</td>";
                    }
                    tableBodyRow += "<td>" + value["LEAVE_DAYS"] + "</td>";
                    tableBodyRow += "<td>" + value["LEAVE_MAX"] + "</td>";
                    if (string.IsNullOrEmpty(value["ISCashable"].ToString())) {
                        tableBodyRow += "<td>No</td>";
                    } else {
                        if (Convert.ToInt32(value["ISCashable"]) == 0) {
                            tableBodyRow += "<td>No</td>";
                        } else {
                            tableBodyRow += "<td>Yes</td>";
                        }
                    }
                    tableBodyRow += "<td>" + value["MAX_DAYS_AT_A_TIME"] + "</td>";
                    tableBodyRow += "<td>" + value["service_period"] + "</td>";
                    if (string.IsNullOrEmpty(value["mustexhaustotherleaves"].ToString())) {
                        tableBodyRow += "<td>Monthly Earning</td>";
                    } else {
                        if (Convert.ToInt32(value["mustexhaustotherleaves"]) == 0) {
                            tableBodyRow += "<td>Monthly Earning</td>";
                        } else {
                            tableBodyRow += "<td>Must Exhaust All The Leave</td>";
                        }
                    }
                    if (Convert.ToInt32(value["sta"]) == 0) {
                        tableBodyRow += "<td>Inactive </td>";
                    } else {
                        tableBodyRow += "<td>Active </td>";
                    }
                    tableBodyRow += "<td><div class='button-list'><button type='button' id='" + value["LEAVE_ID"] + "' class='btn btn-warning w-xs waves-effect waves-light btn-xs edit' data-toggle='modal' data-target='#addContent'><i class='mdi mdi-pencil'></i> Edit</button></div></td>";
                    tableBodyRow += "</tr>";
                    i++;
                }
                tableBody.Text = tableBodyRow;
            }
        }

        protected void saveClick(object sender, EventArgs e) {
            string table = "Tbl_Org_Leave_info";
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("LEAVE_NAME", leaveName.Value);
            if (expireYearly.Checked) {
                data.Add("LEAVE_TYPE", "1");
            }
            if (accumulative.Checked) {
                data.Add("LEAVE_TYPE", "2");
            }
            if (servicePeriod.Checked) {
                data.Add("LEAVE_TYPE", "3");
            }
            if (cashableYes.Checked) {
                data.Add("ISCashable", "1");
            }
            if (cashableNo.Checked) {
                data.Add("ISCashable", "0");
            }
            data.Add("LEAVE_DAYS", dayAnnually.Value);
            data.Add("MAX_DAYS_AT_A_TIME", maxDaysAtTime.Value);
            data.Add("LEAVE_MAX", maxAccumulationDay.Value);
            data.Add("service_period", servicePeriodInAYear.Value);
            if (monthlyEarning.Checked) {
                data.Add("others", "0");
            }
            if (mustExhaustAllTheLeave.Checked) {
                data.Add("others", "1");
            }
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
                condition.Add("LEAVE_ID", id.Value);
                attendanceObject.updateTableData(table, data, condition);
            }
            Response.Redirect(baseUrl + "/leave");
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getData(int id) {
            List<string> field = new List<string>();
            field.Add("*");
            string table = "Tbl_Org_Leave_info";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("LEAVE_ID", id);
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