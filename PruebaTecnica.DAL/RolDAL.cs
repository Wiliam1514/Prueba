using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.EN;
using System.Data.SqlClient;

namespace PruebaTecnica.DAL
{
    public class RolDAL
    {
        public static int Guardar(Rol pRol)
        {
            string consulta = @"INSERT INTO Roles(Nombre, Descripcion, Estado) 
                                VALUES(@Nombre, @Descripcion, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pRol.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", pRol.Descripcion);  
            comando.Parameters.AddWithValue("@Estado", pRol.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Modificar(Rol pRol)
        {
            string consulta = @"UPDATE Roles 
                                SET Nombre = @Nombre, Descripcion= @Descripcion
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pRol.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", pRol.Descripcion);   
            comando.Parameters.AddWithValue("@Id", pRol.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Eliminar(Rol pRol)
        {
           
            string consulta = @"UPDATE Roles
                                SET Estado = 0
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pRol.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static Rol BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, Nombre,Descripcion, Estado
                                FROM Roles 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Rol objRol = new Rol();
            while (reader.Read())
            {
                objRol.Id = reader.GetInt32(0);
                objRol.Nombre = reader.GetString(1);
                objRol.Descripcion = reader.GetString(2);
                objRol.Estado = reader.GetByte(3);
            }

            return objRol;
        }
        public static List<Rol> Buscar(Rol pRol)
        {
            byte contador = 0;

            string consulta = @"SELECT TOP 50 Id, Nombre, Descripcion, Estado
                                FROM Roles";
            string whereSQL = " ";

            SqlCommand comando = ComunDB.ObtenerComando();

            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
            {
                if (contador > 0)
                {
                    whereSQL += " AND ";
                }
                contador++;
                whereSQL += " Nombre Like @Nombre ";
                comando.Parameters.AddWithValue("@Nombre", "%" + pRol.Nombre + "%");
            }

            if (pRol.Estado > 0)
            {
                if (contador > 0)
                {
                    whereSQL += " AND ";
                }
                contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pRol.Estado);
            }

           
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }

            // Definir consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Rol> lista = new List<Rol>();
            while (reader.Read())
            {
                Rol objRol = new Rol();
                objRol.Id = reader.GetInt32(0);
                objRol.Nombre = reader.GetString(1);
                objRol.Descripcion = reader.GetString(2);
                objRol.Estado = reader.GetByte(3);
                lista.Add(objRol);
            }
            return lista;
        }
    }
}
