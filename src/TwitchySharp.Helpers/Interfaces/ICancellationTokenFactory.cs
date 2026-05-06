using System.Threading;

namespace TwitchySharp.Helpers.Interfaces;

public interface ICancellationTokenFactory
{
    CancellationToken CreateCancellationToken();
}
