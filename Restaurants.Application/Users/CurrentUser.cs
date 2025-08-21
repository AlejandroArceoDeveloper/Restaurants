using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users
{
    public class CurrentUser(string Id, string Email, IEnumerable<string> Roles)
    {

        public string Id { get; } = Id;
        public string Email { get; } = Email;

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }
    }
    
    
}
