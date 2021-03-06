﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DiscordBugBot.Config
{
    public struct WriteLock : IDisposable
    {
        private bool lockHeld;
        private ReaderWriterLockSlim rwLock;

        public WriteLock(ReaderWriterLockSlim rwLock)
        {
            this.rwLock = rwLock;
            rwLock.EnterWriteLock();
            lockHeld = true;
        }

        public void Dispose()
        {
            if (lockHeld)
            {
                rwLock.ExitWriteLock();
            }
        }
    }
}
