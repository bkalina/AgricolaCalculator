using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;

namespace AgricolaCalculator
{
    public class StateManager
    {
        private static PhoneApplicationService appService = PhoneApplicationService.Current;

        public static void Set<T>(string name, T value)
        {
            appService.State[name] = value;
        }

        public static T Get<T>(string name)
        {
            T result = default(T);

            if (appService.State.ContainsKey(name))
            {
                result = (T)appService.State[name];
            }

            return result;
        }
    }

}
