using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonotrodeProject
{
    //переделать логику с геттерами и сеттерами, абстрактными классами?
    //переделать в конструкторы с перегрузками???

    /*
     * АМПЛИТУДА В МКМ
     * ДЛИНА ВОЛНЫ В ММ ПЕРЕВОДИМ В М
     * ЧАСТОТА ИЗ КГЦ В ГЦ
     * СКОРОСТЬ ВОЛНЫ М/C
     */

    //10 000 000 - это для масштабирования


    //private get - чтобы получать значения только внутри класса
    //время и центр int так как сложно делать цикл с вещественными значениями

    //waveparameters отвечает за некоторые параметры на участке волны
    internal class WaveParameters
    {
        internal double L { private get; set; } //длина волны - длина сонотрода
        internal double ON { private get; set; } //узел колебаний
        internal double U { private get; set; } //скорость волны, равна speed of sound
        internal int T { get { return (int)L; } }
        internal int Center { get { return (int)Math.Round(T * ON); } }
        internal double Shift(double area)
        {
            return (Math.PI * (1 - ((float)ON  / ((float)area * 2))));
        }

        internal double ZoneCoeff(double length)
        {
            return (Math.Asin(1)) / (40000 * Math.PI * length);
        }
    }

    //архитектура координат будет переделываться
    internal class WaveCoordinates
    {
        //10 000 000 - это масштаб
        //формула синуса выведена экспериментальным путем, подробности в записке
        internal double WaveX { get; }
        internal double WaveY { get; }
        //это координаты волны для отрисовки
        internal double X { get; set; }
        internal double Y { get; }
        //public WaveCoordinates(double a, double x, double height, int scale, double k, double coeff, double shift)
        internal WaveCoordinates(double a, double x, double height, int scale, double coeff, double shift)
        {
            this.X = x;
            WaveX = X;
            //WaveY = a * Math.Sin(40000 * Math.PI * (WaveX / k) * coeff + shift);
            WaveY = a * Math.Sin(40000 * Math.PI * WaveX * coeff + shift);
            Y = height - WaveY * scale;
        }
        //устанавливать значения можно только для X, поэтому сет только для него
        //wave - это координаты основной волны
    }

    //красим волну
    //пока реализовываем покрас по амплитуде напряжений
    internal abstract class ColorManipulation
    {
        internal static double GetHue(double x1, double x2, double y1, double y2, double a)
        {
            var x = ((a - y1) / (y2 - y1)) * (x2 - x1) + x1;
            return x;
        }
        internal static Color HSVtoRGB(double H)
        {
            double R = 0, G = 0, B = 0;

            var X = (1 - Math.Abs((H / 60) % 2 - 1));

            if (0 <= H && H < 60) { R = 255; G = X * 255; }
            else if (60 <= H && H < 120) { R = X * 255; G = 255; }
            else if (120 <= H && H < 180) { G = 255; B = X * 255; }
            else if (180 <= H && H < 240) { G = X * 255; B = 255; }
            else if (240 <= H && H < 300) { R = X * 255; B = 255; }
            else if (300 <= H && H < 360) { R = 255; B = X * 255; }

            return Color.FromArgb((int)R, (int)G, (int)B);
        }
        internal static Color AmplitudeGradient(double a, double curA)
        {
            var hue = ((curA - a) / (- a)) * 240;

            return HSVtoRGB(hue);
        }
        internal static Color CompstretchGradient(double a, double curA)
        {
            var hue = ((curA + a) / (2 * a)) * 120 + 240;

            return HSVtoRGB(hue);
        }
    }

    internal class WaveGradient : ColorManipulation
    {
        internal Pen PenColor { get; }
        internal PointF LinePixel { get; }
        internal PointF BorderPixel { get; }

        internal WaveGradient(double a, PointF l, double curY, PointF border)
        {
            this.PenColor = new Pen(AmplitudeGradient(a, Math.Abs(curY)));
            this.LinePixel = l;
            this.BorderPixel = border;
        }
    }
    internal class CompstretchGradient : ColorManipulation
    {
        internal Pen PenColor { get; set; }
        internal PointF LinePixel { get; set; }
        internal PointF BorderPixel { get; set; }
        internal CompstretchGradient(double a, double height, PointF p, PointF border, int sc)
        {
            this.PenColor = new Pen(CompstretchGradient(a, (height / 2 - p.Y) / sc));
            this.LinePixel = p;
            this.BorderPixel = border;
        }
    }
}
