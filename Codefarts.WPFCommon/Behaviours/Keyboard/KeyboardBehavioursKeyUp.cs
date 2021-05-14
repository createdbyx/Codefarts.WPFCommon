// <copyright file="KeyboardBehavioursKeyUp.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.WPFCommon.Behaviours
{
    using System.Windows;
    using System.Windows.Input;

    public partial class KeyboardBehaviours
    {
        public static readonly DependencyProperty KeyUpCommandProperty =
            DependencyProperty.RegisterAttached("KeyUpCommand", typeof(ICommand), typeof(KeyboardBehaviours),
                new FrameworkPropertyMetadata(KeyUpCommandChanged));

        private static void KeyUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.KeyUp += element_KeyUp;
        }

        static void element_KeyUp(object sender, KeyEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetKeyUpCommand(element);
            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetKeyUpCommand(UIElement element, ICommand value)
        {
            element.SetValue(KeyUpCommandProperty, value);
        }

        public static ICommand GetKeyUpCommand(UIElement element)
        {
            return (ICommand)element.GetValue(KeyUpCommandProperty);
        }
    }
}