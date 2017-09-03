﻿using System;
using System.Runtime.CompilerServices;

namespace dotNES.Mappers
{
    abstract class AbstractMapper : IAddressable
    {
        protected readonly Emulator _emulator;
        protected readonly byte[] _prgROM;
        protected readonly byte[] _prgRAM = new byte[0x2000];
        protected readonly byte[] _chrROM;
        protected readonly uint _lastBankOffset;

        public AbstractMapper(Emulator emulator)
        {
            _emulator = emulator;
            var cart = emulator.Cartridge;
            _prgROM = cart.PRGROM;
            _chrROM = cart.CHRROM;
            _lastBankOffset = (uint)_prgROM.Length - 0x4000;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract uint ReadByte(uint addr);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual uint ReadBytePPU(uint addr)
        {
            if (addr < 0x2000)
            {
                return _chrROM[addr];
            }
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void WriteByte(uint addr, uint val);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void WriteBytePPU(uint addr, uint val)
        {
            if (addr < 0x2000)
            {
                _chrROM[addr] = (byte) val;
            }
            else throw new NotImplementedException();
        }
    }
}