using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Configuration;

namespace BillAutomation
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            ElectricityBoard eb = null;
            DBHandler db = new DBHandler();
            SqlConnection con = db.GetConnection();
            List<ElectricityBill> l2 = new List<ElectricityBill>();
            Console.WriteLine("Enter Number of Bills To Be Added : ");
            int totBill = Convert.ToInt32(Console.ReadLine());
            for (int cnt = 0; cnt < totBill; cnt++)
            {
                Console.WriteLine("Enter Consumer Numer : ");
                String conNo = Console.ReadLine();
                Console.WriteLine("Enter Consumer Name : ");
                String name = Console.ReadLine();
                Console.WriteLine("Enter Units Consumed : ");
                int units = Convert.ToInt32(Console.ReadLine());
               
                ElectricityBill ebill = new ElectricityBill();
                try
                {
                    ebill.ConsumerNumber = conNo;
                }
                //catch (InvalidConsumerNumberException e)
                catch (FormatException e)
                {
                    Console.WriteLine(e);
                }
                ebill.ConsumerName = name;
                ebill.UnitsConsumed = units;

                eb = new ElectricityBoard();
                eb.SqlCon = con;
                eb.CalculateBill(ebill);
                eb.AddBill(ebill);
                l2.Add(ebill);
            }
            Console.WriteLine();
            Console.Write("Enter Last 'N' Number of Bills To Generate : ");
            int num=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            foreach(var p in l2)
            {
                Console.WriteLine(((ElectricityBill)p).ConsumerNumber);
                Console.WriteLine(((ElectricityBill)p).ConsumerName);
                Console.WriteLine(((ElectricityBill)p).UnitsConsumed);
                Console.WriteLine("Bill Amount: "+((ElectricityBill)p).BillAmount);
            }

            List<ElectricityBill> l1=eb.Generate_N_BillDetails(num);
            Console.WriteLine("Details of Bill Generation");
            foreach(var ie in l1)
            {
                Console.WriteLine("EB Bill for " + ((ElectricityBill)ie).ConsumerName +" is "+ ((ElectricityBill)ie).BillAmount);
            }

        }

   
        }
            public class BillValidator{
            public string ValidateUnitsConsumed(int UnitsConsumed)
            {
            if(UnitsConsumed<0)
               {
                return "Given units is invalid";
            }else{
                return"";
            }
        }
    }


    }
}
