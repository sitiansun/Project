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

        public Tuple<int, char> getCoordinate()
        {
            
            return new Tuple<int, char>(pRow, Column[pColum]);

        }

        public void goTruck(int co)//stay on rest area before go pick
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
        
       
        public void toOrder(PackInfo pTarget) //stay on row1 or 5 before to order
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

        public void placeOrder(int co)//put order on truck
        {
            mut.WaitOne();
            isOnDuty = true;
            mut.ReleaseMutex();

            while (pRow <5)
            {
                pRow++;
                Thread.Sleep(moveTime);
                battery--;
            }

            while (pColum > co)
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

        public bool isRobotOnDuty()
        {
            return isOnDuty;
        }

        public void onDuty()
        {
            isOnDuty = true;
        }
        public int checkBattery()
        {
            return battery;
        }
    }
}