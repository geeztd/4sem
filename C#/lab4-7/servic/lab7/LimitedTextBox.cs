using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using static servic.lab7Window;

namespace servic.lab7
{
    public class LimitedTextBox : TextBox
    {
        public static readonly DependencyProperty MaxLengthCastomProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(LimitedTextBox),
                new FrameworkPropertyMetadata(int.MaxValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnMaxLengthChanged,
                    CoerceLengthValue));

        private static void OnMaxLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LimitedTextBox textBox = (LimitedTextBox)d;
            textBox.MaxLength = (int)e.NewValue;
        }
        private static object CoerceLengthValue(DependencyObject d, object baseValue)
        {
            return baseValue;
        }
        public int MaxLengthCastom
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }
    }
}

