using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CaptchaApp.Views.Pages;
using CaptchaApp.Classes;
using CaptchaApp.Models;
using System.Diagnostics;

namespace CaptchaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _skip = false;
        List<Skip> skips = new List<Skip>();
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Действие при загрузке окна
        /// </summary>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new HomePage(skips));
        }
        /// <summary>
        /// Действие пропуск ввода капчи
        /// </summary>
        private void BtnSkipCaptchaClick(object sender, RoutedEventArgs e)
        {
            // Создание состояния скипа капчи
            Skip skip = new Skip
            {
                state = true,
            };
            skips.Add(skip);
            BtnSkipCaptcha.IsEnabled = false;
            
        }

        /// <summary>
        /// Вернуться на предыдущую страницу
        /// </summary>
        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }
        /// <summary>
        /// Content rendered
        /// </summary>
        private void MainFrameContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
                BtnBack.IsEnabled = true;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
                BtnBack.IsEnabled = false;
            }
        }
    }
}
