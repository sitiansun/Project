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
        
        

        

       

        public void changeDock(int num)
        {
            dockNum = num;
        }

        public int getDock()
        {
            return dockNum;
        }

        public void changeStatus(bool go)
        {
            canGo = go;
        }

        public Dictionary<PackInfo, int> getList()
        {
            return packList;
        }
    }
}