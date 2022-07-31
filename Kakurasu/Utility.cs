﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakurasu
{
    public static  class Utility
    {
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max + 1);
            }
        }
        public static String NowAsString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssss");
        }
       
        public static bool IsInt(String value)
        {
            int Result = 0;
            return int.TryParse(value, out Result);

        }
        public static int ToInt(this String value)
        {
            return int.Parse(value);
        }
        
        public static Boolean IsBetween(this int value, int MinValue, int MaxValue)
        {

            return (value >= MinValue && value <= MaxValue);
        }
        public static Boolean IsBetween(this String value, int MinValue, int MaxValue)
        {

            return (value.ToInt() >= MinValue && value.ToInt() <= MaxValue);
        }
        
        public static Position From1DimensionTo2(int index, int NoofRow)
        {
            Position P = new Position(index / NoofRow, index % NoofRow);
            return P;
        }
        public static int From2DimensionTo1(Position pos, int RowSize)
        {
            int Result = (pos.Row * (RowSize)) + pos.Column;
           
            return Result;
        }
    }
}
