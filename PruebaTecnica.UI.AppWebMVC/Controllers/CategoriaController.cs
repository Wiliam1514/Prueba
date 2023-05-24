using PruebaTecnica.BL;
using PruebaTecnica.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica.UI.AppWebMVC.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }
        public bool ValidarDatos(Categoria pCategoria)
        {
            bool resultado = true;

            if (string.IsNullOrWhiteSpace(pCategoria.Nombre))

            {
                resultado = false;
            }
            return resultado;
        }
        public JsonResult Guardar(Categoria pCategoria)
        {
            int resultado = 0;
            try
            {
                if (ValidarDatos(pCategoria) == true)
                {
                    resultado = new CategoriaBL().Guardar(pCategoria);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);

        }

        public JsonResult Modificar(Categoria pCategoria)
        {
            int resultado = 0;
            try
            {
                if (pCategoria.Id > 0 && ValidarDatos(pCategoria) == true)
                {
                    resultado = new CategoriaBL().Modificar(pCategoria);

                }
            }
            catch (Exception ex)
            {
                resultado = 0;

            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Categoria pCategoria)
        {
            int resultado = 0;
            try
            {
                if (pCategoria.Id > 0)
                {
                    resultado = new CategoriaBL().Eliminar(pCategoria);
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
            Categoria objCategoria = new CategoriaBL().BuscarPorId(pId);
            return Json(objCategoria);
        }
        public JsonResult Buscar(Categoria pCategoria)
        {
            List<Categoria> lista = new CategoriaBL().Buscar(pCategoria);
            return Json(lista);
        }
        public JsonResult ObtenerCategorias()
        {
            List<Categoria> lista = new CategoriaBL().Buscar(new Categoria { Estado = 1 });
            return Json(lista);
        }
    }
}