using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5_Contatos.Models
{
    public class ContatoViewModel
    {
        public int ContatoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public virtual EnderecoViewModel Endereco { get; set; }
        public virtual int EnderecoId { get; set; }
    }
}