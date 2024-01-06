using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar(string id="")
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=DESKTOP-IAERP4O\\SQLAS2022; database=CATALOGO_WEB_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, ImagenUrl, Precio, A.IdMarca, A.IdCategoria, A.Id From ARTICULOS A, CATEGORIAS C, MARCAS M Where C.Id = A.IdCategoria And M.Id = A.IdMarca ";
                if (id != "")
                {
                    comando.CommandText += " and A.Id = " + id;
                }
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Precio = (decimal)lector["Precio"];

                    if (!(lector["ImagenUrl"] is DBNull))
                    {
                        aux.ImagenUrl = (string)lector["ImagenUrl"];
                    }

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)lector["Categoria"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)lector["IdMarca"];
                    aux.Marca.Descripcion = (string)lector["Marca"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<Articulo> listarConSP()
        //{
        //    List<Articulo> lista = new List<Articulo>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        //string consulta = "Select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, M.Descripcion Marca, C.Descripcion Categoria, A.IdMarca, A.IdCategoria, A.Id From ARTICULOS A, MARCAS M, CATEGORIAS C Where M.Id = A.IdMarca And C.Id = A.IdCategoria";
        //        //datos.setearConsulta(consulta);

        //        datos.setearProcedimiento("storedListar");
                
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Articulo aux = new Articulo();
        //            aux.Id = (int)datos.Lector["Id"];
        //            aux.Codigo = (string)datos.Lector["Codigo"];
        //            aux.Nombre = (string)datos.Lector["Nombre"];
        //            aux.Descripcion = (string)datos.Lector["Descripcion"];
        //            aux.Precio = (decimal)datos.Lector["Precio"];
        //            if (!(datos.Lector["ImagenUrl"] is DBNull))
        //            {
        //                aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
        //            }
        //            aux.Marca = new Marca();
        //            aux.Marca.Id = (int)datos.Lector["IdMarca"];
        //            aux.Marca.Descripcion = (string)datos.Lector["Marca"];
        //            aux.Categoria = new Categoria();
        //            aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
        //            aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

        //            lista.Add(aux);
        //        }

        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria, ImagenUrl) Values ('" + nuevo.Codigo + "', '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', " + nuevo.Precio + ", @idMarca, @idCategoria, @imagenUrl)");
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@imagenUrl", nuevo.ImagenUrl);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //public void agregarConSP(Articulo nuevo)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearProcedimiento("storedAltaArticulo");
        //        datos.setearParametro("@codigo", nuevo.Codigo);
        //        datos.setearParametro("@nombre", nuevo.Nombre);
        //        datos.setearParametro("@desc", nuevo.Descripcion);
        //        datos.setearParametro("@idMarca", nuevo.Marca.Id);
        //        datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
        //        datos.setearParametro("@imagenUrl", nuevo.ImagenUrl);
        //        datos.setearParametro("@precio", nuevo.Precio);
        //        datos.ejecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @desc, ImagenUrl = @img, Precio = @precio, IdMarca = @idMarca, IdCategoria = @idCategoria Where Id = @id");
                datos.setearParametro("@codigo", articulo.Codigo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@desc", articulo.Descripcion);
                datos.setearParametro("@img", articulo.ImagenUrl);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@idMarca", articulo.Marca.Id);
                datos.setearParametro("@idCategoria", articulo.Categoria.Id);
                datos.setearParametro("@id", articulo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //public void modificarConSP(Articulo articulo)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearProcedimiento("storedModificarArticulo");
        //        datos.setearParametro("@codigo", articulo.Codigo);
        //        datos.setearParametro("@nombre", articulo.Nombre);
        //        datos.setearParametro("@desc", articulo.Descripcion);
        //        datos.setearParametro("@imagenUrl", articulo.ImagenUrl);
        //        datos.setearParametro("@precio", articulo.Precio);
        //        datos.setearParametro("@idMarca", articulo.Marca.Id);
        //        datos.setearParametro("@idCategoria", articulo.Categoria.Id);
        //        datos.setearParametro("@id", articulo.Id);

        //        datos.ejecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "Select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, M.Descripcion Marca, C.Descripcion Categoria, A.IdMarca, A.IdCategoria, A.Id From ARTICULOS A, MARCAS M, CATEGORIAS C Where M.Id = A.IdMarca And C.Id = A.IdCategoria And ";
                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Precio < " + filtro;
                            break;
                        default:
                            consulta += "Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Código")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Codigo like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Codigo like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Codigo like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("Delete from ARTICULOS Where Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
