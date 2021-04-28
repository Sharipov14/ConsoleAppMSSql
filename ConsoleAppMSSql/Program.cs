using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMSSql
    /*CREATE TABLE Orders(
    CustomerID INTEGER     NOT NULL,
    OrderID INTEGER     PRIMARY KEY AUTOINCREMENT
                           UNIQUE
                           NOT NULL,
    OrderDate DATETIME    NOT NULL,
    FilledDate DATETIME,
    Status VARCHAR (1) NOT NULL,
Amount     INT NOT NULL
)*/
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Data Source=DESKTOP-2LFTD5R\\SQLEXPRESS;Initial Catalog=Sales;Integrated Security=True;Pooling=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string sql = @"SELECT * FROM Sales.Orders";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                string columnNameCustomerID = dataReader.GetName(0);
                                string columnNameOrderID = dataReader.GetName(1);
                                string columnNameOrderDate = dataReader.GetName(2);
                                string columnNameFilledDate = dataReader.GetName(3);
                                string columnNameStatus = dataReader.GetName(4);
                                string columnNameAmount = dataReader.GetName(5);

                                Console.WriteLine($"{columnNameCustomerID}\t{columnNameOrderID} \t{columnNameOrderDate}\t\t{columnNameFilledDate}\t\t{columnNameStatus}\t{columnNameAmount}");

                                while (dataReader.Read())
                                {
                                    var CustomerID = dataReader.GetValue(0);
                                    var OrderID = dataReader.GetValue(1);
                                    var OrderDate = dataReader.GetValue(2);
                                    var FilledDate = dataReader.GetValue(3);
                                    var Status = dataReader.GetValue(4);
                                    var Amount = dataReader.GetValue(5);

                                    Console.WriteLine($"{CustomerID}\t\t{OrderID}\t\t{OrderDate}\t{FilledDate}\t{Status}\t{Amount}");
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        // Закрываем соединение.
                        connection.Close();
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
