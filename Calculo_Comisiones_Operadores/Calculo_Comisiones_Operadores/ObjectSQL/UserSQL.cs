using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using Calculo_Comisiones_Operadores.Object;
using System.Web.UI;

namespace Calculo_Comisiones_Operadores.ObjectSQL
{
    public class UserSQL
    {
        public static List<User> SelectUserActive(string strUser, string strPassword)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_users WHERE scco_status = '1' AND scco_user = '" + strUser + "' AND scco_password = '" + strPassword + "';";
            List<User> _listUser = new List<User>();

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);
                //_myCommand.Parameters.Add("@scco_user", MySqlDbType.String).Value = strUser;
                //_myCommand.Parameters.Add("@scco_password", MySqlDbType.String).Value = strPassword;

                try
                {
                    _myConnection.Open();
                    MySqlDataAdapter _myAdapter = new MySqlDataAdapter(_myCommand);
                    DataTable _dt = new DataTable();
                    _myAdapter.Fill(_dt);

                    foreach (DataRow _dtRow in _dt.Rows)
                    {
                        User _objUser = new User();
                        _objUser.intID = (int)_dtRow["scco_id"];
                        _objUser.strName = (string)_dtRow["scco_name"];
                        _objUser.strLastName = (string)_dtRow["scco_lastname"];
                        _objUser.strMlastName = (string)_dtRow["scco_mlastname"].ToString();
                        _objUser.strUser = (string)_dtRow["scco_user"];
                        _objUser.strPassword = (string)_dtRow["scco_password"];
                        _objUser.intStatus = (int)_dtRow["scco_status"];
                        _objUser.strTipo = (string)_dtRow["scco_tipo"];
                        _listUser.Add(_objUser);
                    }

                }
                catch (Exception e)
                {
                    _listUser = new List<User>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listUser;
        }

        public static DataTable SelectUser()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_users;";
            DataTable _dtTable = new DataTable();

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);

                try
                {
                    _myConnection.Open();
                    MySqlDataAdapter _myAdapter = new MySqlDataAdapter(_myCommand);
                    _myAdapter.Fill(_dtTable);
                }
                catch (Exception e)
                {
                    _dtTable = new DataTable();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _dtTable;
        }

        public static DataTable SelectUser(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_users WHERE scco_id = '" + intID + "';";
            DataTable _dtTable = new DataTable();

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);

                try
                {
                    _myConnection.Open();
                    MySqlDataAdapter _myAdapter = new MySqlDataAdapter(_myCommand);
                    _myAdapter.Fill(_dtTable);
                }
                catch (Exception e)
                {
                    _dtTable = new DataTable();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _dtTable;
        }

        public static User SelectUserObject(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_users WHERE scco_id = '" + intID + "';";
            User _objUser = new User();

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);

                try
                {
                    _myConnection.Open();
                    MySqlDataAdapter _myAdapter = new MySqlDataAdapter(_myCommand);
                    DataTable _dt = new DataTable();
                    _myAdapter.Fill(_dt);

                    foreach (DataRow _dtRow in _dt.Rows)
                    {
                        _objUser.intID = (int)_dtRow["scco_id"];
                        _objUser.strName = (string)_dtRow["scco_name"];
                        _objUser.strLastName = (string)_dtRow["scco_lastname"];
                        _objUser.strMlastName = (string)_dtRow["scco_mlastname"].ToString();
                        _objUser.strUser = (string)_dtRow["scco_user"];
                        _objUser.strPassword = (string)_dtRow["scco_password"];
                        _objUser.intStatus = (int)_dtRow["scco_status"];
                        _objUser.strTipo = (string)_dtRow["scco_tipo"];
                    }

                }
                catch (Exception e)
                {
                    _objUser = new User();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _objUser;
        }

        public static void UpdateUser(User objUser)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "UPDATE scco_users SET ";
            _strQuery += string.Format("scco_name = '{0}', ", objUser.strName);
            _strQuery += string.Format("scco_lastname = '{0}', ", objUser.strLastName);
            _strQuery += string.Format("scco_mlastname = '{0}', ", objUser.strMlastName);
            _strQuery += string.Format("scco_user = '{0}', ", objUser.strUser);
            _strQuery += string.Format("scco_password = '{0}', ", objUser.strPassword);
            _strQuery += string.Format("scco_tipo = '{0}', ", objUser.strTipo);
            _strQuery += string.Format("scco_status = '{0}' ", objUser.intStatus);
            _strQuery += string.Format("WHERE scco_id = '{0}';", objUser.intID);

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                try
                {
                    _myConnection.Open();
                    MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);
                    _myCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //string script = "alert(\"Error, Es necesario ingresar el apellido paterno del usuario.\");";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                finally
                {
                    _myConnection.Close();
                }
            }
        }

        public static void DeleteUser(string intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "DELETE from scco_users WHERE scco_id = '" + intID + "';";

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                try
                {
                    _myConnection.Open();
                    MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);
                    _myCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
                finally
                {
                    _myConnection.Close();
                }
            }
        }

        public static void InsertUser(User objUser)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "INSERT INTO scco_users";
            _strQuery += "(scco_name, scco_lastname, scco_mlastname, scco_user, scco_password, scco_status, scco_tipo) VALUES ";
            _strQuery += string.Format("('{0}', ", objUser.strName);
            _strQuery += string.Format("'{0}', ", objUser.strLastName);
            _strQuery += string.Format("'{0}', ", objUser.strMlastName);
            _strQuery += string.Format("'{0}', ", objUser.strUser);
            _strQuery += string.Format("'{0}', ", objUser.strPassword);
            _strQuery += string.Format("'{0}', ", objUser.intStatus);
            _strQuery += string.Format("'{0}');", objUser.strTipo);

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                try
                {
                    _myConnection.Open();
                    MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);
                    _myCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
                finally
                {
                    _myConnection.Close();
                }
            }
        }

        public static DataTable SelectUserSearch(string strText)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_users WHERE scco_name LIKE '%" + strText + "%' ";
            _strQuery += "UNION ";
            _strQuery += "SELECT * FROM scco_users WHERE scco_user LIKE '%" + strText + "%';";

            DataTable _dtTable = new DataTable();

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);

                try
                {
                    _myConnection.Open();
                    MySqlDataAdapter _myAdapter = new MySqlDataAdapter(_myCommand);
                    _myAdapter.Fill(_dtTable);
                }
                catch (Exception e)
                {
                    _dtTable = new DataTable();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _dtTable;
        }
    }
}