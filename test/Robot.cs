using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
namespace test
{

    public class Robot
    {
        protected int pRow;
        protected char[] Column = { 'A', 'B', 'C', 'D', 'E', 'F' };
        protected int pColum;
        
        protected int robotNum;
        protected int battery;
        protected bool isOnDuty;
        private int moveTime =500;
        Mutex mut = new Mutex();


        public Robot( int num)
        {

            pRow = 0;//rest area
            robotNum = num;
            pColum = robotNum;
            isOnDuty = false;
            battery = 100;
           
        }
        /**
     *return current coordinate of the robot
     * @param nothing
     * 
     * @return a tuple shows coordinate in row and column.
     */
        public Tuple<int, char> getCoordinate()
        {
            
            return new Tuple<int, char>(pRow, Column[pColum]);

        }

        /**
    *send robot to dock. any horizontal move is made on row 5
    * @param co target column
    * 
    * @return nothing
    */
        public void goTruck(int co)
        {
            mut.WaitOne();
            isOnDuty = true;
            mut.ReleaseMutex();
            while (pRow < 5)
            {
                pRow++;
                Thread.Sleep(moveTime);
                battery--;
            }
            while (pColum>co)
            {
                pColum--;
                Thread.Sleep(moveTime);
                battery--;
            }
            while (pColum < co)
            {
                pColum++;
                Thread.Sleep(moveTime);
                battery--;
            }

        }

        /**
     *send robot to item shelf. any horizontal move is made on row 5 or row 1. robot must stay on row 5 
     *row 1 or rest area before call the method to avoid collision
     * @param pTarget the item that contain position infomation
     * 
     * @return nothing
     */
        public void toOrder(PackInfo pTarget) //stay on row1, 0 or 5 before use toOrder
        {
            mut.WaitOne();
            isOnDuty = true;
            mut.ReleaseMutex();

            while (pTarget.getColumnIndex() > pColum)
                {
                    pColum++;
                Thread.Sleep(moveTime);
                battery--;

            }

                while (pTarget.getColumnIndex() < pColum)
                {
                    pColum--;
                Thread.Sleep(moveTime);
                battery--;
            }


                while ((pTarget.getRow()) > pRow)
                {
                    pRow++;
                Thread.Sleep(moveTime);
                battery--;
            }

                while ((pTarget.getRow()) < pRow)
                {
                    pRow--;
                Thread.Sleep(moveTime);
                battery--;
            }
        }


        /**
    *send robot to rest area. any horizontal move is made on row 0
    * @param nothing
    * 
    * @return nothing
    */
        public void toRestArea()
        {
            while (pRow>0)
            {
                pRow--;
                Thread.Sleep(moveTime);
                battery--;
               
            }

            while (pColum<robotNum)
            {
                pColum++;
                Thread.Sleep(moveTime);
                battery--;
                
            }

            while (pColum > robotNum)
            {
                pColum--;
                Thread.Sleep(moveTime);
                battery--;
            }

            mut.WaitOne();
            isOnDuty = false;
            mut.ReleaseMutex();

            battery = 100;
        }

        /**
    *check if robot is busy
    * @param nothing
    * 
    * @return true if on work, false if free
    */
        public bool isRobotOnDuty()
        {
            return isOnDuty;
        }
        /**
   *make the robot status on duty
   * @param nothing
   * 
   * @return nothing
   */
        public void onDuty()
        {
            isOnDuty = true;
        }

        /**
   *check robot's battery
   * @param nothing
   * 
   * @return bettery percentage.
   */
        public int checkBattery()
        {
            return battery;
        }
    }
}