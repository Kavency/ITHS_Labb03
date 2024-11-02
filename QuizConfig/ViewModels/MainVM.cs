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

            this.AddPackCommand = new RelayCommand(AddPack);
            this.SetActivePackCMD = new RelayCommand(SetActivePack);
            this.DeletePackCMD = new RelayCommand(DeletePack);


            for (int i = 0; i < 5; i++)
            {
                QuestionPacks.Add(new QuestionPackModel(new QuestionModel()) { Name = $"Pack {i + 1}" });
                Debug.WriteLine($"{QuestionPacks[i].Name} was added.");
            }

            this.ActivePack = this.QuestionPacks.First();

        }
            
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
