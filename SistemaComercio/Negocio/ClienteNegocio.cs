using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                
                datos.SetearConsulta("SELECT ID, DNI, Nombre, Apellido, Email, Telefono, FechaRegistro, Estado FROM Clientes WHERE Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.DNI = (string)datos.Lector["DNI"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];

                   
                    if (!(datos.Lector["Telefono"] is DBNull))
                    {
                        aux.Telefono = (string)datos.Lector["Telefono"];
                    }

                    

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

        public void Agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
             
                datos.SetearConsulta("INSERT INTO Clientes (DNI, Nombre, Apellido, Email, Telefono) VALUES (@Dni, @Nombre, @Apellido, @Email, @Telefono)");

                datos.SetearParametro("@Dni", nuevo.DNI);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@Email", nuevo.Email);

                
                datos.SetearParametro("@Telefono", nuevo.Telefono != null ? nuevo.Telefono : (object)DBNull.Value);

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

        public void Modificar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Clientes SET DNI = @Dni, Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE ID = @Id");

                datos.SetearParametro("@Dni", cliente.DNI);
                datos.SetearParametro("@Nombre", cliente.Nombre);
                datos.SetearParametro("@Apellido", cliente.Apellido);
                datos.SetearParametro("@Email", cliente.Email);
                datos.SetearParametro("@Telefono", cliente.Telefono != null ? cliente.Telefono : (object)DBNull.Value);
                datos.SetearParametro("@Id", cliente.Id);

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
                
                datos.SetearConsulta("UPDATE Clientes SET Estado = 0 WHERE ID = @Id");
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