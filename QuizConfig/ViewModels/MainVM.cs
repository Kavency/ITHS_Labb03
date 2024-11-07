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
        private Visibility _playVisibility = Visibility.Hidden;
        #endregion


        #region Properties
        public MenuVM MenuVM { get; set; }
        public ConfigVM ConfigVM { get; }
        public ConfirmExitVM ConfirmExitVM { get; set; }
        public FileHandler FileHandler { get; set; }
        public CreatePackVM CreatePackVM { get; set; }
        public OptionsDialogVM OptionsDialogVM { get; set; }
        public PlayVM PlayVM { get; set; }
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
            WaitForLoadToComplete();

            this.MenuVM = new MenuVM(this);
            this.ConfigVM = new ConfigVM(this);
            this.ConfirmExitVM = new ConfirmExitVM(this);
            this.CreatePackVM = new CreatePackVM(this);
            this.OptionsDialogVM = new OptionsDialogVM(this);
            this.PlayVM = new PlayVM(this);
        }
        #endregion


        #region Methods
        private async void WaitForLoadToComplete()
        {
            await FileHandler.LoadFromFileAsync();
            
            if (QuestionPacks is not null)
                this.ActivePack = this.QuestionPacks[0];
            else
                this.ActivePack = null;
        }
        #endregion
    }
}
