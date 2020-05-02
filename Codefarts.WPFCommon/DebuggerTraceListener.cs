namespace Codefarts.WPFCommon
{
    using System.Diagnostics;

    public class DebuggerTraceListener : TraceListener
    {
        public override void Write(string message)
        {
        }
 
        public override void WriteLine(string message)
        {
            Debugger.Break();
        }
    }
}