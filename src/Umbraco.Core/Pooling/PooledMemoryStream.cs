using System.Buffers;
using Microsoft.IO;

namespace Umbraco.Cms.Core.Pooling
{
    /// <summary>
    /// Pooled Memory Stream.
    /// </summary>
    /// <remarks>Dispose to return to the pool.</remarks>
    public class PooledMemoryStream : Stream, IBufferWriter<byte>
    {
        private readonly RecyclableMemoryStream _recyclableMemoryStream;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recyclableMemoryStream">RecyclableMemoryStream</param>
        public PooledMemoryStream(RecyclableMemoryStream recyclableMemoryStream)
        {
            _recyclableMemoryStream = recyclableMemoryStream;
        }

        /// <inheritdoc/>
        public override bool CanRead => _recyclableMemoryStream.CanRead;

        /// <inheritdoc/>
        public override bool CanSeek => _recyclableMemoryStream.CanSeek;

        /// <inheritdoc/>
        public override bool CanWrite => _recyclableMemoryStream.CanWrite;

        /// <inheritdoc/>
        public override long Length => _recyclableMemoryStream.Length;

        /// <inheritdoc/>
        public override long Position { get => _recyclableMemoryStream.Position; set => _recyclableMemoryStream.Position = value; }

        public void Advance(int count) => _recyclableMemoryStream.Advance(count);

        /// <inheritdoc/>
        public override void Flush() => _recyclableMemoryStream.Flush();

        /// <inheritdoc/>
        public Memory<byte> GetMemory(int sizeHint = 0) => _recyclableMemoryStream.GetMemory(sizeHint);

        /// <inheritdoc/>
        public Span<byte> GetSpan(int sizeHint = 0) => _recyclableMemoryStream.GetSpan(sizeHint);

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count) => _recyclableMemoryStream.Read(buffer, offset, count);

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin) => _recyclableMemoryStream.Seek(offset, origin);

        /// <inheritdoc/>
        public override void SetLength(long value) => _recyclableMemoryStream.SetLength(value);

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count) => _recyclableMemoryStream.Write(buffer, offset, count);
    }
}
