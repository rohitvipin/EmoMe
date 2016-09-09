using System.Threading.Tasks;
using EmoMe.ViewModels.Interfaces;
using EmoMe.Views.Interfaces;
using Xamarin.Forms;

namespace EmoMe.Views
{
    public partial class ImageFilterView : IImageFilterView
    {
        private readonly IImageFilterViewModel _imageFilterViewModel;
        public ImageFilterView(IImageFilterViewModel imageFilterViewModel)
        {
            InitializeComponent();
            BindingContext = _imageFilterViewModel = imageFilterViewModel;
            BindablePage = this;
        }

        public Page BindablePage { get; }

        public async Task Initialize() => await _imageFilterViewModel.Initialize();
    }
}
