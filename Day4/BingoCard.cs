using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day4
{
    internal class BingoCard
    {
        private int[] myValues;
        private bool[] hasValueBeen;
        private int sizeX, sizeY;

        public BingoCard(string[] values, int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.myValues = new int[sizeX * sizeY];
            this.hasValueBeen = new bool[sizeX * sizeY];
            for (int i = 0; i < values.Length; i++)
            {
                string value = values[i];
                string[] values2 = value.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                for (int j = 0; j < values2.Length; j++)
                {
                    if (int.TryParse(values2[j].Trim(), out int myLocalValues))
                    {
                        myValues[i * sizeX + j] = myLocalValues;
                    }
                }
            }
        }

        public void AddValue(int value)
        {
            for (int i = 0; i < myValues.Length / sizeX; i++)
            {
                for (int j = 0; j < myValues.Length / sizeY; j++)
                {
                    if (myValues[i * sizeX + j] == value)
                    {
                        hasValueBeen[i * sizeX + j] = true;
                    }
                }
            }
        }

        public bool HasBingo()
        {
            bool returnable = false;
            for (int i = 0; i < myValues.Length / sizeX; i++)
            {
                if (returnable || CheckColumn(i))
                {
                    returnable = true;
                    break;
                }
            }
            for (int i = 0; i < myValues.Length / sizeY; i++)
            {
                if (returnable || CheckRow(i))
                {
                    returnable = true;
                    break;
                }
            }
            return returnable;
        }

        public int GetFinalValue(int lastValueToMultiplyWith)
        {
            int unmarkedValues = 0;
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (!hasValueBeen[x * sizeX + y])
                    {
                        unmarkedValues += myValues[x * sizeX + y];
                    }
                }
            }
            return unmarkedValues * lastValueToMultiplyWith;
        }

        private bool CheckColumn(int index)
        {
            bool returnable = true;
            for (int i = 0; i < myValues.Length / sizeY; i++)
            {
                returnable = returnable && hasValueBeen[index * sizeX + i];
            }
            return returnable;
        }
        private bool CheckRow(int index)
        {
            bool returnable = true;
            for (int i = 0; i < myValues.Length / sizeY; i++)
            {
                returnable = returnable && hasValueBeen[i * sizeX + index];
            }
            return returnable;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    s += $"{(hasValueBeen[i * sizeX + j] ? "+" : myValues[i * sizeX + j])}\t";
                }
                s += '\n';
            }
            return s;
        }
    }
}
