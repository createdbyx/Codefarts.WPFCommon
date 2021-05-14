// <copyright file="MouseBehaviours.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.WPFCommon.Behaviours
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class MouseBehaviours
    {
        public static readonly DependencyProperty MouseUpCommandProperty =
            DependencyProperty.RegisterAttached("MouseUpCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseUpCommandChanged));

        private static void MouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseUp += element_MouseUp;
        }

        private static void element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseUpCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseUpCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(MouseUpCommandProperty, value);
        }

        public static ICommand GetMouseUpCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseUpCommandProperty);
        }

        public static readonly DependencyProperty MouseDownCommandProperty =
            DependencyProperty.RegisterAttached("MouseDownCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseDownCommandChanged));

        private static void MouseDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseDown += element_MouseDown;
        }

        private static void element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseDownCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseDownCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            element.SetValue(MouseDownCommandProperty, value);
        }

        public static ICommand GetMouseDownCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseDownCommandProperty);
        }

        public static readonly DependencyProperty MouseLeaveCommandProperty =
            DependencyProperty.RegisterAttached("MouseLeaveCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseLeaveCommandChanged));

        private static void MouseLeaveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseLeave += element_MouseLeave;
        }

        private static void element_MouseLeave(object sender, MouseEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseLeaveCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseLeaveCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(MouseLeaveCommandProperty, value);
        }

        public static ICommand GetMouseLeaveCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseLeaveCommandProperty);
        }

        public static readonly DependencyProperty MouseLeftButtonDownCommandProperty =
            DependencyProperty.RegisterAttached("MouseLeftButtonDownCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseLeftButtonDownCommandChanged));

        private static void MouseLeftButtonDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseLeftButtonDown += element_MouseLeftButtonDown;
        }

        private static void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseLeftButtonDownCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseLeftButtonDownCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(MouseLeftButtonDownCommandProperty, value);
        }

        public static ICommand GetMouseLeftButtonDownCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseLeftButtonDownCommandProperty);
        }

        public static readonly DependencyProperty MouseLeftButtonUpCommandProperty =
            DependencyProperty.RegisterAttached("MouseLeftButtonUpCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseLeftButtonUpCommandChanged));

        private static void MouseLeftButtonUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseLeftButtonUp += element_MouseLeftButtonUp;
        }

        private static void element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseLeftButtonUpCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseLeftButtonUpCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(MouseLeftButtonUpCommandProperty, value);
        }

        public static ICommand GetMouseLeftButtonUpCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseLeftButtonUpCommandProperty);
        }

        public static readonly DependencyProperty MouseMoveCommandProperty =
            DependencyProperty.RegisterAttached("MouseMoveCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseMoveCommandChanged));

        private static void MouseMoveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseMove += element_MouseMove;
        }

        private static void element_MouseMove(object sender, MouseEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseMoveCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseMoveCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(MouseMoveCommandProperty, value);
        }

        public static ICommand GetMouseMoveCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseMoveCommandProperty);
        }

        public static readonly DependencyProperty MouseRightButtonDownCommandProperty =
            DependencyProperty.RegisterAttached("MouseRightButtonDownCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseRightButtonDownCommandChanged));

        private static void MouseRightButtonDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseRightButtonDown += element_MouseRightButtonDown;
        }

        private static void element_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseRightButtonDownCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseRightButtonDownCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(MouseRightButtonDownCommandProperty, value);
        }

        public static ICommand GetMouseRightButtonDownCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseRightButtonDownCommandProperty);
        }

        public static readonly DependencyProperty MouseRightButtonUpCommandProperty =
            DependencyProperty.RegisterAttached("MouseRightButtonUpCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseRightButtonUpCommandChanged));

        private static void MouseRightButtonUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseRightButtonUp += element_MouseRightButtonUp;
        }

        private static void element_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseRightButtonUpCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseRightButtonUpCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(MouseRightButtonUpCommandProperty, value);
        }

        public static ICommand GetMouseRightButtonUpCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseRightButtonUpCommandProperty);
        }

        public static readonly DependencyProperty MouseWheelCommandProperty =
            DependencyProperty.RegisterAttached("MouseWheelCommand", typeof(ICommand), typeof(MouseBehaviours), new FrameworkPropertyMetadata(MouseWheelCommandChanged));

        private static void MouseWheelCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.MouseWheel += element_MouseWheel;
        }

        private static void element_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetMouseWheelCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetMouseWheelCommand(UIElement element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(MouseWheelCommandProperty, value);
        }

        public static ICommand GetMouseWheelCommand(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (ICommand)element.GetValue(MouseWheelCommandProperty);
        }
    }
}