using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _04___Imobiliaria.Models
{
    public class item
    {
        public int id_item {get; set;}
        public string name {get; set;}
        public float value {get; set;}
        public int n_rooms {get; set;}
        public DateTime date_added {get; set;}

        public location location {get; set;}
        public category category {get; set;}
        public business business {get; set;}

        public List<images> images  {get; set;}
    }
}