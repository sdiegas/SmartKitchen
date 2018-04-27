using System;
using System.Diagnostics;
using Hsr.CloudSolutions.SmartKitchen.Util;

namespace Hsr.CloudSolutions.SmartKitchen.UI.Communication
{
    public abstract class BaseClient 
        : IDisposable
    {
        protected void LogException(string message, Exception ex)
        {
            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff}: {message}\r\n{ex.CreateExceptionDialogMessage()}");
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            
        }
    }
}
