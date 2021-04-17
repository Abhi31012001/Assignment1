using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


/*student name- Abhi Patel;
 * 
 * Student No:040978822;
 * 
 partner Name -Meet Patel;

Student no: 040979409

Assignment 1

Lab Instructor - Aamir Rad 

*/
namespace lab4.Models
{
    public class AdsCommunity 
    {
        public int ID { get; set; }
        public int AdvertisementID { get; set; }
        public Advertisement Advertisement { get; set; }

        public string CommunityID { get; set; }

        public Community Community { get; set; }
    }
}
