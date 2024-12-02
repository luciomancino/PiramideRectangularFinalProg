using PiramideRectangularFinalProg.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiramideRectangularFinalProg.Datos
{
    public class RepositorioPiramideRegular
    {
        private readonly string _archivo = Environment.CurrentDirectory + "//PiramidesRegulares.txt";
        private readonly string _archivoCopia = Environment.CurrentDirectory + "//PiramidesRegulares.bak";


        private List<PiramideRegular> listaPiramides;

        public RepositorioPiramideRegular() {
            listaPiramides = new List<PiramideRegular>();
            LeerDatos();
    }

        private void LeerDatos()
        {
            listaPiramides.Clear();
            if (File.Exists(_archivo))
            {
                var lector = new StreamReader(_archivo);
                while (!lector.EndOfStream)
                {
                    string lineaLeida = lector.ReadLine();
                    PiramideRegular rectangulo = ConstruirPiramide(lineaLeida);
                    listaPiramides.Add(rectangulo);
                }
                lector.Close();
            }
        }

        private PiramideRegular ConstruirPiramide(string? lineaLeida)
        {
            var campos = lineaLeida.Split('|');
            int ladoBase = int.Parse(campos[0]);
            int altura = int.Parse(campos[1]);
            int cantidadLados = int.Parse(campos[2]);
            Entidades.Color color = (Entidades.Color)int.Parse(campos[3]);
            Material material = (Material)int.Parse(campos[4]);
            PiramideRegular r = new PiramideRegular(ladoBase, altura, cantidadLados, color, material);

            return r;
        }

        public void Agregar(PiramideRegular piramide)
        {
            using (var escritor = new StreamWriter(_archivo, true))
            {
                string lineaEscribir = ConstruirLinea(piramide);
                escritor.WriteLine(lineaEscribir);
            }
            listaPiramides.Add(piramide);
        }
        private string ConstruirLinea(PiramideRegular piramide)
        {
            return $"{piramide.GetLadoBase()}|" +
               $"{piramide.GetAltura()}|" +
               $"{piramide.GetCantidadLados()}|" +
               $"{piramide.Color.GetHashCode()}|" +
               $"{piramide.Material.GetHashCode()}";
        }

        public int GetCantidad(int? valorFiltro = 0)
        {
            if (valorFiltro > 0)
            {
                return listaPiramides.Count(c => c.LadoBase > valorFiltro);
            }
            return listaPiramides.Count();
        }
        public List<PiramideRegular> GetLista()
        {
            LeerDatos();
            return listaPiramides;
        }

        public void Borrar(PiramideRegular piramideBorrar)
        {
            using (var lector = new StreamReader(_archivo))
            {
                using (var escritor = new StreamWriter(_archivoCopia))
                {
                    while (!lector.EndOfStream)
                    {
                        string lineaLeida = lector.ReadLine();
                        PiramideRegular piramideLeido = ConstruirPiramide(lineaLeida);
                        if (piramideBorrar.GetLadoBase() != piramideLeido.GetLadoBase())
                        {
                            escritor.WriteLine(lineaLeida);
                        }
                    }
                }
            }
            File.Delete(_archivo);
            File.Move(_archivoCopia, _archivo);
            listaPiramides.Remove(piramideBorrar);
        }
        public bool Existe(PiramideRegular piramide)
        {
            listaPiramides.Clear();
            LeerDatos();
            bool existe = false;
            foreach (var itemPiramide in listaPiramides)
            {
                if (itemPiramide.GetLadoBase() == piramide.GetLadoBase() && itemPiramide.GetLadoBase() == piramide.GetAltura() && itemPiramide.GetCantidadLados() == piramide.GetCantidadLados())
                {
                    return true;
                }
            }
            return false;
        }
        public void Editar(PiramideRegular piramideViejo, PiramideRegular piramideEditar)
        {
            using (var lector = new StreamReader(_archivo))
            {
                using (var escritor = new StreamWriter(_archivoCopia))
                {
                    while (!lector.EndOfStream)
                    {
                        string lineaLeida = lector.ReadLine();
                        PiramideRegular piramide = ConstruirPiramide(lineaLeida);
                        if (piramide.GetLadoBase() == piramideViejo.GetLadoBase() && piramide.GetAltura() == piramideViejo.GetAltura() && piramide.GetCantidadLados() == piramideViejo.GetCantidadLados())
                        {
                            lineaLeida = ConstruirLinea(piramideEditar);
                            escritor.WriteLine(lineaLeida);

                        }
                        else
                        {
                            escritor.WriteLine(lineaLeida);

                        }
                    }
                }
            }
        }
    }
}
