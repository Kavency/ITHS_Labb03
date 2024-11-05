using System.Windows;
using System.Windows.Threading;

namespace QuizConfig.ViewModels
{
    internal class PlayVM : BaseVM
    {
        private DispatcherTimer _timer;
        private int _timeLimit;
        private int _elapsedTime;
        private int _timerProgress = 100;
        private QuestionVM _currentQuestion;

        public MainVM MainVM { get; set; }
        public QuestionPackVM QuizQuestions { get => MainVM.ActivePack; private set { } }
        public QuestionVM CurrentQuestion { get => _currentQuestion; set { _currentQuestion = value; OnPropertyChanged(); } }
        public Visibility ResultVisibility { get; set; } = Visibility.Collapsed;
        public Visibility QuizVisibility { get; set; } = Visibility.Visible;
        public int TimerProgress { get => _timerProgress; set { _timerProgress = value; OnPropertyChanged(); } }

        public PlayVM(MainVM mainVM)
        {
            this.MainVM = mainVM;
            InitializeTimer(30);
            DisplayQuestion();

        }

        private void DisplayQuestion()
        {
            CurrentQuestion = QuizQuestions.Questions[0];
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
