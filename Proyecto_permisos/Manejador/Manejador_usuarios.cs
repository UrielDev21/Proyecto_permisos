using Acceso_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manejador
{
   public class Manejador_usuarios
    {
        Funciones f = new Funciones(); 
public void Guardar(TextBox Nombre, TextBox ApellidoP, TextBox ApellidoM, DateTimePicker Fecha_nacimiento, TextBox RFC, TextBox User, TextBox Pass, DataGridView Permisos)
{
    // Verificar si algún TextBox o el DataGridView es null
    if (Nombre == null || ApellidoP == null || ApellidoM == null || Fecha_nacimiento == null || RFC == null || User == null || Pass == null || Permisos == null)
    {
        MessageBox.Show("Por favor, verifica que todos los campos y el DataGridView estén correctamente inicializados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    // Verificar si los TextBox tienen datos
    if (string.IsNullOrWhiteSpace(Nombre.Text) || string.IsNullOrWhiteSpace(ApellidoP.Text) || string.IsNullOrWhiteSpace(ApellidoM.Text) ||
        string.IsNullOrWhiteSpace(RFC.Text) || string.IsNullOrWhiteSpace(User.Text) || string.IsNullOrWhiteSpace(Pass.Text))
    {
        MessageBox.Show("Por favor, completa todos los campos de texto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    string FechaNacimiento = Fecha_nacimiento.Value.ToString("yyyy-MM-dd");
    string GuardarUsuario = $"call p_insertar_usuarios('{Nombre.Text}', '{ApellidoP.Text}', '{ApellidoM.Text}', '{FechaNacimiento}', '{RFC.Text}', '{User.Text}', sha1('{Pass.Text}'));";
    
    // Mostrar el comando para depuración
    MessageBox.Show(GuardarUsuario);

    string resultado = f.Guardar(GuardarUsuario); 

    if (resultado != "Correcto")
    {
        MessageBox.Show("Error al guardar el usuario", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return; 
    }

    // Verificar que el DataGridView tenga al menos una fila
    if (Permisos.Rows.Count == 0)
    {
        MessageBox.Show("Por favor, asegúrate de agregar al menos un permiso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    foreach (DataGridViewRow row in Permisos.Rows)
    {

                string nombreFormulario = row.Cells["Formulario"].Value.ToString();

                // Si las celdas de permisos son nulas, tratarlas como 'false'
                bool lectura = row.Cells["Lectura"].Value != null && Convert.ToBoolean(row.Cells["Lectura"].Value);
                bool escritura = row.Cells["Escritura"].Value != null && Convert.ToBoolean(row.Cells["Escritura"].Value);
                bool actualizacion = row.Cells["Actualizacion"].Value != null && Convert.ToBoolean(row.Cells["Actualizacion"].Value);
                bool eliminacion = row.Cells["Eliminacion"].Value != null && Convert.ToBoolean(row.Cells["Eliminacion"].Value);

                string GuardarPemiso = $"call p_insertar_permiso('{User.Text}', '{nombreFormulario}', {lectura}, {escritura}, {actualizacion}, {eliminacion})";
                string resultadoPermiso = f.Guardar(GuardarPemiso);

                if (resultadoPermiso != "Correcto")
                {
                    MessageBox.Show("Error al guardar el permiso", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
    MessageBox.Show("Se han guardado correctamente", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
}

        public void Modificar(TextBox Nombre, TextBox ApellidoP, TextBox ApellidoM, DateTimePicker Fecha_nacimiento, TextBox RFC, TextBox User, TextBox Pass, DataGridView Permisos)
        {
            string FechaNacimiento = Fecha_nacimiento.Value.ToString("yyyy-MM-dd");

            string ModificarUsuario = $"call p_modificar_usuarios('{Nombre.Text}', '{ApellidoP.Text}', '{ApellidoM.Text}', '{FechaNacimiento}', '{RFC.Text}', '{User.Text}', '{Pass.Text}')";

            string ResultadoUsuario = f.Guardar(ModificarUsuario); 

            if(ResultadoUsuario != "Correto")
            {
                MessageBox.Show("Error al hacer la modificacion");
                return; 
            }

            string BorrarPermisos = $"call p_eliminar_permisos('{User.Text}')";
            f.Guardar(BorrarPermisos);  

            foreach(DataGridViewRow row in Permisos.Rows)
            {
                string NombreFormulario = row.Cells["Formulario"].Value.ToString();
                bool lectura = Convert.ToBoolean(row.Cells["Lectura"].Value);
                bool escritura = Convert.ToBoolean(row.Cells["Escritura"].Value);
                bool actualizacion = Convert.ToBoolean(row.Cells["Actualizacion"].Value);
                bool eliminacion = Convert.ToBoolean(row.Cells["Eliminacion"].Value);

                string GuardarPemiso = $"call p_insertar_permiso('{User.Text}', '{NombreFormulario}', {lectura}, {escritura}, {actualizacion}, {eliminacion})";
                string ResultadoPermiso = f.Guardar(GuardarPemiso); 

                if(ResultadoPermiso != "Correcto")
                {
                    MessageBox.Show("Error al guardar el permiso", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }
            }
            MessageBox.Show("Usuario y permiso se han modificado correctamente", "ATENCIO", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
        public void Borrar(string Username)
        {
            DialogResult rs = MessageBox.Show($"Estas seguro de borrar a {Username}?", "ATENCIO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                string BorrarPermiso = $"call p_eliminar_permisos('{Username}')";
                string resultadoPermiso = f.Borrar(BorrarPermiso);

                if(resultadoPermiso != "Correcto")
                {
                    MessageBox.Show("Error al tratar de eliminar el permiso");
                    return;
                }

                string BorrarUsuario = $"call p_eliminar_usuario('{Username}')"; 
                string resultadoUsuario = f.Borrar(BorrarUsuario);

                if (resultadoUsuario != "Correcto")
                {
                    MessageBox.Show("Error al tratar de borrar al usuario");
                    return; 
                }
            }
            MessageBox.Show("Usuario y permiso eliminado correctamente"); 
        }
        public void MostrarVistaGeneral(DataGridView tabla, string filtro)
        {
            tabla.Rows.Clear();
            tabla.DataSource = f.Mostrar($"select * from v_vista_general where nombre like '{filtro}'", "Usuarios").Tables[0]; 
            tabla.AutoResizeColumns();
            tabla.AutoResizeRows();
        }
        public DataTable Obtenerermisos(string user)
        {
            return f.Mostrar($"select * from v_vista_general where fk_username = '{user}'", "v_vista_general").Tables[0];
        }
    }
}
