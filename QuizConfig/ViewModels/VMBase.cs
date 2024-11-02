using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizConfig.ViewModels
{
    internal class VMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
