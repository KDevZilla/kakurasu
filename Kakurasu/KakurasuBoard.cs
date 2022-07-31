using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakurasu
{
    public class KakurasuBoard
    {
       
        public int NumberofBlackCell { get; private set; }
        public int RowSize { get; private set ; }
        public int ColumnSize { get; private set; }
        public bool[,] CellvalueMatrix { get; set; }
        public bool[,] CorrectCellvalueMatrix { get; set; }
        public int[] RowWeightlist { get; private set; }
        public int[] ColumnWeightlist { get; private set; }
        public int[] CorrectRowSumAnswerlist { get; private set; }
        public int[] CorrectColumnSumAnswerlist { get; private set; }

        public int[] RowSumUserAnswerlist { get; private set; }
        public int[] ColumnUserSumAnwserlist { get; private set; }
        public AnswerResult [] RowAnswerResultlist { get; private set; }
        public AnswerResult[] ColAnswerResultlist { get; private set; }
        public bool IsFinshed { get; private set; }
        public IBoardGenerator BoardGenerator { get; private set; }
        public void GiveUp()
        {
            IsFinshed = true;
        }
        public enum AnswerResult
        {
            NotChooseYet,
            Correct,
            Incorrect
        }
        //public event EventHandler CellValueUpdated;
        public event EventHandler<Position> CellValueUpdated;
        public void SwitchValue(int Row,int Column)
        {
            SwitchValue(new Position(Row, Column));
        }
        public void SwitchValue(Position position)
        {
            if(this.IsFinshed)
            {
                return;
            }
            this.CellvalueMatrix[position.Row, position.Column] = !this.CellvalueMatrix[position.Row, position.Column];
            CalculateAnswer();
            CellValueUpdated?.Invoke(this, position);
            
        }
        public void CalculateAnswer()
        {
            int iSum = 0;
            int i;
            int j;
            for (i = 0; i < this.RowSize; i++)
            {
                iSum = 0;
                for (j = 0; j < this.RowSize; j++)
                {
                    if (this[i, j])
                    {
                        iSum += ColumnWeightlist[j];
                    }
                }
                RowSumUserAnswerlist[i] = iSum;
            }

            for (i = 0; i < this.ColumnSize; i++)
            {
                iSum = 0;
                for (j = 0; j < this.RowSize; j++)
                {
                    if (this[j, i])
                    {
                        iSum += RowWeightlist[j];
                    }
                }
                ColumnUserSumAnwserlist[i] = iSum;
            }
            for (i = 0; i < this.RowSumUserAnswerlist.Length; i++)
            {
                if (this.RowSumUserAnswerlist[i] == this.CorrectRowSumAnswerlist[i])
                {
                    RowAnswerResultlist[i] = AnswerResult.Correct;
                } else
                {
                    if (this.RowSumUserAnswerlist[i] == 0)
                    {
                        RowAnswerResultlist[i] = AnswerResult.NotChooseYet;
                    } else
                    {
                        RowAnswerResultlist[i] = AnswerResult.Incorrect;
                    }
                }
            }

            for (i = 0; i < this.ColumnUserSumAnwserlist.Length; i++)
            {
                if (this.ColumnUserSumAnwserlist[i] == this.CorrectColumnSumAnswerlist[i])
                {
                    ColAnswerResultlist[i] = AnswerResult.Correct;

                }
                else
                {
                    if (this.ColumnUserSumAnwserlist[i] == 0)
                    {
                        ColAnswerResultlist[i] = AnswerResult.NotChooseYet;
                    }
                    else
                    {
                        ColAnswerResultlist[i] = AnswerResult.Incorrect;
                    }
                }
            }

            IsFinshed = true;
            for (i = 0; i < ColAnswerResultlist.Length; i++)
            {
                if (ColAnswerResultlist[i] != AnswerResult.Correct)
                {
                    IsFinshed = false;
                    break;
                }
            }
            if (IsFinshed)
            {
                for (i = 0; i < RowAnswerResultlist.Length; i++)
                {
                    if (RowAnswerResultlist[i] != AnswerResult.Correct)
                    {
                        IsFinshed = false;
                        break;
                    }
                }
            }


        }
        public bool this[int row,int column]
        {
            get
            {
                return this.CellvalueMatrix[row, column];
            }
            set
            {
                this.CellvalueMatrix[row, column] = value;
            }
        }
        public KakurasuBoard(int rowSize, 
            int columnSize,   
            IBoardGenerator  Generator)
        {
     

          //  this.NumberofBlackCell = numberofBlackCell;
            this.RowSize = rowSize;
            this.ColumnSize = columnSize;
            CellvalueMatrix = new bool[RowSize, ColumnSize];
            CorrectCellvalueMatrix = new bool[RowSize, ColumnSize];
            RowWeightlist = new int[RowSize];
            CorrectRowSumAnswerlist = new int[RowSize];
            RowSumUserAnswerlist = new int[RowSize];

            ColumnWeightlist = new int[ColumnSize];
            CorrectColumnSumAnswerlist = new int[ColumnSize];
            ColumnUserSumAnwserlist = new int[ColumnSize];


            //RowSumUserAnswerlist = new int[ColumnSize];
            //ColumnUserSumAnwserlist = new int[RowSize];
            ColAnswerResultlist = new AnswerResult[ColumnSize];
            RowAnswerResultlist = new AnswerResult[RowSize];

            this.BoardGenerator = Generator;
            InitialWeight();
           // GenerateBoard();
            /*
            this.CellValueUpdated -= KakurasuBoard_CellValueUpdated;
            this.CellValueUpdated += KakurasuBoard_CellValueUpdated;
            */
        }
        public KakurasuBoard Clone()
        {
            KakurasuBoard NewBoard = new KakurasuBoard(this.RowSize,
                this.ColumnSize,
                this.BoardGenerator);
            int i;
            int j;
            for(i=0;i<=NewBoard.CellvalueMatrix.GetUpperBound(0); i++)
            {
                for(j=0;j<=NewBoard.CellvalueMatrix.GetUpperBound(1); j++)
                {
                    NewBoard[i, j] = this[i, j]; //this.CorrectCellvalueMatrix[i, j];
                    NewBoard.CorrectCellvalueMatrix[i, j] = this.CorrectCellvalueMatrix[i, j];
                }
            }
            for(i=0;i<=NewBoard.CorrectRowSumAnswerlist.GetUpperBound(0); i++)
            {
                NewBoard.CorrectRowSumAnswerlist[i] = this.CorrectRowSumAnswerlist[i];
            }
            for (i = 0; i <= NewBoard.CorrectColumnSumAnswerlist.GetUpperBound(0); i++)
            {
                NewBoard.CorrectColumnSumAnswerlist[i] = this.CorrectColumnSumAnswerlist[i];
            }
            for (i = 0; i <= NewBoard.RowAnswerResultlist.GetUpperBound (0); i++)
            {
                NewBoard.RowAnswerResultlist[i] = this.RowAnswerResultlist[i];
            }
            for (i = 0; i <= NewBoard.ColAnswerResultlist.GetUpperBound(0); i++)
            {
                NewBoard.ColAnswerResultlist[i] = this.ColAnswerResultlist[i];
            }
            NewBoard.IsFinshed = this.IsFinshed;
            return NewBoard;
        }
        private void KakurasuBoard_CellValueUpdated(object sender, Position e)
        {
            
            //throw new NotImplementedException();
        }
        public String ReturnTextFor2DArray()
        {
            StringBuilder strB = new StringBuilder();
            int i;
            int j;
            String template = "                    list.Add(new Kakurasu.Position(!ROW!,!COL!));";
            for (i = 0; i < CorrectCellvalueMatrix.GetLength(0); i++)
            {
                for (j = 0; j < CorrectCellvalueMatrix.GetLength(1); j++)
                {
                    if(CorrectCellvalueMatrix[i, j])
                    {
                        String text = template.Replace(@"!ROW!", i.ToString ())
                            .Replace(@"!COL!", j.ToString ());
                        strB.Append(text).Append(Environment.NewLine);
                    }
                }
               
            }


            return strB.ToString();
        }
        public String ReturnText()
        {
            StringBuilder strB = new StringBuilder();
            int i;
            int j;
            for (i = 0; i < CorrectCellvalueMatrix.GetLength(0); i++)
            {
                    for (j = 0; j < CorrectCellvalueMatrix.GetLength(1); j++)
                    {
                        strB.Append (CorrectCellvalueMatrix [i, j] + "\t");
                    }
                strB.Append("|");
                strB.Append(CorrectRowSumAnswerlist [i]).Append("\t");
                strB.Append(Environment.NewLine);
                if(i==CorrectCellvalueMatrix.GetLength(0) - 1)
                {
                    for (j = 0; j < CorrectCellvalueMatrix.GetLength(1); j++)
                    {
                        strB.Append(CorrectColumnSumAnswerlist[j] + "\t");
                    }
                    strB.Append(Environment.NewLine);
                }
               
            }

            
            return strB.ToString();
        }
        private void InitialWeight()
        {
            int i;
            for (i = 0; i < RowSize; i++)
            {
                RowWeightlist[i] = (i + 1);
            }
            for (i = 0; i < ColumnSize; i++)
            {
                ColumnWeightlist[i] = (i + 1);
            }
        }
        private static Random random = new Random();
        private static int GetRandom(int Min,int Max)
        {
            return random.Next(Min, Max);
        }
        Dictionary<String, Position> dicBlackPosition = new Dictionary<string, Position>();
        private void UpdateSumValue()
        {
            // int i;
            int i;
            int j;
            for (i = 0; i < RowSize; i++)
            {
                //RowWeightlist[i] = (i + 1);
                int SumRowi = 0;
                for (j = 0; j < ColumnSize; j++)
                {
                    if (CorrectCellvalueMatrix[i, j])
                    {
                        SumRowi += ColumnWeightlist[j];
                    }
                }
                CorrectRowSumAnswerlist[i] = SumRowi;
            }
            for (i = 0; i < ColumnSize; i++)
            {
                int SumColi = 0;
                for (j = 0; j < RowSize; j++)
                {
                    if (CorrectCellvalueMatrix[j, i])
                    {
                        SumColi += RowWeightlist[j];
                    }
                }
                CorrectColumnSumAnswerlist[i] = SumColi;
            }
        }
        public void GenerateBoard()
        {
            BoardGenerator.Generate(this);
            UpdateSumValue();
            
        }

        public interface IBoardGenerator
        {
            void Generate(KakurasuBoard board);
            void SetNumberofBlackCell(int noofBlackCell);
           
        }
        public class ManualGenerator : IBoardGenerator
        {
            // public bool[,] SetBlackCellValue;
            public List<Position> listBlackCellValue = null;
            public void SetNumberofBlackCell(int noofBlackCell)
            {
                throw new NotImplementedException("No need to use this method");
            }
            public ManualGenerator(List<Position> plistBlackCellValue)
            {
                listBlackCellValue = new List<Position>();
                plistBlackCellValue.ForEach(x => listBlackCellValue.Add(new Position(x.Row, x.Column)));
                

            }
            public void Generate(KakurasuBoard board)
            {
                int i;
                int j;
                listBlackCellValue.ForEach(x => board.CorrectCellvalueMatrix[x.Row, x.Column] = true);
                
            }
        }
        public class BasicGenerator : IBoardGenerator
        {
            private int NumberofBlackCell = 0;
            public void SetNumberofBlackCell(int noofBlackCell)
            {
                    NumberofBlackCell = noofBlackCell;
            }

            public void Generate(KakurasuBoard board)
            {
                // throw new NotImplementedException();
                //  board.RowSize = 5;
                int i;
                int j;
                // HashSet<String> hashBlackPostion = new HashSet<string>();
                board.dicBlackPosition = new Dictionary<string, Position>();
                for (i = 1; i <= NumberofBlackCell; i++)
                {
                    Boolean IsValidRandom = false;
                    while (!IsValidRandom)
                    {
                        Position RandomPosition = Position.Empty;
                        RandomPosition.Row = GetRandom(0, board.RowSize);
                        RandomPosition.Column = GetRandom(0, board.ColumnSize);
                        if (!board.dicBlackPosition.ContainsKey(RandomPosition.CustomHash))
                        {
                            board.dicBlackPosition.Add(RandomPosition.CustomHash,
                                RandomPosition);
                            IsValidRandom = true;
                            board.CorrectCellvalueMatrix[RandomPosition.Row, RandomPosition.Column] = true;
                        }
                    }

                }

               
            }
        }
    }
}
