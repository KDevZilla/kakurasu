using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakurasu
{
    public class Game
    {

        public KakurasuBoard Board { get; private set; }
        /*
        public enum GameStateEnum
        {
            Playing,
            Finished
        }
        */
        UI.IUI UIBoard = null;
        public int RowSize { get; private set; }
        public int ColumnSize { get; private set; }
        public Game(UI.IUI pUIBoard, int pRowSize, 
            int pColumnSize,
            KakurasuBoard.IBoardGenerator Generator
            )
        {
            RowSize = pRowSize;
            ColumnSize = pColumnSize;

            Board = new KakurasuBoard(RowSize, 
                ColumnSize, 
                Generator );
            this.UIBoard = pUIBoard;
            this.UIBoard.SetGame(this);
            this.UIBoard.RenderBoard();

            this.UIBoard.CellClick -= UIBoard_CellClick;
            this.UIBoard.CellClick += UIBoard_CellClick;
            this.Board.CellValueUpdated -= Board_CellValueUpdated;
            this.Board.CellValueUpdated += Board_CellValueUpdated;
            this.Board.GenerateBoard();
        }
     
        private void Board_CellValueUpdated(object sender, Position e)
        {
            // throw new NotImplementedException();
            if (this.UIBoard == null)
            {
                return;
            }

            this.UIBoard.RenderBoard();
        }

        public void ReleaseResource()
        {
            if (this.UIBoard != null)
            {
                this.UIBoard.CellClick -= UIBoard_CellClick;
                this.UIBoard.ReleaseResource();
            }
            if (this.Board != null)
            {
                this.Board.CellValueUpdated -= Board_CellValueUpdated;
            }
         
        }

        private void UIBoard_CellClick(object sender, Position position)
        {
            // throw new NotImplementedException();
            if(this.Board.IsFinshed)
            {
                return;
            }

            this.Board.SwitchValue(position);

            if(this.Board.IsFinshed)
            {
                UIBoard.InformUserWon();
            }
          //  this.Board.CellvalueMatrix[position.Row, position.Column] = !this.Board.CellvalueMatrix [position ;
           // this.UIBoard.RenderBoard();
        }
    }
}
