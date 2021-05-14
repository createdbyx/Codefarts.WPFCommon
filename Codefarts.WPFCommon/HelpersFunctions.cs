namespace Codefarts.WPFCommon
{
    using System;
    using System.Windows;
#if NETCOREAPP3_1 || NET5_0_OR_GREATER
    using System.Windows.Interop;
#else
    using System.Windows.Forms;
#endif
    using System.Windows.Media;

    internal class HelpersFunctions
    {
#if NETCOREAPP3_1 || NET5_0_OR_GREATER
        public static System.Windows.Forms.IWin32Window GetIWin32WindowForm(Visual visual)
        {
            var source = PresentationSource.FromVisual(visual) as System.Windows.Interop.HwndSource;
            var win = new LegacyWindowsFormHandle(source.Handle);
            return win;
        }

        private class LegacyWindowsFormHandle : System.Windows.Forms.IWin32Window
        {
            private readonly IntPtr controlHandle;

            public LegacyWindowsFormHandle(IntPtr controlHandle)
            {
                this.controlHandle = controlHandle;
            }

            IntPtr System.Windows.Forms.IWin32Window.Handle
            {
                get
                {
                    return this.controlHandle;
                }
            }
        }
#endif

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

            IntPtr IWin32Window.Handle
            {
                get
                {
                    return this.controlHandle;
                }
            }
        }
    }
}