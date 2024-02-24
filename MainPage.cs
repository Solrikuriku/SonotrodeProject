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
        private readonly List<PointF> CurveWave = new();
        private readonly List<WaveGradient> GradientWave = new();

        private void SonotrodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SonotrodeType.SelectedIndex != 2)
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

        private static double AluminiumLength()
        {
            return 20f;
        }

        private void UpdateData_Click(object sender, EventArgs e)
        {
            TestBoxForValues.Clear();
            SonotrodeWavePixels.Clear();
            CurveWave.Clear();

            if (SonotrodeType.SelectedIndex == 0)
            {

                var sonotrodetest = new StepwiseCircularSonotrode()
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

                var sonotrodeWave = new WaveParameters()
                {
                    L = sonotrodetest.L,
                    ON = sonotrodetest.OscillationNode,
                    U = sonotrodetest.SpeedOfSound
                };

                MakeCurve(sonotrodetest, sonotrodeWave);

                TestBoxForValues.AppendText
                    (
                        sonotrodetest.D1.ToString() + "\n"
                      + sonotrodetest.D2.ToString() + "\n"
                      + sonotrodetest.A1.ToString() + "\n"
                      + sonotrodetest.A2.ToString() + "\n"
                      + sonotrodetest.L1.ToString() + "\n"
                      + sonotrodetest.L2.ToString() + "\n"
                      + sonotrodetest.L.ToString()
                    );
            }
            else if (SonotrodeType.SelectedIndex == 1)
            {

                var sonotrodetest = new ConicCircularSonotrode()
                {
                    SpeedOfSound = (int)AlSound.Value,
                    //исходное
                    AmplitudeElement = 20,
                    AmplitudeDetail = int.Parse(Amplitude.Text),
                    //исходное
                    Frequency = 20,
                    K = 1.03,
                    D2 = double.Parse(Diameter.Text),
                    L1 = 10,
                    L2 = 10
                };

                var sonotrodeWave = new WaveParameters()
                {
                    L = sonotrodetest.L,
                    ON = sonotrodetest.OscillationNode,
                    U = sonotrodetest.SpeedOfSound
                };

                MakeCurve(sonotrodetest, sonotrodeWave);

                TestBoxForValues.AppendText
                    (
                        sonotrodetest.D1.ToString() + "\n"
                      + sonotrodetest.D2.ToString() + "\n"
                      + sonotrodetest.A1.ToString() + "\n"
                      + sonotrodetest.A2.ToString() + "\n"
                      + sonotrodetest.L1.ToString() + "\n"
                      + sonotrodetest.L2.ToString() + "\n"
                      + sonotrodetest.L.ToString()
                    );
            }
            else
            {
                var sonotrodetest = new ConicRectangleSonotrode()
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
                    L1 = 10,
                    L2 = 10
                };

                TestBoxForValues.AppendText
                    (
                        sonotrodetest.A1.ToString() + "\n"
                      + sonotrodetest.A2.ToString() + "\n"
                      + sonotrodetest.L1.ToString() + "\n"
                      + sonotrodetest.L2.ToString() + "\n"
                      + sonotrodetest.L.ToString()
                    );
            }
        }

        private void ProcessWave_Paint(object sender, PaintEventArgs e)
        {
            if (MakeWave.Checked)
            {
                foreach (var p in GradientWave)
                {
                    e.Graphics.DrawLine(p.PenColor, p.LinePixel, p.BorderPixel);
                }

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
    }
}