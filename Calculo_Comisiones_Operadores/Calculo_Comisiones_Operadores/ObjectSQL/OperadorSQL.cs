using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using Calculo_Comisiones_Operadores.Object;

namespace Calculo_Comisiones_Operadores.ObjectSQL
{
    public class OperadorSQL
    {
        public static List<Operador> SelectOperadorrActive(int intOperadorId)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_operador WHERE scco_status = '1' AND scco_id = '" + intOperadorId + "';";
            List<Operador> _listOperador = new List<Operador>();

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
                        Operador _objOperador = new Operador();
                        _objOperador.intID = (int)_dtRow["scco_id"];
                        _objOperador.strName = (string)_dtRow["scco_name"];
                        _objOperador.strLastName = (string)_dtRow["scco_lastname"];
                        _objOperador.strMlastName = (string)_dtRow["scco_mlastname"];
                        _objOperador.strSexo = (string)_dtRow["scco_sex"];
                        _objOperador.intAge = (int)_dtRow["scco_age"];
                        _objOperador.dateBirthdate = (DateTime)_dtRow["scco_birthdate"];
                        _objOperador.intStatus = (int)_dtRow["scco_status"];
                        _listOperador.Add(_objOperador);
                    }

                }
                catch (Exception e)
                {
                    _listOperador = new List<Operador>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listOperador;
        }

        public static DataTable SelectOperador()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_operador;";
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

        public static DataTable SelectOperador(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_operador WHERE scco_id = '" + intID + "';";
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

        public static Operador SelectOperadorObject(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_operador WHERE scco_id = '" + intID + "';";
            Operador _objOperador = new Operador();

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
                        _objOperador.intID = (int)_dtRow["scco_id"];
                        _objOperador.strName = (string)_dtRow["scco_name"];
                        _objOperador.strLastName = (string)_dtRow["scco_lastname"];
                        _objOperador.strMlastName = (string)_dtRow["scco_mlastname"];
                        _objOperador.strSexo = (string)_dtRow["scco_sex"];
                        _objOperador.intAge = (int)_dtRow["scco_age"];
                        _objOperador.dateBirthdate = (DateTime)_dtRow["scco_birthdate"];
                        _objOperador.intStatus = (int)_dtRow["scco_status"];
                    }

                }
                catch (Exception e)
                {
                    _objOperador = new Operador();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _objOperador;
        }

        public static void UpdateOperador(Operador objOperador)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "UPDATE scco_operador SET ";
            _strQuery += string.Format("scco_name = '{0}', ", objOperador.strName);
            _strQuery += string.Format("scco_lastname = '{0}', ", objOperador.strLastName);
            _strQuery += string.Format("scco_mlastname = '{0}', ", objOperador.strMlastName);
            _strQuery += string.Format("scco_sex = '{0}', ", objOperador.strSexo);
            _strQuery += string.Format("scco_age = '{0}', ", objOperador.intAge);
            _strQuery += string.Format("scco_birthdate = '{0}', ", objOperador.dateBirthdate.ToString("yyyy-MM-dd"));
            _strQuery += string.Format("scco_status = '{0}' ", objOperador.intStatus);
            _strQuery += string.Format("WHERE scco_id = '{0}';", objOperador.intID);

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

        public static void DeleteOperador(string intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "DELETE from scco_operador WHERE scco_id = '" + intID + "';";

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

        public static void InsertOperador(Operador objOperador)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "INSERT INTO scco_operador";
            _strQuery += "(scco_name, scco_lastname, scco_mlastname, scco_sex, scco_age, scco_birthdate, scco_status) VALUES ";
            _strQuery += string.Format("('{0}', ", objOperador.strName);
            _strQuery += string.Format("'{0}', ", objOperador.strLastName);
            _strQuery += string.Format("'{0}', ", objOperador.strMlastName);
            _strQuery += string.Format("'{0}', ", objOperador.strSexo);
            _strQuery += string.Format("'{0}', ", objOperador.intAge);
            _strQuery += string.Format("'{0}', ", objOperador.dateBirthdate.ToString("yyyy-MM-dd"));            
            _strQuery += string.Format("'{0}');", objOperador.intStatus);

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

        public static DataTable SelectOperadorSearch(string strText)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_operador WHERE scco_name LIKE '%" + strText + "%' ";
            _strQuery += "UNION ";
            _strQuery += "SELECT * FROM scco_operador WHERE scco_lastname LIKE '%" + strText + "%';";

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

        public static List<Operador> SelectOperadorActive()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_operador WHERE scco_status = '1';";
            List<Operador> _listOperador = new List<Operador>();

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
                        Operador _objOperador = new Operador();
                        _objOperador.intID = (int)_dtRow["scco_id"];
                        _objOperador.strName = (string)_dtRow["scco_name"];
                        _objOperador.strLastName = (string)_dtRow["scco_lastname"];
                        _objOperador.strMlastName = (string)_dtRow["scco_mlastname"];
                        _objOperador.strSexo = (string)_dtRow["scco_sex"];
                        _objOperador.intAge = (int)_dtRow["scco_age"];
                        _objOperador.dateBirthdate = (DateTime)_dtRow["scco_birthdate"];
                        _objOperador.intStatus = (int)_dtRow["scco_status"];
                        _listOperador.Add(_objOperador);
                    }

                }
                catch (Exception e)
                {
                    _listOperador = new List<Operador>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listOperador;
        }

    }
}