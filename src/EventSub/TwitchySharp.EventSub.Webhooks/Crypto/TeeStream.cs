namespace TwitchySharp.EventSub.Webhooks;

/// <summary>
/// Helper class for splitting a <see cref="Stream"/>, allowing the original stream to be consumed once while writing to a second stream concurrently.
/// </summary>
/// <param name="origin">The origin stream.</param>
/// <param name="destination">The stream that the origin will be copied to as it is read.</param>
/// <param name="leaveOpen">Determines whether the streams will be disposed upon disposing this instance. Defaults to <see langword="false"/>.</param>
internal sealed class TeeStream(Stream origin, Stream destination, bool leaveOpen = false) : Stream
{
    private readonly Stream _origin = origin;
    private readonly Stream _dest = destination;
    private readonly bool _leaveOpen = leaveOpen;

    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken ct)
    {
        int bytesRead = await _origin.ReadAsync(buffer, ct).ConfigureAwait(false);
        if (bytesRead > 0)
            await _dest.WriteAsync(buffer[..bytesRead], ct).ConfigureAwait(false);
        return bytesRead;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _origin.Read(buffer, offset, count);
        if (bytesRead > 0)
            _dest.Write(buffer, offset, count);
        return bytesRead;
    }

    public override int Read(Span<byte> buffer)
    {
        int bytesRead = _origin.Read(buffer);
        if (bytesRead > 0)
            _dest.Write(buffer[..bytesRead]);
        return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    public override bool CanRead => _origin.CanRead;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => _origin.Length;
    public override long Position { get => _origin.Position; set => throw new NotSupportedException(); }
    public override void Flush()
    {
        _origin.Flush();
        _dest.Flush();
    }
    public override async Task FlushAsync(CancellationToken ct)
    {
        await _origin.FlushAsync(ct).ConfigureAwait(false);
        await _dest.FlushAsync(ct).ConfigureAwait(false);
    }

    public new void Dispose(bool disposing)
    {
        if (disposing && !_leaveOpen)
        {
            _origin.Dispose();
            _dest.Dispose();
        }
        base.Dispose(disposing);
    }

    public async override ValueTask DisposeAsync()
    {
        if (!_leaveOpen)
        {
            await _origin.DisposeAsync().ConfigureAwait(false);
            await _dest.DisposeAsync().ConfigureAwait(false);
        }
        await base.DisposeAsync().ConfigureAwait(false);
    }
}
