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

        /**
   *check item name
   * @param nothing
   * 
   * @return item name
   */
        public string getName()
        {
            return name;
        }

        /**
   *check item row coordinate
   * @param nothing
   * 
   * @return row number
   */
        public int getRow()
        {
            return row;
        }


        /**
   *check item column coordinate
   * @param nothing
   * 
   * @return column number
   */
        public int getColumnIndex()
        {
            return columnIndex;
        }

        /**
   *check item column coordinate in alphabet
   * @param nothing
   * 
   * @return column in alphabet
   */
        public char getColumn()
        {
            return Column[columnIndex];

        }
        /**
   *check item face which direction
   * @param nothing
   * 
   * @return true if on left. false if on right
   */
        public bool getDirection()
        {
            return direc;
        }
        /**
   *check item shelf number
   * @param nothing
   * 
   * @return shelf number
   */
        public int getShelfNum()
        {
            return pShelf;
        }

        

        
    }
}