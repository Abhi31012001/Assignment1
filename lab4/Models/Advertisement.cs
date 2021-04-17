using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    public class Advertisement
    {
        public int Id
        {
            get; set;
        }
        [Required]
        [DisplayName("File Name")]
        public string FileName
        {
            get;
            set;
        }
        [Required]
        [Url]
        public string Url
        {
            get;
            set;
        }
    }
}
