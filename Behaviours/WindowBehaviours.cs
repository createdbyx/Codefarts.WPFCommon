namespace Codefarts.WPFCommon.Behaviours
{
    using System.Windows;
    using System.Windows.Input;

    public class WindowBehaviours
    {
        #region Loaded

        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.RegisterAttached("LoadedCommand", typeof(ICommand), typeof(WindowBehaviours), new FrameworkPropertyMetadata(LoadedCommandChanged));

        public static readonly DependencyProperty ClosingCommandProperty =
            DependencyProperty.RegisterAttached("ClosingCommand", typeof(ICommand), typeof(WindowBehaviours), new FrameworkPropertyMetadata(ClosingCommandChanged));

        private static void LoadedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;
            element.Loaded += element_Loaded;
        }

        private static void ClosingCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (Window)d;
            element.Closing += Window_Closing;
        }

        private static void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var element = (Window)sender;
            var command = GetClosingCommand(element);
            command.Execute(e);
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

        public static void SetClosingCommand(Window element, ICommand value)
        {
            element.SetValue(ClosingCommandProperty, value);
        }

        public static ICommand GetLoadedCommand(UIElement element)
        {
            return (ICommand)element.GetValue(LoadedCommandProperty);
        }

        public static ICommand GetClosingCommand(Window window)
        {
            return (ICommand)window.GetValue(ClosingCommandProperty);
        }

        #endregion
    }
}