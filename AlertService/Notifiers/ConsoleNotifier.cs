using System;
using System.Reflection;
using Com.AlertService.Alerters;
using log4net;

namespace Com.AlertService.Notifiers
{
    public class ConsoleNotifier : INotifier
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public void Notify(float value, string sensor, AlertType alertType)
        {
            Console.WriteLine($"A {alertType} alert has been identifier on the {sensor} " +
                $"sensor, wich catch a value of {value}");
            
            log.Info("A notification had been sent from Console Notifier " + 
                $"AlertType: {alertType}, Sensor: {sensor}, Value: {value}");
        }
    }
}