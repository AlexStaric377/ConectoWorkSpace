using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//----------------
//using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ConectoWorkSpace.Administrator;

namespace ConectoWorkSpace.Systems
{
    public static class Interface
    {
        // Функция переключения доступа к текстовым полям открыть / закрыть
        public static void TextBoxTrueFalse(string KeyOb, string NameObJWindow ,  TextBox SetTextBoxOff, int IndexFor, TextBox SetTextBoxOn)
        {

            int isOn = Convert.ToInt32(AppStart.TableReestr[KeyOb].ToString()); // (int)AppStart.rkAppSetingAllUser.GetValue(KeyOb);
            string ForNameOff = SetTextBoxOff.Name;
            string ForNameOn = SetTextBoxOn.Name;
            for (int indPoint = 1; indPoint <= IndexFor; indPoint = indPoint + 1)
            {
                ForNameOff = SetTextBoxOff.Name.Substring(0, SetTextBoxOff.Name.Length-1)+ indPoint.ToString();
                if ((TextBox)LogicalTreeHelper.FindLogicalNode(SystemConecto.ListWindowMain(NameObJWindow), ForNameOff) != null)
                {
                    var TextOff= (TextBox)LogicalTreeHelper.FindLogicalNode(SystemConecto.ListWindowMain(NameObJWindow), ForNameOff);
                    TextOff.IsEnabled = isOn == 2 ? false : true;
                }
                ForNameOn = SetTextBoxOn.Name.Substring(0, SetTextBoxOn.Name.Length - 1) + indPoint.ToString();
                if ((TextBox)LogicalTreeHelper.FindLogicalNode(SystemConecto.ListWindowMain(NameObJWindow), ForNameOn) != null)
                {
                 var TextOn = (TextBox)LogicalTreeHelper.FindLogicalNode(SystemConecto.ListWindowMain(NameObJWindow), ForNameOn);
                 TextOn.IsEnabled = isOn == 2 ? true : false ;
                }

            }
        }

        /// <summary>
        ///  Функция синхронизации взаимозависящих переключателей ON Off. Если первый включен второй выключен. Например IP4 и IP6. 
        /// 
        /// </summary>
        public static void MouseLeftButtonUp(string KeyOb, string KeyOff, ref Image KeyImage, ref Image KeyOffImage)
        {
            string StatusPic = "on_off_1.png";
            string SetKeyValue = "0";
            if (Convert.ToInt32(AppStart.TableReestr[KeyOb].ToString()) == 0) 
            {
                StatusPic = "on_off_4.png";
                SetKeyValue = "3";
            }
            if (Convert.ToInt32(AppStart.TableReestr[KeyOb].ToString()) == 3) return;
            Interface.CurrentStateInst(KeyOff, SetKeyValue, StatusPic, KeyOffImage);
            Interface.ObjektOnOff(KeyOb, ref KeyImage, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }

        /// <summary>
        ///  Функция инициализации значения текстовых констант (адрес прота, псевдоним данных) из реестра Windows
        /// 
        /// </summary>
        public static void InitTextBox(string KeyOb, TextBox SetTextBox)
        {
            SetTextBox.Text = SetTextBox.Text.Length == 0 || SetTextBox.Text.Length < 4 ? "3055" : AppStart.TableReestr[KeyOb].ToString();
        }
 
 
        /// <summary>
        ///  Функция сохранения результатов ввода текстовых констант (адрес прота, псевдоним данных)
        /// 
        /// </summary>
        public static void SaveTextBox(string KeyOb, TextBox SetTextBox, int SaveReg)
        {
            SetTextBox.Text = SetTextBox.Text.Trim();
            //BorderBrush = "#FFFF9E12"
            if (SetTextBox.Text.Length == 0) // || SetTextBox.Text.Length < 4 || SetTextBox.Text.Length < 3)
            {
             //   SetTextBox.Margin = new Thickness(SetTextBox.Margin.Left - 1, SetTextBox.Margin.Top - 1, 0, 0);
                SetTextBox.BorderThickness = new Thickness(2, 2, 2, 2);
                SetTextBox.BorderBrush = Brushes.Orange;
                if (KeyOb == "BackOfAdresPortServer" || KeyOb == "AdresPortServer")
                {
                   SetTextBox.Text = Convert.ToInt32(AppStart.TableReestr[Administrator.AdminPanels.NameObj].ToString()) == 2 ? "3055" : "3056"; //(int)AppStart.rkAppSetingAllUser.GetValue(Administrator.AdminPanels.NameObj)
                   SetTextBox.BorderBrush = Brushes.Gray;

                }
 
            }
            else
            {
                SetTextBox.BorderThickness = new Thickness(1, 1, 1, 1);
                SetTextBox.BorderBrush = Brushes.Gray;//null
            }
            if (SaveReg == 1)
            {
                Administrator.AdminPanels.UpdateKeyReestr(KeyOb, SetTextBox.Text);
            }
            Administrator.AdminPanels.IpAdr = SetTextBox.Text; 
  
        }


            /// <summary>
            ///  Функция завершения процедуры инсталяции
            /// 
            /// </summary>
        public static void EndEvensIstall(bool ReturnValue, ref Image picture)
        {

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + (ReturnValue == true ? "on_off_2.png" : "on_off_1.png"), UriKind.Relative);
            picture.Source = new BitmapImage(uriSource);

        }
    
