using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakurasu.UI
{

        public interface IUI
        {

            void RenderBoard();
        void NewGame(int BoardSize);
        void SetGame(Game game);
        void Initial(int BoardSize);
        void ReleaseResource();
        void InformUserWon();
        void ShowFin();
        event PictureBoxBoard.PictureBoxCellClick CellClick;
        //    event EventInt MoveBoardToSpecificTurnClick;
        //    event EventHandler ContinuePlayingClick;
        }
    
}
