using Manejador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_permisos
{
    public partial class Frm_BuscarHerramintas : Form
    {
        // Se manda llamar a la clase para crear el objeto
        Manejador_taller mt;

        // Variables para usar al momento de crear las columnas en el datagrid
        int fila = 0, columna = 0;

        // Variables publicas estaticas para usar en los diferentes formularios
        public static string CodigoHerramienta = "";
        public static string Nombre = "";
        public static string Medida = "";
        public static string Marca = "";
        public static string Descripcion = "";

        public Frm_BuscarHerramintas()
        {
            InitializeComponent();
            mt = new Manejador_taller();
        }
        private void dtgvTaller_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            fila = e.RowIndex;
            columna = e.ColumnIndex;

            switch(columna)
            {
                case 5:
                    {
                        CodigoHerramienta = (dtgvTaller.Rows[fila].Cells[0].Value.ToString());
                        Nombre = (dtgvTaller.Rows[fila].Cells[1].Value.ToString());
                        Medida = (dtgvTaller.Rows[fila].Cells[2].Value.ToString()); 
                        Marca = (dtgvTaller.Rows[fila].Cells[3].Value.ToString());  
                        Descripcion = (dtgvTaller.Rows[fila].Cells[4].Value.ToString());
                        
                        Frm_AgregarHerramientas fam = new Frm_AgregarHerramientas();
                        fam.ShowDialog();
                        dtgvTaller.Visible = false; 

                    }break; 
            }
        }

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            dtgvTaller.Visible = true; 
            mt.MostrarHerramientas(dtgvTaller, txtBuscar.Text);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
