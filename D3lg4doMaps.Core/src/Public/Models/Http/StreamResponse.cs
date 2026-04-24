namespace DelgadoMaps.Core.Models.Http;

/// <summary>
/// Represents a streamed HTTP response, encapsulating both the response <see cref="Stream"/>
/// and its underlying <see cref="HttpResponseMessage"/>.
/// </summary>
/// <remarks>
/// This type provides controlled access to a streaming HTTP response and ensures proper
/// resource management by owning the lifetime of both the response stream and the associated
/// HTTP response message.
/// <para>
/// It is designed for endpoints that return large or streaming payloads, allowing consumers
/// to process the response incrementally without buffering the entire content into memory.
/// </para>
/// <para>
/// <b>Ownership:</b> This type owns the underlying resources. Consumers should dispose this
/// instance when they are finished using it, unless it is passed to a higher-level API
/// (e.g., a deserializer) that explicitly takes responsibility for disposal.
/// </para>
/// </remarks>
public sealed class StreamResponse : IDisposable, IAsyncDisposable {
    private bool _disposed;

    /// <summary>
    /// Gets the response content stream.
    /// </summary>
    /// <remarks>
    /// The returned stream is tied to the underlying HTTP response and must not be accessed
    /// after the <see cref="StreamResponse"/> has been disposed.
    /// </remarks>
    public Stream Stream { get; }

    /// <summary>
    /// Gets the underlying HTTP response message.
    /// </summary>
    /// <remarks>
    /// Provides access to the raw <see cref="HttpResponseMessage"/> for scenarios such as
    /// inspecting headers, status codes, or diagnostics.
    /// </remarks>
    public HttpResponseMessage ResponseMessage { get; }

    internal StreamResponse(Stream stream, HttpResponseMessage responseMessage) {
        Stream = stream;
        ResponseMessage = responseMessage;
    }

    /// <summary>
    /// Releases all resources used by the <see cref="StreamResponse"/>.
    /// </summary>
    /// <remarks>
    /// Disposes both the <see cref="Stream"/> and the associated <see cref="HttpResponseMessage"/>.
    /// <para>
    /// This method is idempotent and safe to call multiple times.
    /// </para>
    /// <para>
    /// After disposal, accessing <see cref="Stream"/> or <see cref="ResponseMessage"/> is unsafe
    /// and may result in exceptions.
    /// </para>
    /// </remarks>
    public void Dispose() {
        if (_disposed) return;

        Stream.Dispose();
        ResponseMessage.Dispose();

        _disposed = true;
    }

    /// <summary>
    /// Asynchronously releases all resources used by the <see cref="StreamResponse"/>.
    /// </summary>
    /// <remarks>
    /// If the underlying stream supports asynchronous disposal, it will be disposed
    /// asynchronously; otherwise, a synchronous disposal is performed.
    /// <para>
    /// This method is idempotent and safe to call multiple times.
    /// </para>
    /// </remarks>
    public async ValueTask DisposeAsync() {
        if (_disposed) return;

        if (Stream is IAsyncDisposable asyncStream)
            await asyncStream.DisposeAsync().ConfigureAwait(false);
        else
            Stream.Dispose();

        ResponseMessage.Dispose();

        _disposed = true;
    }
}