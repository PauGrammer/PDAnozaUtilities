using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Data;

namespace PDAnozaUtilities.Database
{
    public class MSSQLHandler
    {
        private string _ConnectionString;
        protected string Command { private get; set; }
        protected CommandType CommandType { private get; set; }
        protected List<SqlParameter> Parameters { get; private set; }
        protected static SecurityHandler SecurityHandler;
        public ErrorHandler ErrorHandler;

        public MSSQLHandler(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public bool TestConnection()
        {
            bool isSuccessful = true;

            using (var dbConnection = new SqlConnection(_ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                }
                catch (Exception err)
                {
                    isSuccessful = false;
                    ErrorHandler.SetErrorDetails(err.Source, err.Message, err.StackTrace);
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return isSuccessful;
        }

        public string GetString()
        {
            string result = "";
            using (var dbConnection = new SqlConnection(_ConnectionString))
            {
                using (var dbCommand = new SqlCommand(Command, dbConnection))
                {
                    dbCommand.CommandType = CommandType;
                    foreach (var paramer in Parameters)
                        dbCommand.Parameters.Add(paramer);

                    try
                    {
                        dbConnection.Open();
                        result = dbCommand.ExecuteScalar().ToString();
                    }
                    catch (Exception err)
                    {
                        ErrorHandler.SetErrorDetails(err.Source, err.Message, err.StackTrace);
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }
            
            return result;
        }

        public int GetInt()
        {
            int result = 0;
            using (var dbConnection = new SqlConnection(_ConnectionString))
            {
                using (var dbCommand = new SqlCommand(Command, dbConnection))
                {
                    dbCommand.CommandType = CommandType;
                    foreach (var paramer in Parameters)
                        dbCommand.Parameters.Add(paramer);

                    try
                    {
                        dbConnection.Open();
                        result = Convert.ToInt32(dbCommand.ExecuteScalar());
                    }
                    catch (Exception err)
                    {
                        ErrorHandler.SetErrorDetails(err.Source, err.Message, err.StackTrace);
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }
            return result;
        }

        public bool GetBool()
        {
            bool result = false;
            using (var dbConnection = new SqlConnection(_ConnectionString))
            {
                using (var dbCommand = new SqlCommand(Command, dbConnection))
                {
                    dbCommand.CommandType = CommandType;
                    foreach (var paramer in Parameters)
                        dbCommand.Parameters.Add(paramer);

                    try
                    {
                        dbConnection.Open();
                        result = Convert.ToBoolean(dbCommand.ExecuteScalar());
                    }
                    catch (Exception err)
                    {
                        ErrorHandler.SetErrorDetails(err.Source, err.Message, err.StackTrace);
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }
            return result;
        }

        public DataTable GetDataTable()
        {
            var dataTable = new DataTable();
            using (var dbConnection = new SqlConnection(_ConnectionString))
            {
                using (var dbCommand = new SqlCommand(Command, dbConnection))
                {
                    dbCommand.CommandType = CommandType;

                    foreach (var paramer in Parameters)
                        dbCommand.Parameters.Add(paramer);

                    using (var dbAdapter = new SqlDataAdapter(dbCommand))
                    {
                        try
                        {
                            dbAdapter.Fill(dataTable);
                        }
                        catch (Exception err)
                        {
                            ErrorHandler.SetErrorDetails(err.Source, err.Message, err.StackTrace);
                        }
                    }
                }
            }
            return dataTable;
        }

        public void ExecuteCommand()
        {
            using (var dbConnection = new SqlConnection(_ConnectionString))
            {
                using (var dbCommand = new SqlCommand(Command, dbConnection))
                {
                    dbCommand.CommandType = CommandType;
                    foreach (var paramer in Parameters)
                        dbCommand.Parameters.Add(paramer);

                    try
                    {
                        dbConnection.Open();
                        dbCommand.ExecuteNonQuery();
                    }
                    catch (Exception err)
                    {
                        ErrorHandler.SetErrorDetails(err.Source, err.Message, err.StackTrace);
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }
        }

        protected void SetProperties()
        {
            Command = null;
            CommandType = CommandType.StoredProcedure;
            Parameters = new List<SqlParameter>();
            SecurityHandler = new SecurityHandler();
            ErrorHandler = new ErrorHandler();
        }
            

    }
}
