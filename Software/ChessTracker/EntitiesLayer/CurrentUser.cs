using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public static class CurrentUser
    {
        public static int? UserId { get; set; }
        public static string Role { get; set; }

        public static string Username { get; set; }
    }
}
