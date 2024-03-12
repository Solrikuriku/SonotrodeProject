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

    public abstract class Sonotrode
    {
        public int SpeedOfSound { get; set; }
        public int AmplitudeElement { get; set; }
        public int AmplitudeDetail { get; set; }
        public int Frequency { get; set; }
        public double K { get; set; }
        public double Beta { get { return (double)AmplitudeDetail / (double)AmplitudeElement; } }
        public double L { get { return SpeedOfSound / (2 * Frequency) * K + 3; } }
        public abstract double A1 { get; }
        public abstract double A2 { get; }
        public abstract double OscillationNode { get; }
    }

    public abstract class CircularSonotrode : Sonotrode
    {
        public abstract double D1 { get; }
        public double D2 { get; set; }
    }
    public abstract class RectangleSonotrode : Sonotrode
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public class StepwiseCircularSonotrode : CircularSonotrode
    {
        public override double D1 { get { return Math.Sqrt(Beta) * D2; } }
        public override double A1 { get { return GetArea(D1); } }
        public override double A2 { get { return GetArea(D2); } }
        public override double OscillationNode { get { return 0.5f; } }
        public double L1 { get { return (1.5) / (2 * (Math.PI / (L * 2))); } }
        public double L2 { get { return (1.6) / (2 * (Math.PI / (L * 2))) + 3; } }
        public double GetArea(double d) { return Math.PI * ((d / 2) * (d / 2)); }
        //public double GetLength(double A) { return (0.0126667 * SpeedOfSound - 0.833333) - (A / 30); }
    }
    public class ConicCircularSonotrode : CircularSonotrode
    {
        public override double D1 { get { return Beta * Beta * D2; } }
        public override double A1 { get { return GetArea(D1); } }
        public override double A2 { get { return GetArea(D2); } }
        public override double OscillationNode { get { return 0.4f; } }
        public double I1 { get; set; }
        public double I2 { get; set; }
        public double GetArea(double d) { return Math.PI * ((d / 2) * (d / 2)); }
    }
    public class ConicRectangleSonotrode : RectangleSonotrode
    {
        public override double A2 { get { return Width * Height; } }
        public override double A1 { get { return A2 * Beta * Beta; } }
        public override double OscillationNode { get { return 0.4f; } }
        public double I1 { get; set; }
        public double I2 { get; set; }
    }

    //возможно делать гет-сет для l1-l2 решение не очень хорошее
    //но на данный момент я не придумала ничего лучше :[ 
}
