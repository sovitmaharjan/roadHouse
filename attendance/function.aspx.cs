using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance {
    public partial class function : System.Web.UI.Page {
        static attendance attendanceObject = new attendance();


        [WebMethod]
        public static List<Dictionary<string, object>> getDeptByBranchId(int id) {
            List<string> field = new List<string>();
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT DISTINCT(DEPT_ID), DEPT_NAME FROM view_Emp_Info WHERE BRANCH_ID = '" + id + "'");

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtDepartment.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtDepartment.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getEmpByBranchId(int id) {
            List<string> field = new List<string>();
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT EMP_ID, emp_Fullname FROM view_Emp_Info WHERE BRANCH_ID = '" + id + "'");

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtDepartment.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtDepartment.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getEmpByDeptId(int id) {
            List<string> field = new List<string>();
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT EMP_ID, emp_Fullname, BRANCH_ID FROM view_Emp_Info WHERE DEPT_ID = '" + id + "'");

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtDepartment.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtDepartment.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getBraByDeptId(int id) {
            List<string> field = new List<string>();
            DataTable dtDepartment = attendanceObject.queryFunction("select distinct(BRANCH_ID), BRANCH_NAME from view_Emp_Info where DEPT_ID = '" + id + "'");

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtDepartment.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtDepartment.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getDataByEmpId(int id) {
            List<string> field = new List<string>();
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT EMP_ID, emp_Fullname, BRANCH_ID, DEPT_ID, DEG_ID, DEG_NAME FROM view_Emp_Info WHERE EMP_ID = '" + id + "'");

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtDepartment.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtDepartment.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getAllDepartment() {
            List<string> field = new List<string>();
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT DEPT_ID, DEPT_NAME FROM Tbl_Org_Dept WHERE LEVEL = 1 ORDER BY DEPT_NAME ASC");

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtDepartment.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtDepartment.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }

        [WebMethod]
        public static List<Dictionary<string, object>> getAllEmployee() {
            List<string> field = new List<string>();
            DataTable dtDepartment = attendanceObject.queryFunction("SELECT EMP_ID, emp_Fullname from view_emp_info ORDER BY emp_Fullname ASC");

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dtDepartment.Rows) {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn tableColumn in dtDepartment.Columns) {
                    dic.Add(tableColumn.ColumnName, dataTableRow[tableColumn]);
                }
                data.Add(dic);
            }
            return data;
        }
    }
}