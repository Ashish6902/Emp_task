using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Emp_task
{
    internal class Program
    {
        static string ConnectionString = "Data Source=LAPTOP-5G43KHGC;Initial Catalog=ConsoleApp;Integrated Security=True";
        static void Main(string[] args)
        {
            int Choice;
            do
            {
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1. Get All The Records:");
                Console.WriteLine("2. Insert Data:");
                Console.WriteLine("3. Find data by id:");
                Console.WriteLine("4. To Delete:");
                Console.WriteLine("5. To Exit:");

                 Choice = Convert.ToInt32(Console.ReadLine());

                switch (Choice)
                {
                    case 1:
                        Represent();
                        break;

                    case 2:
                        InsertData();
                        break;

                    case 3:
                        Search();
                        break;
                    case 4:
                        delete();
                        break;
                    case 5:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Wrong Answer");
                        break;
                }
            } while (Choice != 5);
            
        }


        static void InsertData()
        {
            Console.WriteLine("Enter the no of record you want to add:");
            int record = Convert.ToInt32(Console.ReadLine());

            for(int i =0; i <record;i++)
            {
                emp e1 = new emp();
                string comp = e1.Comp;
                Console.WriteLine("Enter the Phone no.");
                e1.Emp_phone = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter the Emial ID");
                e1.Emp_mail = Console.ReadLine();
                Console.WriteLine("Enter the Location");
                e1.Emp_location = Console.ReadLine();
                Console.WriteLine("Enter the Department");
                e1.Emp_Department = Console.ReadLine();

                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string Query = "INSERT INTO emp(Phone,Email,place,Department,Company) VALUES(@phone,@email,@place,@depart,@Company)";
                    using (SqlCommand cmd = new SqlCommand(Query, conn))
                    {
                        cmd.Parameters.AddWithValue("@phone", e1.Emp_phone);
                        cmd.Parameters.AddWithValue("@email", e1.Emp_mail);
                        cmd.Parameters.AddWithValue("@place", e1.Emp_location);
                        cmd.Parameters.AddWithValue("@depart", e1.Emp_Department);
                        cmd.Parameters.AddWithValue("@Company", comp);


                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }

        static void Represent()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string Query = "SELECT * FROM emp";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        foreach (DataRow row in table.Rows)
                        {
                            foreach (DataColumn column in table.Columns)
                            {
                                Console.Write(column.ColumnName + ":");
                                Console.Write(row[column] + "\n");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        static void Search()
        {
            Console.WriteLine("Enter The Employee ID:");
            int employeeId = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT Email FROM emp WHERE Emp_id = @employeeId ;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string email = reader["Email"].ToString();
                            Console.WriteLine("Email: " + email);
                        }
                        else
                        {
                            Console.WriteLine("Employee not found.");
                        }
                    }
                }
            }
        }

        static void delete()
        {
            Console.WriteLine("Enter The Employee ID:");
            int employeeId = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM emp WHERE Emp_id = @empID;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@empID", employeeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
