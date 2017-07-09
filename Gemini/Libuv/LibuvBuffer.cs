using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RedisConnector.Libuv
{
    internal class LibuvBuffer
    {
        public readonly static LibuvBuffer Default = new LibuvBuffer();

        public static LibuvManaged.alloc_callback_unix AllocateUnixBuffer { get; set; }
        public static LibuvManaged.alloc_callback_win AllocWindowsBuffer { get; set; }

        private LibuvBuffer() { }

        static LibuvBuffer()
        {
            AllocateUnixBuffer = OnAllocateUnixBuffer;
            AllocWindowsBuffer = OnAllocateWindowsBuffer;
        }

        static void OnAllocateUnixBuffer(IntPtr memoryBuffer, uint length, out Unix buffer)
        {
            var memory = Marshal.AllocHGlobal((int)length);
            buffer = new Unix(memory, length);
        }

        static void OnAllocateWindowsBuffer(IntPtr memoryBuffer, uint length, out Windows buffer)
        {
            var memory = Marshal.AllocHGlobal((int)length);
            buffer = new Windows(memory, length);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Windows
        {
            internal uint Length;
            internal IntPtr Buffer;

            internal Windows(IntPtr buffer, uint length)
            {
                Buffer = buffer;
                Length = length;
            }

            internal void Dispose()
            {
                Marshal.FreeHGlobal(Buffer);
                Length = 0;
                Buffer = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Unix
        {
            internal IntPtr Buffer;
            internal IntPtr Length;

            internal Unix(IntPtr buffer, uint length)
            {
                Buffer = buffer;
                Length = (IntPtr)length;
            }

            internal void Dispose()
            {
                Marshal.FreeHGlobal(Buffer);
                Length = IntPtr.Zero;
                Buffer = IntPtr.Zero;
            }
        }
    }
}
