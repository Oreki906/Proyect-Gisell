using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Login.fomrs
{
    public static class ConexionBD
    {
        public static MySqlConnection conexion;

        public static void Conectar()
        {
            try
            {
                if (conexion == null)
                {
                    string strConn = "Server=localhost;Database=ProyectoGisell;Uid=root;Pwd=;";
                    conexion = new MySqlConnection(strConn);
                }

                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar a la BD: " + ex.Message, ex);
            }
        }

        public static void Desconectar()
        {
            try
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            catch { /* ignorar */ }
        }

        // Ejecuta un procedimiento, permitiendo configurar parámetros.
        public static long EjecutarProcedimiento(string nombreSP, Action<MySqlCommand> configurarParametros = null)
        {
            Conectar();
            using (MySqlCommand cmd = new MySqlCommand(nombreSP, conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                configurarParametros?.Invoke(cmd);
                cmd.ExecuteNonQuery();
                try
                {
                    return cmd.LastInsertedId;
                }
                catch
                {
                    return 0;
                }
            }
        }

        // Ejecuta procedimiento con parámetros
        public static long EjecutarProcedimiento(string nombreSP, params (string, object)[] parametros)
        {
            return EjecutarProcedimiento(nombreSP, cmd =>
            {
                foreach (var p in parametros)
                    cmd.Parameters.AddWithValue(p.Item1, p.Item2 ?? DBNull.Value);
            });
        }

        // Ejecuta una consulta que devuelve decimal
        public static decimal EjecutarFuncionDecimal(string sqlQuery, params (string, object)[] parametros)
        {
            Conectar();
            using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conexion))
            {
                foreach (var p in parametros)
                    cmd.Parameters.AddWithValue(p.Item1, p.Item2 ?? DBNull.Value);

                object res = cmd.ExecuteScalar();
                if (res == null || res == DBNull.Value) return 0m;
                return Convert.ToDecimal(res);
            }
        }

        // Ejecuta una consulta que devuelve int 
        public static int EjecutarFuncionInt(string sqlQuery, params (string, object)[] parametros)
        {
            Conectar();
            using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conexion))
            {
                foreach (var p in parametros)
                    cmd.Parameters.AddWithValue(p.Item1, p.Item2 ?? DBNull.Value);

                object res = cmd.ExecuteScalar();
                if (res == null || res == DBNull.Value) return 0;
                return Convert.ToInt32(res);
            }
        }

        // Ejecutar scalar genérico
        // Para obtener un solo valor sin armar MySqlCommand manual cada vez
        public static object EjecutarScalar(string sqlQuery, params (string, object)[] parametros)
        {
            Conectar();
            using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conexion))
            {
                foreach (var p in parametros)
                    cmd.Parameters.AddWithValue(p.Item1, p.Item2 ?? DBNull.Value);

                return cmd.ExecuteScalar();
            }
        }

        // Ejecutar non-query simple
        // No devuelve datos, solo afecta filas
        public static int EjecutarNonQuery(string sql, params (string, object)[] parametros)
        {
            Conectar();
            using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
            {
                foreach (var p in parametros)
                    cmd.Parameters.AddWithValue(p.Item1, p.Item2 ?? DBNull.Value);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
