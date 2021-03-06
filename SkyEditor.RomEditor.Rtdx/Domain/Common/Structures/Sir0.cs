﻿using SkyEditor.IO.Binary;
using System;
using System.Collections.Generic;

namespace SkyEditor.RomEditor.Domain.Common.Structures
{
    public sealed class Sir0 : IDisposable
    {
        public Sir0(IReadOnlyBinaryDataAccessor data)
        {
            Data = data;
            Init();
        }

        public Sir0(byte[] data, long offset, long length)
        {
            BinaryFile = new BinaryFile(data);
            Data = (BinaryFile as IReadOnlyBinaryDataAccessor).Slice(offset, length);
            Init();
        }

        public Sir0(byte[] data)
        {
            BinaryFile = new BinaryFile(data);
            Data = BinaryFile;
            Init();
        }

        private void Init()
        {
            Magic = Data.ReadInt32(0);

            // Detect pointer size
            // For a 32bit SIR0, 0x04 should contain the sub header pointer
            // For a 64bit SIR0, 0x04 instead contains 4 null chars at the end of Magic, the sub header being at 0x8 instead
            var int04 = Data.ReadInt32(4);
            if (int04 == 0)
            {
                pointerSize = 8;
            }
            else
            {
                pointerSize = 4;
            }

            var offset = pointerSize;
            SubHeaderOffset = Data.ReadInt32(offset); offset += pointerSize;
            FooterOffset = Data.ReadInt32(offset); offset += pointerSize;
            SubHeader = Data.Slice(SubHeaderOffset, FooterOffset - SubHeaderOffset);

            PointerOffsets = new List<long>();
            long pointerIndex = 0;
            var currentFooterOffset = FooterOffset;
            var rawByte = Data.ReadByte(currentFooterOffset++);
            while (rawByte != 0)
            {
                if (rawByte < 0x80)
                {
                    pointerIndex += rawByte;
                    PointerOffsets.Add(pointerIndex);
                    rawByte = Data.ReadByte(currentFooterOffset++);
                }
                else
                {
                    long workingPointer;
                    do
                    {
                        workingPointer = rawByte & 0x7F;
                        workingPointer <<= 7;
                        rawByte = Data.ReadByte(currentFooterOffset++);
                    } while (rawByte >= 0x80);
                    pointerIndex += workingPointer;
                    PointerOffsets.Add(pointerIndex);
                }
            }
        }

        private int pointerSize;

        private BinaryFile? BinaryFile { get; }
        public int Magic { get; private set; }
        public long SubHeaderOffset { get; private set; }
        public long FooterOffset { get; private set; }

        /// <summary>
        /// The raw data of the SIR0 file
        /// </summary>
        public IReadOnlyBinaryDataAccessor Data { get; }

        /// <summary>
        /// The portion of <see cref="Data"/> that is the sub-header
        /// </summary>
        public IReadOnlyBinaryDataAccessor SubHeader { get; private set; } = default!;

        /// <summary>
        /// Offsets of 64 bit pointers in <see cref="Data"/>
        /// </summary>
        public List<long> PointerOffsets { get; private set; } = default!;

        public void Dispose()
        {
            BinaryFile?.Dispose();
        }
    }
}
