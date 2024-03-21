using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticulosNegocio
    {
        public List<Articulo> listar()
        {   
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select a.Id, Codigo, Nombre, a.Descripcion, ImagenUrl, Precio, c.Descripcion Categoria, m.Descripcion Marca from ARTICULOS a, CATEGORIAS c, MARCAS m where a.IdCategoria=c.Id AND a.IdMarca=m.Id");
                datos.ejecutarLectura();

                List<Articulo> lista = new List<Articulo>();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    lista.Add(aux);
                }

                datos.cerrarConexion();
                return lista;

            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
