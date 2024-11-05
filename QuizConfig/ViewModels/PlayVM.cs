using System.Windows;
using System.Windows.Threading;

namespace QuizConfig.ViewModels
{
    internal class PlayVM
    {
        private DispatcherTimer _timer;
        private int _timeLimit;
        private int _elapsedTime;

        public MainVM MainVM { get; set; }
        public QuestionPackVM QuizQestions { get => MainVM.ActivePack; private set { } }
        public QuestionVM CurrentQuestion { get; set; }
        public Visibility ResultVisibility { get; set; } = Visibility.Collapsed;
        public Visibility QuizVisibility { get; set; } = Visibility.Visible;


        public PlayVM(MainVM mainVM)
        {
            this.MainVM = mainVM;
        }
    }
}
