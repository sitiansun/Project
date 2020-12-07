using System;
using System.Collections.Generic;
using System.Threading;
namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            CentralServer server = new CentralServer();
            PackInfo pack1 = new PackInfo("water", 4, 5, true, 6);
            PackInfo pack2 = new PackInfo("juice", 2, 2, true, 1);
            PackInfo pack3 = new PackInfo("coke", 3, 0, false, 10);
            PackInfo pack4 = new PackInfo("fanta", 3, 1, false, 9);
            PackInfo pack5 = new PackInfo("crush", 3, 4, true, 2);

            foreach (var aPack in server.getInventory())
            {
                Console.WriteLine(aPack.Key);
                Console.WriteLine(aPack.Value);
            }

            Dictionary<PackInfo, int> list = new Dictionary<PackInfo, int> { };
            int count = 0;

            Console.WriteLine("press 1 for Administrator, press 2 for customer. Change later by type 'customer' or 'admin' directly");
            string type = Console.ReadLine();

            while(true)
            {
                while (type == "1")//User interface for admin
                {

                    Console.WriteLine("You are admin, what do you need?");
                    string key = Console.ReadLine();

                    if (key == "restock water")
                    {
                        Console.WriteLine($"How many? Max {10 - count}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if(num>10-count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else
                        {
                            try
                            {
                                list.Add(pack1, num);
                            }
                            
                            catch(Exception)
                            {
                                list[pack1] += num;
                            }
                            count += num;
                        }

                       
                        
                    }

                    if (key == "restock juice")
                    {
                        Console.WriteLine($"How many? Max {10 - count}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else
                        {
                            try
                            {
                                list.Add(pack2, num);
                            }

                            catch (Exception)
                            {
                                list[pack2] += num;
                            }
                            count += num;
                        }



                    }

                    if (key == "restock coke")
                    {
                        Console.WriteLine($"How many? Max {10 - count}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else
                        {
                            try
                            {
                                list.Add(pack3, num);
                            }

                            catch (Exception)
                            {
                                list[pack3] += num;
                            }
                            count += num;
                        }



                    }

                    

                    if (key == "restock fanta")
                    {
                        Console.WriteLine($"How many? Max {10 - count}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else
                        {
                            try
                            {
                                list.Add(pack4, num);
                            }

                            catch (Exception)
                            {
                                list[pack4] += num;
                            }
                            count += num;
                        }



                    }




                    if (key == "restock crush")
                    {
                        Console.WriteLine($"How many? Max {10 - count}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else
                        {
                            try
                            {
                                list.Add(pack5, num);
                            }

                            catch (Exception)
                            {
                                list[pack5] += num;
                            }
                            count += num;
                        }



                    }





                    if (key == "check restock list")
                    {
                        foreach(var good in list)
                        {
                            Console.WriteLine(good.Key.getName());
                            Console.WriteLine(good.Value);
                        }
                    }




                    if (key == "check inventory")
                    {
                        foreach (var aPack in server.getInventory())
                        {
                            Console.WriteLine(aPack.Key);
                            Console.WriteLine(aPack.Value);
                        }
                    }


                    if (key == "check battery")
                    {
                        server.showRobotBattery();
                    }

                    if (key == "check coordinate")
                    {
                        server.showRobotCoordinate();
                    }


                    if (key == "call restock truck")
                    {
                        Truck reTruck = server.addRestockTruck(list);
                        Thread restock = new Thread(() => {
                            server.reStock(reTruck);
                            });
                        restock.Start();
                        
                        list = new Dictionary<PackInfo, int> { };
                        count = 0;
                        Console.WriteLine("Restock starting....");
                    }




                    if (key == "rechoose")
                    {
                        list = new Dictionary<PackInfo, int> { };
                        count = 0;
                        Console.WriteLine("Restock list cleared");
                    }



                    if (key == "customer")
                    {
                        type = "2";
                        list = new Dictionary<PackInfo, int> { };
                        count = 0;
                    }


                }




                while(type=="2")
                {
                    Console.WriteLine("You are customer, what do you need?");
                    string key = Console.ReadLine();



                    if (key == "admin")
                    {
                        type = "1";
                        list = new Dictionary<PackInfo, int> { };
                        count = 0;
                    }

                    if (key == "check inventory")
                    {
                        foreach (var aPack in server.getInventory())
                        {
                            Console.WriteLine(aPack.Key);
                            Console.WriteLine(aPack.Value);
                        }
                    }




                    if (key == "add water")
                    {
                        Console.WriteLine($"How many? Max {10 - count}, Water in stock: {server.waterInventory()}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else if (num > server.waterInventory())
                        {
                            Console.WriteLine("Not that much in stock");
                        }

                        else
                        {
                            try
                            {
                                list.Add(pack1, num);
                            }

                            catch (Exception)
                            {
                                list[pack1] += num;
                            }
                            count += num;
                        }
                    }



                    if (key == "add juice")
                    {
                        Console.WriteLine($"How many? Max {10 - count}, Juice in stock: {server.juiceInventory()}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else if (num > server.juiceInventory())
                        {
                            Console.WriteLine("Not that much in stock");
                        }

                        else
                        {
                            try
                            {
                                list.Add(pack2, num);
                            }

                            catch (Exception)
                            {
                                list[pack2] += num;
                            }
                            count += num;
                        }
                    }


                    if (key == "add coke")
                    {
                        Console.WriteLine($"How many? Max {10 - count}, Coke in stock: {server.cokeInventory()}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else if (num > server.cokeInventory())
                        {
                            Console.WriteLine("Not that much in stock");
                        }

                        else
                        {
                            try
                            {
                                list.Add(pack3, num);
                            }

                            catch (Exception)
                            {
                                list[pack3] += num;
                            }
                            count += num;
                        }
                    }

                    if (key == "add fanta")
                    {
                        Console.WriteLine($"How many? Max {10 - count}, Fanta in stock: {server.fantaInventory()}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else if(num> server.fantaInventory())
                        {
                            Console.WriteLine("Not that much in stock");
                        }
                        else
                        {
                            try
                            {
                                list.Add(pack4, num);
                            }

                            catch (Exception)
                            {
                                list[pack4] += num;
                            }
                            count += num;
                        }
                    }



                    if (key == "add crush")
                    {
                        Console.WriteLine($"How many? Max {10 - count}, Fanta in stock: {server.crushInventory()}");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 10 - count)
                        {
                            Console.WriteLine("Reached truck capacity");
                        }

                        else if (num > server.crushInventory())
                        {
                            Console.WriteLine("Not that much in stock");
                        }
                        else
                        {
                            try
                            {
                                list.Add(pack5, num);
                            }

                            catch (Exception)
                            {
                                list[pack5] += num;
                            }
                            count += num;
                        }
                    }


                    if (key == "check shopping list")
                    {
                        foreach (var good in list)
                        {
                            Console.WriteLine(good.Key.getName());
                            Console.WriteLine(good.Value);
                        }
                    }
                    if (key == "reorder")
                    {
                        list = new Dictionary<PackInfo, int> { };
                        count = 0;
                        Console.WriteLine("Shopping list cleared");
                    }

                    if (key == "send order")
                    {
                        Truck deTruck = server.addDeliveryTruck(list);
                        foreach (var good in list)
                        {
                            Console.WriteLine(good.Key.getName());
                            Console.WriteLine(good.Value);
                        }
                        Thread order = new Thread(() => {
                           
                            server.sendPack(deTruck);
                            
                        });
                        
                        order.Start();

                        list = new Dictionary<PackInfo, int> { };
                        count = 0;
                        Console.WriteLine("Packages preparing....");
                    }
                }
            }
            
            
           
            

        
            
        }
    }
}
