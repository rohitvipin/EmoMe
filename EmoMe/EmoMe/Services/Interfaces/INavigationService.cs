using System.Threading.Tasks;
using EmoMe.Views.Interfaces;

namespace EmoMe.Services.Interfaces
{
    public interface INavigationService
    {
        App AppRoot { get; set; }
        void SetRoot(IView view, string pageTitle);
        Task NavigateFromRootPage(IView view, string pageTitle);
        Task GoBack(bool isModal);
    }
}