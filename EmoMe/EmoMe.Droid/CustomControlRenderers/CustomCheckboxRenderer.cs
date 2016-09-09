using System.ComponentModel;
using Android.Widget;
using EmoMe.CustomControls;
using EmoMe.Droid.CustomControlRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomCheckbox), typeof(CustomCheckboxRenderer))]
namespace EmoMe.Droid.CustomControlRenderers
{
    public class CustomCheckboxRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<CustomCheckbox, CheckBox>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomCheckbox> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var checkBox = new CheckBox(Context);
                checkBox.CheckedChange += CheckBoxCheckedChange;
                SetNativeControl(checkBox);
            }

            if (Control != null)
            {
                Control.Checked = e.NewElement.Checked;
            }
        }

        private void CheckBoxCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.Checked = e.IsChecked;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
                    break;
            }
        }

        protected override CheckBox CreateNativeControl()
        {
            throw new System.NotImplementedException();
        }
    }
}