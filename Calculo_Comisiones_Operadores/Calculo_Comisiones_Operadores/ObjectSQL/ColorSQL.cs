using Calculo_Comisiones_Operadores.Object;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.ObjectSQL
{
    public class ColorSQL
    {
        public static DataTable SelectColor()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_color;";
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

        public static DataTable SelectColorSearch(string strText)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_color WHERE scco_name LIKE '%" + strText + "%';";
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

        public static Color SelectColorObject(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_color WHERE scco_id = '" + intID + "';";
            Color _objColor = new Color();

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
                        _objColor.intID = (int)_dtRow["scco_id"];
                        _objColor.strName = (string)_dtRow["scco_name"];
                        _objColor.timeHora = (TimeSpan)_dtRow["scco_hora"];
                        _objColor.decTarfica = (decimal)_dtRow["scco_tarifa"];
                        _objColor.strPrioridad = (string)_dtRow["scco_prioridad"]; ;
                    }

                }
                catch (Exception e)
                {
                    _objColor = new Color();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _objColor;
        }

        public static DataTable SelectColor(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_color WHERE scco_id = '" + intID + "';";
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

        public static void InsertColor(Color objColor)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "INSERT INTO scco_color";
            _strQuery += "(scco_name, scco_hora, scco_prioridad, scco_tarifa) VALUES ";
            _strQuery += string.Format("('{0}', ", objColor.strName);
            _strQuery += string.Format("'{0}', ", objColor.timeHora);
            _strQuery += string.Format("'{0}', ", objColor.strPrioridad);
            _strQuery += string.Format("'{0}');", objColor.decTarfica);

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

        public static void UpdateColor(Color objColor)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "UPDATE scco_color SET ";
            _strQuery += string.Format("scco_name = '{0}', ", objColor.strName);
            _strQuery += string.Format("scco_prioridad = '{0}', ", objColor.strPrioridad);
            _strQuery += string.Format("scco_hora = '{0}', ", objColor.timeHora);
            _strQuery += string.Format("scco_tarifa = '{0}', ", objColor.decTarfica);
            _strQuery += string.Format("WHERE scco_id = '{0}';", objColor.intID);

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

        public static void DeleteColor(string intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "DELETE from scco_color WHERE scco_id = '" + intID + "';";

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

        public static Color SelectColorTravel(int intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_color WHERE scco_id = (SELECT scco_color_id FROM scco_cliente WHERE scco_id = '" + intID + "');";
            Color _objColor = new Color();

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
                        _objColor.intID = (int)_dtRow["scco_id"];
                        _objColor.strName = (string)_dtRow["scco_name"];
                        _objColor.timeHora = (TimeSpan)_dtRow["scco_hora"];
                        _objColor.decTarfica = (decimal)_dtRow["scco_tarifa"];
                        _objColor.strPrioridad = (string)_dtRow["scco_prioridad"]; ;
                    }

                }
                catch (Exception e)
                {
                    _objColor = new Color();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _objColor;
        }
    }
}