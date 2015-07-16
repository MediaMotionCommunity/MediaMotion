using System;
using System.Runtime.InteropServices;

namespace MediaMotion.Core.Utils
{
    /// <summary>
    /// Wrap a C# object to be accessible as C pointer
    /// </summary>
    public class AutoPinner : object
    {
        /**
         * Class internals
         */
        protected object     _obj;
        protected GCHandle   _obj_handle;

        /**
         * Constructor (pinn an object)
         */
        public AutoPinner(object obj)
        {
            _obj = obj;
            _obj_handle = GCHandle.Alloc(_obj, GCHandleType.Pinned);
        }

        /**
         * Destructor (garbage collect)
         */
        ~AutoPinner()
        {
            _obj_handle.Free();
        }

        /**
         * Access the pinned object pointer
         */
        public IntPtr Ptr()
        {
            return _obj_handle.AddrOfPinnedObject();
        }

        /**
         * Access the pinned object
         */
        public object Obj()
        {
            return _obj;
        }
    }
}
