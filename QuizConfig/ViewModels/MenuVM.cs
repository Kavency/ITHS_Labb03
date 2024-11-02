using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizConfig.ViewModels
{
    internal class MenuVM
    {
        public MainVM MainVM { get; set; }
        public MenuVM(MainVM mainVM)
        {
            this.MainVM = mainVM;
        }
    }
}
