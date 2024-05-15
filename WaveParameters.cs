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
        public double L { private get; set; } //длина волны - длина сонотрода
        public double ON { private get; set; } //узел колебаний
        public double U { private get; set; } //скорость волны, равна speed of sound
        //умножение на 1000 для перевода из м в мм, на 10 000 000 - для масштабирования
        public int T { get { return (int)Math.Round(L / U * 10000); } }
        //public int T { get { return (int)Math.Round(L); } }
        public int Center { get { return (int)Math.Round(T * ON); } }
        public double Coefficient(double area)
        {
            //берем 0.5, так как это некое стандартное значение для сдвига
            return 0.5 / area;
        }
        public double Shift(double area)
        {
            return (Math.PI * (1 - ((float)ON  / ((float)area * 2))));
        }
    }

    //архитектура координат будет переделываться
    internal class WaveCoordinates
    {
        //10 000 000 - это масштаб
        //формула синуса выведена экспериментальным путем, подробности в записке
        double WaveX { get; }
        double WaveY { get; }
        //это координаты волны для отрисовки
        public double X { get; set; }
        public double Y { get; }
        public WaveCoordinates(double a, double x, double height, int scale, double k, double coeff, double shift)
        {
            this.X = x;
            WaveX = X / 10000000;
            WaveY = a * Math.Sin(40000 * Math.PI * (WaveX / k) * coeff + shift);
            Y = height - WaveY * scale;
        }
        //устанавливать значения можно только для X, поэтому сет только для него
        //wave - это координаты основной волны
    }

    //красим волну
    //пока реализовываем покрас по амплитуде напряжений
    internal abstract class ColorManipulation
    {
        public static double GetHue(double x1, double x2, double y1, double y2, double a)
        {
            var x = ((a - y1) / (y2 - y1)) * (x2 - x1) + x1;
            return x;
        }
        public static Color HSVtoRGB(double H)
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
        public static Color AmplitudeGradient(double a, double curA)
        {
            //var step = a / 4;

            var hue = ((curA - a) / (- a)) * 240;
            //double hue = 0;

            //if (0 <= curA && curA <= step)
            //    hue = GetHue(240, 180, 0, step, curA);
            //else if (step < curA && curA <= 2 * step)
            //    hue = GetHue(180, 120, step, step * 2, curA);
            //else if (2 * step < curA && curA <= 3 * step)
            //    hue = GetHue(120, 60, step * 2, step * 3, curA);
            //else if (3 * step < curA && curA <= 4 * step)
            //    hue = GetHue(60, 0, step * 3, step * 4, curA);

            return HSVtoRGB(hue);
        }
        public static Color CompstretchGradient(double a, double curA)
        {
            //var step = a / 2;
            //double hue = 0;

            var hue = ((curA + a) / (2 * a)) * 120 + 240;

            //if (-step <= curA && curA <= 0)
            //    hue = GetHue(240, 300, -step, 0, curA);
            //else if (0 < curA && curA <= step)
            //    hue = GetHue(300, 360, 0, step, curA);

            return HSVtoRGB(hue);
        }
    }

    //ну и пиздец...
    //ктож так код пишет
    //как же хочется пивка..........

    //wavegradient сделать как интерфейс-виртуальный класс
    internal class WaveGradient : ColorManipulation
    {
        public Pen PenColor { get; set; }
        public PointF LinePixel { get; set; }
        public PointF BorderPixel { get; set; }
        //public static double AmplitudeValue { private get; set; }
        public WaveGradient(double a, double height, PointF p, PointF border, int sc)
        {
            this.PenColor = new Pen(AmplitudeGradient(a, Math.Abs((height / 2 - p.Y) / sc)));
            this.LinePixel = p;
            this.BorderPixel = border;
        }
    }
    internal class CompstretchGradient : ColorManipulation
    {
        public Pen PenColor { get; set; }
        public PointF LinePixel { get; set; }
        public PointF BorderPixel { get; set; }
        //public static double AmplitudeValue { private get; set; }
        public CompstretchGradient(double a, double height, PointF p, PointF border, int sc)
        {
            this.PenColor = new Pen(CompstretchGradient(a, (height / 2 - p.Y) / sc));
            //this.PenColor = new Pen(CompstretchGradient(a, Math.Abs((height / 2 - p.Y) / 5)));
            this.LinePixel = p;
            this.BorderPixel = border;
        }
    }
}
