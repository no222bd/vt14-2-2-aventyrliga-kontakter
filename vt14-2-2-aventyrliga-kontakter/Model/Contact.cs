using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vt14_2_2_aventyrliga_kontakter.Model
{
    // Class corresponding to database table 'Contact'
    public class Contact
    {
        public int ContactID { get; set; }
        
        [Required(ErrorMessage="Ett förnamn måste anges")]
        [StringLength(50, ErrorMessage = "Förnamnet överstiget antalet tillåtna tecken")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ett efternamn måste anges")]
        [StringLength(50, ErrorMessage = "Efternamnet överstiget antalet tillåtna tecken")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "En e-postadress måste anges")]
        [StringLength(50, ErrorMessage = "Efternamnet överstiget antalet tillåtna tecken")]
        [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "E-postadressen har ett ogiltigt format")]    
        public string EmailAddress { get; set; }
    }
}