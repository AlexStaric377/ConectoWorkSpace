// Decompiled with JetBrains decompiler
// Type: ConectoWorkSpace.Bus.AdminBus
// Assembly: Conecto® WorkSpace, Version=1.3.3.52, Culture=neutral, PublicKeyToken=null
// MVID: EFC301ED-76A8-4C96-A859-6A0DA31B4530
// Assembly location: D:\Kassa24.com.ua\Conecto® WorkSpace.exe

using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ConectoWorkSpace.Bus
{
  public class AdminBus
  {
    public static int LoadTableBus = 0;
    public static string BdBUS = "";
    public static string ErrMessage = "";
    public static Dictionary<string, string> TableConfig = new Dictionary<string, string>();
    public static string[] ButtonPanel;

    public static void BlockOpisBus()
    {
      //DBConecto.ParamStringServerFB[1] = (object) "SYSDBA";
      //DBConecto.ParamStringServerFB[2] = (object) "alarm";
      //DBConecto.ParamStringServerFB[3] = (object) "TCP/IP";
      //DBConecto.ParamStringServerFB[4] = (object) "127.0.0.1/3056:bus";
      //DBConecto.ParamStringServerFB[5] = (object) (SystemConectoServers.PutchServerData + "bus.fdb");
      //DBConecto.ParamStringServerFB[7] = (object) "3056";
      AdminBus.BdBUS = DBConecto.StringServerFB();
      using (FbConnection fbConnection = new FbConnection(AdminBus.BdBUS))
      {
        try
        {
          fbConnection.Open();
        }
        catch (FbException ex)
        {
          SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
          AdminBus.ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
        }
        try
        {
          fbConnection.Dispose();
          fbConnection.Close();
        }
        catch (FbException ex)
        {
          SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
          AdminBus.ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
        }
      }
    }

    public static void InitConfigBus()
    {
      AdminBus.ButtonPanel = new string[1];
      AdminBus.ButtonPanel[0] = "BusOnOff";
      if (AdminBus.LoadTableBus == 0)
        AdminBus.CheckConfigBus("InitKey");
      foreach (string KeyOb in AdminBus.ButtonPanel)
        AdminBus.InitKeyReestr(KeyOb, "0");
    }

    public static void InitKeyOnOff(string NameCheck)
    {
      AdminBus.InitKeyReestr(NameCheck, "0");
    }

    public static void InitTextOnOff()
    {
      foreach (string KeyOb in AdminBus.ButtonPanel)
        AdminBus.InitKeyReestr(KeyOb, "");
    }

    public static void CheckConfigBus(string intvar)
    {
      if (AdminBus.LoadTableBus != 0)
        return;
      foreach (string key in AdminBus.ButtonPanel)
      {
        int num = 0;
        string cmdText = "select * from CONFIGBUS where CONFIGBUS.NAMEVAR = '" + key + "'";
        FbConnection connection = new FbConnection(DBConecto.StringServerFB().ToString());
        connection.Open();
        FbDataReader fbDataReader = new FbCommand(cmdText, connection).ExecuteReader();
        while (fbDataReader.Read())
          ++num;
        fbDataReader.Close();
        if (num == 0)
        {
          string str = intvar == "InitKey" ? "0" : "";
          new FbCommand("INSERT INTO CONFIGBUS  values ('" + key + "', '" + str + "')", connection).ExecuteScalar();
          AdminBus.TableConfig.Add(key, str);
        }
        connection.Dispose();
        connection.Close();
      }
    }

    public static void InitKeyReestr(string KeyOb, string SetVar)
    {
      FbConnection connection = new FbConnection(DBConecto.StringServerFB().ToString());
      connection.Open();
      int num = 0;
      string str = "";
      FbDataReader fbDataReader = new FbCommand("select * from CONFIGBUS where CONFIGBUS.NAMEVAR = '" + KeyOb + "'", connection).ExecuteReader();
      while (fbDataReader.Read())
      {
        str = fbDataReader[1].ToString();
        ++num;
      }
      fbDataReader.Close();
      if (num == 0)
      {
        new FbCommand("INSERT INTO CONFIGBUS  values ('" + KeyOb + "', '" + SetVar + "')", connection).ExecuteScalar();
        AdminBus.TableConfig.Add(KeyOb, SetVar);
      }
      else if (str == "")
        AdminBus.TableConfig[KeyOb] = SetVar;
      connection.Dispose();
      connection.Close();
    }

    public static void UpdateKeyReestr(string KeyOb, string SetKey)
    {
      FbConnection connection = new FbConnection(DBConecto.StringServerFB().ToString());
      connection.Open();
      new FbCommand("UPDATE CONFIGSOFT SET SETVAR =  '" + SetKey + "'  WHERE CONFIGSOFT.NAMEVAR = '" + KeyOb + "'", connection).ExecuteScalar();
      AdminBus.TableConfig[KeyOb] = SetKey;
    }

    public static int CheckBUSBD()
    {
      int num1 = 1;
      if (!File.Exists(SystemConectoServers.PutchServerData + "bus.fdb") && App.aSystemVirable["UserWindowIdentity"] != "1")
        return num1;
      if (App.aSystemVirable["UserWindowIdentity"] == "1")
      {
        if (!File.Exists(SystemConectoServers.PutchServerData + "bus.fdb"))
        {
          if (SystemConecto.IsFilesPRG((object) new Dictionary<string, string>()
          {
            {
              SystemConectoServers.PutchServerData + "BUS.FDB",
              "bus/fdb/"
            }
          }, -1, "- Проверка файлов во время установки сервера bus.fdb") != "True")
          {
            InstallB52.MessageErorInst("Отсутствует инсталяционный файл B52BackOffice8.zip." + Environment.NewLine + "Выполнение процедуры остановлено ");
            return num1;
          }
          try
          {
            FbConnection.CreateDatabase(AdminBus.BdBUS, 16384, true, false);
          }
          catch (FbException ex)
          {
            SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
            AdminBus.ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
          }
          catch (Exception ex)
          {
            SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
          }
        }
        try
        {
          if (AdminBus.LoadTableBus == 0)
          {
            using (FbConnection connection = new FbConnection(AdminBus.BdBUS))
            {
              try
              {
                connection.Open();
              }
              catch (FbException ex)
              {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
                AdminBus.ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
              }
              catch (Exception ex)
              {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
              }
              try
              {
                FbCommand fbCommand1 = new FbCommand("select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'CONFIGBUS'", connection);
                if ((fbCommand1.ExecuteScalar() == null ? "" : fbCommand1.ExecuteScalar().ToString()) == "")
                {
                  FbCommand fbCommand2 = new FbCommand("CREATE TABLE CONFIGBUS  (NAMEVAR  VARCHAR(40) , SETVAR  VARCHAR(50)) ;", connection);
                  string str = fbCommand2.ExecuteScalar() == null ? "" : fbCommand2.ExecuteScalar().ToString();
                  fbCommand2.Dispose();
                  AdminBus.InitConfigBus();
                }
                else
                {
                  int num2 = 0;
                  FbDataReader fbDataReader = new FbCommand("SELECT * from CONFIGBUS", connection).ExecuteReader();
                  while (fbDataReader.Read())
                  {
                    AppStart.TableReestr.Add(fbDataReader[0].ToString(), fbDataReader[1].ToString());
                    ++num2;
                  }
                  fbDataReader.Close();
                  if (num2 == 0)
                    AdminBus.InitConfigBus();
                }
                AdminBus.CreateProduct();
                num1 = 0;
              }
              catch (FbException ex)
              {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
                AdminBus.ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
              }
              catch (Exception ex)
              {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
              }
              if (connection != null)
              {
                connection.Dispose();
                connection.Close();
              }
            }
          }
        }
        catch (FbException ex)
        {
          SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
        }
        catch (Exception ex)
        {
          SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag) null);
        }
      }
      else
      {
        num1 = 0;
        AdminBus.BlockOpisBus();
        int num2 = 0;
        FbCommand fbCommand = new FbCommand("SELECT * from CONFIGBUS", new FbConnection(AdminBus.BdBUS));
        fbCommand.CommandType = CommandType.Text;
        FbDataReader fbDataReader = fbCommand.ExecuteReader();
        while (fbDataReader.Read())
        {
          AppStart.TableReestr.Add(fbDataReader[1].ToString(), fbDataReader[2].ToString());
          ++num2;
        }
        fbDataReader.Close();
        if (num2 == 0)
          AdminBus.InitConfigBus();
      }
      AdminBus.LoadTableBus = 1;
      SystemConecto.ErorDebag("Проверка наличия БД BUS и ее содержания успешно", 0, 0, (SystemConecto.StruErrorDebag) null);
      return num1;
    }

    public static void CreateProduct()
    {
    }

    public static void CreateCenaTov()
    {
    }

    public static void CreateSedi()
    {
    }

    public static void CreateVidProd()
    {
    }

    public static void CreateGrupProd()
    {
    }

    public static void CreateOrganizacion()
    {
    }

    public static void CreateCompany()
    {
    }

    public static void CreateReestdog()
    {
    }

    public static void CreateDol()
    {
    }

    public static void CreateKpr()
    {
    }

    public static void CreateMolKpr()
    {
    }

    public static void CreatePpo()
    {
    }

    public static void CreateService()
    {
    }

    public static void CreateCenServis()
    {
    }

    public static void CreateVidServis()
    {
    }

    public static void CreateValuta()
    {
    }

    public static void CreateSkidka()
    {
    }

    public static void CreateClient()
    {
    }

    public static void CreateHozop()
    {
    }

    public static void CreatePttov()
    {
    }

    public static void CreateTzTov()
    {
    }

    public static void CreateInventov()
    {
    }

    public static void CreateWttov()
    {
    }

    public static void CreatePtbatch()
    {
    }

    public static void CreateWtbatch()
    {
    }

    public static void CreateCheckTztov()
    {
    }

    public static void CreatePtedi()
    {
    }

    public static void CreateWtedi()
    {
    }

    public static void CreateCalculyac()
    {
    }

    public static void CreateReestrCalc()
    {
    }

    public static void CreateCalcBatch()
    {
    }
  }
}
