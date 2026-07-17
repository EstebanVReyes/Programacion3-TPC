using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProductoNegocio
    {

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetearConsulta("SELECT P.ID, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.PorcentajeGanancia, P.StockActual, P.StockMinimo, P.UrlImagen, P.Marca_ID, P.Categoria_ID, M.Nombre AS MarcaNombre, C.Nombre AS CategoriaNombre FROM Productos P INNER JOIN Marcas M ON P.Marca_ID = M.ID INNER JOIN Categorias C ON P.Categoria_ID = C.ID WHERE P.Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["Marca_ID"];
                    aux.Marca.Descripcion = (string)datos.Lector["MarcaNombre"];

                
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["Categoria_ID"];
                    aux.Categoria.Descripcion = (string)datos.Lector["CategoriaNombre"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        
        public void Agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Productos (Codigo, Nombre, Descripcion, Precio, PorcentajeGanancia, StockActual, StockMinimo, Categoria_ID, Marca_ID, UrlImagen) VALUES (@Codigo, @Nombre, @Desc, @Precio, @Porcentaje, @Stock, @StockMin, @IdCat, @IdMarca, @UrlImagen)");

                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Desc", nuevo.Descripcion);
                datos.SetearParametro("@Precio", nuevo.Precio);
                datos.SetearParametro("@Porcentaje", nuevo.PorcentajeGanancia);
                datos.SetearParametro("@Stock", nuevo.StockActual);
                datos.SetearParametro("@StockMin", nuevo.StockMinimo);
                datos.SetearParametro("@IdCat", nuevo.Categoria.Id);
                datos.SetearParametro("@IdMarca", nuevo.Marca.Id);
                datos.SetearParametro("@UrlImagen", string.IsNullOrWhiteSpace(nuevo.UrlImagen) ? (object)DBNull.Value : nuevo.UrlImagen);
                
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void Modificar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.SetearConsulta("UPDATE Productos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Desc, Precio = @Precio, PorcentajeGanancia = @Porcentaje, StockActual = @Stock, StockMinimo = @StockMin, Categoria_ID = @IdCat, Marca_ID = @IdMarca, UrlImagen = @UrlImagen WHERE ID = @Id");

                datos.SetearParametro("@Codigo", producto.Codigo);
                datos.SetearParametro("@Nombre", producto.Nombre);
                datos.SetearParametro("@Desc", producto.Descripcion);
                datos.SetearParametro("@Precio", producto.Precio);
                datos.SetearParametro("@Porcentaje", producto.PorcentajeGanancia);
                datos.SetearParametro("@Stock", producto.StockActual);
                datos.SetearParametro("@StockMin", producto.StockMinimo);
                datos.SetearParametro("@IdCat", producto.Categoria.Id);
                datos.SetearParametro("@IdMarca", producto.Marca.Id);
                datos.SetearParametro("@UrlImagen", string.IsNullOrWhiteSpace(producto.UrlImagen) ? (object)DBNull.Value : producto.UrlImagen);
                datos.SetearParametro("@Id", producto.Id);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.SetearConsulta("UPDATE Productos SET Estado = 0 WHERE ID = @Id");
                datos.SetearParametro("@Id", id);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
             
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public List<Proveedor> ObtenerProveedores(int idProducto)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
                    SELECT PR.ID, PR.Nombre
                    FROM Productos_Proveedores PP
                    INNER JOIN Proveedores PR ON PP.Proveedor_ID = PR.ID
                    WHERE PP.Producto_ID = @idProducto");
                datos.SetearParametro("@idProducto", idProducto);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void actualizarStock(int idProducto, int cantidadAjuste)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT StockActual FROM Productos WHERE ID = @idProducto");
                datos.SetearParametro("@idProducto", idProducto);
                datos.EjecutarLectura();

                int stockActual = 0;
                if (datos.Lector.Read())
                {
                    stockActual = (int)datos.Lector["StockActual"];
                }
                datos.CerrarConexion();

                if (stockActual + cantidadAjuste < 0)
                {
                    throw new Exception("Stock insuficiente. Stock actual: " + stockActual + ", intentando restar: " + Math.Abs(cantidadAjuste));
                }

                datos = new AccesoDatos();
                datos.SetearConsulta("UPDATE Productos SET StockActual = StockActual + @cantidad WHERE ID = @idProducto");

                datos.SetearParametro("@cantidad", cantidadAjuste);
                datos.SetearParametro("@idProducto", idProducto);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public List<ProductoBajoStock> ListarProductosConMenosStock()
        {
            List<ProductoBajoStock> lista = new List<ProductoBajoStock>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC SP_ProductosConMenosStock");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ProductoBajoStock aux = new ProductoBajoStock();

                    aux.IdProducto = Convert.ToInt32(datos.Lector["IdProducto"]);
                    aux.NombreProducto = datos.Lector["NombreProducto"].ToString();
                    aux.StockActual = Convert.ToInt32(datos.Lector["StockActual"]);
                    aux.StockMinimo = Convert.ToInt32(datos.Lector["StockMinimo"]);
                    aux.Precio = Convert.ToDecimal(datos.Lector["Precio"]);

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public List<Producto> listarPorCategoria(int idCategoria)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
               
                string consulta = "SELECT P.ID, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.PorcentajeGanancia, P.StockActual, P.StockMinimo, P.Marca_ID, P.Categoria_ID, M.Nombre AS MarcaNombre, C.Nombre AS CategoriaNombre, P.UrlImagen FROM Productos P INNER JOIN Marcas M ON P.Marca_ID = M.ID INNER JOIN Categorias C ON P.Categoria_ID = C.ID WHERE P.Estado = 1 AND P.Categoria_ID = @idCategoria";

                datos.SetearConsulta(consulta);
                datos.SetearParametro("@idCategoria", idCategoria);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id = (int)datos.Lector["ID"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                    {
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];
                    }

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["Marca_ID"];
                    aux.Marca.Descripcion = (string)datos.Lector["MarcaNombre"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["Categoria_ID"];
                    aux.Categoria.Descripcion = (string)datos.Lector["CategoriaNombre"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }

}
