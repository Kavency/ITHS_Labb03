using QuizConfig.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QuizConfig.ViewModels
{
    internal class MainVM : VMBase
    {
        private int number = 1;
        public ObservableCollection<QuestionPackModel> QuestionPacks { get; set; }
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
