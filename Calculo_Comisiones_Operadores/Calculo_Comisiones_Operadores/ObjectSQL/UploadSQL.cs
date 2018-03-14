using Calculo_Comisiones_Operadores.Object;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace Calculo_Comisiones_Operadores.ObjectSQL
{
    public class UploadSQL
    {
        public static void InsertFileUpload(Upload objUpload)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "INSERT INTO scco_upload ";
            _strQuery += "(scco_name, scco_type, scco_size, scco_file, scco_date_up, scco_user, scco_ext) ";
            _strQuery += "VALUES (@strName, @strType, @intSize, @bFile, @dateUp, @strUser, @strExt)";

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                try
                {
                    _myConnection.Open();
                    MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);
                    _myCommand.Parameters.Add("@strName", MySqlDbType.String).Value = objUpload.strName;
                    _myCommand.Parameters.Add("@dateUp", MySqlDbType.DateTime).Value = objUpload.dateUp.ToString("yyyy-MM-dd HH:mm:ss");
                    _myCommand.Parameters.Add("@bFile", MySqlDbType.MediumBlob).Value = objUpload.bFile;
                    _myCommand.Parameters.Add("@strExt", MySqlDbType.String).Value = objUpload.strExt;
                    _myCommand.Parameters.Add("@intSize", MySqlDbType.Int32).Value = objUpload.intSize;
                    _myCommand.Parameters.Add("@strUser", MySqlDbType.String).Value = objUpload.strUser;
                    _myCommand.Parameters.Add("@strType", MySqlDbType.String).Value = objUpload.strType;
                    _myCommand.ExecuteNonQuery();
                }
                catch
                {

                }
                finally
                {
                    _myConnection.Close();
                }
            }
        }

        public static DataTable SelectFiles()
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_upload ORDER BY scco_date_up DESC;";
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

        public static void DeleteFile(string intID)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "DELETE from scco_upload WHERE scco_id = '" + intID + "'; ";

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

        public static Upload SelectFile(string strIndex)
        {
            ConfigDataBase _objConfig = new ConfigDataBase();
            string _strConnection = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", _objConfig.server, _objConfig.database, _objConfig.user, _objConfig.pass);
            string _strQuery = "SELECT * FROM scco_upload WHERE scco_id = '" + strIndex + "';";
            Upload _objUpload = new Upload();

            using (MySqlConnection _myConnection = new MySqlConnection(_strConnection))
            {
                MySqlCommand _myCommand = new MySqlCommand(_strQuery, _myConnection);

                try
                {
                    _myConnection.Open();
                    MySqlDataAdapter _myAdapter = new MySqlDataAdapter(_myCommand);
                    //_myAdapter.Fill(_dtTable);
                    DataTable _dtResult = new DataTable();
                    _myAdapter.Fill(_dtResult);

                    if(_dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow _dtRow in _dtResult.Rows)
                        {
                            _objUpload.strName = (string)_dtRow["scco_name"];
                            _objUpload.strExt = (string)_dtRow["scco_ext"];
                            _objUpload.intSize = (int)_dtRow["scco_size"];
                            _objUpload.bFile = (byte[])_dtRow["scco_file"];
                        }
                    }
                }
                catch (Exception e)
                {
                    _objUpload = new Upload();
                }
                finally
                {
                    _myConnection.Close();
                }
            }
            return _objUpload;
        }
    }
}