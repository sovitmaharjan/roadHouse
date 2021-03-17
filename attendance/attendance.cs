using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace attendance {
    public class attendance {
        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString);

        public string baseUrl() {
            return System.Configuration.ConfigurationManager.AppSettings["baseUrl"];
        }

        public string projectName() {
            return System.Configuration.ConfigurationManager.AppSettings["projectName"];
        }

        public DataTable queryFunction(string query) {
            DataTable dataTable = new DataTable();
            try {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                connection.Close();
            } finally {
                connection.Close();
            }
            return dataTable;
        }

        public DataTable getTableData(List<string> field, string table, Dictionary<string, object> condition) {
            string fieldData = "";
            string conditionData = "";
            string query;

            if (field.Count == 1) {
                fieldData = field[0];
            } else {
                for (int i = 0; i < field.Count; i++) {
                    if (fieldData == "") {
                        fieldData = field[i];
                    } else {
                        fieldData += " , " + field[i];
                    }
                }
            }
            if (condition.Count == 0) {
                query = "select " + fieldData + " from " + table;
            } else {
                if (condition.Count == 1) {
                    foreach (KeyValuePair<string, object> value in condition) {
                        conditionData = value.Key + " = '" + value.Value + "' ";
                    }
                } else {
                    foreach (KeyValuePair<string, object> value in condition) {
                        if (conditionData == "") {
                            conditionData = value.Key + " = '" + value.Value + "' ";
                        } else {
                            conditionData += " and " + value.Key + " = '" + value.Value + "' ";
                        }
                    }
                }
                query = "select " + fieldData + " from " + table + " where " + conditionData;
            }
            return queryFunction(query);
        }

        public int insertTableData(string table, Dictionary<string, object> data) {
            string query;
            string column = "";
            string columnValue = "";

            if (data.Count == 1) {
                foreach (KeyValuePair<string, object> value in data) {
                    column = value.Key;
                    columnValue = "@" + value.Key;
                }
            } else if (data.Count > 1) {
                foreach (KeyValuePair<string, object> value in data) {
                    if (column == "") {
                        column = value.Key;
                    } else {
                        column += " , " + value.Key;
                    }
                    if (columnValue == "") {
                        columnValue = "@" + value.Key;
                    } else {
                        columnValue += " , @" + value.Key;
                    }
                }
            }
            query = "INSERT INTO " + table + " ( " + column + " ) VALUES ( " + columnValue + " )";
            SqlCommand command = new SqlCommand(query, connection);
            foreach (KeyValuePair<string, object> value in data) {
                command.Parameters.AddWithValue("@" + value.Key, value.Value);
            }
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i;
        }

        public int updateTableData(string table, Dictionary<string, object> data, Dictionary<string, object> condition) {
            string updateData = "";
            string conditionData = "";
            string query;

            if (data.Count == 1) {
                foreach (KeyValuePair<string, object> value in data) {
                    updateData = value.Key + " = @" + value.Key;
                }
            } else if (data.Count > 1) {
                foreach (KeyValuePair<string, object> value in data) {
                    if(updateData == ""){
                        updateData = value.Key + " = @" + value.Key;
			        } else {
                        updateData += " , " + value.Key + " = @" + value.Key;
			        }
                }
            }

            if(condition.Count == 1){
		        foreach(KeyValuePair<string, object> value in condition){
                    conditionData = value.Key + " = @" + value.Key;
		        }
	        }else if(condition.Count > 1){
		        foreach(KeyValuePair<string, object> value in condition){
			        if(conditionData == ""){
                        conditionData = value.Key + " = @" + value.Key;
			        } else {
                        conditionData += " and " + value.Key + " = @" + value.Key;
			        }
		        }
	        }

            query = "UPDATE " + table + " SET " + updateData + " WHERE " + conditionData;
            SqlCommand command = new SqlCommand(query, connection);
            foreach (KeyValuePair<string, object> value in data) {
                command.Parameters.AddWithValue("@" + value.Key, value.Value);
            }
            foreach (KeyValuePair<string, object> value in condition) {
                command.Parameters.AddWithValue("@" + value.Key, value.Value);
            }
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i;
        }

        

        public void imageSave(Byte[] bytes) {
            int id = Convert.ToInt32(queryFunction("select max(EMP_ID)+1 from tbl_emp_info").Rows[0][0]);
            string sql = "insert into tbl_emp_info (EMP_ID, EMP_PHOTO) values (@EMP_ID, @bytes)";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@EMP_ID", id);
            cmd.Parameters.AddWithValue("@bytes", bytes);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public DataTable quickAttendance(int employeeId, DateTime startDate, DateTime endDate) {
            connection.Close();
            DataTable dtQuickAttendance = new DataTable();
            string sql;
            connection.Open();
            if (employeeId == 0) {
                sql = "SELECT EMP_ID FROM view_emp_info";
                DataTable allEmployee = queryFunction(sql);
                foreach (DataRow value in allEmployee.Rows) {
                    sql = "proc_Attn_Mon_General";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emp_id", value["EMP_ID"]);
                    cmd.Parameters.AddWithValue("@date_from", startDate);
                    cmd.Parameters.AddWithValue("@date_to", endDate);
                    cmd.Parameters.AddWithValue("@Aflag", 0);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtQuickAttendance);
                }
                connection.Close();
            } else {
                Dictionary<string, object> procedureData = new Dictionary<string, object>();
                procedureData.Add("@emp_id", employeeId);
                procedureData.Add("@date_from", startDate);
                procedureData.Add("@date_to", endDate);
                procedureData.Add("@AFlag", 0);
                sql = "SELECT * FROM rpt_attendance_data_byEMP where Emp_Id = " + employeeId;
                dtQuickAttendance = procedureAndQuery("proc_Attn_Mon_General_By_Emp", procedureData, sql);
            }
            return dtQuickAttendance;
        }

        public DataTable procedureAndQuery(string procedureName, Dictionary<string, object> procedureData, string query) {
            DataTable dtResult = new DataTable();
            string sql;
            connection.Open();
            sql = procedureName;
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var value in procedureData) {
                cmd.Parameters.AddWithValue(value.Key, value.Value);
            }
            cmd.ExecuteNonQuery();
            connection.Close();
            dtResult = queryFunction(query);
            return dtResult;
        }

        public DataTable procedure(string procedureName, Dictionary<string, object> procedureData) {
            DataTable dtResult = new DataTable();
            string sql;
            connection.Open();
            sql = procedureName;
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach(var value in procedureData){
                cmd.Parameters.AddWithValue(value.Key, value.Value);
            }
            da.Fill(dtResult);
            connection.Close();
            return dtResult;
        }

        public DataTable shiftIndication(string startDate, string deptId) {
            DataTable dtResult = new DataTable();
            string sql = "SELECT EMP_ID FROM view_emp_info where DEPT_ID = '" + deptId + "'";
            DataTable allEmployee = queryFunction(sql);
            string aFlag = "0";
            connection.Open();
            foreach (DataRow value in allEmployee.Rows) {
                sql = "proc_Attn_Mon_General";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_id", value["EMP_ID"]);
                cmd.Parameters.AddWithValue("@date_from", startDate);
                cmd.Parameters.AddWithValue("@date_to", startDate);
                cmd.Parameters.AddWithValue("@Aflag", aFlag);
                cmd.ExecuteNonQuery();
                aFlag = "1";
            }
            string a = "1";

            sql = "proc_ShiftIndication";
            SqlCommand cmd2 = new SqlCommand(sql, connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@tdate", startDate);
            da.Fill(dtResult);
            connection.Close();
            return dtResult;
        }

    }
}