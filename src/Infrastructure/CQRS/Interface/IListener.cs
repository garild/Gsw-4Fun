using System.Threading;

namespace Cqrs.Interface
{
    public interface IListener
    {
        void Listen(CancellationToken cancellationToken);
    }
}
