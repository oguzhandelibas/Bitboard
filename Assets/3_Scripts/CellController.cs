using UnityEngine;
using System;
using UnityEngine.UI;

namespace Bitboard
{
    public struct CellController
    {
        private Text _score;
        public CellController(Text score)
        {
            _score = score;
        }

        public void PrintBB(string name, long BB)
        {
            Debug.Log(name + ": " + Convert.ToString(BB, 2).PadLeft(64, '0'));
        }
        public long SetCellState(long bitboard, int row, int col)
        {
            long newBit = 1L << (row * 8 + col);
            return (bitboard |= newBit);
        }
        public bool GetCellState(long bitboard, int row, int col)
        {
            long mask = 1L << (row * 8 + col);
            return ((bitboard & mask) != 0);
        }
        public int CellCount(long bitboard)
        {
            int count = 0;
            long bb = bitboard;
            while (bb != 0)
            {
                bb &= bb - 1;
                count++;
            }

            return count;
        }
    }
}
