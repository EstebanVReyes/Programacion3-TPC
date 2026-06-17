using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class VentaNegocio
    {

        public List<Venta> listar()
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT V.ID, V.NumeroFactura, V.FechaVenta, V.Estado, V.Total, C.ID AS IdCliente, C.Nombre, C.Apellido FROM Ventas V INNER JOIN Clientes C ON V.Cliente_ID = C.ID");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.NumeroFactura = (string)datos.Lector["NumeroFactura"];
                    aux.Fecha = (DateTime)datos.Lector["FechaVenta"];
                    aux.Estado = (string)datos.Lector["Estado"];
                    aux.Total = (decimal)datos.Lector["Total"];

                    aux.Cliente = new Cliente();
                    aux.Cliente.Id = (int)datos.Lector["Id"];
                    aux.Cliente.Nombre = (string)datos.Lector["Nombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["Apellido"];

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


        public List<DetalleVenta> listarDetalles(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT DV.ID, DV.Cantidad, DV.PrecioUnitario, P.ID AS IdProducto, P.Nombre, P.Codigo FROM Detalles_Venta DV INNER JOIN Productos P ON DV.Producto_ID = P.ID WHERE DV.Venta_ID = @idVenta");
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta aux = new DetalleVenta();

                    aux.Id = (int)datos.Lector["ID"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];

                    aux.Producto = new Producto();
                    aux.Producto.Id = (int)datos.Lector["Id"];
                    aux.Producto.Nombre = (string)datos.Lector["Nombre"];
                    aux.Producto.Codigo = (string)datos.Lector["Codigo"];

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


        public void agregar(Venta nuevaVenta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetearConsulta("INSERT INTO Ventas (NumeroFactura, Estado, Total, Cliente_ID, Usuario_ID) OUTPUT INSERTED.ID VALUES (@numeroFactura, @estado, @total, @idCliente, @idUsuario)");

                datos.SetearParametro("@numeroFactura", nuevaVenta.NumeroFactura);
                datos.SetearParametro("@estado", "Pagado");
                datos.SetearParametro("@total", nuevaVenta.Total);
                datos.SetearParametro("@idCliente", nuevaVenta.Cliente.Id);
                datos.SetearParametro("@idUsuario", 2);

                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {

                    nuevaVenta.Id = (int)datos.Lector["ID"];
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

            foreach (DetalleVenta item in nuevaVenta.Detalles)
            {
                AccesoDatos datosDetalle = new AccesoDatos();
                try
                {

                    datosDetalle.SetearConsulta("INSERT INTO Detalles_Venta (Venta_ID, Producto_ID, Cantidad, PrecioUnitario) VALUES (@idVenta, @idProducto, @cantidad, @precioUnitario)");

                    datosDetalle.SetearParametro("@idVenta", nuevaVenta.Id);
                    datosDetalle.SetearParametro("@idProducto", item.Producto.Id);
                    datosDetalle.SetearParametro("@cantidad", item.Cantidad);
                    datosDetalle.SetearParametro("@precioUnitario", item.PrecioUnitario);

                    datosDetalle.EjecutarAccion();



                    negocioProd.actualizarStock(item.Producto.Id, item.Cantidad * -1);
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