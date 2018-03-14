using Calculo_Comisiones_Operadores.Object;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.ObjectSQL
{
    public class TravelSQL
    {
        public static void InsertTravel(Travel objTravel, List<TravelCliente> listTravelCliente)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "INSERT INTO scco_travel";
            _strQuery += "(scco_kilometraje_out, scco_control, scco_date_out, scco_unidad_id, scco_operador_id, scco_tipo, scco_user, scco_date_up) VALUES ";
            _strQuery += string.Format("('{0}', ", objTravel.intKilometrajeOut);
            _strQuery += string.Format("'{0}', ", objTravel.intControl);
            _strQuery += string.Format("'{0}', ", objTravel.dateOut.ToString("yyyy-MM-dd HH:mm:ss"));
            _strQuery += string.Format("'{0}', ", objTravel.intUnidadId);
            _strQuery += string.Format("'{0}', ", objTravel.intOperadorId);
            _strQuery += string.Format("'{0}', ", objTravel.intTipo);
            _strQuery += string.Format("'{0}', ", objTravel.strUser);
            _strQuery += string.Format("'{0}'); ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            _strQuery += "SELECT LAST_INSERT_ID() as zk_id;";

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                try
                {
                    _myConnection.Open();
                    MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);
                    //_myCommand.ExecuteNonQuery();
                    int _intTravelId = -1;
                    object _objReturn = null;

                    MySqlDataAdapter _myAdapter = new MySqlDataAdapter(_myCommand);
                    DataTable _dtResult = new DataTable();
                    _myAdapter.Fill(_dtResult);

                    if (_dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow _dtRow in _dtResult.Rows)
                        {
                            _objReturn = _dtRow["zk_id"];
                        }
                    }

                    if (_objReturn != null)
                        _intTravelId = Convert.ToInt32(_objReturn.ToString());

                    if (_intTravelId > 0)
                        InsertTravelCliente(listTravelCliente, _intTravelId);
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

        private static void InsertTravelCliente(List<TravelCliente> listTravelCliente, int intTravelId)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);

            foreach (TravelCliente _objTravelCliente in listTravelCliente)
            {
                string _strQuery = "INSERT INTO scco_travel_cliente";
                _strQuery += "(scco_travel_id, scco_cliente_id, scco_date_up, scco_factura, scco_user) VALUES ";
                _strQuery += string.Format("('{0}', ", intTravelId);
                _strQuery += string.Format("'{0}', ", _objTravelCliente.intClienteId);
                _strQuery += string.Format("'{0}', ", _objTravelCliente.dateUp.ToString("yyyy-MM-dd HH:mm:ss"));
                _strQuery += string.Format("'{0}', ", _objTravelCliente.strFactura);
                _strQuery += string.Format("'{0}'); ", _objTravelCliente.strUser);

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
        }

        public static DataTable SelectTravels()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_travel;";
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

        public static void DeleteTravel(string intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "DELETE from scco_travel WHERE scco_id = '" + intID + "'; ";
            _strQuery += "DELETE from scco_travel_cliente WHERE scco_travel_id = '" + intID + "';";

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

        public static List<Travel> SelectTravelOperador(string strFlag)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_travel WHERE scco_control = '" + 0 + "';";

            List<Travel> _listTravel = new List<Travel>();

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
                        Travel _objTravel = new Travel();
                        if (strFlag == "operador")
                            _objTravel.intOperadorId = (int)_dtRow["scco_operador_id"];
                        else
                            _objTravel.intUnidadId = (int)_dtRow["scco_unidad_id"];

                        _listTravel.Add(_objTravel);
                    }

                }
                catch (Exception e)
                {
                    _listTravel = new List<Travel>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listTravel;
        }

        public static List<TravelCliente> SelectTravelCliente(int intTravelId)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_travel_cliente WHERE scco_travel_id = '" + intTravelId + "';";

            List<TravelCliente> _listTravel = new List<TravelCliente>();

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
                        TravelCliente _objTravel = new TravelCliente();
                        _objTravel.intId = (int)_dtRow["scco_id"];
                        _objTravel.intClienteId = (int)_dtRow["scco_cliente_id"];
                        _objTravel.intTravelId = (int)_dtRow["scco_travel_id"];
                        _objTravel.strFactura = (string)_dtRow["scco_factura"];
                        _listTravel.Add(_objTravel);
                    }

                }
                catch (Exception e)
                {
                    _listTravel = new List<TravelCliente>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listTravel;
        }

        public static Travel SelectTravelObject(int intId)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_travel WHERE scco_id = '" + intId + "';";

            Travel _objTravel = new Travel();

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
                        _objTravel.intId = (int)_dtRow["scco_id"];
                        _objTravel.intOperadorId = (int)_dtRow["scco_operador_id"];
                        _objTravel.intUnidadId = (int)_dtRow["scco_unidad_id"];
                        _objTravel.intKilometrajeOut = (int)_dtRow["scco_kilometraje_out"];
                        _objTravel.dateOut = Convert.ToDateTime(_dtRow["scco_date_out"].ToString());
                        try
                        {
                            _objTravel.dateIn = Convert.ToDateTime(_dtRow["scco_date_in"].ToString());
                        }
                        catch
                        {
                            _objTravel.dateIn = DateTime.Now;
                        }
                        try
                        {
                            _objTravel.intKilometrajeIn = (int)_dtRow["scco_kilometraje_in"];
                        }
                        catch
                        {
                            _objTravel.intKilometrajeIn = 0;
                        }
                        _objTravel.intControl = (int)_dtRow["scco_control"];
                        _objTravel.intTipo = (string)_dtRow["scco_tipo"];
                    }

                }
                catch (Exception e)
                {
                    _objTravel = new Travel();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _objTravel;
        }

        public static void UpdateTravel(Travel objTravel, List<TravelCliente> listTravelCliente)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "UPDATE scco_travel SET ";
            //_strQuery += "(scco_kilometraje_out, scco_control, scco_date_out, scco_unidad_id, scco_operador_id, scco_tipo) VALUES ";
            _strQuery += string.Format("scco_kilometraje_out = '{0}', ", objTravel.intKilometrajeOut);
            _strQuery += string.Format("scco_kilometraje_in = '{0}', ", objTravel.intKilometrajeIn);
            _strQuery += string.Format("scco_date_out = '{0}', ", objTravel.dateOut.ToString("yyyy-MM-dd HH:mm:ss"));
            _strQuery += string.Format("scco_date_in = '{0}', ", objTravel.dateIn.ToString("yyyy-MM-dd HH:mm:ss"));
            _strQuery += string.Format("scco_date_mod = '{0}', ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            _strQuery += string.Format("scco_user = '{0}', ", objTravel.strUser);
            _strQuery += string.Format("scco_control = '{0}', ", objTravel.intControl);
            _strQuery += string.Format("scco_unidad_id = '{0}', ", objTravel.intUnidadId);
            _strQuery += string.Format("scco_operador_id = '{0}', ", objTravel.intOperadorId);
            _strQuery += string.Format("scco_tipo = '{0}' ", objTravel.intTipo);
            _strQuery += "WHERE ";
            _strQuery += string.Format("scco_id = '{0}'; ", objTravel.intId);
            //_strQuery += "SELECT LAST_INSERT_ID() as zk_id;";

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                try
                {
                    _myConnection.Open();
                    MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);
                    _myCommand.ExecuteNonQuery();

                    UpdateTravelCliente(listTravelCliente, objTravel.intId);
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

        private static void UpdateTravelCliente(List<TravelCliente> listTravelCliente, int intTravelId)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);

            foreach (TravelCliente _objTravelCliente in listTravelCliente)
            {
                string _strQuery = "UPDATE scco_travel_cliente SET ";
                //_strQuery += "(scco_travel_id, scco_cliente_id, scco_date_up, scco_factura) VALUES ";
                _strQuery += string.Format("scco_cliente_id ='{0}', ", _objTravelCliente.intClienteId);
                _strQuery += string.Format("scco_factura = '{0}', ", _objTravelCliente.strFactura);
                _strQuery += string.Format("scco_date_mod = '{0}', ", _objTravelCliente.dateMod.ToString("yyyy-MM-dd HH:mm:ss"));
                _strQuery += string.Format("scco_user = '{0}' ", _objTravelCliente.strUser);
                _strQuery += "WHERE ";
                _strQuery += string.Format("scco_id = '{0}' ", _objTravelCliente.intId);
                _strQuery += "AND ";
                _strQuery += string.Format("scco_travel_id = '{0}';", intTravelId);

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
        }

        public static DataTable SelectTravelSearch(string strTipo, int intStatus)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            //string _strTipo = string.Empty;
            string _strQuery = string.Empty;

            if (strTipo == "----Tipo Viaje-----" && intStatus == 0)
                _strQuery = "SELECT * FROM scco_travel;";
            if (strTipo == "----Tipo Viaje-----" && intStatus != 0)
                _strQuery = "SELECT * FROM scco_travel WHERE scco_control = '" + intStatus + "';";
            if (strTipo != "----Tipo Viaje-----" && intStatus == 0)
                _strQuery = "SELECT * FROM scco_travel WHERE scco_tipo = '" + strTipo + "';";
            if (strTipo != "----Tipo Viaje-----" && intStatus != 0)
                _strQuery = "SELECT * FROM scco_travel WHERE scco_tipo = '" + strTipo + "' AND scco_control = '" + intStatus + "';";

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

        public static List<Travel> SelectTravelTotalOperador(DateTime dateStart, DateTime dateEnd)
        {
            //Total de Operadores dentro del rango solicitado de busqueda
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_travel WHERE scco_control = '" + 1 + "' AND DATE(scco_date_out) BETWEEN DATE('" + dateStart.ToString("yyyy-MM-dd") + "') AND DATE('" + dateEnd.ToString("yyyy-MM-dd") + "') GROUP BY scco_operador_id order by scco_operador_id ASC, scco_date_out ASC;";

            List<Travel> _listTravel = new List<Travel>();

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
                        Travel _objTravel = new Travel();
                        _objTravel.intId = (int)_dtRow["scco_id"];
                        _objTravel.intKilometrajeOut = (int)_dtRow["scco_kilometraje_out"];
                        _objTravel.intKilometrajeIn = (int)_dtRow["scco_kilometraje_in"];
                        _objTravel.dateOut = (DateTime)_dtRow["scco_date_out"];
                        _objTravel.dateIn = (DateTime)_dtRow["scco_date_in"];
                        _objTravel.intUnidadId = (int)_dtRow["scco_unidad_id"];
                        _objTravel.intOperadorId = (int)_dtRow["scco_operador_id"];
                        _listTravel.Add(_objTravel);
                    }

                }
                catch (Exception e)
                {
                    _listTravel = new List<Travel>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listTravel;
        }

        public static List<Travel> SelectTravelTotalTravel(DateTime dateStart, DateTime dateEnd)
        {
            //Total de Operadores dentro del rango solicitado de busqueda
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_travel WHERE scco_control = '" + 1 + "' AND DATE(scco_date_out) BETWEEN DATE('" + dateStart.ToString("yyyy-MM-dd") + "') AND DATE('" + dateEnd.ToString("yyyy-MM-dd") + "') ORDER BY scco_operador_id ASC, scco_date_out ASC;";

            List<Travel> _listTravel = new List<Travel>();

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
                        Travel _objTravel = new Travel();
                        _objTravel.intId = (int)_dtRow["scco_id"];
                        _objTravel.intKilometrajeOut = (int)_dtRow["scco_kilometraje_out"];
                        _objTravel.intKilometrajeIn = (int)_dtRow["scco_kilometraje_in"];
                        _objTravel.dateOut = (DateTime)_dtRow["scco_date_out"];
                        _objTravel.dateIn = (DateTime)_dtRow["scco_date_in"];
                        _objTravel.intUnidadId = (int)_dtRow["scco_unidad_id"];
                        _objTravel.intOperadorId = (int)_dtRow["scco_operador_id"];
                        _objTravel.intTipo = (string)_dtRow["scco_tipo"];
                        _listTravel.Add(_objTravel);
                    }

                }
                catch (Exception e)
                {
                    _listTravel = new List<Travel>();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _listTravel;
        }
    }
}