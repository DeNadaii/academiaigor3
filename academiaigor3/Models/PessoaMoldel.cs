using System.ComponentModel.DataAnnotations;

namespace AcademiaIgor.Models
{
	public class PessoaModel
	{
		[Key]
		public int Codigo { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public DateTime DataDeNascimento { get; set; }
		public int QuantideDeFilhos { get; set; }
		public int Salario { get; set; }
		public string? situação { get; set; }
	}
}
