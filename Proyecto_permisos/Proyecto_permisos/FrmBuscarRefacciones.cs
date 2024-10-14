using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manejador;

namespace Proyecto_permisos
{
    public partial class FrmBuscarRefacciones : Form
    {
        Manejador_Refacciones mr;
        int fila = 0, columna = 0;
        public static string CodigoBarras = "", Nombre = "", Descripcion = "", Marca = "";

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        public FrmBuscarRefacciones()
        {
            InitializeComponent();
            mr = new Manejador_Refacciones();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dtgvRefacciones.Visible = true;
            mr.MostrarRefacciones(dtgvRefacciones, txtBuscar.Text);
        }

        private void dtgvRefacciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            fila = e.RowIndex;
            columna = e.ColumnIndex;

            switch (columna)
            {
                case 4:
                    {
                        CodigoBarras = dtgvRefacciones.Rows[fila].Cells[0].Value.ToString();
                        mr.Borrar(CodigoBarras, dtgvRefacciones.Rows[fila].Cells[1].Value.ToString());
                        dtgvRefacciones.Visible = false;
                    }
                    break;
                case 5:
                    {
                        CodigoBarras = dtgvRefacciones.Rows[fila].Cells[0].Value.ToString();
                        Nombre = dtgvRefacciones.Rows[fila].Cells[1].Value.ToString();
                        Descripcion = dtgvRefacciones.Rows[fila].Cells[2].Value.ToString();
                        Marca = dtgvRefacciones.Rows[fila].Cells[3].Value.ToString();
                    }
                    break;
            }
        }
    }
}
