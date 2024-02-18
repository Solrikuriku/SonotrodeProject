namespace SonotrodeProject
{
    public partial class SonotrodeGenerator : Form
    {
        public SonotrodeGenerator()
        {
            InitializeComponent();
            SonotrodeType.SelectedIndex = 0;
        }

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
                    K = 1,
                    D2 = double.Parse(Diameter.Text),
                    L1 = 10,
                    L2 = 10
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
    }
}