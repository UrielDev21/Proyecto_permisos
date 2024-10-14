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
    public partial class Frm_InicioSesion : Form
    {
        Manejador_login ml;

        public Frm_InicioSesion()
        {
            InitializeComponent();
            ml = new Manejador_login();
        }
        private void btnAcceder_Click(object sender, EventArgs e)
        {
            string[] r = ml.Validar(txtUser.Text, txtPass.Text);
            if (r[0].Equals("Correcto"))
            {
                this.Hide(); 

                Frm_Menu fm = new Frm_Menu();   
                fm.Show();
            }
            else
            {
                MessageBox.Show("Nombre o contraseña incorrectos", "ATENCIO", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }
    }
}
