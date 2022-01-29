using System;
using Com.AlertService.Alerters;

namespace Com.AlertService.Notifiers
{
    public class ConsoleNotifier : INotifier
    {
        public void Notify(double value, string sensor, AlertType alertType)
        {
            Console.WriteLine($"A {alertType} alert has been identifier on the {sensor} " +
                $"sensor, wich catch a value of {value}");
        }
    }
}