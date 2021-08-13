using Common;
using System;
using System.Data.SqlClient;


namespace Service
{
    public  class TestServices
    {

        public  static void TestConnection()
        {
            try
            {
                using (var context = new SqlConnection(Parametro.ConnectionString))
                {
                    context.Open();
                    Console.WriteLine("SQL Conection Successful");
                }
            }
            catch (Exception ex)
            {

              Console.WriteLine($"Sql Server error: {ex.Message}");
            }
        }
    }
}
