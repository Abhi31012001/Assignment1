using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
namespace lab4.Models.ViewModels
{  public class FileInputViewModel
    {
        public string CommunityId { get; set; }
        public string CommunityTitle { get; set; }
        public IFormFile Advertisement { get; set; }
    }
}
