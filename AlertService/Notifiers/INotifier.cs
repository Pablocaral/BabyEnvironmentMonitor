using Com.AlertService.Alerters;

namespace Com.AlertService.Notifiers
{
    public interface INotifier
    {
        void Notify(double value, string sensorType, AlertType alertType);
    }
}