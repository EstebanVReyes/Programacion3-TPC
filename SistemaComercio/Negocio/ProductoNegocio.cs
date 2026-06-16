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

                datos.SetearConsulta("SELECT P.ID, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.PorcentajeGanancia, P.StockActual, P.StockMinimo, P.Marca_ID, P.Categoria_ID, M.Nombre AS MarcaNombre, C.Nombre AS CategoriaNombre FROM Productos P INNER JOIN Marcas M ON P.Marca_ID = M.ID INNER JOIN Categorias C ON P.Categoria_ID = C.ID");
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
                datos.SetearConsulta("INSERT INTO Productos (Codigo, Nombre, Descripcion, Precio, PorcentajeGanancia, StockActual, StockMinimo, Categoria_ID, Marca_ID) VALUES (@Codigo, @Nombre, @Desc, @Precio, @Porcentaje, @Stock, @StockMin, @IdCat, @IdMarca)");

                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Desc", nuevo.Descripcion);
                datos.SetearParametro("@Precio", nuevo.Precio);
                datos.SetearParametro("@Porcentaje", nuevo.PorcentajeGanancia);
                datos.SetearParametro("@Stock", nuevo.StockActual);
                datos.SetearParametro("@StockMin", nuevo.StockMinimo);
                datos.SetearParametro("@IdCat", nuevo.Categoria.Id);
                datos.SetearParametro("@IdMarca", nuevo.Marca.Id);

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

                datos.SetearConsulta("UPDATE Productos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Desc, Precio = @Precio, PorcentajeGanancia = @Porcentaje, StockActual = @Stock, StockMinimo = @StockMin, Categoria_ID = @IdCat, Marca_ID = @IdMarca WHERE ID = @Id");

                datos.SetearParametro("@Codigo", producto.Codigo);
                datos.SetearParametro("@Nombre", producto.Nombre);
                datos.SetearParametro("@Desc", producto.Descripcion);
                datos.SetearParametro("@Precio", producto.Precio);
                datos.SetearParametro("@Porcentaje", producto.PorcentajeGanancia);
                datos.SetearParametro("@Stock", producto.StockActual);
                datos.SetearParametro("@StockMin", producto.StockMinimo);
                datos.SetearParametro("@IdCat", producto.Categoria.Id);
                datos.SetearParametro("@IdMarca", producto.Marca.Id);
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
    }
}