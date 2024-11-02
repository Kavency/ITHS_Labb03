using QuizConfig.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QuizConfig.ViewModels
{
    internal class MainVM : VMBase
    {
        private QuestionPackModel _activePack;
        private int _selectedMenuItem;

        public ObservableCollection<QuestionPackModel> QuestionPacks { get; set; }


        public QuestionPackModel ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                OnPropertyChanged();
            }
        }

        public int SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { _selectedMenuItem = value; OnPropertyChanged(); }
        }

        public RelayCommand AddPackCommand { get; }
        public RelayCommand SetActivePackCMD { get; }
        public RelayCommand DeletePackCMD { get; }
        public MenuVM MenuVM { get; set; }
        public MainVM()
        {
            this.MenuVM = new MenuVM(this);

            this.QuestionPacks = new ObservableCollection<QuestionPackModel>();
            this.QuestionPacks.Add(new QuestionPackModel(new QuestionModel()) { Name = "Pack 1"});
            
            for(int i = 0; i < 10; i++)
            {
                number++;
                this.QuestionPacks.Add(new QuestionPackModel(new QuestionModel()) { Name = $"Pack {number}"});
                Debug.WriteLine(QuestionPacks[number - 1].Name);
                Thread.Sleep(1000);
            }

        }
    }
}
