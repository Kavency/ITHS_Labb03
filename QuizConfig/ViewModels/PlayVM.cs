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
        private string _answerButton1;
        private string _answerButton2;
        private string _answerButton3;
        private string _answerButton4;
        private Visibility _quizStartViewVisibility = Visibility.Visible;
        private Visibility _quizViewVisibility = Visibility.Hidden;
        private Visibility _quizEndViewVisibility = Visibility.Hidden;
        private List<string> _answers = new();
        private List<string> _randomAnswerOrder = new();
        #endregion


        #region Properties
        public MainVM MainVM { get; set; }
        public RelayCommand StartQuizCMD { get; }
        public RelayCommand AnswerCMD { get; }
        public Visibility QuizStartViewVisibility { get => _quizStartViewVisibility; set { _quizStartViewVisibility = value; OnPropertyChanged(); } }
        public Visibility QuizViewVisibility { get => _quizViewVisibility; set { _quizViewVisibility = value; OnPropertyChanged(); } }
        public Visibility QuizEndViewVisibility { get => _quizEndViewVisibility; set { _quizEndViewVisibility = value; OnPropertyChanged(); } }
        public int TimerProgressBar { get => _timerProgressBar; set { _timerProgressBar = value; OnPropertyChanged(); } }
        public int TotalNumberOfQuestions { get => _totalNumberOfQuestions; set { _totalNumberOfQuestions = value; OnPropertyChanged(); } }
        public int PlayerScore { get => _playerScore; set { _playerScore = value; OnPropertyChanged(); } }
        public int QuestionTimeLimit { get => _questionTimeLimit; set { _questionTimeLimit = value; OnPropertyChanged(); } }
        public int QuestionNumber { get => _questionNumber; set { _questionNumber = value; OnPropertyChanged(); } }
        public string CurrentQuestion { get => _currentQuestion; set { _currentQuestion = value; OnPropertyChanged(); } }
        public string AnswerButton1 { get => _answerButton1; set { _answerButton1 = value; OnPropertyChanged(); } }
        public string AnswerButton2 { get => _answerButton2; set { _answerButton2 = value; OnPropertyChanged(); } }
        public string AnswerButton3 { get => _answerButton3; set { _answerButton3 = value; OnPropertyChanged(); } }
        public string AnswerButton4 { get => _answerButton4; set { _answerButton4 = value; OnPropertyChanged(); } }
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
            QuestionNumber = 0;
            PlayerScore = 0;
            RunQuiz();
        }


        private void RunQuiz(object? obj = null)
        {
            QuizStartViewVisibility = Visibility.Hidden;
            QuizViewVisibility = Visibility.Visible;
            QuestionTimeLimit = MainVM.ActivePack.TimeLimit;
            _timerProgressBar = MainVM.ActivePack.TimeLimit;

            TotalNumberOfQuestions = MainVM.ActivePack.Questions.Count;

            _answers.Clear();
            _randomAnswerOrder.Clear();

            if (_questionNumber <= MainVM.ActivePack.Questions.Count - 1)
            {
                _answers.Add(MainVM.ActivePack.Questions[_questionNumber].CorrectAnswer);
                _answers.Add(MainVM.ActivePack.Questions[_questionNumber].IncorrectAnswers[0]);
                _answers.Add(MainVM.ActivePack.Questions[_questionNumber].IncorrectAnswers[1]);
                _answers.Add(MainVM.ActivePack.Questions[_questionNumber].IncorrectAnswers[2]);

                _randomAnswerOrder = _answers.OrderBy(x => _rnd.Next()).ToList();

                CurrentQuestion = MainVM.ActivePack.Questions[_questionNumber].Question;
                AnswerButton1 = _randomAnswerOrder[0];
                AnswerButton2 = _randomAnswerOrder[1];
                AnswerButton3 = _randomAnswerOrder[2];
                AnswerButton4 = _randomAnswerOrder[3];

                InitializeTimer(_timerProgressBar);
            }
            else
            {
                ResetTimer();
                ShowResult();
            }
        }


        private void ShowResult(object? obj = null)
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
            if (obj == MainVM.ActivePack.Questions[_questionNumber].CorrectAnswer)
            {
                PlayerScore++;
                Debug.WriteLine("Correct answer choosen");
            }

            // TODO: Create a timer and hold for 5 seconds.
            QuestionNumber++;
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
