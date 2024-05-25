using System;
using VG.Internal;

namespace VG
{
    public abstract class SaveService : Service
    {
        public abstract string GetData();
        public abstract void Commit(string data, Action<bool> onCommited);


    }
}


