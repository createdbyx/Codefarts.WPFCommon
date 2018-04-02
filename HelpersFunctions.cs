namespace Codefarts.WPFCommon
{
    using System;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Media;

    internal class HelpersFunctions
    {
        public static IWin32Window GetIWin32Window(Visual visual)
        {
            var source = PresentationSource.FromVisual(visual) as System.Windows.Interop.HwndSource;
            var win = new LegacyWindowHandle(source.Handle);
            return win;
        }

        private class LegacyWindowHandle : IWin32Window
        {
            private readonly IntPtr controlHandle;

            public LegacyWindowHandle(IntPtr controlHandle)
            {
                this.controlHandle = controlHandle;
            }

            #region IWin32Window Members

            IntPtr IWin32Window.Handle
            {
                get
                {
                    return this.controlHandle;
                }
            }

            #endregion
        }
    }
}