using System.ComponentModel;
using System.Threading.Tasks;

namespace EmoMe.ViewModels.Interfaces
{
    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsBusy { get; }

        bool IsActionsAllowed { get; }

        void BeginBusy();

        void EndBusy();

        string PageTitle { get; }

        Task Initialize();
    }
}