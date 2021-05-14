namespace Codefarts.WPFCommon.Behaviours
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class SelectorBehaviours
    {
        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.RegisterAttached("SelectionChanged", typeof(ICommand), typeof(SelectorBehaviours), new FrameworkPropertyMetadata(SelectionChangedCommandChanged));

        private static void SelectionChangedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (Selector)d;
            if (e.NewValue != null)
            {
                element.SelectionChanged += Selector_SelectionChanged;
            }
            else
            {
                element.SelectionChanged -= Selector_SelectionChanged;
            }
        }

        private static void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var element = (Selector)sender;
            var command = GetSelectionChanged(element);
            if (command == null)
            {
                return;
            }

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetSelectionChanged(UIElement element, ICommand value)
        {
            element.SetValue(SelectionChangedCommandProperty, value);
        }

        public static ICommand GetSelectionChanged(UIElement element)
        {
            return (ICommand)element.GetValue(SelectionChangedCommandProperty);
        }
    }
}