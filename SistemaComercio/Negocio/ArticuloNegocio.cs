using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetearConsulta("SELECT A.ID, A.CodigoSKU, A.Nombre, A.Descripcion, A.PrecioLista, A.StockFisico, A.Marca_ID, A.Categoria_ID, M.Nombre AS MarcaNombre, C.Nombre AS CategoriaNombre FROM Articulos A INNER JOIN Marcas M ON A.Marca_ID = M.ID INNER JOIN Categorias C ON A.Categoria_ID = C.ID");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Codigo = (string)datos.Lector["CodigoSKU"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["PrecioLista"];
                    aux.StockActual = (int)datos.Lector["StockFisico"];

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

        
        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Articulos (CodigoSKU, Nombre, Descripcion, PrecioLista, StockFisico, Categoria_ID, Marca_ID) VALUES (@Codigo, @Nombre, @Desc, @Precio, @Stock, @IdCat, @IdMarca)");

                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Desc", nuevo.Descripcion);
                datos.SetearParametro("@Precio", nuevo.Precio);
                datos.SetearParametro("@Stock", nuevo.StockActual);
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

        
        public void Modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
               
                datos.SetearConsulta("UPDATE Articulos SET CodigoSKU = @Codigo, Nombre = @Nombre, Descripcion = @Desc, PrecioLista = @Precio, StockFisico = @Stock, Categoria_ID = @IdCat, Marca_ID = @IdMarca WHERE ID = @Id");

                datos.SetearParametro("@Codigo", art.Codigo);
                datos.SetearParametro("@Nombre", art.Nombre);
                datos.SetearParametro("@Desc", art.Descripcion);
                datos.SetearParametro("@Precio", art.Precio);
                datos.SetearParametro("@Stock", art.StockActual);
                datos.SetearParametro("@IdCat", art.Categoria.Id);
                datos.SetearParametro("@IdMarca", art.Marca.Id);

               
                datos.SetearParametro("@Id", art.Id);

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
             
                datos.SetearConsulta("DELETE FROM Articulos WHERE ID = @Id");
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