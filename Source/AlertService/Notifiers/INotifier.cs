using Com.AlertService.Alerters;

namespace Com.AlertService.Notifiers
{
    public interface INotifier
    {
        void Notify(float value, string sensorType, AlertType alertType);
    }
}