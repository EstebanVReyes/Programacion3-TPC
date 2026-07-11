using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT ID, Nombre, Telefono, Descripcion, Estado FROM Proveedores WHERE Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["Telefono"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Estado = (bool)datos.Lector["Estado"];
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

        public Proveedor ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT ID, Nombre, Telefono, Descripcion, Estado FROM Proveedores WHERE ID = @Id");
                datos.SetearParametro("@Id", id);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["Telefono"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Estado = (bool)datos.Lector["Estado"];
                    return aux;
                }
                return null;
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

        public void Agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                    "INSERT INTO Proveedores (Nombre, Telefono, Descripcion) " +
                    "VALUES (@Nombre, @Telefono, @Descripcion)");

                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Telefono",
                    nuevo.Telefono != null ? nuevo.Telefono : (object)DBNull.Value);
                datos.SetearParametro("@Descripcion",
                    nuevo.Descripcion != null ? nuevo.Descripcion : (object)DBNull.Value);

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

        public void Modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                    "UPDATE Proveedores SET Nombre = @Nombre, Telefono = @Telefono, " +
                    "Descripcion = @Descripcion WHERE ID = @Id");

                datos.SetearParametro("@Nombre", proveedor.Nombre);
                datos.SetearParametro("@Telefono",
                    proveedor.Telefono != null ? proveedor.Telefono : (object)DBNull.Value);
                datos.SetearParametro("@Descripcion",
                    proveedor.Descripcion != null ? proveedor.Descripcion : (object)DBNull.Value);
                datos.SetearParametro("@Id", proveedor.Id);

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
                datos.SetearConsulta("UPDATE Proveedores SET Estado = 0 WHERE ID = @Id");
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

        public List<DetalleProveedor> ObtenerProductosDeProveedor(int proveedorId)
        {
            List<DetalleProveedor> lista = new List<DetalleProveedor>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                    "SELECT PP.Cantidad, P.ID AS IdProducto, P.Nombre AS NombreProducto " +
                    "FROM Productos_Proveedores PP " +
                    "INNER JOIN Productos P ON PP.Producto_ID = P.ID " +
                    "WHERE PP.Proveedor_ID = @ProveedorId");
                datos.SetearParametro("@ProveedorId", proveedorId);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleProveedor detalle = new DetalleProveedor();
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalle.Producto = new Producto();
                    detalle.Producto.Id = (int)datos.Lector["IdProducto"];
                    detalle.Producto.Nombre = (string)datos.Lector["NombreProducto"];
                    lista.Add(detalle);
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

        public void GuardarProductosProveedor(int proveedorId, List<DetalleProveedor> detalles)
        {
            AccesoDatos datosBorrar = new AccesoDatos();
            try
            {
                datosBorrar.SetearConsulta(
                    "DELETE FROM Productos_Proveedores WHERE Proveedor_ID = @ProveedorId");
                datosBorrar.SetearParametro("@ProveedorId", proveedorId);
                datosBorrar.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosBorrar.CerrarConexion();
            }

            foreach (DetalleProveedor detalle in detalles)
            {
                AccesoDatos datosInsertar = new AccesoDatos();
                try
                {
                    datosInsertar.SetearConsulta(
                        "INSERT INTO Productos_Proveedores (Producto_ID, Proveedor_ID, Cantidad) " +
                        "VALUES (@ProductoId, @ProveedorId, @Cantidad)");
                    datosInsertar.SetearParametro("@ProductoId", detalle.Producto.Id);
                    datosInsertar.SetearParametro("@ProveedorId", proveedorId);
                    datosInsertar.SetearParametro("@Cantidad", detalle.Cantidad);
                    datosInsertar.EjecutarAccion();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    datosInsertar.CerrarConexion();
                }
            }
        }
    }
}