using System.Windows;
using QuizConfig.ViewModels;

namespace QuizConfig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double StartupWidth { get; set; }
        public double StartupHeight { get; set; }
        public double StartupTop { get; set; }
        public double StartupLeft { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            StartupWidth = this.Width;
            StartupHeight = this.Height;
            StartupTop = this.Top;
            StartupLeft = this.Left;
        }
    }
}