namespace Codefarts.WPFCommon.Behaviours
{
    using System.Windows;
    using System.Windows.Input;

    public class FrameworkElementBehaviours
    {
        #region Loaded

        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.RegisterAttached("Loaded", typeof(ICommand), typeof(FrameworkElementBehaviours), new FrameworkPropertyMetadata(LoadedCommandChanged));

        private static void LoadedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;
            if (e.NewValue != null)
            {
                element.Loaded += Window_Loaded;
            }
            else
            {
                element.Loaded -= Window_Loaded;
            }
        }

        private static void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            var command = GetLoaded(element);
            if (command == null)
            {
                return;
            }

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetLoaded(UIElement element, ICommand value)
        {
            element.SetValue(LoadedCommandProperty, value);
        }

        public static ICommand GetLoaded(UIElement element)
        {
            return (ICommand)element.GetValue(LoadedCommandProperty);
        }

        #endregion
    }
}