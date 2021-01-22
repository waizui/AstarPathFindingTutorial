using System;
using System.Collections;
using System.Collections.Generic;

namespace CellGrid.Core
{


    public class FourDArray<T>
    {
        public const int RowSize = 4096; //初始化
        public const int RowSizeBits = 12;
        public const int RowSizeModBits = 0xfff;//binary 111111111111
        public const int ColSize = 256;  //目前先用256 以后再扩展
        public const int ColSizeBits = 8;
        public const int ColSizeModBits = 0xff;//binary 1111111 

        private T[][][][] rows;

        public FourDArray()
        {
            this.rows = new T[RowSize][][][];
        }

        private int maxRow = -1, maxCol = -1;
        public int MaxRow { get { return maxRow; } set { maxRow = value; } }
        public int MaxCol { get { return maxCol; } set { maxCol = value; } }

        public int RowCapacity { get { return 1048576; } }
        public int ColCapacity { get { return 32768; } }

        public T this[int row, int col]
        {
            get
            {
                //equivalent row/power(2,12)  eg row/4096 相当于把行转为4darry树 的索引 
                int l1row = row >> RowSizeBits;
                var l1cols = this.rows[l1row];
                if (l1cols == null) return default(T);

                int l1col = col >> ColSizeBits;
                var l2rows = l1cols[l1col];
                if (l2rows == null) return default(T);

                int l2row = row & RowSizeModBits; // % RowSize;
                var l2cols = l2rows[l2row];
                if (l2cols == null) return default(T);

                int l2col = col & ColSizeModBits; // % ColSize;
                return l2cols[l2col];
            }
            set
            {
                int l1row = row >> RowSizeBits;
                var l1cols = this.rows[l1row];

                if (l1cols == null)
                {
                    l1cols = new T[ColSize][][];
                    this.rows[l1row] = l1cols;
                }

                int l1col = col >> ColSizeBits;
                var l2rows = l1cols[l1col];

                if (l2rows == null)
                {
                    l2rows = new T[RowSize][];
                    l1cols[l1col] = l2rows;
                }

                int l2row = row & RowSizeModBits;
                var l2cols = l2rows[l2row];

                if (l2cols == null)
                {
                    l2cols = new T[ColSize];
                    l2rows[l2row] = l2cols;
                }

                int l2col = col & ColSizeModBits;
                l2cols[l2col] = value;

                if (value != null)
                {
                    if (maxRow < row) maxRow = row;
                    if (maxCol < col) maxCol = col;
                }
            }
        }

        /// <summary>
        ///  一行内从左到右迭代    返回向右增加的列数
        /// </summary>
        /// <param name="iterator"></param>
        public void Iterate(Func<int, int, T, int> iterator)
        {
            Iterate(0, 0, maxRow + 1, maxCol + 1, true, iterator);
        }

        /// <summary>
        /// 不跳过null单元格的迭代
        /// </summary>
        /// <param name="iterator"></param>
        public void IterateWithNull(Func<int, int, T, int> iterator)
        {
            Iterate(0, 0, maxRow + 1, maxCol + 1, false, iterator);
        }

        public void Iterate(int row, int col, int rows, int cols, bool ignoreNull, Func<int, int, T, int> iterator)
        {
            int endrow = row + rows;
            int endcol = col + cols;

            for (int r = row; r < endrow; r++)
            {
                int l1row = r >> RowSizeBits;
                var l1cols = this.rows[l1row];
                if (l1cols == null)
                {
                    if (!ignoreNull)
                    {
                        var dt = default(T);

                        for (int c = col; c < endcol;)
                        {
                            int cspan = iterator(r, c, dt);
                            if (cspan < 1) return;
                            c += cspan;
                        }
                    }

                    continue;
                }

                for (int c = col; c < endcol;)
                {
                    int l1col = c >> ColSizeBits;
                    var l2rows = l1cols[l1col];
                    if (l2rows == null)
                    {
                        if (ignoreNull) { c++; }
                        else
                        {
                            int cspan = iterator(r, c, default(T));
                            if (cspan < 1) return;
                            c += cspan;
                        }
                        continue;
                    }

                    int l2row = r % RowSize;
                    var l2cols = l2rows[l2row];
                    if (l2cols == null)
                    {
                        if (ignoreNull) { c++; }
                        else
                        {
                            int cspan = iterator(r, c, default(T));
                            if (cspan < 1) return;
                            c += cspan;
                        }
                        continue;
                    }

                    int l2col = c % ColSize;

                    T value = l2cols[l2col];

                    if (value == null && ignoreNull)
                    {
                        c++;
                    }
                    else
                    {
                        int cspan = iterator(r, c, value);
                        if (cspan < 1) return;
                        c += cspan;
                    }
                }
            }
        }

        internal void Reset()
        {
            this.rows = new T[RowSize][][][];
            this.maxCol = -1;
            this.maxRow = -1;
        }

    }

}