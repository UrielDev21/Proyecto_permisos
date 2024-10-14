using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manejador;

namespace Proyecto_permisos
{
    public partial class Frm_AgregarRefacciones : Form
    {
        Manejador_Refacciones mr;
        int fila = 0, columna = 0;
        public static string CodigoBarras = "", Nombre = "", Descripcion = "", Marca = "";
        public Frm_AgregarRefacciones()
        {
            InitializeComponent();
            mr = new Manejador_Refacciones();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CodigoBarras))
            {
                mr.Modificar(CodigoBarras, txtNombre, txtDescripcion, txtMarca);
            }
            else
            {
                mr.Guardar(txtBarras, txtNombre, txtDescripcion, txtMarca);
            }
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
