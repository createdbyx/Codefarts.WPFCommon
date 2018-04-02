namespace Codefarts.WPFCommon.Behaviours
{
    using System.Windows;
    using System.Windows.Input;

    public class WindowBehaviours
    {
        #region Loaded

        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.RegisterAttached("LoadedCommand", typeof(ICommand), typeof(WindowBehaviours), new FrameworkPropertyMetadata(LoadedCommandChanged));

        private static void LoadedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.Loaded += element_Loaded;
        }

        static void element_Loaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetLoadedCommand(element);

            command.Execute(e);
        }

        public static void SetLoadedCommand(UIElement element, ICommand value)
        {
            element.SetValue(LoadedCommandProperty, value);
        }

        public static ICommand GetLoadedCommand(UIElement element)
        {
            return (ICommand)element.GetValue(LoadedCommandProperty);
        }

        #endregion
    }
}