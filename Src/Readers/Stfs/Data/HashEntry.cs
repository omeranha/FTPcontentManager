﻿using FTPcontentManager.Src.Attributes;
using FTPcontentManager.Src.Constants;
using FTPcontentManager.Src.Models;

namespace FTPcontentManager.Src.Stfs.Data
{
    public class HashEntry : BinaryModelBase
    {
        public const int Size = 0x18;

        [BinaryData(0x14)]
        public virtual byte[] BlockHash { get; set; }

        [BinaryData(0x1)]
        public virtual BlockStatus Status { get; set; }

        [BinaryData(0x3)]
        public virtual int NextBlock { get; set; }

        public int? Block { get; set; }
        public int RealBlock { get; set; }

        public HashEntry(OffsetTable offsetTable, BinaryContainer binary, int startOffset) : base(offsetTable, binary, startOffset)
        {
        }
    }
}