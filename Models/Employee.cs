using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDwithADONet.Models
{
    public class Employee
    {

        [Key] // This marks the 'Id' property as the primary key
        public int Id { get; set; }
        [DisplayName ("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Date of Birth")]
        [Required]  
        public DateTime DateOfBirth { get; set; }
        [DisplayName("E-Mail")]
        [Required]  
        public string Email { get; set; }
        
        [Required]
        public double Salary { get; set; }

        //join first name and last name 

        [NotMapped]
        public string FullName
        {


            get { return FirstName + " " + LastName; }    

        }
    }
}
