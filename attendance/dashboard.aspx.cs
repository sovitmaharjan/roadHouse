using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

namespace attendance {
    public partial class dashboard : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Dashboard";
            pageNamePlace2.Text = "Dashboard";

            branch.Text = attendanceObject.queryFunction("select count(*) from Tbl_comp_branch").Rows[0][0].ToString();
            department.Text = attendanceObject.queryFunction("select count(*) from Tbl_Org_Dept where sta = '1' and LEVEL= '2'").Rows[0][0].ToString();
            female.Text = attendanceObject.queryFunction("select count(*) from view_emp_info where STATUS_ID = '1' and EMP_GENDER = 'F'").Rows[0][0].ToString();
            male.Text = attendanceObject.queryFunction("select count(*) from view_emp_info where STATUS_ID = '1' and EMP_GENDER = 'M'").Rows[0][0].ToString();
        }

        [WebMethod]
        public static List<List<string>> pieChartData() {
            DataTable dtPieChartData = attendanceObject.queryFunction("EXECUTE Barchart_info B");
            int count = dtPieChartData.Rows.Count;
            List<string> name = new List<string>();
            List<string> quantity = new List<string>();
            foreach (DataRow value in dtPieChartData.Rows) {
                name.Add(value["branch_name"].ToString());
                quantity.Add(value["totalemp"].ToString());
            }
            List<List<string>> data = new List<List<string>>();
            data.Add(name);
            data.Add(quantity);
            return data;
        }
    }
}