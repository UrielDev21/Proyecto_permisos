using System;
using System.Collections.Generic;
using System.Drawing;
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

        // Funcion para modificar los registros en la tabla herramientas
        public void ModificarHerramientas(TextBox CodigoHerramienta, TextBox Nombre, TextBox Medida, TextBox Marca, TextBox Descripcion)
        {
            MessageBox.Show(f.Guardar($"call p_modificar_herramienta('{CodigoHerramienta.Text}', '{Nombre.Text}', '{Medida.Text}', '{Marca.Text}', '{Descripcion.Text}')"),
                "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        // Funcion para eliminar los registros de la tabla de herramientas
        public void EliminarHerramientas(string CodigoHerramienta, string dato)
        {
            DialogResult rs = MessageBox.Show($"Estas seguro de eliminar {dato}", "ATENCION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (rs == DialogResult.Yes)
            {
                f.Borrar($"call p_eliminar_herramienta('{CodigoHerramienta}')");
                MessageBox.Show("Registro eliminado", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Funcion para poder crear botones dentro del datagrid
        DataGridViewButtonColumn Boton(string Texto, Color fondo)
        {
            DataGridViewButtonColumn b = new DataGridViewButtonColumn();
            b.Text = Texto;
            b.UseColumnTextForButtonValue = true;
            b.FlatStyle = FlatStyle.Popup; 
            b.DefaultCellStyle.BackColor = fondo;
            b.DefaultCellStyle.ForeColor = Color.White; 
            return b;
        }

        //Funcion para poder mostrar las herramientas y los botones
        public void MostrarHerramientas(DataGridView tabla, string filtro)
        {
            tabla.Columns.Clear();
            tabla.DataSource = f.Mostrar($"select * from v_vista_taller where nombre like '%{filtro}%'", "taller").Tables[0];
            tabla.Columns.Add(Boton("Modificar", Color.Green));
            // Nuevo boton para eliminar
            tabla.Columns.Add(Boton("Eliminar", Color.Red)); 
            tabla.AutoResizeColumns();
            tabla.AutoResizeRows(); 
        }

    }
}
