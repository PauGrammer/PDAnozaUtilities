using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using System.Data;

namespace PDAnozaUtilities.Database
{
    public class MySQLHandler
    {
        private string _ConnectionString;
        protected string Command { private get; set; }
        protected CommandType CommandType { private get; set; }
        protected List<MySqlParameter> Parameters { private get; set; }
        public ErrorHandler ErrorHandler;

        public MySQLHandler(string connectionString)
        {
            _ConnectionString = connectionString;
            ErrorHandler = new ErrorHandler();
        }

        public MySQLHandler(string connectionString, string command, List<MySqlParameter> parameters)
        {
            _ConnectionString = connectionString;
            Command = command;
            Parameters = parameters;
            ErrorHandler = new ErrorHandler();
        }

        public bool TestConnection()
        {
            bool isSuccessful = true;

            using (var dbConnection = new MySqlConnection(_ConnectionString))
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
            using (var dbConnection = new MySqlConnection(_ConnectionString))
            {
                using (var dbCommand = new MySqlCommand(Command, dbConnection))
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
            using (var dbConnection = new MySqlConnection(_ConnectionString))
            {
                using (var dbCommand = new MySqlCommand(Command, dbConnection))
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
            using (var dbConnection = new MySqlConnection(_ConnectionString))
            {
                using (var dbCommand = new MySqlCommand(Command, dbConnection))
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
            DataTable dataTable = new DataTable();
            using (var dbConnection = new MySqlConnection(_ConnectionString))
            {
                using (var dbCommand = new MySqlCommand(Command, dbConnection))
                {
                    dbCommand.CommandType = CommandType;

                    foreach (var paramer in Parameters)
                        dbCommand.Parameters.Add(paramer);

                    using (var dbAdapter = new MySqlDataAdapter(dbCommand))
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
            using (var dbConnection = new MySqlConnection(_ConnectionString))
            {
                using (var dbCommand = new MySqlCommand(Command, dbConnection))
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

    }
}
