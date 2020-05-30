using Prueba.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Models
{
    public class UserInfo : TableBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Phone { get; set; }
    }
}
