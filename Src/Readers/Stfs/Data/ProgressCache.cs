﻿using System;
using FTPcontentManager.Src.Attributes;
using FTPcontentManager.Src.Constants;
using FTPcontentManager.Src.Models;

namespace FTPcontentManager.Src.Stfs.Data
{
    public class ProgressCache : BinaryModelBase, IInstallerInformation
    {
        [BinaryData(4)]
        public virtual OnlineContentResumeState ResumeState { get; set; }

        [BinaryData]
        public virtual uint CurrentFileIndex { get; set; }

        [BinaryData]
        public virtual uint CurrentFileOffset { get; set; }

        [BinaryData]
        public virtual long ByteProcessed { get; set; }

        [BinaryData]
        public virtual DateTime LastModified { get; set; }

        [BinaryData(0x15D0)]
        public virtual byte[] CabResumeData { get; set; }

        public ProgressCache(OffsetTable offsetTable, BinaryContainer binary, int startOffset) : base(offsetTable, binary, startOffset)
        {
        }
    }
}