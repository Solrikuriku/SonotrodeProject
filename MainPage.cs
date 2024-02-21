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
        }

        private readonly List<WaveCoordinates> sonotrodeWavePixels = new List<WaveCoordinates>();
        private List<PointF> curveWave = new List<PointF>();
        //private readonly WaveParameters sonotrodeWave = new WaveParameters();
        private void SonotrodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SonotrodeType.SelectedIndex != 2)
            {
                Diameter.Enabled = true;
                Width.Enabled = false;
                Height.Enabled = false;
            }
            else
            {
                Diameter.Enabled = false;
                Width.Enabled = true;
                Height.Enabled = true;
            }
        }

        public double AluminiumLength()
        {
            return 20f;
        }

        private void UpdateData_Click(object sender, EventArgs e)
        {
            TestBoxForValues.Clear();

            if (SonotrodeType.SelectedIndex == 0)
            {

                var sonotrodetest = new StepwiseCircularSonotrode()
                {
                    SpeedOfSound = AlSound.Value,
                    //исходное
                    AmplitudeElement = 20,
                    AmplitudeDetail = int.Parse(Amplitude.Text),
                    //исходное
                    Frequency = 20,
                    K = 1,
                    D2 = double.Parse(Diameter.Text)
                };

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
                    SpeedOfSound = AlSound.Value,
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
                    l = sonotrodetest.L, 
                    on = sonotrodetest.OscillationNode, 
                    u = sonotrodetest.SpeedOfSound
                };
                //richTextBox1.AppendText(sonotrodeWave.Center() + "\n" + sonotrodeWave.T());
                //for (int i = 0; i < sonotrodeWave.Center; i++)
                //{
                //    sonotrodeWavePixels.Add(new WaveCoordinates(sonotrodetest.AmplitudeElement, i, ProcessWave.Height / 2, 5, sonotrodetest.K, sonotrodeWave.Coefficient(sonotrodetest.OscillationNode), Math.PI / 2));
                //    curveWave.Add(new PointF((float)sonotrodeWavePixels[i].X, (float)sonotrodeWavePixels[i].Y));
                //}
                //for (int i = sonotrodeWave.Center; i < sonotrodeWave.T; i++)
                //{
                //    sonotrodeWavePixels.Add(new WaveCoordinates(sonotrodetest.AmplitudeDetail, i, ProcessWave.Height / 2, 5, sonotrodetest.K, sonotrodeWave.Coefficient(1 - sonotrodetest.OscillationNode), 2 * Math.PI / 3));
                //    curveWave.Add(new PointF((float)sonotrodeWavePixels[i].X, (float)sonotrodeWavePixels[i].Y));
                //}

                //curveWave.Clear();
                MakeConicCurve(sonotrodetest, sonotrodeWave);

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
                    SpeedOfSound = AlSound.Value,
                    //исходное
                    AmplitudeElement = 20,
                    AmplitudeDetail = int.Parse(Amplitude.Text),
                    Width = int.Parse(Width.Text),
                    Height = int.Parse(Height.Text),
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

        private void AlSound_Scroll(object sender, EventArgs e)
        {
            SoundValues.Text = AlSound.Value.ToString();
        }

        private void ProcessWave_Paint(object sender, PaintEventArgs e)
        {
            if (MakeWave.Checked)
            {
                e.Graphics.DrawCurve(new Pen(Color.Black), curveWave.ToArray());
            }
            this.Refresh();
        }

        //перегрузки?
        private void MakeConicCurve(Sonotrode sonotrode, WaveParameters wave)
        {
            for (int i = 0; i < wave.Center; i++)
            {
                sonotrodeWavePixels.Add(new WaveCoordinates(sonotrode.AmplitudeElement, i, ProcessWave.Height / 2, 5, sonotrode.K, wave.Coefficient(sonotrode.OscillationNode), wave.Shift(sonotrode.OscillationNode)));
                curveWave.Add(new PointF((float)sonotrodeWavePixels[i].X, (float)sonotrodeWavePixels[i].Y));
            }
            for (int i = wave.Center; i < wave.T; i++)
            {
                sonotrodeWavePixels.Add(new WaveCoordinates(sonotrode.AmplitudeDetail, i, ProcessWave.Height / 2, 5, sonotrode.K, wave.Coefficient(1 - sonotrode.OscillationNode), wave.Shift(1 - sonotrode.OscillationNode)));
                curveWave.Add(new PointF((float)sonotrodeWavePixels[i].X, (float)sonotrodeWavePixels[i].Y));
            }
        }

        private void MakeStepwiseCurve(int a, int b)
        {

        }
    }
}