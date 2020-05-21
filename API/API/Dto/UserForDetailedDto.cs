using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class UserForDetailedDto
    {
        public string Username { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }
        //public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}
