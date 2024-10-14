using Manejador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades; 

namespace Proyecto_permisos
{
    public partial class Frm_BuscarUsuarios : Form
    {
        Manejador_usuarios mu; 

        public static string nombre = "";
        public static string apellidoM = "";
        public static string apellidoP = "";
        public static string fecha_nacimiento = "";
        public static string Rfc = "";
        public static string user = "";
        public static string pass = ""; 

        public Frm_BuscarUsuarios()
        {
            InitializeComponent();
            mu = new Manejador_usuarios();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(dtgvUsuarios.SelectedRows.Count > 0) 
            {
                string userSeleccionado = dtgvUsuarios.SelectedRows[5].Cells["Username"].Value.ToString();
                mu.Borrar(userSeleccionado);
            }
            else
            {
                MessageBox.Show("Debe de seleccionar el usuario para poder borrarlo", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Stop); 
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Frm_AgregarUsuarios fau = new Frm_AgregarUsuarios();
            fau.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(user)) 
            {
                Frm_AgregarUsuarios fau = new Frm_AgregarUsuarios();
                fau.DatosUsuarios(nombre, apellidoP, apellidoM, fecha_nacimiento, Rfc, user, pass);
                fau.ShowDialog(); 
            }
            else
            {
                MessageBox.Show("Debes de seleccionar un usuario para poder borrarlo", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Stop); 
            }
        }
        public void Verificar()
        {
            btnAgregar.Enabled = Permisos.Usuarios_Escritura;
            btnModificar.Enabled = Permisos.Usuarios_Actualizacion;
            btnBorrar.Enabled = Permisos.Usuarios_Eliminacion; 
        }

        private void dtgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                DataGridViewRow row = dtgvUsuarios.Rows[e.RowIndex];

                nombre = row.Cells["Nombre"].Value.ToString();
                apellidoP = row.Cells["Apellido paterno"].Value.ToString();
                apellidoM = row.Cells["Apellido materno"].Value.ToString();
                fecha_nacimiento = Convert.ToDateTime(row.Cells["Fecha de nacimiento"].Value).ToString("yyyy-MM-dd");
                Rfc = row.Cells["RFC"].Value.ToString();
                user = row.Cells["Username"].Value.ToString();
                pass = row.Cells["Passowrd"].Value.ToString(); 

                row.Selected = true;    
            }
        }
    }
}
