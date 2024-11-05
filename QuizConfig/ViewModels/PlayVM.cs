using System.Windows;
using System.Windows.Threading;

namespace QuizConfig.ViewModels
{
    internal class PlayVM : BaseVM
    {
        private DispatcherTimer _timer;
        private int _timeLimit;
        private int _elapsedTime;
        private int timerProgress = 100;

        public MainVM MainVM { get; set; }
        public QuestionPackVM QuizQestions { get => MainVM.ActivePack; private set { } }
        public QuestionVM CurrentQuestion { get; set; }
        public Visibility ResultVisibility { get; set; } = Visibility.Collapsed;
        public Visibility QuizVisibility { get; set; } = Visibility.Visible;
        public int TimerProgress { get => timerProgress; set { timerProgress = value; OnPropertyChanged(); } }

        public PlayVM(MainVM mainVM)
        {
            this.MainVM = mainVM;
            InitializeTimer(30);
        }




        private void InitializeTimer(int totalSeconds)
        {
            _timeLimit = totalSeconds;
            _elapsedTime = 0;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            _elapsedTime++;

            double normalizedTime = ((double)_elapsedTime / _timeLimit * 100);

            if (_elapsedTime <= _timeLimit)
            {
                TimerProgress = 100 - (int)normalizedTime;
            }
            else
            {
                _timer.Stop();
            }
        }
    }
}
