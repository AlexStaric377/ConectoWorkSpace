using System;
using System.Windows;
using System.Windows.Input;
using ConectoWorkSpace;

namespace Main_Window
{ 

    public class NotifyIconViewModel
    {
        /// <summary>
        /// Показывает окно, если оно уже открыто.
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        //Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow = new ConectoWorkSpace.Main_Window.PanelWorkSpace();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// Скрывает главное окно. Эта команда активируется только в том случае, если открыто окно.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.MainWindow.Close(),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }


        /// <summary>
        /// Shuts down the application.
        /// Закрывает приложение.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => AppStart.EndWorkPC() }; 
                // - Единая функция заменяет  Application.Current.Shutdown()  и еще куча всего.
            }
        }
    }


    /// <summary>
    /// Simplistic delegate command for the demo.
    /// Упрощенная команда делегата для демонстрации.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
