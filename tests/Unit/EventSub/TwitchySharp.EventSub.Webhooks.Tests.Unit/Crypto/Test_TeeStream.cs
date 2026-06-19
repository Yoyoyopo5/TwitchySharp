using System.Text;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit.Crypto;

public class Test_TeeStream
{
    [Fact]
    public void Read_TeeStream_DestinationContainsSourceBytes()
    {
        const string SOURCE_STRING = "test_stream_content";

        using MemoryStream source = new(Encoding.UTF8.GetBytes(SOURCE_STRING));
        using MemoryStream dest = new();

        using TeeStream tee = new(source, dest, true);

        byte[] sourceBuffer = new byte[source.Length];
        byte[] destBuffer = new byte[source.Length];

        int sourceBytesRead = tee.Read(sourceBuffer, 0, (int)source.Length);

        dest.Position = 0;
        int destBytesRead = dest.Read(destBuffer, 0, (int)source.Length);

        Assert.Equal(sourceBuffer, destBuffer);
    }

    [Fact]
    public async Task ReadAsync_TeeStream_DestinationContainsSourceBytes()
    {
        const string SOURCE_STRING = "test_stream_content";

        await using MemoryStream source = new(Encoding.UTF8.GetBytes(SOURCE_STRING));
        await using MemoryStream dest = new();

        await using TeeStream tee = new(source, dest, true);

        Memory<byte> sourceBuffer = new byte[source.Length];
        Memory<byte> destBuffer = new byte[source.Length];

        int sourceBytesRead = await tee.ReadAsync(sourceBuffer, TestContext.Current.CancellationToken);

        dest.Position = 0;
        int destBytesRead = await dest.ReadAsync(destBuffer, TestContext.Current.CancellationToken);

        Assert.Equal(sourceBuffer.ToArray(), destBuffer.ToArray());
    }

    [Fact]
    public async Task Dispose_LeaveOpenTeeStream_DoesNotDisposeStreams()
    {
        using MemoryStream source = new();
        using MemoryStream dest = new();

        TeeStream tee = new(source, dest, true);
        await tee.DisposeAsync();

        Assert.True(source.CanRead);
        Assert.True(dest.CanRead);
    }
}
