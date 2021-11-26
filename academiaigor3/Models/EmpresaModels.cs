using System.ComponentModel.DataAnnotations;

namespace AcademiaIgor.Models
{
    public class EmpresaModels
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public int Cnpj { get; set; }

    }
}