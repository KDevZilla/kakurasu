using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakurasu
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string CustomHash
        {
            get
            {
                return Row.ToString() + "_" +
                    Column.ToString();
            }
        }
        public static Position Empty
        {
            get
            {
                return new Position(-1, -1);
            }
        }
        public Position(int pRow, int pColumn)
        {
            Row = pRow;
            Column = pColumn;
        }

    }
}
