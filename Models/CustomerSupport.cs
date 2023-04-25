using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Online_Recharge_WebApp.Models
{
   
    public class CustomerSupport
    {
        [Key]
        [Required]  
        public int Id { get; set; }
        //  [ForeignKey("IdentityUserEmail")]
        [Required]
        public string EmailId { get; set; }
       // public IdentityUser User { get; set; }
        [Required]
        public string EntryType { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }= DateTime.Now;
        [Required]
        public string ComplaintMessage { get; set; }
        [Required]
        public Boolean Completed { get; set; }


    }
}
