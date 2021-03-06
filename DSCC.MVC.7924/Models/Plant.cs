using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCC.MVC._7924.Models
{
    public class Plant
    {
        public int? Id { get; set; }
        public string PlantName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; } 
    }
}
