using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmoMe.CustomControls
{
    public class CustomCheckbox : View
    {
        public event EventHandler<bool> CheckedChanged;

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(bool), typeof(CustomCheckbox), false, BindingMode.TwoWay, propertyChanged: OnCheckedPropertyChanged);

        public bool Checked
        {
            get
            {
                return (bool)GetValue(CheckedProperty);
            }

            set
            {
                if (Checked == value)
                {
                    return;
                }
                SetValue(CheckedProperty, value);
                CheckedChanged?.Invoke(this, value);
            }
        }

        private static void OnCheckedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkBox = (CustomCheckbox)bindable;
            checkBox.Checked = (bool)newvalue;
        }
    }
}
