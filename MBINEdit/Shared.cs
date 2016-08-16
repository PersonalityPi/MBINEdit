using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace MBINEdit
{
    // ReSharper disable once InconsistentNaming
    public class IO : IDisposable
    {
        public BinaryReader Reader;
        public BinaryWriter Writer;
        public Stream Stream;

        public IO(string filePath)
        {
            Stream = new FileStream(filePath, FileMode.Open);
            InitIo();
        }

        public IO(string filePath, FileMode mode)
        {
            Stream = new FileStream(filePath, mode);
            InitIo();
        }

        public IO(Stream baseStream)
        {
            Stream = baseStream;
            InitIo();
        }

        public void Dispose()
        {
            Stream.Dispose();
            Reader.Dispose();
            Writer.Dispose();
        }

        public bool AddBytes(long numBytes)
        {
            const int blockSize = 0x1000;

            long startPos = Stream.Position;
            long startSize = Stream.Length;
            long endPos = startPos + numBytes;
            long endSize = Stream.Length + numBytes;

            Stream.SetLength(endSize);

            long totalWrite = startSize - startPos;

            while (totalWrite > 0)
            {
                int toRead = totalWrite < blockSize ? (int)totalWrite : blockSize;

                Stream.Position = startPos + (totalWrite - toRead);
                var data = Reader.ReadBytes(toRead);

                Stream.Position = startPos + (totalWrite - toRead);
                var blankData = new byte[toRead];
                Writer.Write(blankData);

                Stream.Position = endPos + (totalWrite - toRead);
                Writer.Write(data);

                totalWrite -= toRead;
            }

            Stream.Position = startPos;

            return true;
        }

        public bool DeleteBytes(long numBytes)
        {
            if (Stream.Position + numBytes > Stream.Length)
                return false;

            const int blockSize = 0x1000;

            long startPos = Stream.Position;
            long endPos = startPos + numBytes;
            long endSize = Stream.Length - numBytes;
            long i = 0;

            while (i < endSize)
            {
                long totalRemaining = endSize - i;
                int toRead = totalRemaining < blockSize ? (int)totalRemaining : blockSize;

                Stream.Position = endPos + i;
                byte[] data = Reader.ReadBytes(toRead);

                Stream.Position = startPos + i;
                Writer.Write(data);

                i += toRead;
            }

            Stream.SetLength(endSize);
            return true;
        }

        private void InitIo()
        {
            Reader = new BinaryReader(Stream);
            Writer = new BinaryWriter(Stream);
        }
    }

    static class Shared
    {
        /// <summary>
        /// Reads in a block from a file and converts it to the struct
        /// type specified by the template parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T ReadStruct<T>(this BinaryReader reader)
        {
            var size = Marshal.SizeOf(typeof(T));
            // Read in a byte array
            var bytes = reader.ReadBytes(size);

            return BytesToStruct<T>(bytes);
        }

        public static bool WriteStruct<T>(this BinaryWriter writer, T structure)
        {
            byte[] bytes = StructToBytes(structure);

            writer.Write(bytes);

            return true;
        }

        public static T BytesToStruct<T>(byte[] bytes)
        {
            // Pin the managed memory while, copy it out the data, then unpin it
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return theStructure;
        }

        public static byte[] StructToBytes<T>(T structure)
        {
            var bytes = new byte[Marshal.SizeOf(typeof(T))];

            // Pin the managed memory while, copy in the data, then unpin it
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            Marshal.StructureToPtr(structure, handle.AddrOfPinnedObject(), true);
            handle.Free();

            return bytes;
        }
    }
}
