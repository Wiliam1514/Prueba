using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.EN
{
    public class Usuario
    {
        public int Id { get; set; }
        public byte IdRol { get; set; }
        public  string Nombre { get; set; }
        public string CorreElectronico { get; set; }
        public string Contraseña { get; set; }
        public byte Estado { get; set; }

    }
}
