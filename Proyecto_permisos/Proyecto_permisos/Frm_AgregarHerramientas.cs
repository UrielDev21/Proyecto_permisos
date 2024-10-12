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
    public partial class Frm_AgregarHerramientas : Form
    {
        // Mandar a llamar a la clase de manejador para crear su objeto
        Manejador_taller mt; 

        public Frm_AgregarHerramientas()
        {
            InitializeComponent();
            mt = new Manejador_taller();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Se manda a llamar a la funcion para guardar herramientas
            mt.GuardarHerramientas(txtCodigoHerramienta, txtNombre, txtMedida, txtMarca, txtDescrpcion);
            Close(); 
        }
    }
}
