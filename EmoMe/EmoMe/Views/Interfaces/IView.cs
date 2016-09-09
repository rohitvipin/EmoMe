using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmoMe.Views.Interfaces
{
    public interface IView
    {
        Page BindablePage { get; }

        Task Initialize();
    }
}