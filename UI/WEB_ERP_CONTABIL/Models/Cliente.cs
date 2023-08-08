using System.ComponentModel;

namespace WEB_ERP_CONTABIL.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }

        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [DisplayName("Sobrenome")]
        public string? Sobrenome { get; set; }

        [DisplayName("Data de Nascimento")]        
        public DateTime DataNascimento { get; set; }
    }
}
