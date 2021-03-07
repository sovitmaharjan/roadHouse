using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance.report {
    public partial class securityGuard : System.Web.UI.Page {
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
            DataTable dtEmployee = attendanceObject.queryFunction("SELECT EMP_ID, emp_Fullname FROM view_Emp_Info ORDER BY emp_Fullname ASC");
            employee.DataSource = dtEmployee;
            employee.DataTextField = "emp_Fullname";
            employee.DataValueField = "EMP_ID";
            employee.DataBind();
            employee.Items.Insert(0, new ListItem("Select Employee", ""));

            if (!IsPostBack) {
                if (!string.IsNullOrEmpty(Request.Params["startDate"])) {
                    if (Request.Params["employeeId"] == "0") {
                        DataTable dtHeaderInfo = attendanceObject.queryFunction("SELECT DISTINCT(DEPT_NAME), BRANCH_NAME FROM view_emp_info WHERE DEPT_ID = '" + Request.Params["departmentId"] + "' AND BRANCH_ID = '" + Request.Params["branchId"] + "'");
                        heading.Text = "<b>" + Request.Params["startDate"] + " <span style='color: #797979;'>-to-</span> " + Request.Params["endDate"] + "</b><br/><b><span style='font-size: 14px; color: #797979;'>Employee: All</span></b><br/><b>Branch: " + dtHeaderInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: " + dtHeaderInfo.Rows[0]["DEPT_NAME"] + "</b>";
                    } else {
                        DataTable dtHeaderInfo = attendanceObject.queryFunction("SELECT emp_Fullname, BRANCH_NAME, DEPT_NAME FROM view_emp_info WHERE EMP_ID = '" + Request.Params["employeeId"] + "'");
                        heading.Text = "<b>" + Request.Params["startDate"] + " <span style='color: #797979;'>-to-</span> " + Request.Params["endDate"] + "</b><br/><b><span style='font-size: 14px; color: #797979;'>Employee: " + dtHeaderInfo.Rows[0]["emp_fullName"] + "</span></b><br/><b>Branch: " + dtHeaderInfo.Rows[0]["BRANCH_NAME"] + "</b><br /><b>Department: " + dtHeaderInfo.Rows[0]["DEPT_NAME"] + "</b>";
                    }

                    startDate.Value = Request.Params["startDate"];
                    endDate.Value = Request.Params["endDate"];
                    branch.SelectedValue = Request.Params["branchId"];
                    department.SelectedValue = Request.Params["departmentId"];
                    employee.SelectedValue = Request.Params["employeeId"];
                    branchId.Value = Request.Params["branchId"];
                    departmentId.Value = Request.Params["departmentId"];
                    if (Request.Params["employeeId"] == "0") {
                        allEmployee.Checked = true;
                    } else {
                        employeeId.Value = Request.Params["employeeId"];
                    }

                    DataTable dtquickAttendance = attendanceObject.quickAttendance(Convert.ToInt32(Request.Params["employeeId"]), Convert.ToDateTime(Request.Params["startDate"]), Convert.ToDateTime(Request.Params["endDate"]));
                    string tableBodyRow = "";
                    foreach (DataRow value in dtquickAttendance.Rows) {
                        tableBodyRow += "<tr>";
                        tableBodyRow += "<td>" + value["tdate"] + "<br>" + value["Shift"] + "<br>" + value["nepaliDate"] + "</td>";
                        if (value["flag"] == "abs") {
                            tableBodyRow += "<td colspan='13' style='text-align: center;background-color: #ff8080;color:Black;font-size: 18px;'><br>Absent</td>";
                        } else if (value["flag"] == "hol") {
                            tableBodyRow += "<td colspan='13' style='text-align: center;background-color: #35abd6;color:Black;font-size: 18px;'><br>Public Holiday (" + value["Remark"] + ")</td>";
                        } else if (value["flag"] == "lev") {
                            tableBodyRow += "<td colspan='13' style='text-align: center;background-color: #d68b0a;color:Black;font-size: 18px;'><br>" + value["Remark"] + "</td>";
                        } else if (value["flag"] == "wee") {
                            if (string.IsNullOrEmpty(value["InTime"].ToString())) {
                                tableBodyRow += "<br><td colspan='13' style='text-align: center;background-color: #27874b;color:Black;font-size: 18px;'>Weekend Holiday</td>";
                            } else {
                                tableBodyRow += "<td style='color: blue;background: #27874b'><br>" + value["HrsMin"] + "</td>";
                                if (value["INREMARKS"] == "Early In") {
                                    tableBodyRow += "<td style='color: blue; background-color: #27874b'><br>" + value["InTime"] + "<br>" + value["INREMARKS"] + "</td>";
                                } else {
                                    tableBodyRow += "<td style='background: #27874b; color: red;'><br>" + value["InTime"] + "<br>" + value["INREMARKS"] + "</td>";
                                }
                                tableBodyRow += "<td style='color: blue; background: #27874b'> <br>" + value["TDate_Out"] + "</td>";
                                tableBodyRow += "<td style='color: blue; background: #27874b; color: blue'> <br>" + value["OutTime"] + "</td>";
                                tableBodyRow += "<td style='color: blue; background: #27874b'> <br>" + value["OUTREMARKS"] + "</td>";
                                tableBodyRow += "<td style='color: blue; background: #27874b;'> <br>" + value["worked_hrs1"] + "</td>";
                                if (value["INREMARKS1"] == "Early In") {
                                    tableBodyRow += "<td style=' color: blue; background: #27874b;'> <br>" + value["InTime1"] + "<br>" + value["INREMARKS1"] + "<br></td>";
                                } else {
                                    tableBodyRow += "<td style='color: red; background: #27874b;'> <br>" + value["InTime1"] + "<br>" + value["INREMARKS1"] + "</td>";
                                }
                                tableBodyRow += "<td style='color: blue; background: #27874b'> <br>" + value["TDate_Out1"] + "</td>";
                                tableBodyRow += "<td style='color: blue; background: #27874b'> <br>" + value["OutTime1"] + "</td>";
                                tableBodyRow += "<td style='color: blue; background: #27874b'> <br>" + value["OUTREMARKS1"] + "</td>";
                                tableBodyRow += "<td style='color: blue; background: #27874b'> <br>" + value["worked_hrs2"] + "</td>";
                                tableBodyRow += "<td style='color: blue; background: #27874b'> <br>" + value["TotalOT"] + "</td>";
                                tableBodyRow += "<td style='color: blue; background: #27874b'> <br> Worked On Holiday<td>";
                            }
                        } else if (value["flag"] == "Half") {
                            tableBodyRow += "<td>" + value["HrsMin"] + "</td>";
                            tableBodyRow += "<td>" + value["InTime"] + "</td>";
                            tableBodyRow += "<td>" + value["TDate_Out"] + "</td>";
                            tableBodyRow += "<td>" + value["OutTime"] + "</td>";
                            tableBodyRow += "<td>" + value["OUTREMARKS"] + "</td>";
                            tableBodyRow += "<td> <br>" + value["worked_hrs1"] + "</td>";
                            tableBodyRow += "<td colspan='7' style='text-align: center;background-color: #d68b0a;color:Black;font-size: 18px;'><br>" + value["Remark"] + "</td>";

                        } else {
                            tableBodyRow += "<td><br>" + value["HrsMin"] + "</td>";
                            if (value["INREMARKS"] == "Early In") {
                                tableBodyRow += "<td style='color: green;'><br>" + value["InTime"] + "<br>" + value["INREMARKS"] + "</td>";
                            } else {
                                tableBodyRow += "<td style='color: red;'><br>" + value["InTime"] + "<br>" + value["INREMARKS"] + "</td>";
                            }
                            tableBodyRow += "<td> <br>" + value["TDate_Out"] + "</td>";
                            if (string.IsNullOrEmpty(value["OutTime"].ToString())) {
                                tableBodyRow += "<td style='background: red'> <br>" + value["OutTime"] + "</td>";
                            } else {
                                tableBodyRow += "<td style='color: red'> <br>" + value["OutTime"] + "</td>";
                            }
                            tableBodyRow += "<td> <br>" + value["OUTREMARKS"] + "</td>";
                            tableBodyRow += "<td> <br>" + value["worked_hrs1"] + "</td>";
                            if (value["INREMARKS1"] == "Early In") {
                                tableBodyRow += "<td style='color: green;'> <br>" + value["InTime1"] + "<br>" + value["INREMARKS1"] + "</td>";
                            } else {
                                tableBodyRow += "<td style='color: red;'> <br>" + value["InTime1"] + "<br>" + value["INREMARKS1"] + "</td>";
                            }
                            tableBodyRow += "<td> <br>" + value["TDate_Out1"] + "</td>";
                            tableBodyRow += "<td  style='color: blue'> <br>" + value["OutTime1"] + "</td>";
                            tableBodyRow += "<td> <br>" + value["OUTREMARKS1"] + "</td>";
                            tableBodyRow += "<td> <br>" + value["worked_hrs2"] + "</td>";
                            tableBodyRow += "<td> <br>" + value["TotalOT"] + "</td>";
                            if (value["Remark"] == "Present") {
                                tableBodyRow += "<td style='color: green'><br>" + value["Remark"] + "</td>";
                            } else {
                                tableBodyRow += "<td style='color: red'><br>" + value["Remark"] + "</td>";
                            }
                        }
                        tableBodyRow += "</tr>";
                    }
                    tableBody.Text = tableBodyRow;
                }
            }
        }

        protected void loadClick(object sender, EventArgs e) {
            string emp;
            if (allEmployee.Checked) {
                emp = "0";
            } else {
                emp = employeeId.Value.ToString();
            }
            Response.Redirect(baseUrl + "quickAttendance?startDate=" + startDate.Value + "&endDate=" + endDate.Value + "&branchId=" + branchId.Value + "&departmentId=" + departmentId.Value + "&employeeId=" + emp);
        }
    }
}