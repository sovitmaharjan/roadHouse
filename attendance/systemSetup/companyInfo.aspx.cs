using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.systemSetup {
    public partial class companyInfo : System.Web.UI.Page {
        attendance attendanceObject = new attendance();

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
            pageNamePlace1.Text = "Company Information";
            pageNamePlace2.Text = "Company Information";
            if (!IsPostBack) {
                string table = "tbl_Org";
                List<string> field = new List<string>();
                field.Add("*");
                Dictionary<string, object> condition = new Dictionary<string, object>();
                DataTable dtTableData = attendanceObject.getTableData(field, table, condition);
                if (dtTableData.Rows.Count > 0) {
                    id.Value = dtTableData.Rows[0]["Org_Id"].ToString();
                    name.Value = dtTableData.Rows[0]["Org_Name"].ToString();
                    address.Value = dtTableData.Rows[0]["Org_Address"].ToString();
                    address2.Value = dtTableData.Rows[0]["Org_Address2"].ToString();
                    telephone.Value = dtTableData.Rows[0]["Org_Phone"].ToString();
                    fax.Value = dtTableData.Rows[0]["Org_Fax"].ToString();
                    email.Value = dtTableData.Rows[0]["Org_Email"].ToString();
                    website.Value = dtTableData.Rows[0]["Org_Website"].ToString();
                }
            }
        }

        [WebMethod]
        public List<Dictionary<string, object>> getData(string id) {
            List<string> field = new List<string>();
            field.Add("*");
            string table = "Tbl_Comp_Branch";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("BRANCH_ID", "1");
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

        protected void saveClick(object sender, EventArgs e) {
            string table = "tbl_Org";
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Org_Name", name.Value);
            data.Add("Org_Address", address.Value);
            data.Add("Org_Address2", address2.Value);
            data.Add("Org_Phone", telephone.Value);
            data.Add("Org_Fax", fax.Value);
            data.Add("Org_Email", email.Value);
            data.Add("Org_Website", website.Value);
            if (string.IsNullOrEmpty(id.Value)) {
                attendanceObject.insertTableData(table, data);
            } else {
                Dictionary<string, object> condition = new Dictionary<string, object>();
                condition.Add("Org_Id", id.Value);
                attendanceObject.updateTableData(table, data, condition);
            }
        }
    }
}