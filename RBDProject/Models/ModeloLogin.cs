using System.ComponentModel.DataAnnotations;

namespace RBDProject.Models
{
    public class ModeloLogin
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
