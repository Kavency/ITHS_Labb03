using System.Collections.ObjectModel;
using System.Windows;
using QuizConfig.MiscClasses;

namespace QuizConfig.ViewModels
{
    internal class MainVM : BaseVM
    {
        #region Fields
        private QuestionPackVM _activePack;
        private Visibility _editVisibility = Visibility.Visible;
        private Visibility _playVisibility = Visibility.Collapsed;
        #endregion

        #region Properties
        public MenuVM MenuVM { get; set; }
        public ConfigVM ConfigVM { get; }
        public ConfirmExitVM ConfirmExitVM { get; set; }
        public FileHandler FileHandler { get; set; }
        public CreatePackVM CreatePackVM { get; set; }
        public OptionsDialogVM OptionsDialogVM { get; set; }
        public Visibility EditVisibility
        {
            get => _editVisibility;
            set { _editVisibility = value; OnPropertyChanged(); }
        }
        public Visibility PlayVisibility
        {
            get { return _playVisibility; }
            set { _playVisibility = value; OnPropertyChanged(); }
        }
        public ObservableCollection<QuestionPackVM> QuestionPacks { get; set; }
        public QuestionPackVM ActivePack
        {
            get => _activePack;
            set { _activePack = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructor
        public MainVM()
        {
            this.QuestionPacks = new ObservableCollection<QuestionPackVM>();
            this.FileHandler = new FileHandler(this);
            //this.FileHandler.LoadFromFileAsync().Wait(500); // TODO: Not the best way to await the async.
            WaitForLoadToComplete();

            this.MenuVM = new MenuVM(this);
            this.ConfigVM = new ConfigVM(this);
            this.ConfirmExitVM = new ConfirmExitVM(this);
            this.CreatePackVM = new CreatePackVM(this);
            this.OptionsDialogVM = new OptionsDialogVM(this);


            // Hardcoded data
            //QuestionPacks.Add(new QuestionPackVM(new QuestionPackModel("Pack01")));
            //QuestionPacks.Add(new QuestionPackVM(new QuestionPackModel("Pack02")));
            //QuestionPacks.Add(new QuestionPackVM(new QuestionPackModel("Pack03")));
            //QuestionPacks.Add(new QuestionPackVM(new QuestionPackModel("Pack04")));
            //QuestionPacks.Add(new QuestionPackVM(new QuestionPackModel("Pack05")));

            //QuestionPacks[0].Questions.Add(new QuestionVM(new QuestionModel("What day is it?", "Monday", "Thursday", "Saturday", "Sunday")));
            //QuestionPacks[1].Questions.Add(new QuestionVM(new QuestionModel("Am I evil?", "Yes, I am", "I don't know", "Nah", "No clue")));
            //QuestionPacks[2].Questions.Add(new QuestionVM(new QuestionModel("Wich is Pi?", "3,14", "14", "3", "2,14")));
            //QuestionPacks[3].Questions.Add(new QuestionVM(new QuestionModel("Is this app good?", "No", "yes", "Sure is", "LOVE IT")));
            //QuestionPacks[4].Questions.Add(new QuestionVM(new QuestionModel("What year is it?", "2024", "1998", "2004", "1824")));
        }
        #endregion

        private async void WaitForLoadToComplete()
        {
            await FileHandler.LoadFromFileAsync();
            
            if (QuestionPacks is not null)
                this.ActivePack = this.QuestionPacks[0];
            else
                this.ActivePack = null;
        }
    }
}
