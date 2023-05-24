using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.DAL;
using PruebaTecnica.BL;
using PruebaTecnica.EN;

namespace PruebaTecnica.BL
{
    public class ProductoBL
    {
        //public int Guardar(Producto pProducto)
        //{
        //    pProducto.Estado = 1;
        //    return ProductoDAL.Guardar(pProducto);
        //}
        //public int Modificar(Producto pProducto)
        //{

        //    return ProductoDAL.Modificar(pProducto);
        //}
        public int Eliminar(Producto pProducto)
        {
            return ProductoDAL.Eliminar(pProducto);
        }
        public Producto BuscarPorId(int pId)
        {
            return ProductoDAL.BuscarPorId(pId);
        }
        //public List<Producto> Buscar(Producto pProducto)
        //{
        //    pProducto.Estado = 1;
        //    return ProductoDAL.Buscar(pProducto);
        //}
        public void CargarPropiedadVirtualCategoria(List<Producto> pLista, Action<List<Categoria>> pAccion = null)
        {
            //Metodos para cargar los datos de la propiedad virtual de Categoria y Marca
            if (pLista.Count > 0)
            {
                //Dictionary de Categoriaes
                //byte = Tipo de dasto de la llave primaria de Categoria
                //Categoria = clase partav guardar los datos de categorias
                Dictionary<byte, Categoria> diccionarioCategorias = new Dictionary<byte, Categoria>();

                //Buscar los categorias y cargarlos a los usuarios en su7 propiedaad virtual
                foreach (var obj in pLista)
                {
                    //Verificar si existe el Categoria en el diccionario
                    if (diccionarioCategorias.ContainsKey((byte)obj.IdCategoria) == true)
                    {
                        //cargar propiedad virtual desde el diccionario de categorias
                        obj.Categoria = diccionarioCategorias[(byte)obj.IdCategoria];
                    }
                    else
                    {
                        //si el rol no existe en el diccionario, se busca en la base de datos y se agrega al diccionario
                        diccionarioCategorias.Add((byte)obj.IdCategoria, CategoriaDAL.BuscarPorId(obj.IdCategoria));
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Categoria = diccionarioCategorias[(byte)obj.IdCategoria];
                    }
                }
                //accion auxiliar para sobrecargar otra propiedad  virtual dentro de la clase 
                if (pAccion != null && diccionarioCategorias.Count > 0)
                {
                    pAccion(diccionarioCategorias.Select(x => x.Value).ToList());
                }
            }

        }

    }
}
