using System.Collections.Generic;
using System.Threading.Tasks;
using EmoMe.Services.Interfaces;
using EmoMe.Views.Interfaces;
using Xamarin.Forms;

namespace EmoMe.Services
{
    public class NavigationService : INavigationService
    {
        public App AppRoot { get; set; }

        public void SetRoot(IView view, string pageTitle)
        {
            var page = view as Page;
            if (page == null)
            {
                return;
            }
            page.Title = pageTitle;


            AppRoot.MainPage = new NavigationPage(page)
            {
                Title = pageTitle
            };
        }

        public async Task NavigateFromRootPage(IView view, string pageTitle)
        {
            var page = view as Page;
            if (page == null)
            {
                return;
            }
            page.Title = pageTitle;
            await AppRoot.MainPage.Navigation.PushAsync(page, true);
            return;
        }

        public async Task GoBack(bool isModal)
        {
            if (isModal)
            {
                await AppRoot.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                await AppRoot.MainPage.Navigation.PopAsync();
            }
        }
    }
}