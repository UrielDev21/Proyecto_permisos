using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Permisos
    {
        public static bool Usuarios_Lectura {  get; set; }
        public static bool Usuarios_Escritura { get; set; }
        public static bool Usuarios_Actualizacion {  get; set; }
        public static bool Usuarios_Eliminacion { get; set; }

        public static bool Refacciones_Lectura { get; set; }
        public static bool Refacciones_Escritura { get; set; }
        public static bool Refacciones_Actualizacion { get; set;  }
        public static bool Refacciones_Eliminacion { get; set;  }

        public static bool Taller_Lectura { get; set; }
        public static bool Taller_Escritura { get; set; }
        public static bool Taller_Actualizacion { get; set; }
        public static bool Taller_Eliminacion { get; set; }

        public static void LimpiarPermisos()
        {
            Usuarios_Lectura = false; 
            Usuarios_Escritura = false;
            Usuarios_Actualizacion = false;
            Usuarios_Eliminacion = false;   

            Refacciones_Lectura = false;
            Refacciones_Escritura = false;
            Refacciones_Actualizacion = false;
            Refacciones_Eliminacion = false; 

            Taller_Lectura = false;
            Taller_Escritura = false;
            Taller_Actualizacion = false;
            Taller_Eliminacion = false;
        }

        public static void Asignar_Permisos(string formulario, bool lectura, bool escritura, bool actualizacion, bool eliminacion)
        {
            switch(formulario)
            {
                case "Usuarios":
                    {
                        Usuarios_Lectura = lectura; 
                        Usuarios_Escritura = escritura;
                        Usuarios_Actualizacion = actualizacion;
                        Usuarios_Eliminacion= eliminacion;
                    }break;
                case "Refacciones":
                    {
                        Refacciones_Lectura = lectura;
                        Refacciones_Escritura = escritura;
                        Refacciones_Actualizacion = actualizacion;
                        Refacciones_Eliminacion = eliminacion;
                    }break;
                case "Taller":
                    {
                        Taller_Lectura = lectura;
                        Taller_Escritura = escritura; 
                        Taller_Actualizacion = actualizacion;
                        Taller_Eliminacion = eliminacion; 
                    }break;

                default:
                    throw new ArgumentException("Opcion no reconocida"); 
            }
        }
    }
}
