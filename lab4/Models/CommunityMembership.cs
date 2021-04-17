using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


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
    public class CommunityMembership
    {
        public int ID 
        { get; set; }

        public int StudentID 
        { get; set; }

        public string CommunityID 
        { get; set; }

        public Student Student
        { get; set; }

         public Community Community
        { get; set; }
}
}
