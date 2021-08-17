using System;
using System.Windows.Threading;

namespace KafeProject.Infrastructure
{
    public static class Extantions
    {
        public static void InvokeOrExecute(this Dispatcher dispatcher, Action action)
        {
            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                       action);
            }
        }
    }
}