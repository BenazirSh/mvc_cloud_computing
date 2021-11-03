using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCC.MVC._7924.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PlantID { get; set; }
        public Plant Plant { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
}
