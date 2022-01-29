using System.Threading;
using System.Threading.Tasks;

namespace Com.AlertService.Alerters
{
    public interface IAlerter
    {
        Task Run(CancellationToken cancellationToken);
    }
}