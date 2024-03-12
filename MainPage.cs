using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace SonotrodeProject
{
    public partial class SonotrodeGenerator : Form
    {
        public SonotrodeGenerator()
        {
            InitializeComponent();
            SonotrodeType.SelectedIndex = 0;
            ProcessWave.Image = new Bitmap(ProcessWave.Width, ProcessWave.Height);
            ProcessWave.BackColor = Color.White;
            AlSound.Value = 5100;
        }

        private readonly List<WaveCoordinates> SonotrodeWavePixels = new();
        //помен€ть, объединить
        private readonly List<List<PointF>> AnimationWavePixels1 = new();
        private readonly List<List<PointF>> AnimationWavePixels2 = new();
        private readonly List<PointF> CurveWave = new();
        private readonly List<WaveGradient> GradientWave = new();

        private List<List<Pen>> colors = new();
        private List<List<Pen>> colors1 = new();
        //private readonly List<CompstretchGradient> CompstretchWave = new();
        //»—ѕ–ј¬»“№????
        //private readonly List<List<PointF>> AnimationGradientWave = new();
        private SonotrodeTypes Type;
        private enum SonotrodeTypes
        {
            StepwiseCircular,
            ConicCircular,
            ConicRectangle
        }

        private void SonotrodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type = (SonotrodeTypes)SonotrodeType.SelectedIndex;
            if (Type != SonotrodeTypes.ConicRectangle)
            {
                Diameter.Enabled = true;
                WidthR.Enabled = false;
                HeightR.Enabled = false;
            }
            else
            {
                Diameter.Enabled = false;
                WidthR.Enabled = true;
                HeightR.Enabled = true;
            }
        }

        //private static double AluminiumLength()
        //{
        //    return 20f;
        //}

        private void UpdateData_Click(object sender, EventArgs e)
        {
            SonotrodeWavePixels.Clear();
            CurveWave.Clear();
            GradientWave.Clear();
            Sonotrode sonotrodetest;

            if (Type == SonotrodeTypes.StepwiseCircular)
            {
                sonotrodetest = new StepwiseCircularSonotrode()
                {
                    SpeedOfSound = (int)AlSound.Value,
                    //исходное
                    AmplitudeElement = 20,
                    AmplitudeDetail = int.Parse(Amplitude.Text),
                    //исходное
                    Frequency = 20,
                    K = 1,
                    D2 = double.Parse(Diameter.Text)
                };
            }
            else if (Type == SonotrodeTypes.ConicCircular)
            {

                sonotrodetest = new ConicCircularSonotrode()
                {
                    SpeedOfSound = (int)AlSound.Value,
                    //исходное
                    AmplitudeElement = 20,
                    AmplitudeDetail = int.Parse(Amplitude.Text),
                    //исходное
                    Frequency = 20,
                    K = 1.03,
                    D2 = double.Parse(Diameter.Text),
                    I1 = 10,
                    I2 = 10
                };
            }
            else
            {
                sonotrodetest = new ConicRectangleSonotrode()
                {
                    SpeedOfSound = (int)AlSound.Value,
                    //исходное
                    AmplitudeElement = 20,
                    AmplitudeDetail = int.Parse(Amplitude.Text),
                    Width = int.Parse(WidthR.Text),
                    Height = int.Parse(HeightR.Text),
                    //исходное
                    Frequency = 20,
                    K = 1,
                    I1 = 10,
                    I2 = 10
                };
            }

            var sonotrodeWave = new WaveParameters()
            {
                L = sonotrodetest.L,
                ON = sonotrodetest.OscillationNode,
                U = sonotrodetest.SpeedOfSound
            };

            MakeCurve(sonotrodetest, sonotrodeWave);
            MakeAnimationWave(sonotrodetest, sonotrodeWave);
            SonotrodeDescription.Text = GenerateDescription(sonotrodetest);
            //генерить тут?
        }

        async private void ProcessWave_Paint(object sender, PaintEventArgs e)
        {
            if (MakeWave.Checked)
            {
                //foreach (var p in GradientWave)
                //    e.Graphics.DrawLine(p.PenColor, p.LinePixel, p.BorderPixel);

                //e.Graphics.DrawCurve(new Pen(Color.Black, 2), CurveWave.ToArray());
                //Ќ≈ ”ƒјЋя“№
            }
            else if (AnimationWave.Checked)
            {
                //перенести в отдельный асинк
                //flag = !flag;
                //if (flag)
                //{
                //    cts.Dispose();
                //    cts = new CancellationTokenSource();
                //    await ChangeLabel(cts.Token);
                //}
                //else
                //{
                //    cts.Cancel();
                //}
            }

            //e.Graphics.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2), new PointF(ProcessWave.Width, ProcessWave.Height / 2));
            //this.Refresh();
        }

        //перегрузки?
        private void MakeCurve(Sonotrode sonotrode, WaveParameters wave)
        {
            for (int i = 0; i < wave.Center; i++)
            {
                SonotrodeWavePixels.Add(new WaveCoordinates(sonotrode.AmplitudeElement, i, ProcessWave.Height / 2, 5, sonotrode.K, wave.Coefficient(sonotrode.OscillationNode), wave.Shift(sonotrode.OscillationNode)));
                CurveWave.Add(new PointF((float)SonotrodeWavePixels[i].X, (float)SonotrodeWavePixels[i].Y));
            }
            for (int i = wave.Center; i < wave.T; i++)
            {
                SonotrodeWavePixels.Add(new WaveCoordinates(sonotrode.AmplitudeDetail, i, ProcessWave.Height / 2, 5, sonotrode.K, wave.Coefficient(1 - sonotrode.OscillationNode), wave.Shift(1 - sonotrode.OscillationNode)));
                CurveWave.Add(new PointF((float)SonotrodeWavePixels[i].X, (float)SonotrodeWavePixels[i].Y));
            }
            for (int i = 0; i < wave.T; i++)
            {
                GradientWave.Add(new WaveGradient(sonotrode.AmplitudeDetail, ProcessWave.Height, CurveWave[i], new PointF(i, ProcessWave.Height / 2)));
            }
        }
        //господи кака€ же € мудень криворука€
        private string GenerateDescription(Sonotrode sonotrode)
        {
            var description = new StringBuilder();
            description.AppendLine("A1: " + sonotrode.A1.ToString());
            description.AppendLine("A2: " + sonotrode.A2.ToString());
            if (Type == SonotrodeTypes.StepwiseCircular)
            {
                description.AppendLine("D1: " + ((StepwiseCircularSonotrode)sonotrode).D1.ToString());
                description.AppendLine("D2: " + ((StepwiseCircularSonotrode)sonotrode).D2.ToString());
                description.AppendLine("L1: " + ((StepwiseCircularSonotrode)sonotrode).L1.ToString());
                description.AppendLine("L2: " + ((StepwiseCircularSonotrode)sonotrode).L2.ToString());
            }
            else if (Type == SonotrodeTypes.ConicCircular)
            {
                description.AppendLine("I1: " + ((ConicCircularSonotrode)sonotrode).I1.ToString());
                description.AppendLine("I2: " + ((ConicCircularSonotrode)sonotrode).I2.ToString());
            }
            else if (Type == SonotrodeTypes.ConicRectangle)
            {
                description.AppendLine("Width: " + ((RectangleSonotrode)sonotrode).Width.ToString());
                description.AppendLine("Height: " + ((RectangleSonotrode)sonotrode).Height.ToString());
                description.AppendLine("I1: " + ((ConicRectangleSonotrode)sonotrode).I1.ToString());
                description.AppendLine("I2: " + ((ConicRectangleSonotrode)sonotrode).I2.ToString());
            }
            description.AppendLine("L: " + sonotrode.L.ToString());

            return description.ToString();
        }
        private void MakeAnimationWave(Sonotrode sonotrode, WaveParameters wave)
        {
            var zoneI = (float)sonotrode.AmplitudeElement;
            var zoneII = (float)sonotrode.AmplitudeDetail;
            var step = ((zoneII * 2 + 1) / (zoneI * 2 + 1));
            var index = 0;

            while (zoneI >= -sonotrode.AmplitudeElement)
            {
                var pixels1 = new List<WaveCoordinates>();
                var col = new List<CompstretchGradient>();

                for (int i = 0; i <= wave.Center; i++)
                {
                    pixels1.Add(new WaveCoordinates(zoneI, i, ProcessWave.Height / 2, 5, sonotrode.K, wave.Coefficient(sonotrode.OscillationNode), wave.Shift(sonotrode.OscillationNode)));
                    //amp.Add(GetSinus(count, (double)i / 10000000));
                    col.Add(new CompstretchGradient(sonotrode.AmplitudeDetail * 2, ProcessWave.Height, new PointF((float)pixels1[index].X, (float)pixels1[index].Y), new PointF(i, ProcessWave.Height / 2)));
                    index++;
                }
                index = 0;
                AnimationWavePixels1.Add(ConverterToPointF(pixels1));
                colors.Add(ConverterToPen(col));
                //amps.Add(amp);
                zoneI--;
            }

            for (int i = AnimationWavePixels1.Count - 1; i >= 0; i--)
            {
                AnimationWavePixels1.Add(AnimationWavePixels1[i]);
                colors.Add(colors[i]);
            }
            index = 0;
            while (zoneII >= -sonotrode.AmplitudeDetail)
            {
                var pixels2 = new List<WaveCoordinates>();
                var col = new List<CompstretchGradient>();

                for (int i = wave.Center; i < wave.T; i++)
                {
                    pixels2.Add(new WaveCoordinates(zoneII, i, ProcessWave.Height / 2, 5, sonotrode.K, wave.Coefficient(1 - sonotrode.OscillationNode), wave.Shift(1 - sonotrode.OscillationNode)));
                    col.Add(new CompstretchGradient(sonotrode.AmplitudeDetail * 2, ProcessWave.Height, new PointF((float)pixels2[index].X, (float)pixels2[index].Y), new PointF(i, ProcessWave.Height / 2)));
                    index++;
                }
                index = 0;
                AnimationWavePixels2.Add(ConverterToPointF(pixels2));
                colors1.Add(ConverterToPen(col));
                zoneII -= step;
            }

            for (int i = AnimationWavePixels2.Count - 1; i >= 0; i--)
            {
                AnimationWavePixels2.Add(AnimationWavePixels2[i]);
                colors1.Add(colors1[i]);
            }

            //for (int i = pixels1.Count - 1; i >= 0; i--)
            //{
            //    pixels1.Add(pixels1[i]);
            //    colors1.Add(colors1[i]);
            //}
        }
        private List<Pen> ConverterToPen(List<CompstretchGradient> wave)
        {
            List<Pen> pens = new();

            foreach (var p in wave)
                pens.Add(p.PenColor);

            return pens;
        }
        private List<PointF> ConverterToPointF(List<WaveCoordinates> wave)
        {
            List<PointF> clearWave = new();

            foreach (var p in wave)
                clearWave.Add(new PointF((float)p.X, (float)p.Y));

            return clearWave;
        }
        private void SonotrodeGenerator_Load(object sender, EventArgs e)
        {
            Type = (SonotrodeTypes)SonotrodeType.SelectedIndex;
        }
        private bool flag = false;
        CancellationTokenSource cts = new CancellationTokenSource();
        async void ProcessWave_Click(object sender, EventArgs e)
        { 
            flag = !flag;
            if (flag)
            {
                cts.Dispose();
                cts = new CancellationTokenSource();
                await ChangeLabel(cts.Token);
            }
            else
            {
                cts.Cancel();
            }
        }
        async Task ChangeLabel(CancellationToken cancelToken) // этот метод выполн€тс€ в другом потоке
        {
            var i = 0;
            var myBitmap = new Bitmap(ProcessWave.Width, ProcessWave.Height);
            var g = Graphics.FromImage(myBitmap);
            DoubleBuffered = true;
            //g.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2), new PointF(ProcessWave.Width, ProcessWave.Height / 2));
            //var animationArray = new List<PointF>();

            //foreach (var p in AnimationWavePixels)
            //    animationArray.Add(new PointF(p.X, p.Y));
            //var j = 0;
            //var step = (int)Math.Round((double)pixels1.Count / (double)pixels.Count);
            while (!cancelToken.IsCancellationRequested)
            {
                g.Clear(Color.White);

                for (int j = 0; j < colors[i].Count; j++)
                    g.DrawLine(colors[i][j], new PointF(AnimationWavePixels1[i][j].X, AnimationWavePixels1[i][j].Y), new PointF(AnimationWavePixels1[i][j].X, ProcessWave.Height / 2));

                for (int j = 0; j < colors1[i].Count; j++)
                    g.DrawLine(colors1[i][j], new PointF(AnimationWavePixels2[i][j].X, AnimationWavePixels2[i][j].Y), new PointF(AnimationWavePixels2[i][j].X, ProcessWave.Height / 2));

                g.DrawCurve(new Pen(Color.Black, 2), AnimationWavePixels1[i].Union(AnimationWavePixels2[i]).ToArray());
                //g.DrawCurve(new Pen(Color.Black, 2), AnimationWavePixels2[i].ToArray());
                //g.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2 - 100), new PointF(ProcessWave.Width, ProcessWave.Height / 2 - 100));
                //g.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2 + 100), new PointF(ProcessWave.Width, ProcessWave.Height / 2 + 100));
                //g.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2 - 150), new PointF(ProcessWave.Width, ProcessWave.Height / 2 - 150));
                //g.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2 + 150), new PointF(ProcessWave.Width, ProcessWave.Height / 2 + 150));
                //g.DrawCurve(new Pen(Color.Black, 2), pixels1[i].ToArray());
                i = (i >= AnimationWavePixels1.Count - 1) ? i = 0 : i += 1;
                ////j = (j + step >= pixels1.Count - 1) ? j = 0 : j += step;
                await Task.Delay(50);
                ProcessWave.BackgroundImage = myBitmap;
                Refresh();
            }
        }

        private void SonotrodeGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        }
    }
}