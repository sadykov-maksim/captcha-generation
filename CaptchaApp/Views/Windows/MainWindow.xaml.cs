using System;
using System.Windows;
using CaptchaApp.Views.Pages;

namespace CaptchaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Действие при загрузке окна
        /// </summary>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new HomePage());
        }
        
        /// <summary>
        /// Действие при обновление рабочей области
        /// </summary>
        private void WindowLayoutUpdated(object sender, EventArgs e)
        {
            // Если состояние captcha равно true, то делаем кнопку «Пропустить» невидимой
            if (App.stateCaptcha)
            {
                BtnSkipCaptcha.Visibility = Visibility.Hidden;
                BtnSkipCaptcha.IsEnabled = false;
            } else
            {
                BtnSkipCaptcha.Visibility = Visibility.Visible;
                BtnSkipCaptcha.IsEnabled = true;
            }
            TBlockNumberSolutions.Text = $"Количество решений: {App.numberSolutions}";

        }
        
        /// <summary>
        /// Действие при нажатии на кнопку «Пропустить»
        /// </summary>
        private void BtnSkipCaptchaClick(object sender, RoutedEventArgs e)
        {
            // Создание состояния пропуска капчи
            App.stateCaptcha = true;
        }

        /// <summary>
        /// Обработка действия - Отображение контента
        /// </summary>
        private void MainFrameContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnPreviousPage.Visibility = Visibility.Visible;
                BtnPreviousPage.IsEnabled = true;
            }
            else
            {
                BtnPreviousPage.Visibility = Visibility.Hidden;
                BtnPreviousPage.IsEnabled = false;
            }
        }

        /// <summary>
        /// Действие кнопки возврат на предыдущую страницу, если такое возможно
        /// </summary>
        private void BtnPreviousPageClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
               
            }
            // Сброс статуса
            App.stateCaptcha = false;
        }
    }
}
