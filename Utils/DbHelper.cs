using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProjectExam
{
    public static class DbHelper
    {
        private static readonly string ConnStr = "server=localhost;port=3306;database=holding_db;uid=root;pwd=root;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnStr);
        }

        public static DataTable Query(string sql, params MySqlParameter[] pars)
        {
            try
            {
                using (MySqlConnection con = GetConnection())
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    if (pars != null) cmd.Parameters.AddRange(pars);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка чтения БД: " + ex.Message, ex);
            }
        }

        public static void Execute(string sql, params MySqlParameter[] pars)
        {
            try
            {
                using (MySqlConnection con = GetConnection())
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        if (pars != null) cmd.Parameters.AddRange(pars);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка записи БД: " + ex.Message, ex);
            }
        }
    }
}