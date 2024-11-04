using QuizConfig.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class MainVM : BaseVM
    {
        #region Fields
        private QuestionPackModel _activePack;
        private Visibility _editVisibility = Visibility.Visible;
        private Visibility _playVisibility = Visibility.Collapsed;
        #endregion

        #region Properties
        public MenuVM MenuVM { get; set; }
        public ConfigVM ConfigVM { get; }
        public ConfirmExitVM ConfirmExitVM { get; set; }
        public FileHandler FileHandler { get; set; }
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
        public ObservableCollection<QuestionPackModel> QuestionPacks { get; set; }
        public QuestionPackModel ActivePack
        {
            get => _activePack;
            set { _activePack = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructor
        public MainVM()
        {
            this.MenuVM = new MenuVM(this);
            this.ConfigVM = new ConfigVM(this);
            this.ConfirmExitVM = new ConfirmExitVM(this);
            this.QuestionPacks = new ObservableCollection<QuestionPackModel>();

            this.FileHandler = new FileHandler(this);


            // TODO: Is not using await... NOT GOOD!
            this.FileHandler.LoadFromFileAsync();

            if (QuestionPacks is not null)
                this.ActivePack = this.QuestionPacks.First();
            else
                this.ActivePack = null;
        }
        #endregion
    }
}
