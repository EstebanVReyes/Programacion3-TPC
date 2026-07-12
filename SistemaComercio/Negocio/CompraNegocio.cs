using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CompraNegocio
    {
        public List<Compra> listar()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT C.ID, C.FechaCompra, C.Total, P.ID AS IdProveedor, P.Nombre AS NombreProveedor FROM Compras C INNER JOIN Proveedores P ON C.Proveedor_ID = P.ID");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["FechaCompra"];
                    aux.Total = (decimal)datos.Lector["Total"];

                    aux.Proveedor = new Proveedor();
                    aux.Proveedor.Id = (int)datos.Lector["IdProveedor"];
                    aux.Proveedor.Nombre = (string)datos.Lector["NombreProveedor"];

                    aux.Detalles = listarDetalles(aux.Id);

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

        public List<DetalleCompra> listarDetalles(int idCompra)
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
                    SELECT D.ID AS IdDetalle, D.Cantidad, D.PrecioCosto,
                           P.ID AS IdProducto, P.Nombre AS NombreProducto
                    FROM Detalles_Compra D
                    INNER JOIN Productos P ON D.Producto_ID = P.ID
                    WHERE D.Compra_ID = @idCompra");
                datos.SetearParametro("@idCompra", idCompra);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra detalle = new DetalleCompra();
                    detalle.Id = (int)datos.Lector["IdDetalle"];
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalle.PrecioCosto = (decimal)datos.Lector["PrecioCosto"];
                    detalle.Producto = new Producto();
                    detalle.Producto.Id = (int)datos.Lector["IdProducto"];
                    detalle.Producto.Nombre = (string)datos.Lector["NombreProducto"];

                    lista.Add(detalle);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los detalles de la compra: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void agregar(Compra nuevaCompra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Compras (FechaCompra, Total, Proveedor_ID) OUTPUT INSERTED.ID VALUES (@fecha, @total, @idProveedor)");

                datos.SetearParametro("@fecha", nuevaCompra.Fecha);
                datos.SetearParametro("@total", nuevaCompra.Total);
                datos.SetearParametro("@idProveedor", nuevaCompra.Proveedor.Id);

                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    nuevaCompra.Id = (int)datos.Lector["ID"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

            ProductoNegocio negocioProd = new ProductoNegocio();

            foreach (DetalleCompra item in nuevaCompra.Detalles)
            {
                AccesoDatos datosDetalle = new AccesoDatos();
                try
                {
                    datosDetalle.SetearConsulta("INSERT INTO Detalles_Compra (Compra_ID, Producto_ID, Cantidad, PrecioCosto) VALUES (@idCompra, @idProducto, @cantidad, @precioCosto)");

                    datosDetalle.SetearParametro("@idCompra", nuevaCompra.Id);
                    datosDetalle.SetearParametro("@idProducto", item.Producto.Id);
                    datosDetalle.SetearParametro("@cantidad", item.Cantidad);
                    datosDetalle.SetearParametro("@precioCosto", item.PrecioCosto);

                    datosDetalle.EjecutarAccion();

                    negocioProd.actualizarStock(item.Producto.Id, item.Cantidad);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    datosDetalle.CerrarConexion();
                }
            }
        }
    }
}
