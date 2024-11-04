using System.Windows;

namespace QuizConfig.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for OptionsDialog.xaml
    /// </summary>
    public partial class OptionsDialog : Window
    {
        public OptionsDialog()
        {
            InitializeComponent();
            DataContext = ((MainWindow)Application.Current.MainWindow).DataContext;
        }
    }
}
