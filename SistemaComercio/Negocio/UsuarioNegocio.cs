using System;
using System.Collections.Generic;
using Dominio;


namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> ListarActivos()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT ID, Username, PasswordHash, TipoUsuario, Estado FROM Usuarios WHERE Estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.ID = (int)datos.Lector["ID"];
                    aux.Username = (string)datos.Lector["Username"];
                    aux.PasswordHash = (string)datos.Lector["PasswordHash"];
                    aux.TipoUsuario = (string)datos.Lector["TipoUsuario"];
                    aux.Estado = (bool)datos.Lector["Estado"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los usuarios: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool Agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Usuarios (Username, PasswordHash, TipoUsuario, Estado) VALUES (@Username, @PasswordHash, @TipoUsuario, 1)");
                datos.SetearParametro("@Username", nuevo.Username);
                datos.SetearParametro("@PasswordHash", nuevo.PasswordHash);
                datos.SetearParametro("@TipoUsuario", nuevo.TipoUsuario);

                datos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario: " + ex.Message);
            }
        }

        public Usuario ValidarLogin(string username, string passwordHash)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT ID, Username, TipoUsuario, Estado FROM Usuarios WHERE Username = @User AND PasswordHash = @Pass AND Estado = 1");
                datos.SetearParametro("@User", username);
                datos.SetearParametro("@Pass", passwordHash);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuarioLogueado = new Usuario();
                    usuarioLogueado.ID = (int)datos.Lector["ID"];
                    usuarioLogueado.Username = (string)datos.Lector["Username"];
                    usuarioLogueado.TipoUsuario = (string)datos.Lector["TipoUsuario"];
                    usuarioLogueado.Estado = (bool)datos.Lector["Estado"];

                    return usuarioLogueado;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar iniciar sesión: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void BajaLogica(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Estado = 0 WHERE ID = @Id");
                datos.SetearParametro("@Id", idUsuario);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al dar de baja el usuario: " + ex.Message);
            }
        }
    }
}