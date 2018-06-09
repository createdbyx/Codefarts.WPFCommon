namespace Codefarts.WPFCommon
{
    using System.Windows;
    using System.Windows.Controls;

    public class BindingProxyControl : UserControl
    {
        public object Data
        {
            get { return (object)this.GetValue(DataProperty); }
            set { this.SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxyControl), new UIPropertyMetadata(null));
    }
}