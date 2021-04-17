using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
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
namespace lab4.Models.ViewModels
{
    public class CommunityMembersipViewModel
    {
        public string CommunityId { get; set; }
        public string Title { get; set; }
        public bool IsMember { get; set; }
    }
}
