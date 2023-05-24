using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaTecnica.BL;
using PruebaTecnica.EN;


namespace PruebaTecnica.UI.AppWebMVC.Controllers
{
    public class ProductoController : Controller
    {
        static string cadena = "Data Source=.;Initial Catalog=CRUDDB;Integrated Security=True";
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Guardar(string Nombre, string Descripcion, HttpPostedFileBase Img, byte IdCategoria)
        {
            string Extesion = Path.GetExtension(Img.FileName);
            MemoryStream ms = new MemoryStream();
            Img.InputStream.CopyTo(ms);
            byte[] data = ms.ToArray();
            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Productos(Nombre, Descripcion, Imagen, Estado, IdCategoria)values(@Nombre,@Descripcion, @Imagen, @Estado,@IdCategoria); SELECT SCOPE_IDENTITY()", oconexion);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Imagen", data);
                cmd.Parameters.AddWithValue("@Estado", 1);
                cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            TempData["SuccessMessage"] = "Los datos se han guardado correctamente";
            return RedirectToAction("VerProductos", "Producto");
        }
        public ActionResult VerProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection conexion = new SqlConnection(cadena))

            {
                SqlCommand cmd = new SqlCommand("SELECT Productos.*,Categorias.Nombre as NombreCategoria FROM Productos " +
                                                 "INNER JOIN Categorias ON Productos.IdCategoria = Categorias.Id " +
                                                 "WHERE Productos.Estado = 1", conexion);

                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(reader["Id"]);
                    producto.Nombre = reader["Nombre"].ToString();
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Imagen = (byte[])reader["Imagen"];
                    producto.Estado = Convert.ToByte(reader["Estado"]);
                    producto.IdCategoria = Convert.ToByte(reader["IdCategoria"]);
                    producto.NombreCategoria = reader["NombreCategoria"].ToString();


                    productos.Add(producto);
                }
            }
            return View(productos);
        }

        public bool ValidarDatos(Producto pProducto)
        {
            //Metodo para validar todos los campos que son obligatorios para guardaro modificar
            //
            //
            bool resultado = true;
            if (pProducto.IdCategoria == 0)
            {
                resultado = false;
            }
            
            if (string.IsNullOrWhiteSpace(pProducto.Nombre))
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pProducto.Descripcion))
            {
                resultado = false;
            }
            return resultado;
        }

        //public JsonResult Modificar(Producto pProducto)
        //{
        //    int resultado = 0;
        //    try
        //    {
        //        if (pProducto.Id > 0 && ValidarDatos(pProducto) == true)
        //        {
        //            resultado = new ProductoBL().Modificar(pProducto);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        resultado = 0;
        //    }
        //    return Json(resultado);
        //}
        public JsonResult Eliminar(Producto pProducto)
        {
            int resultado = 0;
            try
            {
                if (pProducto.Id > 0)
                {
                    resultado = new ProductoBL().Eliminar(pProducto);
                }
            }
            catch (Exception ex)
            {

                resultado = 0;
            }
            return Json(resultado);
        }
        public JsonResult BuscarPorId(int pId)
        {
            Producto objProducto = new ProductoBL().BuscarPorId(pId);
            return Json(objProducto, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult Buscar(Producto pProducto)
        //{
        //    ProductoBL productoBL = new ProductoBL();
        //    List<Producto> lista = productoBL.Buscar(pProducto);
        //    //Ejecutar el metodo para llenar las propiedades virtuales de Producto y saber a que Categoria y Marca pertenece el producto
        //    productoBL.CargarPropiedadVirtualCategoria(lista);
        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ObtenerCategorias()
        {
            List<Categoria> lista = new CategoriaBL().Buscar(new Categoria { Estado = 1 });
            return Json(lista);
        }
        public ActionResult EliminarProducto(int id)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Productos SET Estado = 0  WHERE Id = @Id", conexion);
                cmd.Parameters.AddWithValue("id", id);
                conexion.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected > 0)
                {
                    return RedirectToAction("VerProductos");

                }
                else
                {
                    return HttpNotFound();
                }

            }

        }
    }
}