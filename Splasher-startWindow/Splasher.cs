using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Security.Permissions;
using System.Windows.Threading;
using System.Diagnostics;

namespace ConectoWorkSpace.Splasher_startWindow
{
    /// <summary>
    /// Helper, чтобы показать или закрыть окно заставки
    /// </summary>
    public static class Splasher
    {
        /// <summary>
        /// 
        /// </summary>
        private static Window mSplash;

        /// <summary>
        /// Получить или установить окно заставки
        /// </summary>
        public static Window Splash
        {
            get
            {
                return mSplash;
            }
            set
            {
                mSplash = value;
            }
        }

        /// <summary>
        /// Показать заставку
        /// </summary>
        public static void ShowSplash()
        {
            if ( mSplash != null )
            {
                mSplash.Show ( );
            }
        }
        /// <summary>
        /// Закрыть заставку
        /// </summary>
        public static void CloseSplash()
        {
            if (mSplash != null)
            {
                //mSplash.Close();
                mSplash.Hide();

                if (mSplash is IDisposable)
                    (mSplash as IDisposable).Dispose();
            }
        }
    }

    #region Симуляция Application.DoEvents функции класса
    /// <summary>
    /// Это вспомогательный класс, осуществлять DoEvents метод, как метод System.Windows.Forms.Application "с DoEvents, <para></para>
    /// если нет такой функции, загрузка сообщение всплеска не будет обновляться правильно.Код показан ниже:
    /// </summary>
    // http://www.codeproject.com/Articles/38291/Implement-Splash-Screen-with-WPF
    // http://www.gotdotnet.ru/blogs/sidr/7392/
    public static class DispatcherHelper
    {
        /// <summary>
        /// Симуляция Application.DoEvents функции класса <see cref=" System.Windows.Forms.Application"/>.
        /// 
        /// </summary>
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrames), frame);

            try
            {
                Dispatcher.PushFrame(frame);
            }
            catch (InvalidOperationException)
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private static object ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;

            return null;
        }
    }
    #endregion

    /// <summary>
    /// Message listener, singlton pattern.
    /// Inherit from DependencyObject to implement DataBinding.
    /// Сообщение слушателя, синглтон шаблону.
    /// Наследование от DependencyObject для реализации DataBinding.
    /// </summary>
    public class MessageListener : DependencyObject
    {
        /// <summary>
        /// 
        /// </summary>
        private static MessageListener mInstance;

        /// <summary>
        /// 
        /// </summary>
        private MessageListener()
        {

        }

        /// <summary>
        /// Получить экземпляр сообщения слушателем
        /// </summary>
        public static MessageListener Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new MessageListener();
                return mInstance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ReceiveMessage(string message)
        {
            Message = message;
            Debug.WriteLine(Message);
            DispatcherHelper.DoEvents();
        }

        /// <summary>
        /// Прочитать или записать полученное сообщение
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageListener), new UIPropertyMetadata(null));

    }


        #region Заставка SplashScreenConecto
        //    <Window 
        //    x:Class="ConectoWorkSpace.SplashScreenConecto"
        //    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        //    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        //    xmlns:local="clr-namespace:SplashConecto"    
        //    Title="SplashScreenConecto" Height="236" Width="414" WindowStartupLocation="CenterScreen" WindowStyle="None" 
        //        Background="Orange" BorderBrush="DarkOrange" BorderThickness="3" ShowInTaskbar="False" ResizeMode="NoResize">
        //    <Grid>
        //        <Label Margin="19,22,17,80" Name="label1" FontSize="48" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Foreground="MintCream">
        //            <Label.BitmapEffect>
        //                <OuterGlowBitmapEffect GlowSize="15" />
        //            </Label.BitmapEffect> Splash screen
        //        </Label>
        //        <Label Height="28" Margin="19,0,17,15" Name="label2" VerticalAlignment="Bottom"
        //               Content="{Binding Source={x:Static local:MessageListener.Instance},Path=Message}" Foreground="White"></Label>
        //    </Grid>
        //</Window>

        #endregion


}
