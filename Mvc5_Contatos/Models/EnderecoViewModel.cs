using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5_Contatos.Models
{
    public class EnderecoViewModel
    {
        public int EnderecoId { get; set; }
        public string Local { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}