using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RedisConnector.Libuv
{
    public class LibuvLoop // TODO: implement IDisposable to shut down the loop
    {
        static LibuvLoop s_default;

        IntPtr _handle;
        LibuvBuffer _pool;

        public static LibuvLoop Default
        {
            get
            {
                if (s_default == null)
                {
                    s_default = new LibuvLoop(LibuvManaged.uv_default_loop(), LibuvBuffer.Default);
                }
                return s_default;
            }
        }

        public LibuvLoop()
        {
            _pool = LibuvBuffer.Default;
            var size = LibuvManaged.uv_loop_size().ToInt32();
            var loopHandle = Marshal.AllocHGlobal(size); // this needs to be deallocated
            _handle = loopHandle;
        }

        LibuvLoop(IntPtr handle, LibuvBuffer pool)
        {
            _handle = handle;
            _pool = pool;
        }

        public IntPtr Handle
        {
            get
            {
                return _handle;
            }
        }

        internal LibuvBuffer Pool { get { return _pool; } }

        public bool IsAlive
        {
            get
            {
                return LibuvManaged.uv_loop_alive(_handle) != 0;
            }
        }

        public void Run()
        {
            LibuvManaged.uv_run(_handle, uv_run_mode.UV_RUN_DEFAULT);
        }

        public void Stop()
        {
            LibuvManaged.uv_stop(_handle);
        }
    }
}
