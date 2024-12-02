using PiramideRectangularFinalProg.Datos;
using PiramideRectangularFinalProg.Entidades;
using System.Drawing;

namespace PiramideRectangularFinalProg.Windows
{
    public partial class frmPiramides : Form
    {
        private RepositorioPiramideRegular repo;
        private List<PiramideRegular> lista;
        int valorFiltro;
        bool filterOn = false;
        public frmPiramides()
        {
            repo = new RepositorioPiramideRegular();
            InitializeComponent();
            ActualizarCantidadDeRegistros();
            txtCantidad.Text = repo.GetCantidad().ToString();
        }
        private void ActualizarCantidadDeRegistros()
        {
            if (valorFiltro > 0)
            {
                txtCantidad.Text = repo.GetCantidad(valorFiltro).ToString();
            }
            else
            {
                txtCantidad.Text = repo.GetCantidad().ToString();
            }

        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPiramideAE form = new frmPiramideAE() { Text = "Agregar Piramide" };
            DialogResult dr = form.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            PiramideRegular piramide = form.GetPiramideRegular();
            if (!repo.Existe(piramide))
            {
                repo.Agregar(piramide);
                ActualizarCantidadDeRegistros();
                DataGridViewRow l = ConstruirFila();
                SetearFila(l, piramide);
                AgregarFila(l);

                MessageBox.Show("Fila agregada con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Registro existente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataGridViewRow ConstruirFila()
        {
            var l = new DataGridViewRow();
            l.CreateCells(dgvDatos);
            return l;
        }

        private void SetearFila(DataGridViewRow l, PiramideRegular piramide)
        {
            l.Cells[colLado.Index].Value = piramide.GetLadoBase();
            l.Cells[colCantidad.Index].Value = piramide.GetCantidadLados();
            l.Cells[colMaterial.Index].Value = piramide.Material;
            l.Cells[colColor.Index].Value = piramide.Color;
            l.Cells[colVolumen.Index].Value = piramide.GetVolumen().ToString(".00");

            l.Tag = piramide;
        }
        private void AgregarFila(DataGridViewRow l)
        {
            dgvDatos.Rows.Add(l);
        }

        private void FrmPiramides_Load(object sender, EventArgs e)
        {
            if (repo.GetCantidad() > 0)
            {
                RecargarGrilla();
            }
        }

        private void RecargarGrilla()
        {
            valorFiltro = 0;
            filterOn = false;
            tsbFiltrar.BackColor = SystemColors.Control;
            lista = repo.GetLista();
            MostrarDatosEnGrilla();
            ActualizarCantidadDeRegistros();
        }
        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (var rect in lista)
            {
                DataGridViewRow l = ConstruirFila();
                SetearFila(l, rect);
                AgregarFila(l);
            }
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Desea eliminar la fila seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            else
            {
                var l = dgvDatos.SelectedRows[0];
                QuitarFila(l);
                var piramBorrar = (PiramideRegular)l.Tag;
                repo.Borrar(piramBorrar);
                ActualizarCantidadDeRegistros();
                MessageBox.Show("Fila eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void QuitarFila(DataGridViewRow l)
        {
            dgvDatos.Rows.Remove(l);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }

            var FilaSeleccionada = dgvDatos.SelectedRows[0];
            PiramideRegular piramide = (PiramideRegular)FilaSeleccionada.Tag;
            PiramideRegular piramideCopia = (PiramideRegular)piramide.Clone();
            frmPiramideAE frm = new frmPiramideAE() { Text = "Editar Piramide" };
            frm.SetPiramide(piramide);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            piramide = frm.GetPiramideRegular();
            if (!repo.Existe(piramide))
            {
                repo.Editar(piramideCopia, piramide);
                SetearFila(FilaSeleccionada, piramide);
                MessageBox.Show("Fila editada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SetearFila(FilaSeleccionada, piramideCopia);
                MessageBox.Show("Registro existente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
