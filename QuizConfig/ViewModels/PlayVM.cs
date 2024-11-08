using QuizConfig.Commands;
using System.Windows;
using System.Windows.Threading;

namespace QuizConfig.ViewModels
{
    internal class PlayVM : BaseVM
    {
        #region Fields
        private Random _rnd;
        private DispatcherTimer _timer;
        private List<string> _answers = new();
        private List<QuestionVM> _randomQuestionOrder = new();
        private List<string> _randomAnswerOrder = new();
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
        private Visibility _correctTick1 = Visibility.Hidden;
        private Visibility _correctTick2 = Visibility.Hidden;
        private Visibility _correctTick3 = Visibility.Hidden;
        private Visibility _correctTick4 = Visibility.Hidden;
        private Visibility _wrongTick1 = Visibility.Hidden;
        private Visibility _wrongTick2 = Visibility.Hidden;
        private Visibility _wrongTick3 = Visibility.Hidden;
        private Visibility _wrongTick4 = Visibility.Hidden;
        #endregion


        #region Properties
        public MainVM MainVM { get; set; }
        public RelayCommand StartQuizCMD { get; }
        public RelayCommand AnswerCMD { get; }
        public Visibility QuizStartViewVisibility { get => _quizStartViewVisibility; set { _quizStartViewVisibility = value; OnPropertyChanged(); } }
        public Visibility QuizViewVisibility { get => _quizViewVisibility; set { _quizViewVisibility = value; OnPropertyChanged(); } }
        public Visibility QuizEndViewVisibility { get => _quizEndViewVisibility; set { _quizEndViewVisibility = value; OnPropertyChanged(); } }
        public Visibility CorrectTick1 { get => _correctTick1; set { _correctTick1 = value; OnPropertyChanged(); } }
        public Visibility CorrectTick2 { get => _correctTick2; set { _correctTick2 = value; OnPropertyChanged(); } }
        public Visibility CorrectTick3 { get => _correctTick3; set { _correctTick3 = value; OnPropertyChanged(); } }
        public Visibility CorrectTick4 { get => _correctTick4; set { _correctTick4 = value; OnPropertyChanged(); } }
        public Visibility WrongTick1 { get => _wrongTick1; set { _wrongTick1 = value; OnPropertyChanged(); } }
        public Visibility WrongTick2 { get => _wrongTick2; set { _wrongTick2 = value; OnPropertyChanged(); } }
        public Visibility WrongTick3 { get => _wrongTick3; set { _wrongTick3 = value; OnPropertyChanged(); } }
        public Visibility WrongTick4 { get => _wrongTick4; set { _wrongTick4 = value; OnPropertyChanged(); } }
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
            QuestionNumber = 1;
            PlayerScore = 0;
            
            ResetVisibility();
            _randomQuestionOrder.Clear();
            _randomQuestionOrder = MainVM.ActivePack.Questions.OrderBy(x => _rnd.Next()).ToList();
            RunQuiz();
        }

        private void ResetVisibility()
        {
            QuizStartViewVisibility = Visibility.Visible;
            QuizEndViewVisibility = Visibility.Hidden;
            QuizViewVisibility = Visibility.Hidden;
            CorrectTick1 = Visibility.Hidden;
            CorrectTick2 = Visibility.Hidden;
            CorrectTick3 = Visibility.Hidden;
            CorrectTick4 = Visibility.Hidden;
            WrongTick1 = Visibility.Hidden;
            WrongTick2 = Visibility.Hidden;
            WrongTick3 = Visibility.Hidden;
            WrongTick4 = Visibility.Hidden;
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


            if (QuestionNumber <= _randomQuestionOrder.Count)
            {
                _answers.Add(_randomQuestionOrder[QuestionNumber - 1].CorrectAnswer);
                _answers.Add(_randomQuestionOrder[QuestionNumber - 1].IncorrectAnswers[0]);
                _answers.Add(_randomQuestionOrder[QuestionNumber - 1].IncorrectAnswers[1]);
                _answers.Add(_randomQuestionOrder[QuestionNumber - 1].IncorrectAnswers[2]);

                _randomAnswerOrder = _answers.OrderBy(x => _rnd.Next()).ToList();


                CurrentQuestion = _randomQuestionOrder[QuestionNumber - 1].Question;
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
                QuestionNumber++;
                Thread.Sleep(1000);
                ResetTimer();
                RunQuiz();
            }
        }


        private async void AnswerClicked(object? obj)
        {
            if (obj == _randomQuestionOrder[QuestionNumber - 1].CorrectAnswer)
            {
                PlayerScore++;
                ShowCorrectTickBox();
            }
            else
            {
                ShowWrongTickBox(obj);
            }

            await ShowTickAsync();
            await Task.Delay(1000);

            QuestionNumber++;
            ResetVisibility();
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


        private async Task ShowTickAsync()
        {
            await Task.Run(() => ShowCorrectTickBox());
        }


        private void ShowCorrectTickBox()
        {
            if (AnswerButton1 == _randomQuestionOrder[QuestionNumber - 1].CorrectAnswer)
                CorrectTick1 = Visibility.Visible;
            else if (AnswerButton2 == _randomQuestionOrder[QuestionNumber - 1].CorrectAnswer)
                CorrectTick2 = Visibility.Visible;
            else if (AnswerButton3 == _randomQuestionOrder[QuestionNumber - 1].CorrectAnswer)
                CorrectTick3 = Visibility.Visible;
            else if (AnswerButton4 == _randomQuestionOrder[QuestionNumber - 1].CorrectAnswer)
                CorrectTick4 = Visibility.Visible;
        }


        private void ShowWrongTickBox(object? obj)
        {
            if (AnswerButton1 == obj)
                WrongTick1 = Visibility.Visible;
            else if (AnswerButton2 == obj)
                WrongTick2 = Visibility.Visible;
            else if (AnswerButton3 == obj)
                WrongTick3 = Visibility.Visible;
            else if (AnswerButton4 == obj)
                WrongTick4 = Visibility.Visible;
        }
        #endregion
    }
}
