namespace QuizConfig.ViewModels
{
    internal class PlayVM
    {
        public MainVM MainVM { get; set; }

        public PlayVM(MainVM mainVM)
        {
            this.MainVM = mainVM;
        }
    }
}
