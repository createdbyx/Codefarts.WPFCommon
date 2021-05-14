// <copyright file="KeyboardBehavioursKeyDown.cs" company="Codefarts">
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
        public static readonly DependencyProperty KeyDownCommandProperty =
            DependencyProperty.RegisterAttached("KeyDownCommand", typeof(ICommand), typeof(KeyboardBehaviours), new FrameworkPropertyMetadata(KeyDownCommandChanged));

        private static void KeyDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            element.KeyDown += element_KeyDown;
        }

        static void element_KeyDown(object sender, KeyEventArgs e)
        {
            var element = (FrameworkElement)sender;

            var command = GetKeyDownCommand(element);

            if (command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        public static void SetKeyDownCommand(UIElement element, ICommand value)
        {
            element.SetValue(KeyDownCommandProperty, value);
        }

        public static ICommand GetKeyDownCommand(UIElement element)
        {
            return (ICommand)element.GetValue(KeyDownCommandProperty);
        }
    }
}