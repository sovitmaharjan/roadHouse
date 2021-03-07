using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report {
    public partial class shiftIndication : System.Web.UI.Page {
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
            pageNamePlace1.Text = "Quick Attendance";
            pageNamePlace2.Text = "Quick Attendance";
            DataTable dtBranch = attendanceObject.queryFunction("SELECT BRANCH_ID, BRANCH_NAME FROM Tbl_Comp_Branch ORDER BY BRANCH_NAME ASC");
            branch.DataSource = dtBranch;
            branch.DataTextField = "BRANCH_NAME";
            branch.DataValueField = "BRANCH_ID";
            branch.DataBind();
            branch.Items.Insert(0, new ListItem("Select Branch", ""));
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT DEPT_ID, DEPT_NAME FROM Tbl_Org_Dept WHERE LEVEL = 1 ORDER BY DEPT_NAME ASC");
            department.DataSource = dtDepartment;
            department.DataTextField = "DEPT_NAME";
            department.DataValueField = "DEPT_ID";
            department.DataBind();
            department.Items.Insert(0, new ListItem("Select Department", ""));

            if (!IsPostBack) {
                if (!string.IsNullOrEmpty(Request.Params["startDate"])) {
                    DataTable dtEmployeeInfo = attendanceObject.queryFunction("SELECT DISTINCT(DEPT_NAME), BRANCH_NAME FROM view_emp_info WHERE DEPT_ID = '" + Request.Params["departmentId"] + "' AND BRANCH_ID = '" + Request.Params["branchId"] + "'");
                    heading.Text = "<b>" + Request.Params["startDate"] + "</b><br/><b>Branch: " + dtEmployeeInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: " + dtEmployeeInfo.Rows[0]["DEPT_NAME"] + "</b>";

                    startDate.Value = Request.Params["startDate"];
                    branch.SelectedValue = Request.Params["branchId"];
                    branchId.Value = Request.Params["branchId"];
                    department.SelectedValue = Request.Params["departmentId"];
                    departmentId.Value = Request.Params["departmentId"];

                    DataTable dtResult = attendanceObject.shiftIndication(Request.Params["startDate"], Request.Params["departmentId"]);
                    string tableBodyRow = "";
                    int i = 1;
                    foreach (DataRow value in dtResult.Rows) {
                        tableBodyRow += "<tr>";
                        tableBodyRow += "<td>" + i + "</td>";
                        if (value["Remark"].ToString() == "Absent") {
                            tableBodyRow += "<td>" + value["emp_Fullname"] + " (" + value["EMP_ID"] + ")</td>";
                            tableBodyRow += "<td>" + value["IN_START"] + " - " + value["OUT_START"] + "</td>";
                            tableBodyRow += "<td colspan='8' style='text-align: center;background-color: red;'>Absent</td>";
                        } else if (value["Remark"].ToString() == "Weekend Holiday") {
                            tableBodyRow += "<td>" + value["emp_Fullname"] + " (" + value["EMP_ID"] + ")</td>";
                            tableBodyRow += "<td colspan='9' style='text-align: center;background-color: blue;'>Weekend Holiday</td>";
                        } else if (value["Remark"].ToString() == "Annual Leave") {
                            tableBodyRow += "<td>" + value["emp_Fullname"] + " (" + value["EMP_ID"] + ")</td>";
                            tableBodyRow += "<td colspan='9' style='text-align: center; background-color: white;'>Annual Leave</td>";
                        } else {
                            DateTime defaultInTime = new DateTime();
                            DateTime userInTime = new DateTime();
                            DateTime graceTime = new DateTime();
                            if(string.IsNullOrEmpty(value["IN_START"].ToString()) == false){
                                defaultInTime = Convert.ToDateTime(value["IN_START"]);
                            }
                            if (string.IsNullOrEmpty(value["InTime"].ToString()) == false) {
                                userInTime = Convert.ToDateTime(value["InTime"]);
                            }
                            if (string.IsNullOrEmpty(value["IN_END"].ToString()) == false) {
                                graceTime = Convert.ToDateTime(value["IN_END"]);
                            }
                            tableBodyRow += "<td>" + value["emp_Fullname"] + " (" + value["EMP_ID"] + ")</td>";
                            tableBodyRow += "<td>" + value["IN_START"] + " - " + value["OUT_START"] + "</td>";
                            if (userInTime < defaultInTime){
                                tableBodyRow += "<td style='background-color: orange;'>" + value["InTime"] + "</td>";
                                tableBodyRow += "<td style='background-color: orange;'>" + value["INREMARKS"] + "</td>";
                            } else if (graceTime > userInTime && userInTime > defaultInTime) {
                                tableBodyRow += "<td style='background-color: green;'>" + value["InTime"] + "</td>";
                                tableBodyRow += "<td style='background-color: green;'>On Time</td>";
                            } else {
                                tableBodyRow += "<td style='background-color: red;'>" + value["InTime"] + "</td>";
                                tableBodyRow += "<td style='background-color: red;'>" + value["INREMARKS"] + "</td>";
                            }
                            tableBodyRow += "<td>" + value["TDate_out"] + "</td>";

                            DateTime defaultOutTime = new DateTime();
                            DateTime userOutTime = new DateTime();
                            DateTime userOutTime1 = new DateTime();
                            if (string.IsNullOrEmpty(value["IN_START"].ToString()) == false) {
                                defaultOutTime = Convert.ToDateTime(value["OUT_START"]);
                            }
                            if (string.IsNullOrEmpty(value["InTime"].ToString()) == false) {
                                userOutTime = Convert.ToDateTime(value["OUTTIME"]);
                            }
                            if (string.IsNullOrEmpty(value["OUTTIME1"].ToString()) == false) {
                                userOutTime1 = Convert.ToDateTime(value["OUTTIME1"]);
                            }
                            if (string.IsNullOrEmpty(value["OUTTIME1"].ToString())) {
                                if (userOutTime > defaultOutTime) {
                                    tableBodyRow += "<td style='background-color: orange;'>" + value["OUTTIME"] + "</td>";
                                } else {
                                    tableBodyRow += "<td>" + value["OUTTIME"] + "</td>";
                                }
                            } else {
                                tableBodyRow += "<td>" + value["OUTTIME"] + "</td>";
                            }
                            tableBodyRow += "<td>" + value["InTime1"] + "</td>";
                            if (value["InTime1"].ToString() == "") {
                                tableBodyRow += "<td></td>";
                            } else {
                                tableBodyRow += "<td>" + value["TDate_out1"] + "</td>";
                            }
                            if (string.IsNullOrEmpty(value["OUTTIME1"].ToString())) {
                                if (userOutTime > defaultOutTime) {
                                    tableBodyRow += "<td>" + value["OUTTIME1"] + "</td>";
                                    tableBodyRow += "<td style='background-color: orange;'>Late Out</td>";
                                } else {
                                    tableBodyRow += "<td>" + value["OUTTIME1"] + "</td>";
                                    if (string.IsNullOrEmpty(userOutTime.ToString())) {
                                        tableBodyRow += "<td style='background-color: red;'>No Out Data</td>";
                                    } else {
                                        tableBodyRow += "<td style='background-color: red;'>Early Out</td>";
                                    }
                                }
                            } else {
                                if (userOutTime1 > defaultOutTime) {
                                    tableBodyRow += "<td style='background-color: orange;'>" + value["OUTTIME1"] + "</td>";
                                    tableBodyRow += "<td style='background-color: orange;'>Late Out</td>";
                                } else {
                                    tableBodyRow += "<td>" + value["OUTTIME1"] + "</td>";
                                    if (string.IsNullOrEmpty(userOutTime.ToString())) {
                                        tableBodyRow += "<td style='background-color: red;'>No Out Data</td>";
                                    } else {
                                        tableBodyRow += "<td style='background-color: red;'>Early Out</td>";
                                    }
                                }
                            }
                        }
                        tableBodyRow += "</tr>";
                        i++;
                    }
                    tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            Response.Redirect(baseUrl + "shiftIndication?startDate=" + startDate.Value + "&branchId=" + branchId.Value + "&departmentId=" + departmentId.Value);
        }
    }
}