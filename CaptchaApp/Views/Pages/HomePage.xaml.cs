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
using CaptchaApp.Classes;
using CaptchaApp.Models;
namespace CaptchaApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        CaptchaGeneration captchaGeneration = new CaptchaGeneration();
        List<Skip> _skips = new List<Skip>();
        public HomePage(List<Skip> skips)
        {
            InitializeComponent();
            this._skips = skips;
        }
        /// <summary>
        /// Повторное генерирование капчи
        /// </summary>
        private void RepeatGeneration()
        {
            captchaGeneration.captcha = string.Empty;
            SPanelCaptcha.Children.Clear();
            captchaGeneration.Generation(SPanelCaptcha);
        }

        /// <summary>
        /// Действие кнопки заключается в создании новой капчи
        /// </summary>
        private void BtnRepeatClick(object sender, RoutedEventArgs e)
        {
            captchaGeneration.captcha = string.Empty;
            SPanelCaptcha.Children.Clear();
            captchaGeneration.Generation(SPanelCaptcha);
        }
        
        /// <summary>
        /// Действие кнопки - Проверить капчу
        /// </summary>
        private void BtnCheckoutClick(object sender, RoutedEventArgs e)
        {
            // Проверка статуса пропуска
            try
            {
                var skip = _skips.First();   
                if (skip.state)
                {
                    TBoxCaptchaInput.Text = captchaGeneration.captcha;
                    BtnRepeat.IsEnabled = false;
                    TBoxCaptchaInput.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("The usual login scenario | State: No Skip");
            }
            // Проверка поля ввода captcha на наличие пустой строки
            if (!String.IsNullOrEmpty(TBoxCaptchaInput.Text))
            {
                if (TBoxCaptchaInput.Text != captchaGeneration.captcha)
                {
                    MessageBox.Show("Капча введена неправильно", "Капча", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TBoxCaptchaInput.Text = null;
                    // Повторная генерация captcha
                    RepeatGeneration();
                }
                else
                {
                    MessageBox.Show("Действие было успешно завершено", "Капча", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new SuccessPage());
                }
            }
            else
            {
                MessageBox.Show("Ошибка проверки", "Капча", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Повторная генерация captcha
                RepeatGeneration();
            }

        }

        /// <summary>
        /// Действие при загрузке страницы
        /// </summary>
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // Генерация captcha
            RepeatGeneration();
        }
    }
}
