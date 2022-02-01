using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_for_game.Models
{
    public class CharachterModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public int Hp { get; set; }
        public int Energy { get; set; }
        public ClassModel classModel { get; set; }
    }
}