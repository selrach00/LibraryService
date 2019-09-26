using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using LibraryService.Models;
using System.Data;

namespace LibraryService.Database
{
    public static class Repository
    {
        public static List<Patron> SPOverdueBooksFromSunnydale(string connectionString)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                List<Patron> patronList = new List<Patron>();
                using (OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "Retrieve_Overdue_Books_From_Sunnydale"
                })
                {
                    OracleParameter oparam = cmd.Parameters.Add("resultItems", OracleDbType.RefCursor);
                    oparam.Direction = ParameterDirection.Output;
                    conn.Open();
                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Patron patron = new Patron
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            CheckoutDate = reader.GetDateTime(2),
                            DueDate = reader.GetDateTime(3),
                            AreaCode = reader.GetString(4),
                            Phone = reader.GetString(5)
                        };
                        patronList.Add(patron);
                    }

                    return patronList;
                }
            }
        }

        public static void RegisterNewPatron(string connectionString, Patron patron)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text
                })
                {
                    cmd.Parameters.Add("firstName", patron.FirstName);
                    cmd.Parameters.Add("lastName", patron.LastName);
                    cmd.Parameters.Add("areaCode", patron.AreaCode);
                    cmd.Parameters.Add("phone", patron.Phone);
                    cmd.Parameters.Add("street", patron.Street);
                    cmd.Parameters.Add("city", patron.City);
                    cmd.Parameters.Add("state", patron.State);
                    cmd.Parameters.Add("zip", patron.Zip);

                    conn.Open();
                    cmd.CommandText = @"INSERT into PATRON (first_name, last_name, areacode, phone, street, city, state, zip) values (:firstName, :lastName, :areaCode, :phoneNumber, :street, :city, :state, :zip)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
