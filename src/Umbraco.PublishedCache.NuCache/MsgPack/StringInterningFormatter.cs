using System;
using System.Text;
using MessagePack;
using MessagePack.Formatters;
using Microsoft.NET.StringTools;

namespace Umbraco.Cms.Infrastructure.PublishedCache.MsgPack
{
    /// <summary>
    /// A <see cref="string" /> formatter that interns strings on deserialization.
    /// </summary>
    public sealed class StringInterningFormatter : IMessagePackFormatter<string?>
    {
        /// <inheritdoc/>
        public string? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            MessagePackReader retryReader = reader;
            if (reader.TryReadStringSpan(out ReadOnlySpan<byte> bytes))
            {
                if (bytes.Length < 4096)
                {
                    if (bytes.Length == 0)
                    {
                        return string.Empty;
                    }

                    Span<char> chars = stackalloc char[bytes.Length];
                    int charLength;
                    charLength = StringEncoding.UTF8.GetChars(bytes, chars);
                    return Strings.WeakIntern(chars.Slice(0, charLength));
                }
                else
                {
                    // Rewind the reader to the start of the string because we're taking the slow path.
                    reader = retryReader;
                }
            }

            return Strings.WeakIntern(reader.ReadString());
        }

        /// <inheritdoc/>
        public void Serialize(ref MessagePackWriter writer, string? value, MessagePackSerializerOptions options) => writer.Write(value);
    }
    internal static class StringEncoding
    {
        internal static readonly Encoding UTF8 = new UTF8Encoding(false);

#if !NETCOREAPP // Define the extension method only where an instance method does not already exist.
        internal static unsafe string GetString(this Encoding encoding, ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length == 0)
            {
                return string.Empty;
            }

            fixed (byte* pBytes = bytes)
            {
                return encoding.GetString(pBytes, bytes.Length);
            }
        }
#endif
    }
}
