using System.Windows;

namespace QuizConfig.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateNewPackDialog.xaml
    /// </summary>
    public partial class CreateNewPackDialog : Window
    {
        public CreateNewPackDialog()
        {
            InitializeComponent();
            DataContext = ((MainWindow)Application.Current.MainWindow).DataContext;
        }
    }
}
