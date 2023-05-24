using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.EN;
using System.Data.SqlClient;

namespace PruebaTecnica.DAL
{
    public class ProductoDAL
    {
        //public static int Guardar (Producto pProducto)
        //{
        //    string consulta = @"INSERT INTO Productos(Nombre, 
        //                        Descripcion,Img, Estado, IdCategoria)
        //                        VALUES(@Nombre,@Descripcion.@Img,@Estado,@IdCategoria)";
        //    SqlCommand comando = ComunDB.ObtenerComando();
        //    comando.CommandText = consulta;
        //    comando.Parameters.AddWithValue("@Nombre", pProducto.Nombre);
        //    comando.Parameters.AddWithValue("@Descripcion", pProducto.Descripcion);
        //    comando.Parameters.AddWithValue("@Img", pProducto.Img);
        //    comando.Parameters.AddWithValue("@Estado", pProducto.Estado);
        //    comando.Parameters.AddWithValue("@IdCategoria", pProducto.IdCategoria);

        //    return ComunDB.EjecutarComando(comando);
        //}
        //public static int Modificar(Producto pProducto)
        //{
        //    string consulta = @"UPDATE Productos 
        //                        SET Nombre = @Nombre, Descripcion= @Descripcion, Img=@Img,
        //                        IdCategoria=@IdCategoria, 
        //                        WHERE Id = @Id";

        //    SqlCommand comando = ComunDB.ObtenerComando();
        //    comando.CommandText = consulta;
        //    comando.Parameters.AddWithValue("@Nombre", pProducto.Nombre);
        //    comando.Parameters.AddWithValue("@Descripcion", pProducto.Descripcion);
        //    comando.Parameters.AddWithValue("@Img", pProducto.Img);
        //    comando.Parameters.AddWithValue("@IdCategoria", pProducto.IdCategoria);
        //    comando.Parameters.AddWithValue("@Id", pProducto.Id);

        //    return ComunDB.EjecutarComando(comando);
        //}
        public static int Eliminar(Rol pRol)
        {

            string consulta = @"UPDATE Productos
                                SET Estado = 0
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pRol.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static Producto BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, Nombre,Descripcion,IdCategoria, Estado
                                FROM Productos 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Producto objProducto = new Producto();
            while (reader.Read())
            {
                objProducto.Id = reader.GetInt32(0);
                objProducto.Nombre = reader.GetString(1);
                objProducto.Descripcion = reader.GetString(2);
                objProducto.IdCategoria = reader.GetByte(3);
                objProducto.Estado = reader.GetByte(4);
            }

            return objProducto;
        }
        //public static List<Producto> Buscar(Producto pProducto)
        //{
        //    byte contador = 0;

        //    string consulta = @"SELECT TOP 50 Id, Nombre, Descripcion,Img,IdCategoria Estado
        //                        FROM Productos";
        //    string whereSQL = " ";

        //    SqlCommand comando = ComunDB.ObtenerComando();

        //    if (!string.IsNullOrWhiteSpace(pProducto.Nombre))
        //    {
        //        if (contador > 0)
        //        {
        //            whereSQL += " AND ";
        //        }
        //        contador++;
        //        whereSQL += " Nombre Like @Nombre ";
        //        comando.Parameters.AddWithValue("@Nombre", "%" + pProducto.Nombre + "%");
        //    }

        //    if (pProducto.Estado > 0)
        //    {
        //        if (contador > 0)
        //        {
        //            whereSQL += " AND ";
        //        }
        //        contador++;
        //        whereSQL += " Estado = @Estado ";
        //        comando.Parameters.AddWithValue("@Estado", pProducto.Estado);
        //    }


        //    if (whereSQL.Trim().Length > 0)
        //    {
        //        whereSQL = " WHERE " + whereSQL;
        //    }

        //    // Definir consulta SQL completa
        //    comando.CommandText = consulta + whereSQL;

        //    SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
        //    List<Producto> lista = new List<Producto>();
        //    while (reader.Read())
        //    {
        //        Producto objProducto = new Producto();
        //        objProducto.Id = reader.GetInt32(0);
        //        objProducto.Nombre = reader.GetString(1);
        //        objProducto.Descripcion = reader.GetString(2);
        //        objProducto.Img = (byte[])reader["Img"];
        //        objProducto.IdCategoria = reader.GetInt32(3);
        //        objProducto.Estado = reader.GetByte(4);
        //        lista.Add(objProducto);
        //    }
        //    return lista;
        //}
        public static int Eliminar(Producto pProducto)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Productos
                                SET Estado = 0
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pProducto.Id);

            return ComunDB.EjecutarComando(comando);
        }

    }
}
