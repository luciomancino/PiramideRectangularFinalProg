using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiramideRectangularFinalProg.Entidades
{
    public class PiramideRegular
    {
        public int LadoBase { get; set; }
        public int Altura { get; set; }
        public int CantidadLados { get; set; }


        public Color color;

        public Color Color 
        {
            get { return color; }
            set { color = value; }
        }

        public Material material;

        public Material Material 
        {
            get { return material; }
            set { material = value; }

        }

        public PiramideRegular()
        {

        }

        public PiramideRegular(int ladoBase, int altura, int cantidadLados, Color color, Material material)
        {
            LadoBase = ladoBase;
            Altura = altura;
            CantidadLados = cantidadLados;
            Color = color;
            Material = material;
        
        }

        public double GetLadoBase() => LadoBase;

        public void SetLadoA(int medida1)
        {
            if (medida1 > 0)
            {
                LadoBase = medida1;
            }
        }
        public double GetAltura() => Altura;

        public void SetAltura(int medida2)
        {
            if (medida2 > 0)
            {
                Altura = medida2;
            }
        }

        public double GetCantidadLados() => CantidadLados;

        public void SetCantidadLados(int medida3)
        {
            if (medida3>0)
            {
                CantidadLados = medida3;
            }
        }

        public double GetAreaBase()=> Math.Pow(LadoBase, 2);

        public double GetVolumen() => (GetAreaBase() * Altura) / 3;

        private double GetPerimetroBase() => LadoBase * CantidadLados;
        private double GetAlturaLateral() => Math.Cbrt(Math.Pow(Altura, 2) + Math.Pow((LadoBase / 2), 2));
        public double GetAreaLateral()=>(GetPerimetroBase()*GetAlturaLateral())/2  ;
        public double GetAreaTotal()=>GetAreaBase() + GetAreaLateral();

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
