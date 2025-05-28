using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDCommerceBCC.Domain
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string Name { get; set; }
        public string Email { get; set;}
        public string Endereco { get; set;}
        public string Telefone { get; set;}
    }
}
