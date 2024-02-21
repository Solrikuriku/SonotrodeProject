using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public double l { private get; set; } //длина волны - длина сонотрода
        public double on { private get; set; } //узел колебаний
        public double u { private get; set; } //скорость волны, равна speed of sound
        //умножение на 1000 для перевода из м в мм, на 10 000 000 - для масштабирования
        public int T { get { return (int)Math.Round((l / 1000) / u * 10000000); } }
        public int Center { get { return (int)Math.Round(T * on); } }
        public double Coefficient(double area)
        {
            //берем 0.5, так как это некое стандартное значение для сдвига
            return 0.5 / area;
        }
        public double Shift(double area)
        {
            return (Math.PI * (1 - ((float)on  / ((float)area * 2))));
        }
    }

    //архитектура координат будет переделываться
    internal class WaveCoordinates
    {
        //10 000 000 - это масштаб
        //формула синуса выведена экспериментальным путем, подробности в записке
        double waveX { get; }
        double waveY { get; }
        //это координаты волны для отрисовки
        public double X { get; set; }
        public double Y { get; }
        public WaveCoordinates(double a, double x, double height, int scale, double k, double coeff, double shift)
        {
            this.X = x;
            waveX = X / 10000000;
            waveY = a * Math.Sin(40000 * Math.PI * (waveX / k) * coeff + shift);
            Y = height - waveY * scale;
        }
        //устанавливать значения можно только для X, поэтому сет только для него
        // wave - это координаты основной волны
        //double waveX { get; }
        //double waveY { get; }
        ////это координаты волны для отрисовки
        //public double X { get; set; }
        //public double Y { get; }
    }
}
