using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.EN
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public int IdCategoria { get; set; }
        public byte Estado { get; set; }
        public virtual Categoria Categoria { get; set; }
        public string NombreCategoria { get; set; }
    }
}
