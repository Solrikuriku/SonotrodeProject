using SharpGL.SceneGraph;
using SonotrodeProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonotrodeProject
{
    /*
    * SpeedofSound - скорость звука, зависит от материала
    * AmplitudeElement - амплитуда трансформирующего элемента,
    *                    которая подается на сонотрод (зависит от устройства)
    * AmplitudeDetail - амплитуда для сварки конкретного материала
    * Frequency - частота (у нас под 20 кГц установки, зависит от устройства)
    * l - общая длина сонтрода, которая равна/кратна длине полуволны (в идеальном случае)
    *     к общей длине всегда добавляется 2-3 мм запасом
    *     и умножается на коэффициент коррекции
    * K - коэффициент коррекции является исходным параметром и должен быть заранее известен
    *     в идеальном случае берем 1, но даем возможность его в дальнейшем поменять
    *     путем отправки нужных параметров в метод класса
    * Beta - коэффициет трансформации
    */

    /*
     * общий родительский класс сонотрода,
     * который содержит только общие параметры
     * справедливые для сонотродов всех типов
     * 
     * продумать адекватную логику
     */

    /*
     * формулы beta и l постоянны, неизменяемы
     */
    /*
     * A1 и A2 - всего лишь площадь круга по ее диаметру
     * по некоторым причинам формула коэффициента трансформации
     * немного отличаются для ступенчатых и конических сонотродов,
     * а отсюда и формулы для подсчетов диаметров и результатов площадей,
     * поэтому они будут в разных классах
     * формулы для l1 и l2 уже давно выведены и испытаны 
     */

    //OscillationNode - узел колебаний, зависит от stepwise-conic

    internal abstract class Sonotrode
    {
        public int SpeedOfSound { get; set; }
        public int AmplitudeElement { get; set; }
        public int AmplitudeDetail { get; set; }
        public int Frequency { get; set; }
        //public double K { get; set; }
        public double Beta { get { return (double)AmplitudeDetail / (double)AmplitudeElement; } }
        public double L { get { return SpeedOfSound / (2 * Frequency) * K; } }
        public abstract double K { get; }
        public abstract double A1 { get; }
        public abstract double A2 { get; }
        public abstract double OscillationNode { get; }
    }

    internal abstract class CircularSonotrode : Sonotrode
    {
        public abstract double D1 { get; }
        public double D2 { get; set; }
    }
    internal abstract class RectangleSonotrode : Sonotrode
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public abstract double W { get; }
    }
    internal class StepwiseCircularSonotrode : CircularSonotrode
    {
        public override double D1 { get { return Math.Sqrt(Beta) * D2; } }
        public override double A1 { get { return GetArea(D1); } }
        public override double A2 { get { return GetArea(D2); } }
        public override double OscillationNode { get { return L1/L; } }
        public override double K { get { return 1; } }
        public double L1 { get { return ((1.5) / (2 * (Math.PI / (L * 2)))); } }
        public double L2 { get { return ((1.6) / (2 * (Math.PI / (L * 2))) + 3); } }
        public double GetArea(double d) { return Math.PI * ((d / 2) * (d / 2)); }
        //public double GetLength(double A) { return (0.0126667 * SpeedOfSound - 0.833333) - (A / 30); }
    }
    internal class ConicCircularSonotrode : CircularSonotrode
    {
        public override double D1 { get { return Beta * D2; } }
        public override double A1 { get { return GetArea(D1); } }
        public override double A2 { get { return GetArea(D2); } }
        //public override double K { get { return Math.Sqrt(1 + (Math.Log(D1/D2)/Math.PI) * (Math.Log(D1 / D2) / Math.PI)) * 1.1f; } }
        public override double K { get { return Math.Sqrt(1 + (Math.Log(D1/D2)/Math.PI) * (Math.Log(D1 / D2) / Math.PI)) * 1.1f; } }
        public override double OscillationNode { get { return 0.4f; } }
        public double I1 { get; set; }
        public double I2 { get; set; }
        public double GetArea(double d) { return Math.PI * ((d / 2) * (d / 2)); }
    }
    //конвертация в stl
    internal class ConicRectangleSonotrode : RectangleSonotrode
    {
        public override double A2 { get { return Width * Height; } }
        public override double A1 { get { return A2 * Beta * Beta; } }
        public override double W { get { return A1 / Height; } }
        public override double K { get { return Math.Sqrt(1 + (Math.Log(Math.Sqrt(A1 / A2)) / Math.PI) * (Math.Log(Math.Sqrt(A1 / A2)) / Math.PI)) * 1.1f; } }
        public override double OscillationNode { get { return 0.4f; } }
        public double I1 { get; set; }
        public double I2 { get; set; }
    }

    //сделать хранилище именно полигонов?
    internal class Carcas3D
    {
        internal List<Vertex> d1 = new();
        internal List<Vertex> d2 = new();
        internal List<Vertex> d3 = new();
        internal List<Vertex> d4 = new();

        internal void Initialization(StepwiseCircularSonotrode sonotrode)
        {
            d1.Add(new Vertex(0.0f, (float)sonotrode.L1, -(float)sonotrode.D1 / 2));
            d2.Add(new Vertex(0.0f, 0.0f, -(float)sonotrode.D1 / 2));
            d3.Add(new Vertex(0.0f, 0.0f, -(float)sonotrode.D2 / 2));
            d4.Add(new Vertex(0.0f, -(float)sonotrode.L2, -(float)sonotrode.D2 / 2));

            d1 = Calculate(d1); 
            d2 = Calculate(d2); 
            d3 = Calculate(d3);
            d4 = Calculate(d4);
        }

        internal void Initialization(ConicCircularSonotrode sonotrode)
        {
            d1.Add(new Vertex(0.0f, (float)sonotrode.L / 2, -(float)sonotrode.D1 / 2));
            d2.Add(new Vertex(0.0f, (float)(sonotrode.L / 2 - sonotrode.I1), -(float)sonotrode.D1 / 2));
            d3.Add(new Vertex(0.0f, -(float)(sonotrode.L / 2 - sonotrode.I2), -(float)sonotrode.D2 / 2));
            d4.Add(new Vertex(0.0f, -(float)sonotrode.L / 2, -(float)sonotrode.D2 / 2));

            d1 = Calculate(d1);
            d2 = Calculate(d2);
            d3 = Calculate(d3);
            d4 = Calculate(d4);
        }

        internal void Initialization(ConicRectangleSonotrode sonotrode)
        {
            d1.Add(new Vertex(-(float)sonotrode.W / 2, (float)sonotrode.L / 2, 0.0f));
            d2.Add(new Vertex(-(float)sonotrode.W / 2, (float)(sonotrode.L / 2 - sonotrode.I1), 0.0f));
            d3.Add(new Vertex(-(float)sonotrode.Width / 2, -(float)(sonotrode.L / 2 - sonotrode.I2), 0.0f));
            d4.Add(new Vertex(-(float)sonotrode.Width / 2, -(float)sonotrode.L / 2, 0.0f));

            d1 = Calculate(d1, (float)sonotrode.W, (float)sonotrode.Height);
            d2 = Calculate(d2, (float)sonotrode.W, (float)sonotrode.Height);
            d3 = Calculate(d3, (float)sonotrode.Width, (float)sonotrode.Height);
            d4 = Calculate(d4, (float)sonotrode.Width, (float)sonotrode.Height);
        }

        internal static List<Vertex> Calculate(List<Vertex> d)
        {
            var a = 0.0f;

            while (a < 360)
            {
                a += 5.625f;
                var X =  - (d[0].Z * (float)Math.Sin(a * Math.PI / 180.0f));
                var Z =  d[0].Z * (float)Math.Cos(a * Math.PI / 180.0f);
                //a += 45;

                d.Add(new Vertex(X, d[0].Y, Z));
            } 

            return d;
        }

        internal static List<Vertex> Calculate(List<Vertex> d, float w, float h)
        {
            //w /= 25; h /= 25;

            d.Add(new Vertex(d[0].X, d[0].Y, d[0].Z + h));
            d.Add(new Vertex(d[1].X + w, d[1].Y, d[1].Z));
            d.Add(new Vertex(d[2].X, d[2].Y, d[2].Z - h));

            return d;
        }
    }

    //возможно делать гет-сет для l1-l2 решение не очень хорошее
    //но на данный момент я не придумала ничего лучше :[ 
}
