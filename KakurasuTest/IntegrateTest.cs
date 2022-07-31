using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kakurasu;
using Kakurasu.UI;
using System.Diagnostics;

namespace KakurasuTest
{
    [TestClass]
    public class IntegrateTest
    {
        /*
         Please look into BoardImage\Board_For_Testing.PNG
         That is the board we use the value to configure the test
         */
       
        List<Kakurasu.Position> list = new List<Kakurasu.Position>();
        private Boolean HasInitial = false;
        Kakurasu.KakurasuBoard OriginalBoard = null;
        private void Initial()
        {
            if (HasInitial)
            {
                return;
            }
            list.Add(new Kakurasu.Position(0, 3));
            list.Add(new Kakurasu.Position(0, 4));
            list.Add(new Kakurasu.Position(1, 0));
            list.Add(new Kakurasu.Position(1, 3));
            list.Add(new Kakurasu.Position(1, 4));
            list.Add(new Kakurasu.Position(2, 1));
            list.Add(new Kakurasu.Position(2, 2));
            list.Add(new Kakurasu.Position(2, 3));
            list.Add(new Kakurasu.Position(3, 1));
            list.Add(new Kakurasu.Position(3, 2));
            list.Add(new Kakurasu.Position(3, 4));
            list.Add(new Kakurasu.Position(4, 0));
            list.Add(new Kakurasu.Position(4, 1));
            list.Add(new Kakurasu.Position(4, 2));
            list.Add(new Kakurasu.Position(4, 4));


            Kakurasu.KakurasuBoard.ManualGenerator manualGenerator = new KakurasuBoard.ManualGenerator(list);

            OriginalBoard = new Kakurasu.KakurasuBoard(5, 5, manualGenerator);
            OriginalBoard.GenerateBoard();
            HasInitial = true;
        }


