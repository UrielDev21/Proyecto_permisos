using Manejador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            if (Frm_BuscarHerramintas.CodigoHerramienta.Length > 0)
            {
                txtCodigoHerramienta.Text = Frm_BuscarHerramintas.CodigoHerramienta; 
                txtNombre.Text = Frm_BuscarHerramintas.Nombre;
                txtDescrpcion.Text = Frm_BuscarHerramintas.Descripcion;
                txtMedida.Text = Frm_BuscarHerramintas.Medida;
                txtMarca.Text = Frm_BuscarHerramintas.Marca;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        { 
            if (Frm_BuscarHerramintas.CodigoHerramienta.Length > 0)
            {
                // Se manda a llamar la funcion para modificar herramientas
                mt.ModificarHerramientas(txtCodigoHerramienta, txtNombre, txtMedida, txtMarca, txtDescrpcion);
            }
            else
            {
                // Se manda a llamar a la funcion para guardar herramientas
                mt.GuardarHerramientas(txtCodigoHerramienta, txtNombre, txtMedida, txtMarca, txtDescrpcion);
            }
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}