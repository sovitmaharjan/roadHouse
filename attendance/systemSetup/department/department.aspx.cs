using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.systemSetup.department {
    public partial class department : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Department Setup";
            pageNamePlace2.Text = "Department Setup";
            if (!IsPostBack) {
                string table = "Tbl_Org_Dept";
                List<string> field = new List<string>();
                field.Add("*");
                Dictionary<string, object> condition = new Dictionary<string, object>();
                condition.Add("LEVEL", 1);
                DataTable dtTableData = attendanceObject.getTableData(field, table, condition);
                string tableBodyRow = "";
                int i = 1;
                foreach (DataRow value in dtTableData.Rows) {
                    tableBodyRow += "<tr>";
                    tableBodyRow += "<td>" + i + "</td>";
                    tableBodyRow += "<td>" + value["DEPT_PARENT"] + "</td>";
                    tableBodyRow += "<td>" + value["DEPT_NAME"] + "</td>";
                    if (Convert.ToInt32(value["sta"]) == 0) {
                        tableBodyRow += "<td>Inactive </td>";
                    } else {
                        tableBodyRow += "<td>Active </td>";
                    }
                    tableBodyRow += "<td><div class='button-list'><a type='button' href='" + baseUrl + "section?i=" + value["DEPT_NAME"] + "' class='btn btn-pink w-xs waves-effect waves-light btn-xs edit'><i class='mdi mdi-pencil'></i> Section</a><button type='button' id='" + value["DEPT_ID"] + "' class='btn btn-warning w-xs waves-effect waves-light btn-xs edit' data-toggle='modal' data-target='#addContent'><i class='mdi mdi-pencil'></i> Edit</button></div></td>";
                    tableBodyRow += "</tr>";
                    i++;
                }
                tableBody.Text = tableBodyRow;

                departmentList.DataSource = dtTableData;
                departmentList.DataTextField = "DEPT_NAME";
                departmentList.DataValueField = "DEPT_ID";
                departmentList.DataBind();
            }
        }

        protected void saveClick(object sender, EventArgs e) {
            string departmentParent, departmentName, selectedNode;
            int flag;
            selectedNode = id.Value;
            if (groupUnder.Checked == false && createDefaultSection.Checked == false) {
                departmentParent = "Main";
                departmentName = departmentSectionName.Value;
                flag = 0;
                attendanceObject.queryFunction("EXECUTE Proc_managedept '" + departmentParent + "', '" + departmentName + "', '" + flag + "', '" + selectedNode + "'");
            }
            if (groupUnder.Checked == true && createDefaultSection.Checked == false) {
                departmentParent = departmentList.SelectedItem.Text;
                departmentName = departmentSectionName.Value;
                flag = 1;
                attendanceObject.queryFunction("EXECUTE Proc_managedept '" + departmentParent + "', '" + departmentName + "', '" + flag + "', '" + selectedNode + "'");
            }
            if (groupUnder.Checked == false && createDefaultSection.Checked == true) {
                departmentParent = departmentSectionName.Value;
                departmentName = section.Value;
                flag = 2;
                attendanceObject.queryFunction("EXECUTE Proc_managedept '" + departmentParent + "', '" + departmentName + "', '" + flag + "', '" + selectedNode + "'");
            }
            Response.Redirect(baseUrl + "department");
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getData(int id) {
            List<string> field = new List<string>();
            field.Add("*");
            string table = "Tbl_Org_Dept";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("DEPT_ID", id);
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