        [TestMethod]
        public void CheckCreateBoard()
        {

           
            Initial();

          
            KakurasuBoard Board2 = OriginalBoard.Clone();

            Trace.Assert(!Board2.IsFinshed);

            //Test Blank Cell
            Trace.Assert(!Board2.CorrectCellvalueMatrix [0, 0]);
            Trace.Assert(!Board2.CorrectCellvalueMatrix[0, 1]);
            Trace.Assert(!Board2.CorrectCellvalueMatrix[0, 2]);

            Trace.Assert(!Board2.CorrectCellvalueMatrix[1, 1]);
            Trace.Assert(!Board2.CorrectCellvalueMatrix[1, 2]);


            Trace.Assert(!Board2.CorrectCellvalueMatrix[2, 0]);
            Trace.Assert(!Board2.CorrectCellvalueMatrix[2, 4]);


            Trace.Assert(!Board2.CorrectCellvalueMatrix[3, 0]);
            Trace.Assert(!Board2.CorrectCellvalueMatrix[3, 3]);

            Trace.Assert(!Board2.CorrectCellvalueMatrix[4, 3]);

            //Test Black Cell
            int i;
            int j;
            for (i = 0; i < list.Count; i++)
            {
                Position positionBlack = list[i];
                Trace.Assert(Board2.CorrectCellvalueMatrix[positionBlack.Row, positionBlack.Column]);
            }

            //Check correct sum;
            Trace.Assert(Board2.CorrectRowSumAnswerlist[0] == 9);
            Trace.Assert(Board2.CorrectRowSumAnswerlist[1] == 10);
            Trace.Assert(Board2.CorrectRowSumAnswerlist[2] == 9);
            Trace.Assert(Board2.CorrectRowSumAnswerlist[3] == 10);
            Trace.Assert(Board2.CorrectRowSumAnswerlist[4] == 11);


            Trace.Assert(Board2.CorrectColumnSumAnswerlist[0] == 7);
            Trace.Assert(Board2.CorrectColumnSumAnswerlist[1] == 12);
            Trace.Assert(Board2.CorrectColumnSumAnswerlist[2] == 12);
            Trace.Assert(Board2.CorrectColumnSumAnswerlist[3] == 6);
            Trace.Assert(Board2.CorrectColumnSumAnswerlist[4] == 12);


            Trace.Assert(!Board2.IsFinshed);
            //All answer state must not choose yet
            for (i = 0; i < 5; i++)
            {
                Trace.Assert(Board2.RowAnswerResultlist[i] == KakurasuBoard.AnswerResult.NotChooseYet);
                Trace.Assert(Board2.ColAnswerResultlist[i] == KakurasuBoard.AnswerResult.NotChooseYet);

            }

            //All cell must be blank at this point;
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    Trace.Assert(!Board2[i, j]);
                }
            }

        }

        [TestMethod]
        public void SwitchValue()
        {
            Initial();
            KakurasuBoard Board2 = OriginalBoard.Clone();

            Trace.Assert(!Board2[0, 0]);

            Trace.Assert(Board2.ColumnUserSumAnwserlist[0] == 0);
            Trace.Assert(Board2.RowSumUserAnswerlist[0] == 0);
            Trace.Assert(Board2.RowAnswerResultlist[0] == KakurasuBoard.AnswerResult.NotChooseYet);
            Trace.Assert(Board2.ColAnswerResultlist[0] == KakurasuBoard.AnswerResult.NotChooseYet);



            Board2.SwitchValue(0, 0);
            Trace.Assert(Board2[0, 0]);

            Trace.Assert(Board2.ColumnUserSumAnwserlist[0] == 1);
            Trace.Assert(Board2.RowSumUserAnswerlist[0] == 1);
            Trace.Assert(Board2.RowAnswerResultlist[0] == KakurasuBoard.AnswerResult.Incorrect);
            Trace.Assert(Board2.ColAnswerResultlist[0] == KakurasuBoard.AnswerResult.Incorrect);

            Board2.SwitchValue(0, 0);
            Trace.Assert(!Board2[0, 0]);

            Trace.Assert(Board2.ColumnUserSumAnwserlist[0] == 0);
            Trace.Assert(Board2.RowSumUserAnswerlist[0] == 0);
            Trace.Assert(Board2.RowAnswerResultlist[0] == KakurasuBoard.AnswerResult.NotChooseYet);
            Trace.Assert(Board2.ColAnswerResultlist[0] == KakurasuBoard.AnswerResult.NotChooseYet);

            //Set all correct cell except last cell
            int i;
            for(i=0;i< list.Count - 1; i++)
            {
                Board2.SwitchValue(list[i].Row, list[i].Column);

            }
            Trace.Assert(!Board2.IsFinshed);
            for (i = 0; i < list.Count - 1; i++)
            {
                
                Trace.Assert(Board2[list[i].Row, list[i].Column]);
            }
            //Check correct sum;
            Trace.Assert(Board2.RowSumUserAnswerlist[0] == 9);
            Trace.Assert(Board2.RowSumUserAnswerlist[1] == 10);
            Trace.Assert(Board2.RowSumUserAnswerlist[2] == 9);
            Trace.Assert(Board2.RowSumUserAnswerlist[3] == 10);
            Trace.Assert(Board2.RowSumUserAnswerlist[4] == 6);


            Trace.Assert(Board2.ColumnUserSumAnwserlist[0] == 7);
            Trace.Assert(Board2.ColumnUserSumAnwserlist[1] == 12);
            Trace.Assert(Board2.ColumnUserSumAnwserlist[2] == 12);
            Trace.Assert(Board2.ColumnUserSumAnwserlist[3] == 6);
            Trace.Assert(Board2.ColumnUserSumAnwserlist[4] == 7);

            Trace.Assert(Board2.RowAnswerResultlist[0] == KakurasuBoard.AnswerResult.Correct);
            Trace.Assert(Board2.RowAnswerResultlist[1] == KakurasuBoard.AnswerResult.Correct);
            Trace.Assert(Board2.RowAnswerResultlist[2] == KakurasuBoard.AnswerResult.Correct);
            Trace.Assert(Board2.RowAnswerResultlist[3] == KakurasuBoard.AnswerResult.Correct);
            Trace.Assert(Board2.RowAnswerResultlist[4] == KakurasuBoard.AnswerResult.Incorrect);

            Trace.Assert(Board2.ColAnswerResultlist[0] == KakurasuBoard.AnswerResult.Correct);
            Trace.Assert(Board2.ColAnswerResultlist[1] == KakurasuBoard.AnswerResult.Correct);
            Trace.Assert(Board2.ColAnswerResultlist[2] == KakurasuBoard.AnswerResult.Correct);
            Trace.Assert(Board2.ColAnswerResultlist[3] == KakurasuBoard.AnswerResult.Correct);
            Trace.Assert(Board2.ColAnswerResultlist[4] == KakurasuBoard.AnswerResult.Incorrect);

            //Last cell/
            //At this point it is supposed to finish
            Board2.SwitchValue(list[list.Count - 1]);

            //Check correct sum;
            Trace.Assert(Board2.RowSumUserAnswerlist[0] == 9);
            Trace.Assert(Board2.RowSumUserAnswerlist[1] == 10);
            Trace.Assert(Board2.RowSumUserAnswerlist[2] == 9);
            Trace.Assert(Board2.RowSumUserAnswerlist[3] == 10);
            Trace.Assert(Board2.RowSumUserAnswerlist[4] == 11);


            Trace.Assert(Board2.ColumnUserSumAnwserlist[0] == 7);
            Trace.Assert(Board2.ColumnUserSumAnwserlist[1] == 12);
            Trace.Assert(Board2.ColumnUserSumAnwserlist[2] == 12);
            Trace.Assert(Board2.ColumnUserSumAnwserlist[3] == 6);
            Trace.Assert(Board2.ColumnUserSumAnwserlist[4] == 12);

            for (i = 0; i < 5; i++)
            {
                Trace.Assert(Board2.RowAnswerResultlist[i] ==  KakurasuBoard.AnswerResult.Correct);
                Trace.Assert(Board2.ColAnswerResultlist[i] == KakurasuBoard.AnswerResult.Correct);

            }

            Trace.Assert(Board2.IsFinshed);

            //After it is Finished it should not be changed.
            int j = 0;
            KakurasuBoard BoardTemp = Board2.Clone();
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    BoardTemp.SwitchValue(i, j);
                }
            }
            Trace.Assert(Utility.IsBoardInputValueTheSame(Board2, BoardTemp));


        }
    }
}
