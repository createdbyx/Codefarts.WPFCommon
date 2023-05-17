namespace Codefarts.WPFCommon.Behaviours
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class WindowBehaviours
    {
        public static readonly DependencyProperty ClosingCommandProperty =
          DependencyProperty.RegisterAttached("Closing", typeof(ICommand), typeof(WindowBehaviours), new FrameworkPropertyMetadata(ClosingCommandChanged));

        private static void ClosingCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window window))
            {
                return;
            }

            if (e.NewValue != null)
            {
                window.Closing += Window_Closing;
            }
            else
            {
                window.Closing -= Window_Closing;
            }
        }

        private static void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var element = (Window)sender;
            var closing = GetClosing(element);
            if (closing == null)
            {
                return;
            }

            if (closing.CanExecute(e))
            {
                closing.Execute(e);
                return;
            }

            var cancelClosing = GetCancelClosing(element);
            if (cancelClosing == null)
            {
                return;
            }

            if (cancelClosing.CanExecute(e))
            {
                cancelClosing.Execute(e);
            }
        }

        public static void SetClosing(Window element, ICommand value)
        {
            element.SetValue(ClosingCommandProperty, value);
        }

        public static ICommand GetClosing(Window window)
        {
            return (ICommand)window.GetValue(ClosingCommandProperty);
        }

        public static ICommand GetCancelClosing(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CancelClosingProperty);
        }

        public static void SetCancelClosing(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CancelClosingProperty, value);
        }

        public static readonly DependencyProperty CancelClosingProperty =
            DependencyProperty.RegisterAttached("CancelClosing", typeof(ICommand), typeof(WindowBehaviours));

        public static ICommand GetClosed(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ClosedProperty);
        }

        public static void SetClosed(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ClosedProperty, value);
        }

        public static readonly DependencyProperty ClosedProperty = DependencyProperty.RegisterAttached("Closed", typeof(ICommand), typeof(WindowBehaviours),
            new UIPropertyMetadata(ClosedChanged));

        private static void ClosedChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var window = target as Window;
            if (window == null)
            {
                return;
            }

            if (e.NewValue != null)
            {
                window.Closed += Window_Closed;
            }
            else
            {
                window.Closed -= Window_Closed;
            }
        }

        static void Window_Closed(object sender, EventArgs e)
        {
            var closed = GetClosed(sender as Window);
            if (closed == null)
            {
                return;
            }

            if (closed.CanExecute(e))
            {
                closed.Execute(e);
            }
        }
    }
}