        /// <summary>
        ///  Функция записи в реестр состояния процесса завершения
        /// 
        /// </summary>

        public static void EndEvensIstallReg(string KeyObjektExit, bool ReturnValue)
        {
            Administrator.AdminPanels.UpdateKeyReestr(KeyObjektExit, ReturnValue == true ? "2" : "0");
        }

        /// <summary>
        ///  Процедура записи в реестр и изменения состояния переключателя отбражающая ход инсталяции
        /// 
        /// </summary>

        public static void CurrentStateInst(string KeyOb, string SetKeyValue, string StatusPic, Image pict)
        {
            
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + StatusPic, UriKind.Relative);
            pict.Source = new BitmapImage(uriSource);
            Administrator.AdminPanels.UpdateKeyReestr(KeyOb, SetKeyValue);
    
        }

        public static void CurrentState(string KeyOb, string SetKeyValue, string StatusPic, Image pict)
        {

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + StatusPic, UriKind.Relative);
            pict.Source = new BitmapImage(uriSource);

        }

        /// <summary>
        ///  Общая процедура инсталяции и деинсталяции  приложений и
        ///  отображения состояния переключателей установки в ходе выполнения процедуры.
        /// </summary>

        //  isOn -  хранит значение текущего состояния процесса, которое записывается в реестр:
        //   0- переключатель в выключеном состоянии,
        //   1 - выполняется процесс установки,
        //   2 - установка завершена успешно,
        //   3 - установка процесса заблокирована.
        //  KeyOb - хранит имя ключа в реестра состояние которого записывается со значением isOn
        //  pict - имя Image переключателя который изменяет своё текущее состояние на екране монитора в процессе выполнения процедуры.
        //  ConectoWorkSpace.Install.Metods Metods - Имя функции которую необходимо выполнить для установки указанного приложения

        public static void ObjektOnOff(string KeyOb, ref Image pict, ConectoWorkSpace.Install.Metods Metods)
        {

            int isOn = 0;
            string StatusPic = "";
            string SetKeyValue = "";
            isOn = Convert.ToInt32(AppStart.TableReestr[KeyOb].ToString()); 
            switch (isOn)
            {
                case 0:
                    // Идет инсталляция объекта (включили)
                    StatusPic = "on_off_3.png";
                    SetKeyValue = "1";
                    
                    var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_3.png" , UriKind.Relative);
                    pict.Source = new BitmapImage(uriSource);
                    Administrator.AdminPanels.UpdateKeyReestr(KeyOb, SetKeyValue);
                    //Metods[0] Metods.Install
                    // Поиск класса

                    Type SherchClass = Type.GetType(Metods.NameClass);
                    // SherchClass.GetType() то же самое, что и typeof(ConectoWorkSpace.KaraokeServer), однако если Type это уже SherchClass

                    if (SherchClass != null)
                    {
                        System.Reflection.MethodInfo loadAppEvents = SherchClass.GetMethod(Metods.Install, new Type[] { });
                        if (loadAppEvents != null)
                        {
                            // Включить индикатор и запустить  процесс
                            Administrator.AdminPanels.SetProcesRun = 1;
                            //SystemConecto.ErorDebag("LoadAppEvents_" , 2);
                           loadAppEvents.Invoke(new object(), new object[] { });

                        }
 
                    }
                    else
                    {
                        // Нет класса и не надо устанавливать приложение, только включить переключатель.
                        Interface.EndEvensIstallReg(KeyOb, true);
                        Interface.EndEvensIstall(true, ref pict);
                    }
                    break;
                case 1:
                    // Текущий процесс не выполняется?
                    //  Процесс инсталяции не завершен необходимо выключить и деинсталировать
                    // Поиск класса
                        Type SherchClassErr = Type.GetType(Metods.NameClass);
                        // SherchClass.GetType() то же самое, что и typeof(ConectoWorkSpace.KaraokeServer), однако если Type это уже SherchClass

                        if (SherchClassErr != null)
                        {
                            //System.Reflection.MethodInfo loadAppEvents = typeof(ConectoWorkSpace.KaraokeServer).GetMethod(NameMetod, new Type[] { }); // typeof(void)typeof(string), typeof(Int32)
                            System.Reflection.MethodInfo loadAppEvents = SherchClassErr.GetMethod(Metods.UnInstal, new Type[] { });

                            if (loadAppEvents != null)
                            {
                                //SystemConecto.ErorDebag("LoadAppEvents_" , 2);
                                loadAppEvents.Invoke(new object(), new object[] { });
                            }

                        }
                        Interface.EndEvensIstallReg(KeyOb, false);
                        Interface.EndEvensIstall(false, ref pict);
                    break;
                case 2:
                    // Объект инсталирован необходимо деинсталировать и выключить переключатель
                    // Поиск класса
                    Type SherchClassUn = Type.GetType(Metods.NameClass);
                    // SherchClass.GetType() то же самое, что и typeof(ConectoWorkSpace.KaraokeServer), однако если Type это уже SherchClass

                    if (SherchClassUn != null)
                    {
                        System.Reflection.MethodInfo loadAppEvents = SherchClassUn.GetMethod(Metods.UnInstal, new Type[] { });
                        if (loadAppEvents != null)
                        {
                            //SystemConecto.ErorDebag("LoadAppEvents_" , 2);
                            loadAppEvents.Invoke(new object(), new object[] { });
                        }

                    }
                    else
                    {
                        Interface.EndEvensIstallReg(KeyOb, false);
                        Interface.EndEvensIstall(false, ref pict);

                    }
                    break;
                 case 3:
                    //   3 - установка процесса заблокирована.
                    break;
                case 4:
                    // Поиск класса
                    Type SherchClass4 = Type.GetType(Metods.NameClass);
                    // SherchClass.GetType() то же самое, что и typeof(ConectoWorkSpace.KaraokeServer), однако если Type это уже SherchClass

                    if (SherchClass4 != null)
                    {
                        System.Reflection.MethodInfo loadAppEvents = SherchClass4.GetMethod(Metods.Install, new Type[] { });
                        if (loadAppEvents != null)
                        {
                            // Включить индикатор и запустить  процесс
                            Administrator.AdminPanels.SetProcesRun = 1;
                            //SystemConecto.ErorDebag("LoadAppEvents_" , 2);
                            loadAppEvents.Invoke(new object(), new object[] { });

                        }

                    }
                    else
                    {
                        // Нет класса и не надо устанавливать приложение, только включить переключатель.
                        Interface.EndEvensIstallReg(KeyOb, true);
                        Interface.EndEvensIstall(true, ref pict);
                    }
                    break;
            }
        }
    }
  
}
