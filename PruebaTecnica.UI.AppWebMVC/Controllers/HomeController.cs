using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaTecnica.EN;
using PruebaTecnica.BL;

namespace PruebaTecnica.UI.AppWebMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public bool ValidarDatos(Rol pRol)
        {
            bool resultado = true;

            if (string.IsNullOrWhiteSpace(pRol.Nombre))
            {
                resultado = false;
            }
           
            return resultado;
        }
        //acciones CRUD
        public JsonResult Guardar(Rol pROL)
        {
            int resultado = 0;

            try
            {
                if (ValidarDatos(pROL) == true)
                {
                    resultado = new RolBL().Guardar(pROL);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }

            return Json(resultado);
        }
        public JsonResult Modificar(Rol pRol)
        {
            int resultado = 0;
            try
            {
                if (pRol.Id > 0 && ValidarDatos(pRol) == true)
                {
                    resultado = new RolBL().Modificar(pRol);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Rol pRol)
        {
            int resultado = 0;
            try
            {
                if (pRol.Id > 0)
                {
                    resultado = new RolBL().Eliminar(pRol);
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
             Rol objRol = new RolBL().BuscarPorId(pId);
             return Json(objRol);
        }
        public JsonResult Buscar(Rol pRol)
        {
            List<Rol> lista = new RolBL().Buscar(pRol);
            return Json(lista);
        }


    }
}
