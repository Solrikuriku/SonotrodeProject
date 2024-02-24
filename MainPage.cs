using System.Text;
using System.Windows.Forms;

namespace SonotrodeProject
{
    public partial class SonotrodeGenerator : Form
    {
        public SonotrodeGenerator()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            SonotrodeType.SelectedIndex = 0;
            ProcessWave.Image = new Bitmap(ProcessWave.Width, ProcessWave.Height);
            ProcessWave.BackColor = Color.White;
            AlSound.Value = 5100;
        }

        private readonly List<WaveCoordinates> SonotrodeWavePixels = new();
        private readonly List<PointF> CurveWave = new();
        private readonly List<WaveGradient> GradientWave = new();
        private SonotrodeTypes Type;
        private enum SonotrodeTypes
        {
            StepwiseCircular,
            ConicCircular,
            ConicRectangle
        }
        //для подавления мерцания?
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}

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
            SonotrodeDescription.Text = GenerateDescription(sonotrodetest);
        }

        private void ProcessWave_Paint(object sender, PaintEventArgs e)
        {
            //this.DoubleBuffered = true;
            if (MakeWave.Checked)
            {
                foreach (var p in GradientWave)
                    e.Graphics.DrawLine(p.PenColor, p.LinePixel, p.BorderPixel);

                e.Graphics.DrawCurve(new Pen(Color.Black, 2), CurveWave.ToArray());
            }

            e.Graphics.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2), new PointF(ProcessWave.Width, ProcessWave.Height / 2));
            this.Refresh();
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
        private string GenerateDescription(Sonotrode sonotrode)
        {
            var description = new StringBuilder();
            description.AppendLine("A1: " + sonotrode.A1.ToString());
            description.AppendLine("A2: " + sonotrode.A2.ToString());
            if (Type == SonotrodeTypes.StepwiseCircular)
            {
                description.AppendLine("D1: " + ((StepwiseCircularSonotrode)sonotrode).D1.ToString());
                description.AppendLine("D2: " + ((StepwiseCircularSonotrode)sonotrode).D2.ToString());
                description.AppendLine("L1: " + ((StepwiseCircularSonotrode)sonotrode).D1.ToString());
                description.AppendLine("L2: " + ((StepwiseCircularSonotrode)sonotrode).D2.ToString());
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

        private void SonotrodeGenerator_Load(object sender, EventArgs e)
        {
            Type = (SonotrodeTypes)SonotrodeType.SelectedIndex;
        }
    }
}