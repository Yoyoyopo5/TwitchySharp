namespace TwitchySharp.Api.Tests.Integration;

public sealed class AccumulatingDisposable : IDisposable
{
    private readonly List<IDisposable> _disposables = [];
    public AccumulatingDisposable Add(IDisposable disposable)
    {
        _disposables.Add(disposable);
        return this;
    }
    public void Dispose()
    {
        foreach (IDisposable disposable in _disposables)
            disposable.Dispose();
    }
}
