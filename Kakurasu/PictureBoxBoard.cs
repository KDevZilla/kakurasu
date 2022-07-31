using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Kakurasu
{
    public class PictureBoxBoard:PictureBox
    {
        //https://stackoverflow.com/questions/7731855/rounded-edges-in-picturebox-c-sharp
        /*
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 50;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            this.Region = new Region(gp);
        
           
        }
        */
        private KakurasuBoard _BoardData = null;
        private KakurasuBoard BoardData
        {
            get
            {
                return _BoardData;
            }
        }
        private int CellSize = 50;
      //  public int NoofRow { get; private set; }
      //  public int NoofCole { get; private set; }
        private Rectangle[,] _arrRac = null;
        private Rectangle[,] arrRac
        {
            get
            {
                if (_arrRac == null)
                {
                    int i, j;
                    _arrRac = new Rectangle[BoardData.RowSize + 2 ,BoardData.ColumnSize + 2];
                    for (i = 0; i < BoardData.RowSize + 2; i++)
                    {
                        for (j = 0; j < BoardData.ColumnSize + 2 ; j++)
                        {
                            int IndexArrRec = (i * BoardData.RowSize) + j;
                            _arrRac [i,j]= new Rectangle(CellSize * j, CellSize * i, CellSize, CellSize);
                            //cWriteLog.WriteLog(" arrRac::" + IndexArrRec);
                            // _arrRac[IndexArrRec] = new Rectangle(CellSize * j, CellSize * i, CellSize, CellSize);
                        }
                    }
                }
                return _arrRac;
            }

        }
        private int ActualRowSize
        {
            get { return this.BoardData.RowSize + 2; }
        }
        private int AcutalColumnSize
        {
            get { return this.BoardData.ColumnSize + 2; }
        }
        public PictureBoxBoard(KakurasuBoard pBoardData, bool IsRenderFinMode)
        {
            this.IsRenderFinMode = IsRenderFinMode;
            this._BoardData = pBoardData;

            this.Paint -= PictureBoxBoard_Paint;
            this.Paint += PictureBoxBoard_Paint;
            this.Height = CellSize * this.ActualRowSize ;
            this.Width = CellSize * this.AcutalColumnSize ;
            this.BorderStyle = BorderStyle.None ;

            this.MouseDown -= PictureBoxBoard_MouseDown;
            this.MouseDown += PictureBoxBoard_MouseDown;

        }
        public delegate void PictureBoxCellClick(object sender, Position position);
        public event PictureBoxCellClick CellClick;
        
        private void PictureBoxBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            int X = e.X;
            int Y = e.Y;
            int RowClick = Y / CellSize;
            int ColClick = X / CellSize;
            if (RowClick > BoardData.RowSize  ||
                ColClick > BoardData.ColumnSize  ||
                RowClick < 1 ||
                ColClick < 1)
            {
                return;
            }
            RowClick -= 1;
            ColClick -= 1;
            if (CellClick != null)
            {
                CellClick(this, new Position(RowClick, ColClick));
            }
        }

        private void DrawBoard(Graphics g, Rectangle[,] arrRac)
        {
            g.Clear(Color.White );
            Pen PenBorder = new Pen(Color.Black, 1.5f);
            int i;
            int j;
            for (i = 1; i < BoardData.RowSize + 1 ; i++)
            {
                for(j=1;j<BoardData.ColumnSize + 1; j++)
                {
                    g.DrawRectangle(PenBorder, arrRac[i, j]);
                }
            }


        }

        private Font _AnswerFont = null;
        private Font AnswerFont
        {
            get
            {
                if(_AnswerFont == null)
                {
                    _AnswerFont =new System.Drawing.Font("Segoe UI", 
                        12F, 
                        System.Drawing.FontStyle.Regular, 
                        System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                return _AnswerFont;
            }
        }
        private Font _SumFont = null;
        private Font SumFont
        {
            get
            {
                if (_SumFont == null)
                {
                    _SumFont = new System.Drawing.Font("Segoe UI",
                        16F,
                        System.Drawing.FontStyle.Bold ,
                        System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                return _SumFont;
            }
        }
        private Font _WeightFont = null;
        private Font WeightFont
        {
            get
            {
                if (_WeightFont == null)
                {
                    _WeightFont = new System.Drawing.Font("Segoe UI",
                        12F,
                        System.Drawing.FontStyle.Regular ,
                        System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                return _WeightFont;
            }
        }
        private Brush _SumBrushCorrect = null;
        private Brush SumBrushCorrect
        {
            get
            {
                if(_SumBrushCorrect == null)
                {
                    _SumBrushCorrect = new SolidBrush(Color.Green);
                }
                return _SumBrushCorrect;
            }
        }
        private Brush _SumBrushWrong = null;
        private Brush SumBrushWrong
        {
            get
            {
                if (_SumBrushWrong == null)
                {
                    _SumBrushWrong = new SolidBrush(Color.Red );
                }
                return _SumBrushWrong;
            }
        }
        private Brush _SumBrush = null;
        private Brush SumBrush
        {
            get
            {
                if (_SumBrush == null)
                {
                    _SumBrush = new SolidBrush(Color.Black);
                }
                return _SumBrush;
            }
        }
        private Brush _WeightBrush = null;
        private Brush WeightBrush
        {
            get
            {
                if (_WeightBrush == null)
                {
                    _WeightBrush = new SolidBrush(Color.Black    );
                }
                return _WeightBrush;
            }
        }
        private Brush _AnswerBrush = null;
        private Brush AnswerBrush
        {
            get
            {
                if (_AnswerBrush == null)
                {
                    _AnswerBrush = new SolidBrush(Color.Black);
                }
                return _AnswerBrush;
            }
        }
        private Rectangle GetSmallerRectangle(Rectangle orignalRectangle)
        {
          
            Rectangle smallRectangle = new Rectangle(orignalRectangle.X + 2,
                orignalRectangle.Y + 2,
                orignalRectangle.Width - 4,
                orignalRectangle.Height - 4);
            return smallRectangle;
        }
        private void DrawAnswerFromPlayer(Graphics g)
        {
            int i;
            int j;

            for (i = 0; i < BoardData.RowSize  ; i++)
            {
                for (j = 0; j < BoardData.ColumnSize  ; j++)
                {
                    if (BoardData.CellvalueMatrix[i, j])
                    {
                        Rectangle reclittleSmaller = GetSmallerRectangle(arrRac[i + 1, j + 1]);
                        
                        g.FillRectangle(AnswerBrush, reclittleSmaller);
                    }
                    //g.DrawRectangle ()
                  // g.DrawString(BoardData.CellvalueMatrix[i, j], AnswerFont , AnswerBrush, _arrRac[i, j]);
                   // g.DrawRectangle(PenBorder, arrRac[i, j]);
                }
            }
        }

        private void DrawFinAnswer(Graphics g)
        {
            int i;
            int j;

            for (i = 0; i < BoardData.RowSize; i++)
            {
                for (j = 0; j < BoardData.ColumnSize; j++)
                {
                    if (BoardData.CorrectCellvalueMatrix[i, j])
                    {
                        Rectangle reclittleSmaller = GetSmallerRectangle(arrRac[i + 1, j + 1]);

                        g.FillRectangle(AnswerBrush, reclittleSmaller);
                    }
                    //g.DrawRectangle ()
                    // g.DrawString(BoardData.CellvalueMatrix[i, j], AnswerFont , AnswerBrush, _arrRac[i, j]);
                    // g.DrawRectangle(PenBorder, arrRac[i, j]);
                }
            }
        }
        private void DrawWeigthString(Graphics g)
        {
            int i;
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            for (i = 0; i < BoardData.ColumnSize; i++)
            {
                g.DrawString(BoardData.ColumnWeightlist [i].ToString (), WeightFont, WeightBrush, arrRac[0, i + 1], format);
            }
            for (i = 0; i < BoardData.RowSize ; i++)
            {
                g.DrawString(BoardData.RowWeightlist[i].ToString(), WeightFont, WeightBrush, arrRac[i + 1, 0], format);
            }

        }
        private void DrawSumString(Graphics g)
        {
            int i = 0;
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            for (i = 0; i < BoardData.ColumnSize; i++)
            {
                Brush DrawBrush = SumBrush;
                switch (BoardData.ColAnswerResultlist [i])
                {
                    case KakurasuBoard.AnswerResult.Correct:
                        DrawBrush = SumBrushCorrect;
                        break;
                    case KakurasuBoard.AnswerResult.Incorrect:
                        DrawBrush = SumBrushWrong;
                        break;
                    
                }
                g.DrawString(BoardData.CorrectColumnSumAnswerlist[i].ToString (), SumFont, DrawBrush  , arrRac[BoardData.RowSize + 1, i+1], format);
            }

            for (i = 0; i < BoardData.RowSize; i++)
            {
                Brush DrawBrush = SumBrush;
                switch (BoardData.RowAnswerResultlist [i])
                {
                    case KakurasuBoard.AnswerResult.Correct:
                        DrawBrush = SumBrushCorrect;
                        break;
                    case KakurasuBoard.AnswerResult.Incorrect:
                        DrawBrush = SumBrushWrong;
                        break;

                }
                g.DrawString(BoardData.CorrectRowSumAnswerlist[i].ToString(), SumFont, DrawBrush, arrRac[i+1, BoardData.ColumnSize + 1], format);
            }
            
        }
        public Boolean IsRenderFinMode { get; private set; }
        private void PictureBoxBoard_Paint(object sender, PaintEventArgs e)
        {
            int i;
            int j;
            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DrawBoard(g, arrRac);
            DrawWeigthString(g);
            DrawSumString(g);

            if (IsRenderFinMode)
            {
                DrawFinAnswer(g);
            }
            else
            {
                DrawAnswerFromPlayer(g);
            }
            /*
            for (i = 0; i < arrRac.Length; i++)
            {
                Position Diskposition =Utility.From1DimensionTo2(i, this.NoofRow);
                // game.board.CellsByPostion (Diskposition )
               // DrawDisk(g, board.CellsByPostion(Diskposition), arrRac[i]);
            }
            */

            /*
            if (this.BoardMode != BoardModeEnum.PlayMode)
            {
                return;
            }
            */
            /*
            List<Position> listCanPut = board.generateMoves();
            foreach (Position position in listCanPut)
            {
                int indexRec = Utility.Utility.From2DimensionTo1(position, this.NoofRow);
                DrawDiskBorder(g, arrRac[indexRec]);
            }
            */

        }

   
    }
}
