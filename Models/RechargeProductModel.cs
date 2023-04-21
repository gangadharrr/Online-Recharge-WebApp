using System.ComponentModel.DataAnnotations;

namespace Online_Recharge_WebApp.Models
{
    public class RechargeProductModel
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public float Plan { get; set; }


        [Required]
        // In days
        public int Validity { get; set; }


        [Required]
        public float Data { get; set; }

        

        
    }
}
