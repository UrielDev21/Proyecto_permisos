using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acceso_datos; 

namespace Manejador
{
    public class Manejador_taller
    {
        Funciones f = new Funciones();

        // Funcion para guardar los registros en la tabla de herramientas
        public void GuardarHerramientas(TextBox CodigoHerramienta, TextBox Nombre, TextBox Medida, TextBox Marca, TextBox Descripcion)
        {
            MessageBox.Show(f.Guardar($"call p_insertar_herramienta('{CodigoHerramienta.Text}', '{Nombre.Text}', '{Medida.Text}', '{Marca.Text}', '{Descripcion.Text}')"), 
                "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
    }
}
