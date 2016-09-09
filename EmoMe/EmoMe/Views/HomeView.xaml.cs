using System.Threading.Tasks;
using EmoMe.ViewModels.Interfaces;
using EmoMe.Views.Interfaces;
using Xamarin.Forms;

namespace EmoMe.Views
{
    public partial class HomeView : IHomeView
    {
        private readonly IHomeViewModel _homeViewModel;

        public HomeView(IHomeViewModel homeViewModel)
        {
            InitializeComponent();
            BindingContext = _homeViewModel = homeViewModel;
            BindablePage = this;
        }

        public Page BindablePage { get; }

        public async Task Initialize() => await _homeViewModel.Initialize();

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _homeViewModel.Initialize();
        }
    }
}
