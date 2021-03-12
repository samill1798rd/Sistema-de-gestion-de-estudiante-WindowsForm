using CapaLogica;
using CapaLogica.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqToDB;


namespace CursoForm6._0
{
    public partial class Form1 : Form
    {
        private Estudiantes estudiante;
        //private Librarys librarys;

        public Form1()
        {
            InitializeComponent();
            //librarys = new Librarys();
            var listTextBox = new List<TextBox>();
            listTextBox.Add(textBoxNid);
            listTextBox.Add(textBoxNombre);
            listTextBox.Add(textBoxApellido);
            listTextBox.Add(textBoxEmail);

            var listLabel = new List<Label>();
            listLabel.Add(labelNid);
            listLabel.Add(labelNombre);
            listLabel.Add(labelApellido);
            listLabel.Add(labelEmail);
            listLabel.Add(lblPaginas);
            Object[] objectos = {
                pictureBoxImagen,
                Properties.Resources._1,
                dataGridView1,
                numericUpDown1
            };

            estudiante = new Estudiantes(listTextBox, listLabel, objectos);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxImagen_Click(object sender, EventArgs e)
        {
            estudiante.uploadimagen.CargarImagen(pictureBoxImagen);
        }

        private void textBoxNid_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNid.Text.Equals(""))
            {
                labelNid.ForeColor = Color.Red;
            }
            else
            {
                labelNid.ForeColor = Color.Green;
                labelNid.Text = "Nid";
            }
        }

        private void textBoxNid_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.numberKeyPress(e);
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNombre.Text.Equals(""))
            {
                labelNombre.ForeColor = Color.Red;
            }
            else
            {
                labelNombre.ForeColor = Color.Green;
                labelNombre.Text = "Nombre";
            }
        }

        private void textBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.textKeyPress(e);
        }

        private void textBoxApellido_TextChanged(object sender, EventArgs e)
        {
            if (textBoxApellido.Text.Equals(""))
            {
                labelApellido.ForeColor = Color.Red;
            }
            else
            {
                labelApellido.ForeColor = Color.Green;
                labelApellido.Text = "Apellido";
            }
        }

        private void textBoxApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.textKeyPress(e);
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEmail.Text.Equals(""))
            {
                labelEmail.ForeColor = Color.Red;
            }
            else
            {
                labelEmail.ForeColor = Color.Green;
                labelEmail.Text = "Email";
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            estudiante.Registrar();
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            estudiante.SearchEstudiantes(textBoxBuscar.Text);
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Primero");
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Anterior");
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Siguiente");
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Ultimo");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            estudiante.Registro_Paginas();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
                estudiante.GetEstudiante();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
                estudiante.GetEstudiante();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            estudiante.Restablecer();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            estudiante.Eliminar();  
        }

    }
}
