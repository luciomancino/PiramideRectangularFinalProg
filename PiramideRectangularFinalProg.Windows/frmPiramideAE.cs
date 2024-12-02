using PiramideRectangularFinalProg.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiramideRectangularFinalProg.Windows
{
    public partial class frmPiramideAE : Form
    {
        private PiramideRegular? piramide;
        public frmPiramideAE()
        {
            InitializeComponent();
        }


        public PiramideRegular GetPiramideRegular()
        { return piramide; }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        private void Agregar_Load(object sender, EventArgs e)
        {
            CargarDatosComboColorRelleno();
            if (piramide != null)
            {
                txtLado.Text = piramide.LadoBase.ToString();
                txtAltura.Text = piramide.Altura.ToString();
                nudCantidad.Text = piramide.CantidadLados.ToString();

                cboColores.SelectedItem = piramide.Color;
                if (piramide.Material == Material.Plastico)
                {
                    rbtPlastico.Checked = true;
                }
                else if (piramide.Material == Material.Vidrio)
                {
                    rbtVidrio.Checked = true;
                }
                else
                {
                    rbtMadera.Checked = true;
                }
            }
        }
        private void CargarDatosComboColorRelleno()
        {
            var listaColores = Enum.GetValues(typeof(Entidades.Color)).Cast<Entidades.Color>().ToList();
            cboColores.DataSource = listaColores;
            cboColores.SelectedIndex = 0;
        }

        public void SetPiramide(PiramideRegular piramide)
        {
            this.piramide = piramide;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (piramide == null)
                {
                    piramide = new PiramideRegular();
                }

                piramide.SetLadoA(int.Parse(txtLado.Text));
                piramide.SetAltura(int.Parse(txtAltura.Text));
                piramide.SetCantidadLados(int.Parse(nudCantidad.Text));
                piramide.Color = (Entidades.Color)cboColores.SelectedItem;
                
                if (rbtPlastico.Checked)
                {
                    piramide.Material = Material.Plastico;
                }
                else if (rbtVidrio.Checked)
                {
                    piramide.Material = Material.Vidrio;
                }
                else
                {
                    piramide.Material = Material.Madera;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();


            if (!int.TryParse(txtLado.Text, out int lado) || lado <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtLado, "Lado no válido");
            }

            if (!int.TryParse(txtAltura.Text, out int altura) || altura <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtAltura, "Altura no válido");
            }
            if (!int.TryParse(nudCantidad.Text, out int cantidad) || (cantidad < 3 && cantidad > 4))
            {
                valido = false;
                errorProvider1.SetError(txtAltura, "Cantidad de Lados debe ser 3 o 4");
            }

           
            return valido;
        }
    }
}
