using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test
{
    public class Truck
    {
        //protected bool isDelivery;
        protected Dictionary<PackInfo,int> packList=new Dictionary<PackInfo, int> { };
        protected bool canGo=false;//0:waitlist, 1:on dock 2: departure
        protected int capacity; //max capacity =10;
        protected int dockNum;//0:waitlist, 1:left dock 2: right dock 3:departured
        public Truck(Dictionary<PackInfo, int> packages)
        {
            packList = packages;
        }






        /**
   *change assigned dock status for truck. 0:waitlist, 1:left dock 2: right dock 3:departured
   * @param num status
   * 
   * @return nothing
   */
        public void changeDock(int num)
        {
            dockNum = num;
        }
        /**
   *return assigned dock number
   * @param nothing
   * 
   * @return dock number
   */
        public int getDock()
        {
            return dockNum;
        }
        /**
   *change assigned working status for truck. false: work not finished. true: work finished
   * @param go show that truck work is finished
   * 
   * @return nothing
   */
        public void changeStatus(bool go)
        {
            canGo = go;
        }
        /**
   *return the assigned list on truck
   * @param nothing
   * 
   * @return the order list 
   */
        public Dictionary<PackInfo, int> getList()
        {
            return packList;
        }
    }
}