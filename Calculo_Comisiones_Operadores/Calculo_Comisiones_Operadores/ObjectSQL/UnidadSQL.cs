using Calculo_Comisiones_Operadores.Object;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.ObjectSQL
{
    public class UnidadSQL
    {
        public static List<Unidad> SelectUnidadActive(int intUnidadId)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_unidad WHERE scco_status = '1' AND scco_id = '" + intUnidadId + "';";
            List<Unidad> _listUnidad = new List<Unidad>();

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
                        Unidad _objUnidad = new Unidad();
                        _objUnidad.intID = (int)_dtRow["scco_id"];
                        _objUnidad.strName = (string)_dtRow["scco_name"];
                        _objUnidad.strModelo = (string)_dtRow["scco_modelo"];
                        _objUnidad.strPlaca = (string)_dtRow["scco_placa"];
                        _objUnidad.intStatus = (int)_dtRow["scco_status"];
                        _listUnidad.Add(_objUnidad);
                    }

                }
                catch (Exception e)
                {
                    _listUnidad = new List<Unidad>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listUnidad;
        }

        public static DataTable SelectUnidad()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_unidad;";
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

        public static DataTable SelectUnidad(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_unidad WHERE scco_id = '" + intID + "';";
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

        public static Unidad SelectUnidadObject(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_unidad WHERE scco_id = '" + intID + "';";
            Unidad _objUnidad = new Unidad();

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
                        _objUnidad.intID = (int)_dtRow["scco_id"];
                        _objUnidad.strName = (string)_dtRow["scco_name"];
                        _objUnidad.strModelo = (string)_dtRow["scco_modelo"];
                        _objUnidad.strPlaca = (string)_dtRow["scco_placa"];
                        _objUnidad.intStatus = (int)_dtRow["scco_status"]; ;
                    }

                }
                catch (Exception e)
                {
                    _objUnidad = new Unidad();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _objUnidad;
        }

        public static void UpdateUnidad(Unidad objUnidad)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "UPDATE scco_unidad SET ";
            _strQuery += string.Format("scco_name = '{0}', ", objUnidad.strName);
            _strQuery += string.Format("scco_modelo = '{0}', ", objUnidad.strModelo);
            _strQuery += string.Format("scco_placa = '{0}', ", objUnidad.strPlaca);
            _strQuery += string.Format("scco_status = '{0}' ", objUnidad.intStatus);
            _strQuery += string.Format("WHERE scco_id = '{0}';", objUnidad.intID);

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

        public static void DeleteUnidad(string intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "DELETE from scco_unidad WHERE scco_id = '" + intID + "';";

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

        public static void InsertUnidad(Unidad objUnidad)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "INSERT INTO scco_unidad";
            _strQuery += "(scco_name, scco_modelo, scco_placa, scco_status) VALUES ";
            _strQuery += string.Format("('{0}', ", objUnidad.strName);
            _strQuery += string.Format("'{0}', ", objUnidad.strModelo);
            _strQuery += string.Format("'{0}', ", objUnidad.strPlaca);
            _strQuery += string.Format("'{0}');", objUnidad.intStatus);

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

        public static DataTable SelectUnidadSearch(string strText)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_unidad WHERE scco_name LIKE '%" + strText + "%' ";
            _strQuery += "UNION ";
            _strQuery += "SELECT * FROM scco_unidad WHERE scco_modelo LIKE '%" + strText + "%';";

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

        public static List<Unidad> SelectUnidadActive()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_unidad WHERE scco_status = '1';";
            List<Unidad> _listUnidad = new List<Unidad>();

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
                        Unidad _objUnidad = new Unidad();
                        _objUnidad.intID = (int)_dtRow["scco_id"];
                        _objUnidad.strName = (string)_dtRow["scco_name"];
                        _objUnidad.strModelo = (string)_dtRow["scco_modelo"];
                        _objUnidad.strPlaca = (string)_dtRow["scco_placa"];
                        _objUnidad.intStatus = (int)_dtRow["scco_status"];
                        _listUnidad.Add(_objUnidad);
                    }

                }
                catch (Exception e)
                {
                    _listUnidad = new List<Unidad>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listUnidad;
        }
    }
}