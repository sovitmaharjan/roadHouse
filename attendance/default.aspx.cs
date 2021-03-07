using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace attendance {
    public partial class _default : System.Web.UI.Page {
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
            if (!IsPostBack) {
                if (Session["message"] != null) {
                    string fadeout = "<script>setTimeout(function() {$('#message').fadeOut('fast');}, 4000);</script>";
                    if (Session["message"].ToString() == "0") {
                        message.Text = "<div class='alert alert-danger' id='message' role='alert'><strong>Invalid Data</strong></div>";
                        timeScript.Text = fadeout;
                    } else {
                        List<string> field = new List<string>();
                        field.Add("*");
                        string table = "Tbl_UserList";
                        Dictionary<string, object> condition = new Dictionary<string, object>();
                        condition.Add("LoginName", Session["loginName"].ToString());
                        condition.Add("Password", Session["Password"].ToString());
                        DataTable dtloginData = attendanceObject.getTableData(field, table, condition);

                        if (dtloginData.Rows.Count > 0) {
                            Session["loginName"] = dtloginData.Rows[0]["loginName"].ToString();
                            Session["fullName"] = dtloginData.Rows[0]["FullName"].ToString();
                            Session["password"] = dtloginData.Rows[0]["Password"].ToString();
                            Session["userTypeId"] = dtloginData.Rows[0]["UserTypeId"].ToString();
                            Session["message"] = 1;
                            Response.Redirect("dashboard");
                        } else {
                            Session["message"] = 0;
                            Response.Redirect("default");
                        }
                    }
                }
            }
        }

        public void loginClick(object sender, System.EventArgs e) {
            List<string> field = new List<string>();
            field.Add("*");
            string table = "Tbl_UserList";
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("LoginName", userId.Value);
            condition.Add("Password", password.Value);
            DataTable dtloginData = attendanceObject.getTableData(field, table, condition);

            if (dtloginData.Rows.Count > 0) {
                Session["loginName"] = dtloginData.Rows[0]["loginName"].ToString();
                Session["fullName"] = dtloginData.Rows[0]["FullName"].ToString();
                Session["password"] = dtloginData.Rows[0]["Password"].ToString();
                Session["userTypeId"] = dtloginData.Rows[0]["UserTypeId"].ToString();
                Session["message"] = 1;
                Response.Redirect("dashboard");
            } else {
                Session["message"] = 0;
                Response.Redirect("default");
            }
        }
    }
}