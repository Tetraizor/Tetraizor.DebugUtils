namespace Tetraizor.DebugUtils.Base
{
    public interface IDebugger
    {
        public void OnPrintMessageEmitted(string message);
        public void OnWarningMessageEmitted(string message);
        public void OnErrorMessageEmitted(string message);
    }
}