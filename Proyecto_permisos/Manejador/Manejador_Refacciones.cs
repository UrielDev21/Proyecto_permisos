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
    public class Manejador_Refacciones
    {
        Funciones f = new Funciones();

        // Método para guardar una nueva refacción
        public void Guardar(TextBox CodigoBarras, TextBox Nombre, TextBox Descripcion, TextBox Marca)
        {
            MessageBox.Show(f.Guardar($"call p_insertar_refacciones('{CodigoBarras.Text}', '{Nombre.Text}', '{Descripcion.Text}', '{Marca.Text}')"),
                "!ATENCIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Método para borrar una refacción
        public void Borrar(string CodigoBarras, string dato)
        {
            DialogResult rs = MessageBox.Show($"¿Estás seguro de borrar la refacción {dato}?", "!ATENCIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (rs == DialogResult.Yes)
            {
                f.Borrar($"call p_eliminar_refacciones('{CodigoBarras}')");
                MessageBox.Show("Refacción eliminada", "!ATENCIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Método para modificar una refacción
        public void Modificar(string CodigoBarras, TextBox Nombre, TextBox Descripcion, TextBox Marca)
        {
            MessageBox.Show(f.Modificar($"call p_modificar_refacciones('{CodigoBarras}', '{Nombre.Text}', '{Descripcion.Text}', '{Marca.Text}')"),
                "!ATENCIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Método para crear botones en la tabla
        DataGridViewButtonColumn Boton(string t, Color fondo)
        {
            DataGridViewButtonColumn b = new DataGridViewButtonColumn();
            b.Text = t;
            b.UseColumnTextForButtonValue = true;
            b.FlatStyle = FlatStyle.Popup;
            b.DefaultCellStyle.BackColor = fondo;
            b.DefaultCellStyle.ForeColor = Color.White;
            return b;
        }

        // Método para mostrar las refacciones en el DataGridView
        public void MostrarRefacciones(DataGridView tabla, string filtro)
        {
            tabla.Columns.Clear();
            tabla.DataSource = f.Mostrar($"select * from v_vista_refacciones where nombre like '%{filtro}%'", "refacciones").Tables[0];
            tabla.Columns.Insert(4, Boton("Borrar", Color.Red));
            tabla.Columns.Insert(5, Boton("Editar", Color.Green));
            tabla.AutoResizeColumns();
            tabla.AutoResizeRows();
        }
    }
}
