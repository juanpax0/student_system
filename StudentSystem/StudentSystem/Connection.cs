using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentSystem
{
    public class Connection
    {
        private const string SERVER = "127.0.0.1";
        private const string DATABASE = "student_system";
        private const string UID = "root";
        private const string PASSWORD = "root";
        private static MySqlConnection dbConn;

        public Connection()
        {
            InitializeDB();
        }

        public static void InitializeDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.UserID = UID;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;

            string connString = builder.ToString();

            builder = null;

            Console.WriteLine(connString);

            dbConn = new MySqlConnection(connString);

            Application.ApplicationExit += (sender, args) =>
            {
                if (dbConn != null)
                {
                    dbConn.Dispose();
                    dbConn = null;
                }
            };
        }

        public List<Student> GetStudents()
        {
            List<Student> users = new List<Student>();
            string query = "call get_students";
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            // Esto es para cuando el query tiene retorno
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string ced = reader["cedula"].ToString();
                string nom = reader["nombre"].ToString();
                int eda = (int)reader["edad"];

                Student u = new Student(ced, nom, eda);
                users.Add(u);
            }

            reader.Close();
            dbConn.Close();

            return users;
        }

        public void InsertStudent(string cedula, string nombre, int edad)
        {
            string query = string.Format("call insert_student ('{0}', '{1}', {2})", cedula, nombre, edad);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();
            cmd.ExecuteNonQuery();
            dbConn.Close();
        }

        public void DeleteStudent(string cedula)
        {
            string query = string.Format("call delete_student ('{0}')", cedula);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();
            cmd.ExecuteNonQuery();
            dbConn.Close();
        }

        public void UpdateStudent(string cedula, string nombre, int edad)
        {
            string query = string.Format("call update_student ('{0}', '{1}', {2})", cedula, nombre, edad);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();
            cmd.ExecuteNonQuery();
            dbConn.Close();
        }
    }
}
