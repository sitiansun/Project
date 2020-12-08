using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
namespace test
{
    public class CentralServer
    {
        protected int totalNum = 4;
        public Dictionary<string, int> inventory = new Dictionary<string, int> { };
        
        protected Robot robot1 = new Robot(1);
        protected Robot robot2 = new Robot(2);
        protected Robot robot3 = new Robot(3);
        protected Robot robot4 = new Robot(4);

        public PackInfo pack1 = new PackInfo("water", 4, 5, true, 6);
        public PackInfo pack2 = new PackInfo("juice", 2, 2, true, 1);
        public PackInfo pack3 = new PackInfo("coke", 3, 0, false, 10);
        public PackInfo pack4 = new PackInfo("fanta", 3, 1, false, 9);
        public PackInfo pack5 = new PackInfo("crush", 3, 4, true, 2);

        protected bool dock1=true;//true: available false
        protected bool dock2=true;

        public CentralServer()
        {
            inventory.Add(pack1.getName(), 10);
            inventory.Add(pack2.getName(), 10);
            inventory.Add(pack3.getName(), 10);
            inventory.Add(pack4.getName(), 10);
            inventory.Add(pack5.getName(), 10);
        }
        
        public int waterInventory()
        {
            return inventory[pack1.getName()];
        }

        public int juiceInventory()
        {
            return inventory[pack2.getName()];
        }
        public int cokeInventory()
        {
            return inventory[pack3.getName()];
        }
        public int fantaInventory()
        {
            return inventory[pack4.getName()];
        }
        public int crushInventory()
        {
            return inventory[pack5.getName()];
        }
        
        

        public Truck addDeliveryTruck(Dictionary<PackInfo, int> order)
        {
            Truck deTruck = new Truck(order);
            while(!dock1&&!dock2)
            {
                deTruck.changeDock(0);
            }

            if(dock1)
            {
                deTruck.changeDock(1);
                dock1 = false;
            }

            else if(dock2)
            {
                deTruck.changeDock(2);
                dock2 = false;
            }

            return deTruck;
        }

        public Truck addRestockTruck(Dictionary<PackInfo, int> order)
        {
            Truck reTruck = new Truck(order);
            while (!dock1 && !dock2)
            {
                reTruck.changeDock(0);
            }

            if (dock1)
            {
                reTruck.changeDock(1);
                dock1 = false;
            }

            else if (dock2)
            {
                reTruck.changeDock(2);
                dock2 = false;
            }

            return reTruck;
        }

        private void releaseDock(Truck truck)
        {
            if (truck.getDock()==1)
            {
                dock1 = true;
            }

            else if (truck.getDock() == 2)
            {
                dock2 = true;
            }

            truck.changeDock(3);
        }


  


        public void sendPack(Truck deTruck)
        {
            List<Thread> workLine = new List<Thread> { };
            int co = deTruck.getDock();
            int count = 0;
            Dictionary<PackInfo, int> order = deTruck.getList();
            
            foreach (var aPack in order)//Ask one robot that is not on duty to do the order
            {
              
                Thread.Sleep(300);
                    workLine.Add(new Thread(() =>
                    {
                       
                        if (!robot1.isRobotOnDuty())
                        {
                            
                            robot1.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {

                                robot1.toOrder(aPack.Key);
                                robot1.goTruck(2 * co - 1);

                                

                                inventory[aPack.Key.getName()]--;

                            }
                            robot1.toRestArea();
                            
                        }

                        else if (!robot2.isRobotOnDuty())
                        {
                            robot2.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {
                               

                                robot2.toOrder(aPack.Key);
                                robot2.goTruck(2 * co - 1);
                                inventory[aPack.Key.getName()]--;

                            }
                            robot2.toRestArea();
                           
                        }

                        else if (!robot3.isRobotOnDuty())
                        {
                            robot3.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {
                                

                                robot3.toOrder(aPack.Key);
                                robot3.goTruck(2 * co);
                                inventory[aPack.Key.getName()]--;

                            }
                            robot3.toRestArea();
                            
                        }
                        else if (!robot4.isRobotOnDuty())
                        {
                            robot4.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {
                                

                                robot4.toOrder(aPack.Key);
                                robot4.goTruck(2 * co);
                                inventory[aPack.Key.getName()]--;

                            }
                            robot4.toRestArea();
                           
                        }


                        else
                        {
                            while (robot1.isRobotOnDuty()) { }
                            robot1.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {

                                robot1.toOrder(aPack.Key);
                                robot1.goTruck(2 * co - 1);



                                inventory[aPack.Key.getName()]--;

                            }
                            robot1.toRestArea();

                        }
                    }));

                workLine[count].Start();
                count++;
            }
            
            
            foreach (Thread work in workLine)
            {
            
                work.Join();
            }

            Console.WriteLine("Order Sent");

            deTruck.changeStatus(true);
            releaseDock(deTruck);
            

            
        }

