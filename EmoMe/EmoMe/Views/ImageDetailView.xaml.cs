using System.Threading.Tasks;
using EmoMe.ViewModels.Interfaces;
using EmoMe.Views.Interfaces;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace EmoMe.Views
{
    public partial class ImageDetailView : IImageDetailView
    {
        private readonly IImageDetailViewModel _imageDetailViewModel;
        public ImageDetailView(IImageDetailViewModel imageDetailViewModel)
        {
            InitializeComponent();
            BindingContext = _imageDetailViewModel = imageDetailViewModel;
            BindablePage = this;
        }

        public Page BindablePage { get; }

        public async Task Initialize() => await _imageDetailViewModel.Initialize();

        public async Task Initialize(int imageId, MediaFile imageFile) => await _imageDetailViewModel.Initialize(imageId, imageFile);
    }
}
