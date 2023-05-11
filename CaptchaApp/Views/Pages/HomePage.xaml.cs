using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CaptchaGenerationLibrary;

namespace CaptchaApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        CaptchaGeneration generation = new CaptchaGeneration();
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Действие при загрузке страницы
        /// </summary>
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // Генерация captcha
            RepeatGeneration();
            // Блокируем кнопку
            BtnCheckout.IsEnabled = false;
            TBoxCaptchaInput.Text = String.Empty;
        }

        /// <summary>
        /// Действие при обновление рабочей области
        /// </summary>
        private void PageLayoutUpdated(object sender, EventArgs e)
        {
            switch (App.stateCaptcha)
            {
                case true:
                    TBoxCaptchaInput.Text = generation.captcha;
                    break;
                //case false: | Автоматическое решение
                //    TBoxCaptchaInput.Text = captchaGeneration.captcha;
                //    break;
                default:
                    break;
            }
            GC.Collect(1, GCCollectionMode.Forced);
        }

        /// <summary>
        /// Повторное генерирование капчи
        /// </summary>
        private void RepeatGeneration()
        {
            // Очистка значения капчи
            generation.captcha = string.Empty;
            // Очистка области отображения капчи
            SPanelCaptcha.Children.Clear();
            // Создание новой капчи
            generation.Generation(SPanelCaptcha);
        }

        /// <summary>
        /// Действие кнопки - Создание новой капчи
        /// </summary>
        private void BtnRepeatClick(object sender, RoutedEventArgs e)
        {
            // Вызов метода
            RepeatGeneration();
        }

        /// <summary>
        /// Проверка поля ввода captcha на наличие пустой строки
        /// </summary>
        private void TBoxCaptchaInputTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TBoxCaptchaInput.Text))
            {
                BtnCheckout.IsEnabled = true;
            }
            else
            {
                BtnCheckout.IsEnabled = false;

            }
        }

        /// <summary>
        /// Действие кнопки - Проверить капчу
        /// </summary>
        private void BtnCheckoutClick(object sender, RoutedEventArgs e)
        {
            // Обработка исключений
            try
            {
                // Проверка состояния капчи
                switch (App.stateCaptcha)
                {
                    case true:
                        this.NavigationService.Navigate(new SuccessPage());
                        break;
                    case false:
                        // Проверка поля ввода captcha на корректность данных
                        if (TBoxCaptchaInput.Text != generation.captcha)
                        {
                            MessageBox.Show("Капча введена неправильно", "Система проверки капчи", MessageBoxButton.OK, MessageBoxImage.Warning);
                            TBoxCaptchaInput.Text = String.Empty;
                            // Повторная генерация captcha
                            RepeatGeneration();
                        }
                        else
                        {
                            MessageBox.Show("Действие было успешно завершено", "Система проверки капчи", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.NavigationService.Navigate(new SuccessPage());
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Система проверки капчи", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
