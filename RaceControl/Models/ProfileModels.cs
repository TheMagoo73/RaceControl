using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceControl.Models
{
    public class ProfileModel
    {
        public int id { get; set; }
        public int? BRCANumber { get; set;}
        public DateTime? DateOfBirth { get; set; }

        // Navigation proprties
        public virtual ApplicationUser user { get; set; }
    }


}