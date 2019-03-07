using System.ComponentModel.DataAnnotations;
namespace Proyecto.Models.Users
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        [StringLength(10)]
        public string Contraseña { get; set; }
    }
}