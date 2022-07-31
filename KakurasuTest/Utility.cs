using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kakurasu;
namespace KakurasuTest
{
    public class Utility
    {
        public static Boolean IsBoardInputValueTheSame(KakurasuBoard board1,KakurasuBoard board2)
        {
            int i;
            int j;
            if(board1.IsFinshed != board2.IsFinshed)
            {
                return false;
            }
            if(board1.RowSize != board2.RowSize ||
                board1.ColumnSize != board2.ColumnSize)
            {
                return false ;
            }

            for (i = 0; i < board1.RowSize; i++)
            {
                for (j = 0; j < board1.ColumnSize; j++)
                {
                    if(board1[i,j] != board2[i, j])
                    {
                        return false;
                    }
                    if(board1.CorrectCellvalueMatrix [i,j] != board2.CorrectCellvalueMatrix[i, j])
                    {
                        return false;
                    }
                }
            }


            for (i = 0; i <= board1.CorrectRowSumAnswerlist.GetUpperBound(0); i++)
            {
                if(board1.CorrectRowSumAnswerlist [i] != board2.CorrectRowSumAnswerlist[i])
                {
                    return false;
                }
               
            }
            for (i = 0; i <= board1.CorrectColumnSumAnswerlist.GetUpperBound(0); i++)
            {
                if (board1.CorrectColumnSumAnswerlist[i] != board2.CorrectColumnSumAnswerlist[i])
                {
                    return false;
                }

            }
          
            for (i = 0; i <= board1.RowAnswerResultlist.GetUpperBound(0); i++)
            {
                if (board1.RowAnswerResultlist [i] != board2.RowAnswerResultlist[i])
                {
                    return false;
                }
            }

            for (i = 0; i <= board1.ColAnswerResultlist.GetUpperBound(0); i++)
            {
                if (board1.ColAnswerResultlist[i] != board2.ColAnswerResultlist[i])
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
