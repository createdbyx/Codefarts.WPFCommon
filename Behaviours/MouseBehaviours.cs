namespace Codefarts.WPFCommon.Behaviours
{
    using System.Windows;
    using System.Windows.Input;

    public class MouseBehaviours
    {
        #region MouseUp

        public static readonly DependencyProperty MouseUpCommandProperty =
            DependencyProperty.RegisterAttached("MouseUpCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseUpCommandChanged));

        private static void MouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseUp += element_MouseUp;
        }

        static void element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseUpCommand(element);

            command.Execute(e);
        }

        public static void SetMouseUpCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseUpCommandProperty, value);
        }

        public static ICommand GetMouseUpCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseUpCommandProperty);
        }

        #endregion

        #region MouseDown

        public static readonly DependencyProperty MouseDownCommandProperty =
            DependencyProperty.RegisterAttached("MouseDownCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseDownCommandChanged));

        private static void MouseDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseDown += element_MouseDown;
        }

        static void element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseDownCommand(element);

            command.Execute(e);
        }

        public static void SetMouseDownCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseDownCommandProperty, value);
        }

        public static ICommand GetMouseDownCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseDownCommandProperty);
        }

        #endregion

        #region MouseLeave

        public static readonly DependencyProperty MouseLeaveCommandProperty =
            DependencyProperty.RegisterAttached("MouseLeaveCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseLeaveCommandChanged));

        private static void MouseLeaveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseLeave += element_MouseLeave;
        }

        static void element_MouseLeave(object sender, MouseEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseLeaveCommand(element);

            command.Execute(e);
        }

        public static void SetMouseLeaveCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseLeaveCommandProperty, value);
        }

        public static ICommand GetMouseLeaveCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseLeaveCommandProperty);
        }
        #endregion

        #region MouseLeftButtonDown

        public static readonly DependencyProperty MouseLeftButtonDownCommandProperty =
            DependencyProperty.RegisterAttached("MouseLeftButtonDownCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseLeftButtonDownCommandChanged));

        private static void MouseLeftButtonDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseLeftButtonDown += element_MouseLeftButtonDown;
        }

        static void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseLeftButtonDownCommand(element);

            command.Execute(e);
        }

        public static void SetMouseLeftButtonDownCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseLeftButtonDownCommandProperty, value);
        }

        public static ICommand GetMouseLeftButtonDownCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseLeftButtonDownCommandProperty);
        }

        #endregion

        #region MouseLeftButtonUp

        public static readonly DependencyProperty MouseLeftButtonUpCommandProperty =
            DependencyProperty.RegisterAttached("MouseLeftButtonUpCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseLeftButtonUpCommandChanged));

        private static void MouseLeftButtonUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseLeftButtonUp += element_MouseLeftButtonUp;
        }

        static void element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseLeftButtonUpCommand(element);

            command.Execute(e);
        }

        public static void SetMouseLeftButtonUpCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseLeftButtonUpCommandProperty, value);
        }

        public static ICommand GetMouseLeftButtonUpCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseLeftButtonUpCommandProperty);
        }

        #endregion

        #region MouseMove

        public static readonly DependencyProperty MouseMoveCommandProperty =
            DependencyProperty.RegisterAttached("MouseMoveCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseMoveCommandChanged));

        private static void MouseMoveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseMove += element_MouseMove;
        }

        static void element_MouseMove(object sender, MouseEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseMoveCommand(element);

            command.Execute(e);
        }

        public static void SetMouseMoveCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseMoveCommandProperty, value);
        }

        public static ICommand GetMouseMoveCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseMoveCommandProperty);
        }

        #endregion

        #region MouseRightButtonDown

        public static readonly DependencyProperty MouseRightButtonDownCommandProperty =
            DependencyProperty.RegisterAttached("MouseRightButtonDownCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseRightButtonDownCommandChanged));

        private static void MouseRightButtonDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseRightButtonDown += element_MouseRightButtonDown;
        }

        static void element_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseRightButtonDownCommand(element);

            command.Execute(e);
        }

        public static void SetMouseRightButtonDownCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseRightButtonDownCommandProperty, value);
        }

        public static ICommand GetMouseRightButtonDownCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseRightButtonDownCommandProperty);
        }

        #endregion

        #region MouseRightButtonUp

        public static readonly DependencyProperty MouseRightButtonUpCommandProperty =
            DependencyProperty.RegisterAttached("MouseRightButtonUpCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseRightButtonUpCommandChanged));

        private static void MouseRightButtonUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseRightButtonUp += element_MouseRightButtonUp;
        }

        static void element_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseRightButtonUpCommand(element);

            command.Execute(e);
        }

        public static void SetMouseRightButtonUpCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseRightButtonUpCommandProperty, value);
        }

        public static ICommand GetMouseRightButtonUpCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseRightButtonUpCommandProperty);
        }

        #endregion

        #region MouseWheel

        public static readonly DependencyProperty MouseWheelCommandProperty =
            DependencyProperty.RegisterAttached("MouseWheelCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseWheelCommandChanged));

        private static void MouseWheelCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseWheel += element_MouseWheel;
        }

        static void element_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseWheelCommand(element);

            command.Execute(e);
        }

        public static void SetMouseWheelCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseWheelCommandProperty, value);
        }

        public static ICommand GetMouseWheelCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseWheelCommandProperty);
        }

        #endregion
    }
}