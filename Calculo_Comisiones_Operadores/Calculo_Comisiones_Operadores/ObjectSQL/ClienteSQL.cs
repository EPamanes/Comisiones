using Calculo_Comisiones_Operadores.Object;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.ObjectSQL
{
    public class ClienteSQL
    {
        public static DataTable SelectCliente()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_cliente;";
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

        public static DataTable SelectClienteSearch(string strText)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_cliente WHERE scco_name LIKE '%" + strText + "%';";
            //_strQuery += "UNION ";
            //_strQuery += "SELECT * FROM scco_color WHERE scco_name LIKE '%" + strText + "%';";

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

        public static Cliente SelectClienteObject(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_cliente WHERE scco_id = '" + intID + "';";
            Cliente _objCliente = new Cliente();

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
                        _objCliente.intId = (int)_dtRow["scco_id"];
                        _objCliente.strName = (string)_dtRow["scco_name"];
                        _objCliente.intStatus = (int)_dtRow["scco_status"];
                        _objCliente.intColorId = (int)_dtRow["scco_color_id"];
                        _objCliente.strTipo = (string)_dtRow["scco_tipo"];
                        _objCliente.intRule1 = (int)_dtRow["scco_travel_rule1"];
                        _objCliente.intRule2 = (int)_dtRow["scco_travel_rule2"];
                    }

                }
                catch (Exception e)
                {
                    _objCliente = new Cliente();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _objCliente;
        }

        public static DataTable SelectCliente(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_cliente WHERE scco_id = '" + intID + "';";
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

        public static void InsertCliente(Cliente objCliente)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "INSERT INTO scco_cliente";
            _strQuery += "(scco_name, scco_status, scco_color_id, scco_tipo, scco_travel_rule1, scco_travel_rule2) VALUES ";
            _strQuery += string.Format("('{0}', ", objCliente.strName);
            _strQuery += string.Format("'{0}', ", objCliente.intStatus);
            _strQuery += string.Format("'{0}', ", objCliente.intColorId);
            _strQuery += string.Format("'{0}', ", objCliente.strTipo);
            _strQuery += string.Format("'{0}', ", objCliente.intRule1);
            _strQuery += string.Format("'{0}')", objCliente.intRule2);

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

        public static void UpdateCliente(Cliente objCliente)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "UPDATE scco_cliente SET ";
            _strQuery += string.Format("scco_name = '{0}', ", objCliente.strName);
            _strQuery += string.Format("scco_status = '{0}', ", objCliente.intStatus);
            _strQuery += string.Format("scco_color_id = '{0}', ", objCliente.intColorId);
            _strQuery += string.Format("scco_tipo = '{0}', ", objCliente.strTipo);
            _strQuery += string.Format("scco_travel_rule1 = '{0}', ", objCliente.intRule1);
            _strQuery += string.Format("scco_travel_rule2 = '{0}' ", objCliente.intRule2);
            _strQuery += string.Format("WHERE scco_id = '{0}';", objCliente.intId);

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

        public static void DeleteCliente(string intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "DELETE from scco_cliente WHERE scco_id = '" + intID + "';";

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

        public static List<Cliente> SelectClientes()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_cliente WHERE scco_status = '" + 1 + "';";
            List<Cliente> _listCliente = new List<Cliente>();

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
                        Cliente _objCliente = new Cliente();
                        _objCliente.intId = (int)_dtRow["scco_id"];
                        _objCliente.strName = (string)_dtRow["scco_name"];
                        _objCliente.intStatus = (int)_dtRow["scco_status"];
                        _objCliente.intColorId = (int)_dtRow["scco_color_id"];
                        _objCliente.strTipo = (string)_dtRow["scco_tipo"];
                        _objCliente.intRule1 = (int)_dtRow["scco_travel_rule1"];
                        _objCliente.intRule2 = (int)_dtRow["scco_travel_rule2"];
                        _listCliente.Add(_objCliente);
                    }

                }
                catch (Exception e)
                {
                    _listCliente = new List<Cliente>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listCliente;
        }

        public static List<Cliente> SelectClientesPreferenciales()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_cliente WHERE scco_status = '" + 1 + "' AND scco_tipo = 'Preferencial';";
            List<Cliente> _listCliente = new List<Cliente>();

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
                        Cliente _objCliente = new Cliente();
                        _objCliente.intId = (int)_dtRow["scco_id"];
                        _objCliente.strName = (string)_dtRow["scco_name"];
                        _objCliente.intStatus = (int)_dtRow["scco_status"];
                        _objCliente.intColorId = (int)_dtRow["scco_color_id"];
                        _objCliente.strTipo = (string)_dtRow["scco_tipo"];
                        _objCliente.intRule1 = (int)_dtRow["scco_travel_rule1"];
                        _objCliente.intRule2 = (int)_dtRow["scco_travel_rule2"];
                        _listCliente.Add(_objCliente);
                    }

                }
                catch (Exception e)
                {
                    _listCliente = new List<Cliente>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listCliente;
        }

        public static List<Cliente> SelectClientesTravel(List<TravelCliente> listTravelCliente)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);

            List<Cliente> _listCliente = new List<Cliente>();

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                try
                {
                    foreach (TravelCliente _objTravelCliente in listTravelCliente)
                    {
                        string _strQuery = "SELECT * FROM scco_cliente WHERE scco_id = '" + _objTravelCliente.intClienteId + "';";
                        MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);

                        _myConnection.Open();
                        MySqlDataAdapter _myAdapter = new MySqlDataAdapter(_myCommand);
                        DataTable _dt = new DataTable();
                        _myAdapter.Fill(_dt);

                        foreach (DataRow _dtRow in _dt.Rows)
                        {
                            Cliente _objCliente = new Cliente();
                            _objCliente.intId = (int)_dtRow["scco_id"];
                            _objCliente.strName = (string)_dtRow["scco_name"];
                            _objCliente.intStatus = (int)_dtRow["scco_status"];
                            _objCliente.intColorId = (int)_dtRow["scco_color_id"];
                            _objCliente.strTipo = (string)_dtRow["scco_tipo"];
                            _objCliente.intRule1 = (int)_dtRow["scco_travel_rule1"];
                            _objCliente.intRule2 = (int)_dtRow["scco_travel_rule2"];
                            _listCliente.Add(_objCliente);
                        }

                        _myConnection.Close();
                    }
                }
                catch (Exception e)
                {
                    _listCliente = new List<Cliente>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listCliente;
        }
    }
}