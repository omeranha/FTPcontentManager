using System;

namespace FTPcontentManager.Src.Stfs.Events
{
    public class ContentCountEventArgs : EventArgs
    {
        public int Count { get; private set; }

        public ContentCountEventArgs(int count)
        {
            Count = count;
        }
    }
}