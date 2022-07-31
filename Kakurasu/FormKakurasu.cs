using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakurasu
{
    public partial class FormKakurasu : Form, UI.IUI
    {
        public FormKakurasu()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Initial(5);
        }

        private DateTime _TimeBegin;
        private DateTime _TimeEnd;

        private PictureBoxBoard picboard = null;
        private PictureBoxBoard picboardFin = null;
        private Game game = null;

        public event PictureBoxBoard.PictureBoxCellClick CellClick;
        public void NewGame(int BoardSize)
        {
            if (game != null)
            {
                if (!game.Board.IsFinshed)
                {
                    if (MessageBox.Show("Do you want to end this game ?", "", MessageBoxButtons.OKCancel)
                        != DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            this.Initial(BoardSize);
        }
        private Dictionary<int, int> _DictionaryBoadsizeNumberofBlackCell = null;
        private Dictionary<int, int> DictionaryBoadsizeNumberofBlackCell
        {
            get
            {
                if(_DictionaryBoadsizeNumberofBlackCell == null)
                {
                    _DictionaryBoadsizeNumberofBlackCell = new Dictionary<int, int>();
                    _DictionaryBoadsizeNumberofBlackCell.Add(5, 12);
                    _DictionaryBoadsizeNumberofBlackCell.Add(6, 20);
                    _DictionaryBoadsizeNumberofBlackCell.Add(7, 25);
                    _DictionaryBoadsizeNumberofBlackCell.Add(8, 40);
                    _DictionaryBoadsizeNumberofBlackCell.Add(9, 55);
                }
                return _DictionaryBoadsizeNumberofBlackCell;
            }
        }
        public void Initial(int BoardSize)
        {
            if(this.Controls.Contains(picboard))
            {
                this.Controls.Remove(picboard);
            }

            if (this.Controls.Contains(picboardFin))
            {

                this.Controls.Remove(picboardFin);
            }

            if (game != null)
            {
                game.ReleaseResource();
            }

            
            int BaseNumberofBlackCell = DictionaryBoadsizeNumberofBlackCell[BoardSize];
            int RandomNumberofBlackCell = Utility.GetRandomNumber(1, BoardSize);
            KakurasuBoard.IBoardGenerator BoardGen = new KakurasuBoard.BasicGenerator();
            BoardGen.SetNumberofBlackCell(BaseNumberofBlackCell + RandomNumberofBlackCell);

            game = new Game(this, BoardSize, BoardSize,BoardGen );
            picboard = new PictureBoxBoard(game.Board,false );
            picboard.CellClick -= Picboard_CellClick;
            picboard.CellClick += Picboard_CellClick;
            picboard.Top = this.menuStrip1.Height + 5;
            picboard.Left = 0;
            this.Controls.Add(picboard);
            this.Height = picboard.Height + picboard.Top + 40;
            this.Width = picboard.Width + picboard.Left + 15;
            _TimeBegin = DateTime.Now;
            IsGiveup = false;

        }
        private void Picboard_CellClick(object sender, Position position)
        {
            //throw new NotImplementedException();
            CellClick?.Invoke(this, position);
        }


        public void RenderBoard()
        {
            if (game.Board == null)
            {
                return;
                //throw new Exception("Board is null");
            }
            if (picboard == null)
            {
                return;
            }
            picboard.Invalidate();

          

        }

        public void SetGame(Game pgame)
        {
            this.game = pgame;

            //throw new NotImplementedException();
        }

        public void ReleaseResource()
        {
            // throw new NotImplementedException();
            picboard.CellClick -= Picboard_CellClick;
        }

        public void InformUserWon()
        {
            // throw new NotImplementedException();
            _TimeEnd = DateTime.Now;


            TimeSpan T = _TimeEnd - _TimeBegin;
            String Wording = "";
            Wording = "Congreatulations, you solved this puzzle. It took you " + T.Minutes.ToString("00") + ":" + T.Seconds.ToString("00") + " to finish.";
            MessageBox.Show(Wording);
           
        }
        Boolean IsGiveup = false;
        public void ShowFin()
        {
           
            if(this.Controls.Contains(picboardFin))
            {
                
                this.Controls.Remove(picboardFin);
            }
           
            this.game.Board.GiveUp();
            picboardFin = new PictureBoxBoard(this.game.Board ,true);
            picboardFin.Top = this.picboard.Top;
            picboardFin.Left = this.picboard.Left + this.picboard.Width + 50;
            this.Controls.Add(picboardFin);
            this.Width = picboardFin.Width + picboardFin.Left + 17;
            IsGiveup = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if(IsGiveup)
            {
                return;
            }
            if(game.Board.IsFinshed)
            {
                return;
            }
            this.ShowFin();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           

        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame(5);
        }

        private void x6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame(6);
        }

        private void x7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame(7);

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            NewGame(8);

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            NewGame(9);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (IsGiveup)
            {
                return;
            }
            if (game.Board.IsFinshed)
            {
                return;
            }
            this.ShowFin();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show ("Do you want to exit?","", MessageBoxButtons.OKCancel )!= DialogResult.OK)
            {
                return;
            }
            Application.Exit();
        }

     

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.game.Board.ReturnTextFor2DArray());

        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAbout f = new FormAbout();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            FormHowToPlay f = new FormHowToPlay();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();

        }
    }
}
