using CapaDatos;
using CapaLogica.Library;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaLogica
{
    public class Estudiantes : Librarys
    {
        private List<TextBox> listTextBox;
        private List<Label> listLabel;
        private PictureBox image;
        private Image _imagBitmap;
        private DataGridView _dataGridView;
        private NumericUpDown _numericUpDown;
        private Paginador<Estudiante> _paginador;
        private string _accion = "insert";

        public Estudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objectos)
        {
            this.listTextBox = listTextBox;
            this.listLabel = listLabel;
            this.image = (PictureBox)objectos[0];
            _imagBitmap = (Bitmap)objectos[1];
            _dataGridView = (DataGridView)objectos[2];
            _numericUpDown = (NumericUpDown)objectos[3];
            Restablecer();
        }

        public void Registrar()
        {
            if (listTextBox[0].Text.Equals(""))
            {
                listLabel[0].Text = "Este Campo requerido";
                listLabel[0].ForeColor = Color.Red;
                listTextBox[0].Focus();
            }
            else
            {
                if (listTextBox[1].Text.Equals(""))
                {
                    listLabel[1].Text = "Este Campo requerido";
                    listLabel[1].ForeColor = Color.Red;
                    listTextBox[1].Focus();
                }
                else
                {
                    if (listTextBox[2].Text.Equals(""))
                    {
                        listLabel[2].Text = "Este Campo requerido";
                        listLabel[2].ForeColor = Color.Red;
                        listTextBox[2].Focus();
                    }
                    else
                    {
                        if (listTextBox[3].Text.Equals(""))
                        {
                            listLabel[3].Text = "Este Campo requerido";
                            listLabel[3].ForeColor = Color.Red;
                            listTextBox[3].Focus();
                        }
                        else
                        {
                            if (textBoxEvent.comprobarFormatoEmail(listTextBox[3].Text))
                            {
                                var useremail = listTextBox[3].Text;
                                var user = _Estudiante.Where(x => x.email.Equals(useremail)).ToList();

                                if (user.Count.Equals(0))
                                {
                                    Save();
                                }
                                else
                                {
                                    if (user[0].id.Equals(_idEstudiante))
                                    {
                                        Save();
                                    }
                                    else
                                    {
                                        listLabel[3].Text = "Email ya esta registrado..";
                                        listLabel[3].ForeColor = Color.Red;
                                        listTextBox[3].Focus();
                                    }
                                }
                            }
                            else
                            {
                                listLabel[3].Text = "Email no valido";
                                listLabel[3].ForeColor = Color.Red;
                                listTextBox[3].Focus();
                            }
                        }
                    }
                }
            }
        }

        private void Save()
        {
            BeginTransactionAsync();
            try
            {
                var imageArray = uploadimagen.ImageToByte(image.Image);
                switch (_accion)
                {
                    case "insert":
                        _Estudiante.Value(x => x.nid, listTextBox[0].Text)
                          .Value(x => x.nombre, listTextBox[1].Text)
                          .Value(x => x.apellido, listTextBox[2].Text)
                          .Value(x => x.email, listTextBox[3].Text)
                          .Value(x => x.imagen, imageArray)
                          .Insert();
                        break;

                    case "update":
                        _Estudiante.Where(x => x.id.Equals(_idEstudiante))
                            .Set(x => x.nid, listTextBox[0].Text)
                            .Set(x => x.nombre, listTextBox[1].Text)
                            .Set(x => x.apellido, listTextBox[2].Text)
                            .Set(x => x.email, listTextBox[3].Text)
                            .Set(x => x.imagen, imageArray)
                            .Update();
                        break;
                }


                CommitTransactionAsync();
                Restablecer();
            }
            catch (Exception ex)
            {

                RollbackTransactionAsync();
            }
        }


        private int _reg_por_pagina = 2, _num_pagina = 1;
        public void SearchEstudiantes(string campo)
        {
            var query = new List<Estudiante>();
            int inicio = (_num_pagina - 1) * _reg_por_pagina;
            if (campo.Equals(""))
            {
                query = _Estudiante.ToList();
            }
            else
            {
                query = _Estudiante.Where(x => x.nid.StartsWith(campo) || x.nombre.StartsWith(campo)
                            || x.apellido.StartsWith(campo)).ToList();
            }
            if (0 < query.Count)
            {
                _dataGridView.DataSource = query.Select(c => new
                {
                    c.id,
                    c.nid,
                    c.nombre,
                    c.apellido,
                    c.email,
                    c.imagen
                }).Skip(inicio).Take(_reg_por_pagina).ToList();
                _dataGridView.Columns[0].Visible = false;
                _dataGridView.Columns[5].Visible = false;

                _dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                _dataGridView.DataSource = query.Select(c => new
                {
                    c.nid,
                    c.nombre,
                    c.apellido,
                    c.email

                }).ToList();
            }
        }

        private int _idEstudiante = 0;
        public void GetEstudiante()
        {
            _accion = "update";
            _idEstudiante = Convert.ToInt32(_dataGridView.CurrentRow.Cells[0].Value);
            listTextBox[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
            listTextBox[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
            listTextBox[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
            listTextBox[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);
            try
            {
                byte[] arrayImage = (byte[])_dataGridView.CurrentRow.Cells[5].Value;
                image.Image = uploadimagen.byteArrayToImage(arrayImage);
            }
            catch (Exception)
            {

                image.Image = _imagBitmap;
            }

        }

        private List<Estudiante> listEstudiante;
        public void Paginador(string metodo)
        {
            switch (metodo)
            {
                case "Primero":
                    _num_pagina = _paginador.primero();
                    break;

                case "Anterior":
                    _num_pagina = _paginador.anterior();
                    break;

                case "Siguiente":
                    _num_pagina = _paginador.siguiente();
                    break;

                case "Ultimo":
                    _num_pagina = _paginador.ultimo();
                    break;

            }
            SearchEstudiantes("");
        }
        public void Registro_Paginas()
        {
            _num_pagina = 1;
            _reg_por_pagina = (int)_numericUpDown.Value;
            var list = _Estudiante.ToList();
            if (0 < list.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listLabel[4], _reg_por_pagina);
                SearchEstudiantes("");
            }

        }
        public void Eliminar()
        {
            if (_idEstudiante.Equals(0))
                MessageBox.Show("Seleccione un estudiante");
            else
            {
                if (MessageBox.Show("Estas Seguro de eleminar el estudiante?", "Eliminar estudiante",
                   MessageBoxButtons.YesNo) == DialogResult.Yes) 
                {
                    _Estudiante.Where(x => x.id.Equals(_idEstudiante)).Delete();
                    Restablecer();
                }
            }
        }
        public void Restablecer()
        {
            _accion = "insert";
            _num_pagina = 1;
            _idEstudiante = 0;
            image.Image = _imagBitmap;
            listLabel[0].Text = "Nid";
            listLabel[1].Text = "Nombre";
            listLabel[2].Text = "Apellido";
            listLabel[3].Text = "Email";
            listLabel[0].ForeColor = Color.LightSlateGray;
            listLabel[1].ForeColor = Color.LightSlateGray;
            listLabel[2].ForeColor = Color.LightSlateGray;
            listLabel[3].ForeColor = Color.LightSlateGray;
            listTextBox[0].Text = "";
            listTextBox[1].Text = "";
            listTextBox[2].Text = "";
            listTextBox[3].Text = "";
            listEstudiante = _Estudiante.ToList();
            if (0 < listEstudiante.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listLabel[4], _reg_por_pagina);
            }
            SearchEstudiantes("");
        }
    }
}

