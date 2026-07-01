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

        public int Agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                    "INSERT INTO Proveedores (Nombre, Telefono, Descripcion) " +
                    "VALUES (@Nombre, @Telefono, @Descripcion); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT)");

                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Telefono",
                    nuevo.Telefono != null ? nuevo.Telefono : (object)DBNull.Value);
                datos.SetearParametro("@Descripcion",
                    nuevo.Descripcion != null ? nuevo.Descripcion : (object)DBNull.Value);

                datos.EjecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector[0];
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

        public Dictionary<int, string> ListarProductosActivos()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT ID, Nombre FROM Productos WHERE Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    dict.Add((int)datos.Lector["ID"], (string)datos.Lector["Nombre"]);
                }
                return dict;
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

        public List<int> ObtenerProductosDeProveedor(int proveedorId)
        {
            List<int> ids = new List<int>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                    "SELECT Producto_ID FROM Productos_Proveedores WHERE Proveedor_ID = @ProveedorId");
                datos.SetearParametro("@ProveedorId", proveedorId);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ids.Add((int)datos.Lector["Producto_ID"]);
                }
                return ids;
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

        public void ActualizarProductosProveedor(int proveedorId, List<int> productosIds)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                    "DELETE FROM Productos_Proveedores WHERE Proveedor_ID = @ProveedorId");
                datos.SetearParametro("@ProveedorId", proveedorId);
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

            foreach (int productoId in productosIds)
            {
                AccesoDatos datos2 = new AccesoDatos();
                try
                {
                    datos2.SetearConsulta(
                        "INSERT INTO Productos_Proveedores (Producto_ID, Proveedor_ID) " +
                        "VALUES (@ProductoId, @ProveedorId)");
                    datos2.SetearParametro("@ProductoId", productoId);
                    datos2.SetearParametro("@ProveedorId", proveedorId);
                    datos2.EjecutarAccion();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    datos2.CerrarConexion();
                }
            }
        }
    }
}