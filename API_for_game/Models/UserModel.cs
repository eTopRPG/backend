using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_for_game.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}