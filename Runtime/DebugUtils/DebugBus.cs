using System.Collections.Generic;
using Tetraizor.DebugUtils.Base;
using UnityEngine;

namespace Tetraizor.DebugUtils
{
    public delegate void LogEmitHandler(string text);

    public static class DebugBus
    {
        #region Events

        private static event LogEmitHandler PrintMessageEmitted;
        private static event LogEmitHandler WarningMessageEmitted;
        private static event LogEmitHandler ErrorMessageEmitted;

        #endregion

        #region Properties

        private static List<IDebugger> _debuggers = new();

        #endregion

        public static void Register(IDebugger debugger)
        {
            if (_debuggers.Contains(debugger))
            {
                LogWarning("This debugger is already registered.");
                return;
            }

            PrintMessageEmitted += debugger.OnPrintMessageEmitted;
            WarningMessageEmitted += debugger.OnWarningMessageEmitted;
            ErrorMessageEmitted += debugger.OnErrorMessageEmitted;

            _debuggers.Add(debugger);
        }

        public static void Deregister(IDebugger debugger)
        {
            if (!_debuggers.Contains(debugger))
            {
                LogWarning("This debugger is not registered.");
                return;
            }

            PrintMessageEmitted -= debugger.OnPrintMessageEmitted;
            WarningMessageEmitted -= debugger.OnWarningMessageEmitted;
            ErrorMessageEmitted -= debugger.OnErrorMessageEmitted;

            _debuggers.Remove(debugger);
        }

        public static void LogPrint(string message)
        {
            PrintMessageEmitted?.Invoke(message);
        }

        public static void LogWarning(string message)
        {
            WarningMessageEmitted?.Invoke(message);
        }

        public static void LogError(string message)
        {
            ErrorMessageEmitted?.Invoke(message);
        }
    }
}
