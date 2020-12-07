using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test
{
    public class PackInfo
    {
        protected string name;
        protected char[] Column = { 'A', 'B', 'C', 'D', 'E', 'F' };
        protected int columnIndex;
        protected int row;
        protected bool direc;//true is left, false is right
        protected int pShelf;
       // protected int status;//0:on truck 1: on robot 2: on shelf
        public PackInfo(string pName, int columnNum, int rowNum, bool direction, int shelfNum)
        {
            name = pName;
            columnIndex = columnNum;
            row = rowNum;
            direc = direction;
            pShelf = shelfNum;
        }


        public string getName()
        {
            return name;
        }
        public int getRow()
        {
            return row;
        }

        public int getColumnIndex()
        {
            return columnIndex;
        }

        public char getColumn()
        {
            return Column[columnIndex];

        }

        public bool getDirection()
        {
            return direc;
        }

        public int getShelfNum()
        {
            return pShelf;
        }

        

        
    }
}