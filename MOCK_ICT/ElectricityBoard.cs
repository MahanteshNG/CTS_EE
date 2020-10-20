using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillAutomation
{
    public class ElectricityBoard
    {
        public SqlConnection SqlCon { get; set; }

        public ElectricityBoard() {
        }

        public void AddBill(ElectricityBill eb)
        {
            try
            {
                string querystring = "insert into ElectricityBill values('"+eb.ConsumerNumber+"','"+eb.ConsumerName+"',"+eb.UnitsConsumed+","+eb.BillAmount+")";
                //Console.Write("query " + querystring);
                SqlCon.Open();
                SqlCommand cmd = new SqlCommand(querystring, SqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                SqlCon.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
            }
            
        }

        public void CalculateBill(ElectricityBill ebill)
        {
            //Console.WriteLine("Units " + ebill.UnitsConsumed);
            if (ebill.UnitsConsumed <= 100)
                ebill.BillAmount = 0;
            else if (ebill.UnitsConsumed > 100 && ebill.UnitsConsumed <= 300){
                int temp=ebill.UnitsConsumed-100;
                ebill.BillAmount = temp * 1.5;
            }
            else if (ebill.UnitsConsumed > 300 && ebill.UnitsConsumed <= 600){
                
                
                int temp200=ebill.UnitsConsumed-100;
                int temp300=temp200-200;
                ebill.BillAmount=(200*1.5)+(temp300*3.5);
            }
            else if (ebill.UnitsConsumed > 600 && ebill.UnitsConsumed <= 1000){
                
                
                int temp200=ebill.UnitsConsumed-100;
                int temp400=temp200-500;
            
                ebill.BillAmount=(200*1.5)+(300*3.5)+(temp400*5.5);
            }
            else if (ebill.UnitsConsumed > 1000){
                
                 int temp200=ebill.UnitsConsumed-100;
                int temp400=temp200-900;
            
                ebill.BillAmount=(200*1.5)+(300*3.5)+(400*5.5)+(temp400*7.5);
        
            }
            //Console.WriteLine("Bill Amouont " + ebill.BillAmount);
        }

        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {
            try
            {
                string querystring = "Select TOP "+num+" * from ElectricityBill ORDER BY consumer_number desc";
                SqlCon.Open();
                SqlCommand cmd = new SqlCommand(querystring, SqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                List<ElectricityBill> l1 = new List<ElectricityBill>();

                while (reader.Read())
                {
                    ElectricityBill eb1 = new ElectricityBill();
                    eb1.ConsumerNumber = reader[0].ToString();
                    eb1.ConsumerName = reader[1].ToString();
                    eb1.UnitsConsumed = (int)reader[2];
                    eb1.BillAmount = (double)reader[3];
                    l1.Add(eb1);
                }

                SqlCon.Close();
                //Console.WriteLine("Size "+l1.Count);
                return l1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error 1: " + e.Message);
            }
            return null;
        }   
    }
}
