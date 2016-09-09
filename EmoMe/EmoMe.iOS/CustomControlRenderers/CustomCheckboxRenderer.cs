using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using EmoMe.CustomControls;
using EmoMe.iOS.CustomControlRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCheckbox), typeof(CustomCheckboxRenderer))]

namespace EmoMe.iOS.CustomControlRenderers
{
    public class CustomCheckboxRenderer : ViewRenderer<CustomCheckbox, CheckBoxView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomCheckbox> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
            {
                return;
            }

            BackgroundColor = Element.BackgroundColor.ToUIColor();

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var checkBox = new CheckBoxView(Bounds);
                    checkBox.TouchUpInside += (s, args) => Element.Checked = Control.Checked;
                    SetNativeControl(checkBox);
                }
                if (Control != null)
                {
                    Control.LineBreakMode = UILineBreakMode.CharacterWrap;
                    Control.VerticalAlignment = UIControlContentVerticalAlignment.Top;
                    Control.Checked = e.NewElement.Checked;
                }
            }

            if (Control == null)
            {
                return;
            }
            Control.Frame = Frame;
            Control.Bounds = Bounds;
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
    }
}