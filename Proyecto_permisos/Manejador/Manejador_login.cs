using Acceso_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Manejador
{
    public class Manejador_login
    {
            Funciones f = new Funciones();

            public string[] Validar(string _user, string _pass)
            {
                string[] resultado = new string[2];
                DataSet r = f.Mostrar($"call p_validar('{_user}', '{Sha1(_pass)}')", "usuarios");
                DataTable dt = r.Tables[0];

                if (dt.Rows.Count > 0) // Asegúrate de que hay filas
                {
                    resultado[0] = "Correcto"; // O cualquier valor que estés usando para indicar que la validación fue correcta
                    resultado[1] = dt.Rows[0]["Username"].ToString(); // Cambia esto según tu tabla

                    // Limpiar los permisos antes de asignar nuevos
                    Permisos.LimpiarPermisos();

                    // Asignar permisos a partir de la tabla
                    foreach (DataRow row in dt.Rows)
                    {
                        string formulario = row["nombre_formulario"].ToString();
                        Permisos.Asignar_Permisos(formulario,
                            Convert.ToBoolean(row["Lectura"]),
                            Convert.ToBoolean(row["Escritura"]),
                            Convert.ToBoolean(row["Actualizacion"]),
                            Convert.ToBoolean(row["Eliminacion"]));
                    }
                }
                else
                {
                    resultado[0] = "Incorrecto"; // O un mensaje adecuado
                    resultado[1] = string.Empty;
                }

                return resultado;
            }

            public static string Sha1(string texto)
            {
                SHA1 sha1 = SHA1CryptoServiceProvider.Create();
                Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(texto);
                Byte[] hash = sha1.ComputeHash(textOriginal);
                StringBuilder cadena = new StringBuilder();
                foreach (byte i in hash)
                {
                    cadena.AppendFormat("{0:x2}", i);
                }
                return cadena.ToString();
            }
        }
    }
