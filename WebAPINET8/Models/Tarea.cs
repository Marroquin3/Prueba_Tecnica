using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPINET8.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public bool Completada { get; set; }

        public int IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [JsonIgnore]
        public Usuario? User { get; set; }
    }
}
