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
    public partial class Frm_AgregarUsuarios : Form
    {
        Manejador_usuarios mu; 

        public Frm_AgregarUsuarios()
        {
            InitializeComponent();
            mu = new Manejador_usuarios();
            Cargar(); 
        }
        private void Cargar()
        {
            dtgvPermisos.Rows.Clear();

            string[] formularios = { "Usuarios", "Taller", "Refacciones" };
            foreach (string formulario in formularios)
            {
                dtgvPermisos.Rows.Add(formulario, false, false, false, false);
            }

            // Ajusta el tamaño de las columnas automáticamente para que se vean bien.
            dtgvPermisos.AutoResizeColumns();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Frm_BuscarUsuarios.user))
            {
                mu.Guardar(txtNombre, txtApellidoP, txtApellidoM, dtpFecha_nacimiento, txtRFC, txtUser, txtPass, dtgvPermisos);
            }
            else
            {
                mu.Modificar(txtNombre, txtApellidoP, txtApellidoM, dtpFecha_nacimiento, txtRFC, txtUser, txtPass, dtgvPermisos); 
            }
            Close(); 
        }
        public void DatosUsuarios(string nombre, string apellidop, string apellidom, string fecha_nacimiento, string rfc, string user, string pass)
        {
            txtNombre.Text = nombre;
            txtApellidoP.Text = apellidop;
            txtApellidoM.Text = apellidom;
            dtpFecha_nacimiento.Value = DateTime.Parse(fecha_nacimiento);
            txtRFC.Text = rfc;
            txtUser.Text = user;
            txtPass.Text = pass;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
