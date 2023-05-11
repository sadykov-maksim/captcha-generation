using System.Windows;
using System.Windows.Controls;

namespace CaptchaApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for SuccessPage.xaml
    /// </summary>
    public partial class SuccessPage : Page
    {
        public SuccessPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Действие при загрузке страницы
        /// </summary>
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            App.stateCaptcha = true;
            App.numberSolutions += 1;
        }
    }
}
