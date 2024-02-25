using System.ComponentModel.DataAnnotations;

namespace L01_2021MM601.Models
{
    public class Motoristas
    {
        [Key]
        public int motoristaId { get; set; }
        public string nombreMotorista { get; set; }
    }
}
