using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace servic.lab7
{
    public class NumericTextBox : TextBox
    {
        public static readonly DependencyProperty NumericValueProperty =
            DependencyProperty.Register("NumericValue", typeof(int), typeof(NumericTextBox),
                new FrameworkPropertyMetadata(0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnNumericValueChanged,
                    CoerceNumericValue));

        private static void OnNumericValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericTextBox textBox = (NumericTextBox)d;
            textBox.Text = e.NewValue.ToString();
        }

        private static object CoerceNumericValue(DependencyObject d, object baseValue)
        {
            int value;
            if (int.TryParse(baseValue.ToString(), out value))
            {
                return value;
            }
            return 0;
        }

        public int NumericValue
        {
            get { return (int)GetValue(NumericValueProperty); }
            set { SetValue(NumericValueProperty, value); }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
            base.OnPreviewTextInput(e);
        }

        private static bool IsTextAllowed(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}

