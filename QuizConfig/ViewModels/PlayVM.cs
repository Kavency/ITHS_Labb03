using QuizConfig.Commands;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace QuizConfig.ViewModels
{
    internal class PlayVM : BaseVM
    {
        #region Fields
        private Random _rnd;
        private DispatcherTimer _timer;
        private int _questionTimeLimit;
        private int _timerLimit;
        private int _elapsedTime = 0;
        private int _timerProgressBar = 100;
        private int _totalNumberOfQuestions;
        private int _playerScore = 0;
        private int _questionNumber;
        private string _currentQuestion;
        private string _answer1;
        private string _answer2;
        private string _answer3;
        private string _answer4;
        private Visibility _quizStartViewVisibility = Visibility.Visible;
        private Visibility _quizViewVisibility = Visibility.Hidden;
        private Visibility _quizEndViewVisibility = Visibility.Hidden;
        private List<string> answers = new();
        private List<string> randomAnswerOrder = new();
        #endregion


        #region Properties
        public MainVM MainVM { get; set; }
        public RelayCommand StartQuizCMD { get; }
        public RelayCommand AnswerCMD { get; }
        public QuestionPackVM ActivePack { get => MainVM.ActivePack; set { this.ActivePack = value; OnPropertyChanged(); } }
        public Visibility QuizStartViewVisibility { get => _quizStartViewVisibility; set { _quizStartViewVisibility = value; OnPropertyChanged(); } }
        public Visibility QuizViewVisibility { get => _quizViewVisibility; set { _quizViewVisibility = value; OnPropertyChanged(); } }
        public Visibility QuizEndViewVisibility { get => _quizEndViewVisibility; set { _quizEndViewVisibility = value; OnPropertyChanged(); } }
        public int TimerProgressBar { get => _timerProgressBar; set { _timerProgressBar = value; OnPropertyChanged(); } }
        public int TotalNumberOfQuestions { get => _totalNumberOfQuestions; set { _totalNumberOfQuestions = value; OnPropertyChanged(); } }
        public int PlayerScore { get => _playerScore; set { _playerScore = value; OnPropertyChanged(); } }
        public int QuestionTimeLimit { get => _questionTimeLimit; set { _questionTimeLimit = value; OnPropertyChanged(); } }
        public string CurreentQuestion { get => _currentQuestion; set { _currentQuestion = value; OnPropertyChanged(); } }
        public string Answer1 { get => _answer1; set { _answer1 = value; OnPropertyChanged(); } }
        public string Answer2 { get => _answer2; set { _answer2 = value; OnPropertyChanged(); } }
        public string Answer3 { get => _answer3; set { _answer3 = value; OnPropertyChanged(); } }
        public string Answer4 { get => _answer4; set { _answer4 = value; OnPropertyChanged(); } }
        #endregion


        #region Constructor
        public PlayVM(MainVM mainVM)
        {
            this.MainVM = mainVM;
            this._rnd = new();
            this.StartQuizCMD = new RelayCommand(SetupQuiz);
            this.AnswerCMD = new RelayCommand(AnswerClicked);
        }
        #endregion


        #region Methods
        private void SetupQuiz(object? obj)
        {
            QuizStartViewVisibility = Visibility.Visible;
            QuizEndViewVisibility = Visibility.Hidden;
            QuizViewVisibility = Visibility.Hidden;
            RunQuiz();
        }


        private void RunQuiz() 
        {
            QuizStartViewVisibility = Visibility.Hidden;
            QuizViewVisibility = Visibility.Visible;
            QuestionTimeLimit = ActivePack.TimeLimit;
            _timerProgressBar = ActivePack.TimeLimit;

            TotalNumberOfQuestions = ActivePack.Questions.Count;

            answers.Clear();
            randomAnswerOrder.Clear();

            if (_questionNumber <= ActivePack.Questions.Count - 1)
            {
                answers.Add(ActivePack.Questions[_questionNumber].CorrectAnswer);
                answers.Add(ActivePack.Questions[_questionNumber].IncorrectAnswers[0]);
                answers.Add(ActivePack.Questions[_questionNumber].IncorrectAnswers[1]);
                answers.Add(ActivePack.Questions[_questionNumber].IncorrectAnswers[2]);

                randomAnswerOrder = answers.OrderBy(x => _rnd.Next()).ToList();

                CurreentQuestion = ActivePack.Questions[_questionNumber].Question;
                Answer1 = randomAnswerOrder[0];
                Answer2 = randomAnswerOrder[1];
                Answer3 = randomAnswerOrder[2];
                Answer4 = randomAnswerOrder[3];

                InitializeTimer(_timerProgressBar);
            }
            else
            {
                ResetTimer();
                ShowResult();
            }
        }


        private void ShowResult()
        {
            QuizViewVisibility = Visibility.Hidden;
            QuizEndViewVisibility = Visibility.Visible;
        }


        private void InitializeTimer(int totalSeconds)
        {
            _timerLimit = totalSeconds;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            _elapsedTime++;
            QuestionTimeLimit--;

            double normalizedTime = ((double)_elapsedTime / _timerLimit * 100);
            int timeLeft = (int)normalizedTime;

            if (_elapsedTime <= _timerLimit)
            {
                TimerProgressBar = 100 - timeLeft;
            }
            else
            {
                _questionNumber++;
                ResetTimer();
                RunQuiz();
            }
        }


        private void AnswerClicked(object? obj)
        {
            if (obj == ActivePack.Questions[_questionNumber].CorrectAnswer)
            {
                // TODO: Correctly answered logic
                PlayerScore++;
                Debug.WriteLine("Correct answer choosen");
            }
            else
            {
                // TODO: Wrongly answered logic
                TotalNumberOfQuestions = -100;
                Debug.WriteLine("Wrong answer choosen");
            }

            // TODO: Create a timer and hold for 5 seconds.
            _questionNumber++;
            ResetTimer();
            RunQuiz();
        }


        private void ResetTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
                TimerProgressBar = 100;
                _elapsedTime = 0;
            }
        }
        #endregion

    }
}
