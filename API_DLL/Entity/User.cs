using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API.Entity
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