        public void reStock( Truck reTruck)
        {
            List<Thread> workLine = new List<Thread> { };
            int co = reTruck.getDock();
            Dictionary<PackInfo, int> order = reTruck.getList();
            int count = 0;
            

            foreach (var aPack in order)//Ask one robot that is not on duty to do the order
            {
                Thread.Sleep(300);
                workLine.Add( new Thread(() =>
                    {
                        
                        if (!robot1.isRobotOnDuty())
                        {
                            robot1.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {
                                robot1.goTruck(2 * co - 1);

                                robot1.toOrder(aPack.Key);
                                
                                inventory[aPack.Key.getName()]++;
                               

                            }
                            
                            robot1.toRestArea();
                           

                        }

                        else if (!robot2.isRobotOnDuty())
                        {
                            robot2.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {
                                robot2.goTruck(2 * co - 1);

                                robot2.toOrder(aPack.Key);

                                inventory[aPack.Key.getName()]++;

                            }
                            robot2.toRestArea();
                            
                        }

                        else if (!robot3.isRobotOnDuty())
                        {
                            robot3.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {
                                robot3.goTruck(2 * co);

                                robot3.toOrder(aPack.Key);

                                inventory[aPack.Key.getName()]++;

                            }
                            robot3.toRestArea();
                            
                        }
                        else if (!robot4.isRobotOnDuty())
                        {
                            robot4.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {
                                robot4.goTruck(2 * co);

                                robot4.toOrder(aPack.Key);

                                inventory[aPack.Key.getName()]++;

                            }
                            robot4.toRestArea();
                            
                        }
                        
                        else
                        {
                            while (robot1.isRobotOnDuty()) { }
                            robot1.onDuty();
                            for (int i = 0; i < aPack.Value; i++)
                            {
                                robot1.goTruck(2 * co - 1);

                                robot1.toOrder(aPack.Key);

                                inventory[aPack.Key.getName()]++;


                            }

                            robot1.toRestArea();
                           

                        }
                    }));
               
                workLine[count].Start();
                
                count++;
               
            }
                foreach (Thread work in workLine)
                {
                
                work.Join();
               
                }


            Console.WriteLine("Restock finished");
            reTruck.changeStatus(true);
            releaseDock(reTruck);

        }

        public Dictionary<String, int> getInventory()
        {
            return inventory;
        }

        public void showRobotBattery()
        {
            Console.WriteLine($"Robot1 {robot1.checkBattery()}");
            Console.WriteLine($"Robot2 {robot2.checkBattery()}");
            Console.WriteLine($"Robot3 {robot3.checkBattery()}");
            Console.WriteLine($"Robot4 {robot4.checkBattery()}");
        }

        public void showRobotCoordinate()
        {
            Console.WriteLine($"Robot1 {robot1.getCoordinate()}");
            Console.WriteLine($"Robot2 {robot2.getCoordinate()}");
            Console.WriteLine($"Robot3 {robot3.getCoordinate()}");
            Console.WriteLine($"Robot4 {robot4.getCoordinate()}");
        }

    }
}