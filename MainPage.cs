using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using Microsoft.Data.SqlClient;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System;
using System.Runtime.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Reflection.Metadata.BlobBuilder;
using System.Diagnostics.Metrics;
using System.ComponentModel;
using SharpGL.Enumerations;
using SharpGL;
using System.Reflection.Metadata;
using System.Collections.Generic;

namespace SonotrodeProject
{
    public partial class SonotrodeGenerator : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        public SonotrodeGenerator()
        {
            InitializeComponent();

            SonotrodeType.SelectedIndex = 0;
            PictureBox ProcessWave = new();
            ProcessWave.Image = new Bitmap(ProcessWave.Width, ProcessWave.Height);
            ProcessWave.BackColor = Color.White;
            AlSound.Value = 5100;

            MaterialsTable.ContextMenuStrip = MenuMaterials;
            MaterialsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            UpdateData.Enabled = false;
            SaveSonotrodeInfo.Enabled = false;

            StressWave.Checked = true;

            SonotrodeType.DropDownStyle = ComboBoxStyle.DropDownList;

            MaterialsTable.Columns[0].ReadOnly = true;
            MaterialsTable.Columns[1].ReadOnly = true;
            MaterialsTable.Columns[2].ReadOnly = true;
        }

        private readonly List<WaveCoordinates> SonotrodeWavePixels = new();
        //поменять, объединить
        private readonly List<List<PointF>> AnimationWavePixels1 = new();
        private readonly List<List<PointF>> AnimationWavePixels2 = new();
        private readonly List<List<Pen>> ColorsZoneFirst = new();
        private readonly List<List<Pen>> ColorsZoneSecond = new();

        private readonly List<PointF> CurveWave = new();
        private readonly List<WaveGradient> GradientWave = new();

        private Sonotrode CalcSonotrode;
        private Carcas3D Sonotrode3DModel = new();

        private int ScaleCoeff = 4;

        private BindingList<MainMaterials> DataMaterials = new();

        internal SonotrodeTypes Type;
        internal SonotrodeStatus Status;
        internal enum SonotrodeTypes
        {
            StepwiseCircular,
            ConicCircular,
            ConicRectangle
        }

        internal enum SonotrodeStatus
        {
            Empty,
            Make
        }

        private void SonotrodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateData.Enabled = false;
            //ClearAll();
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
                UpdateData.Enabled = false;
            }
        }

        private void ClearAll()
        {
            SonotrodeWavePixels.Clear();
            AnimationWavePixels1.Clear();
            AnimationWavePixels2.Clear();
            CurveWave.Clear();
            GradientWave.Clear();
            Sonotrode3DModel.d1.Clear();
            Sonotrode3DModel.d2.Clear();
            Sonotrode3DModel.d3.Clear();
            Sonotrode3DModel.d4.Clear();
            ColorsZoneFirst.Clear();
            ColorsZoneSecond.Clear();
        }

        private void UpdateData_Click(object sender, EventArgs e)
        {
            var isSizeBig = false;
            var amplitude = int.Parse(Amplitude.Text);

            if (!CheckAmplitude(amplitude))
            {
                MessageBox.Show("Enter amplitude in range [20, 60]");
            }
            else
            {
                ClearAll();

                Status = SonotrodeStatus.Make;

                if (Type == SonotrodeTypes.StepwiseCircular)
                {
                    CalcSonotrode = new StepwiseCircularSonotrode()
                    {
                        SpeedOfSound = (int)AlSound.Value,
                        //исходное
                        AmplitudeElement = 20,
                        AmplitudeDetail = amplitude,
                        //исходное
                        Frequency = 20,
                        //K = 1,
                        D2 = double.Parse(Diameter.Text)
                    };

                    //var aaa = new Pixels3D();
                    Sonotrode3DModel.Initialization((StepwiseCircularSonotrode)CalcSonotrode);
                    isSizeBig = TestSize(((StepwiseCircularSonotrode)CalcSonotrode).D1);
                }
                else if (Type == SonotrodeTypes.ConicCircular)
                {
                    CalcSonotrode = new ConicCircularSonotrode()
                    {
                        SpeedOfSound = (int)AlSound.Value,
                        //исходное
                        AmplitudeElement = 20,
                        AmplitudeDetail = amplitude,
                        //исходное
                        Frequency = 20,
                        D2 = double.Parse(Diameter.Text),
                        I1 = 10,
                        I2 = 15
                    };

                    Sonotrode3DModel.Initialization((ConicCircularSonotrode)CalcSonotrode);
                    isSizeBig = TestSize(((ConicCircularSonotrode)CalcSonotrode).D1);
                }
                else
                {
                    CalcSonotrode = new ConicRectangleSonotrode()
                    {
                        SpeedOfSound = (int)AlSound.Value,
                        //исходное
                        AmplitudeElement = 20,
                        AmplitudeDetail = amplitude,
                        Width = int.Parse(WidthR.Text),
                        Height = int.Parse(HeightR.Text),
                        //исходное
                        Frequency = 20,
                        I1 = 10,
                        I2 = 15
                    };

                    Sonotrode3DModel.Initialization((ConicRectangleSonotrode)CalcSonotrode);
                    isSizeBig = TestSize(((ConicRectangleSonotrode)CalcSonotrode).W, ((ConicRectangleSonotrode)CalcSonotrode).Height);
                }

                var sonotrodeWave = new WaveParameters()
                {
                    L = CalcSonotrode.L,
                    ON = CalcSonotrode.OscillationNode,
                    U = CalcSonotrode.SpeedOfSound
                };

                MakeCurve(CalcSonotrode, sonotrodeWave);
                MakeAnimationWave(CalcSonotrode, sonotrodeWave);
                SonotrodeDescription.Text = GenerateDescription(CalcSonotrode);
                MaterialDescription.Text = EnabledMaterials();

                SaveSonotrodeInfo.Enabled = true;

                OnToken();

                if (isSizeBig)
                    MessageBox.Show("WARNING! This sonotrode is big enough. You have to consider transverse waves!");
            }
        }

        private bool TestSize(double d)
        {
            if (d > 90)
                return true;
            else
                return false;
        }

        private bool TestSize(double w, double h)
        {
            if (w > 90 || h > 90)
                return true;
            else
                return false;
        }

        private void ProcessWave_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2), new PointF(ProcessWave.Width, ProcessWave.Height / 2));
        }

        //перегрузки?
        private void MakeCurve(Sonotrode sonotrode, WaveParameters wave)
        {
            var shift1 = wave.Shift(sonotrode.OscillationNode);
            var shift2 = wave.Shift(1 - sonotrode.OscillationNode);


            for (int i = 0; i < wave.Center; i++)
            {
                //SonotrodeWavePixels.Add(new WaveCoordinates(sonotrode.AmplitudeElement, i, ProcessWave.Height / 2, ScaleCoeff, sonotrode.K, wave.Coefficient(sonotrode.OscillationNode), wave.Shift(sonotrode.OscillationNode)));
                SonotrodeWavePixels.Add(new WaveCoordinates(sonotrode.AmplitudeElement, i, ProcessWave.Height / 2, ScaleCoeff, wave.ZoneCoeff(sonotrode.L * sonotrode.OscillationNode), shift1));
                CurveWave.Add(new PointF((float)SonotrodeWavePixels[i].X, (float)SonotrodeWavePixels[i].Y));
            }
            for (int i = wave.Center; i < wave.T; i++)
            {
                //SonotrodeWavePixels.Add(new WaveCoordinates(sonotrode.AmplitudeDetail, i, ProcessWave.Height / 2, ScaleCoeff, sonotrode.K, wave.Coefficient(1 - sonotrode.OscillationNode), wave.Shift(1 - sonotrode.OscillationNode)));
                SonotrodeWavePixels.Add(new WaveCoordinates(sonotrode.AmplitudeDetail, i, ProcessWave.Height / 2, ScaleCoeff, wave.ZoneCoeff(sonotrode.L * (1 - sonotrode.OscillationNode)), shift2));
                CurveWave.Add(new PointF((float)SonotrodeWavePixels[i].X, (float)SonotrodeWavePixels[i].Y));
            }
            for (int i = 0; i < wave.T; i++)
            {
                //GradientWave.Add(new WaveGradient(sonotrode.AmplitudeDetail, ProcessWave.Height, CurveWave[i], new PointF(i, ProcessWave.Height / 2), ScaleCoeff));
                GradientWave.Add(new WaveGradient(sonotrode.AmplitudeDetail, CurveWave[i], SonotrodeWavePixels[i].WaveY, new PointF(i, ProcessWave.Height / 2)));
            }
        }
        //господи какая же я мудень криворукая
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
                description.AppendLine("D1: " + ((ConicCircularSonotrode)sonotrode).D1.ToString());
                description.AppendLine("D2: " + ((ConicCircularSonotrode)sonotrode).D2.ToString());
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
            //var step = ((zoneII * 2 + 1) / (zoneI * 2 + 1));
            var step = ((zoneII) / (zoneI));
            var index = 0;
            var shift1 = wave.Shift(sonotrode.OscillationNode);
            var shift2 = wave.Shift(1 - sonotrode.OscillationNode);

            while (zoneI >= -sonotrode.AmplitudeElement)
            {
                var pixels1 = new List<WaveCoordinates>();
                var col = new List<CompstretchGradient>();

                for (int i = 0; i <= wave.Center; i++)
                {
                    pixels1.Add(new WaveCoordinates(zoneI, i, ProcessWave.Height / 2, ScaleCoeff, wave.ZoneCoeff(sonotrode.L * sonotrode.OscillationNode), shift1));

                    //amp.Add(GetSinus(count, (double)i / 10000000));
                    //col.Add(new CompstretchGradient(sonotrode.AmplitudeDetail * 2, ProcessWave.Height, new PointF((float)pixels1[index].X, (float)pixels1[index].Y), new PointF(i, ProcessWave.Height / 2)));
                    col.Add(new CompstretchGradient(sonotrode.AmplitudeDetail, ProcessWave.Height, new PointF((float)pixels1[index].X, (float)pixels1[index].Y), new PointF(i, ProcessWave.Height / 2), ScaleCoeff));
                    index++;
                }
                index = 0;
                AnimationWavePixels1.Add(ConverterToPointF(pixels1));
                ColorsZoneFirst.Add(ConverterToPen(col));
                //amps.Add(amp);
                zoneI--;
            }

            for (int i = AnimationWavePixels1.Count - 1; i >= 0; i--)
            {
                AnimationWavePixels1.Add(AnimationWavePixels1[i]);
                ColorsZoneFirst.Add(ColorsZoneFirst[i]);
            }
            index = 0;

            while (zoneII >= -sonotrode.AmplitudeDetail)
            {
                var pixels2 = new List<WaveCoordinates>();
                var col = new List<CompstretchGradient>();

                for (int i = wave.Center; i < wave.T; i++)
                {
                    pixels2.Add(new WaveCoordinates(zoneII, i, ProcessWave.Height / 2, ScaleCoeff, wave.ZoneCoeff(sonotrode.L * (1 - sonotrode.OscillationNode)), shift2));

                    //col.Add(new CompstretchGradient(sonotrode.AmplitudeDetail * 2, ProcessWave.Height, new PointF((float)pixels2[index].X, (float)pixels2[index].Y), new PointF(i, ProcessWave.Height / 2)));
                    col.Add(new CompstretchGradient(sonotrode.AmplitudeDetail, ProcessWave.Height, new PointF((float)pixels2[index].X, (float)pixels2[index].Y), new PointF(i, ProcessWave.Height / 2), ScaleCoeff));
                    index++;
                }
                index = 0;
                AnimationWavePixels2.Add(ConverterToPointF(pixels2));
                ColorsZoneSecond.Add(ConverterToPen(col));
                zoneII -= step;
            }

            for (int i = AnimationWavePixels2.Count - 1; i >= 0; i--)
            {
                AnimationWavePixels2.Add(AnimationWavePixels2[i]);
                ColorsZoneSecond.Add(ColorsZoneSecond[i]);
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
            Diameter.TextChanged += new EventHandler(BlockButton_TextChanged);
            Amplitude.TextChanged += new EventHandler(BlockButton_TextChanged);
            WidthR.TextChanged += new EventHandler(BlockButton_TextChanged);
            HeightR.TextChanged += new EventHandler(BlockButton_TextChanged);

            //MakeWave.Enabled = false;

            Info3D.Text = NavigationDescription();
            Type = (SonotrodeTypes)SonotrodeType.SelectedIndex;
            Status = SonotrodeStatus.Empty;
            InitMaterials();
        }

        CancellationTokenSource cts = new CancellationTokenSource();
        //bool flag = false;
        async void OnToken()
        {
            //var myBitmap = new Bitmap(ProcessWave.Width, ProcessWave.Height);
            //var g = Graphics.FromImage(myBitmap);

            ProcessWave.Focus();
            cts.Cancel();
            //cts.Dispose();
            //cts = new CancellationTokenSource();

            cts.Dispose();
            cts = new CancellationTokenSource();
            await ChangeLabel(cts.Token);

            //if (StretchingCompressionWave.Checked && Status == SonotrodeStatus.Make)
            //{
            //    //cts.Cancel();
            //    cts.Dispose();
            //    cts = new CancellationTokenSource();
            //    await ChangeLabel(cts.Token);
            //    //DrawStaticAWaveTest(myBitmap, g);

            //}
            //else if (StressWave.Checked && Status == SonotrodeStatus.Make)
            //    DrawStaticWaveTest(myBitmap, g);

        }
        private void ProcessWave_Click(object sender, EventArgs e)
        {
            //flag = !flag;
            //if (!flag)
            //    cts.Cancel();


            //ProcessWave.Focus();

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

        async Task ChangeLabel(CancellationToken cancelToken) // этот метод выполнятся в другом потоке
        {
            var i = 0;
            var myBitmap = new Bitmap(ProcessWave.Width, ProcessWave.Height);
            var g = Graphics.FromImage(myBitmap);

            while (!cancelToken.IsCancellationRequested)
            {
                g.Clear(Color.White);

                if (StretchingCompressionWave.Checked && Status == SonotrodeStatus.Make)
                {
                    //ColorsZoneFirst[i].Count

                    for (int j = 0; j < ColorsZoneFirst[i].Count; j++)
                        g.DrawLine(ColorsZoneFirst[i][j], new PointF(AnimationWavePixels1[i][j].X, AnimationWavePixels1[i][j].Y), new PointF(AnimationWavePixels1[i][j].X, ProcessWave.Height / 2));

                    for (int j = 0; j < ColorsZoneSecond[i].Count; j++)
                        g.DrawLine(ColorsZoneSecond[i][j], new PointF(AnimationWavePixels2[i][j].X, AnimationWavePixels2[i][j].Y), new PointF(AnimationWavePixels2[i][j].X, ProcessWave.Height / 2));

                    g.DrawCurve(new Pen(Color.Black, 2), AnimationWavePixels1[i].Union(AnimationWavePixels2[i]).ToArray());

                    i = (i >= ColorsZoneFirst.Count - 1) ? 0 : i += 1;

                    //richTextBox1.AppendText(a.ToString() + "\n");

                    //g.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2 + sonotrodetest.AmplitudeDetail * 5), new PointF(ProcessWave.Width, ProcessWave.Height / 2 + sonotrodetest.AmplitudeDetail * 5));
                    //g.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2 - sonotrodetest.AmplitudeDetail * 5), new PointF(ProcessWave.Width, ProcessWave.Height / 2 - sonotrodetest.AmplitudeDetail * 5));

                    await Task.Delay(50);
                }
                else if (StressWave.Checked && Status == SonotrodeStatus.Make)
                {
                    foreach (var p in GradientWave)
                        g.DrawLine(p.PenColor, p.LinePixel, p.BorderPixel);

                    g.DrawCurve(new Pen(Color.Black, 2), CurveWave.ToArray());

                    await Task.Delay(50);
                }
                g.DrawLine(new Pen(Color.Black, 2), new PointF(0, ProcessWave.Height / 2), new PointF(ProcessWave.Width, ProcessWave.Height / 2));
                ProcessWave.BackgroundImage = myBitmap;
                Refresh();
            }
        }

        private void SonotrodeGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SonType.Value = Type;
            //_3DSonotrode f = new _3DSonotrode();
            //f.Owner = this;
            //f.Show();
        }

        internal static class SonType
        {
            public static SonotrodeTypes Value { get; set; }
        }

        private void InitMaterials()
        {
            MaterialsTable.DataSource = null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BindingList<MainMaterials>), new XmlRootAttribute("root"));

            //string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var allUsersAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(allUsersAppData, @"SonotrodeData\DataMaterials.xml");

            //var path = Path.GetFullPath("DataMaterials.xml");
            //string fileName = @"DataMaterials.xml";
            //FileInfo f = new FileInfo(fileName);
            //string path = f.FullName;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                //List<MainMaterials>? materialsCollection = xmlSerializer.Deserialize(fs) as List<MainMaterials>;
                //MaterialsTable.DataSource = materialsCollection;

                DataMaterials = xmlSerializer.Deserialize(fs) as BindingList<MainMaterials>;
                MaterialsTable.DataSource = DataMaterials;
            }

            //DataSet dataSet = new DataSet();
            //dataSet.ReadXml("DataMaterials.xml");

        }

        private void DeleteMaterials_Click(object sender, EventArgs e)
        {

            var selectedRows = MaterialsTable.SelectedRows;
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                int rowIndex = selectedRow.Index;

                if (rowIndex < 0)
                    continue;
                else if (rowIndex >= 0 && rowIndex <= 15)
                {
                    MessageBox.Show("Этот материал нельзя удалить");
                    continue;
                }

                //var material = DataMaterials[rowIndex];
                DataMaterials.RemoveAt(rowIndex);
                UpdateMaterials();
                RefreshTable();
            }

        }

        private void AddMaterial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AmplitudeS.Text) || string.IsNullOrEmpty(AmplitudeE.Text) || string.IsNullOrEmpty(MaterialName.Text))
                MessageBox.Show("Empty values!");
            else if (int.Parse(AmplitudeS.Text) < int.Parse(AmplitudeE.Text))
            {
                DataMaterials.Add(new MainMaterials(MaterialName.Text, int.Parse(AmplitudeS.Text), int.Parse(AmplitudeE.Text)));
                UpdateMaterials();
                RefreshTable();
            }
            else
                MessageBox.Show("Start amplitude can't be more than end amplitude.");

            //MaterialsTable.DataSource = DataMaterials;
        }

        private void UpdateMaterials()
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BindingList<MainMaterials>), new XmlRootAttribute("root"));

            var allUsersAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(allUsersAppData, @"SonotrodeData\DataMaterials.xml");

            //var path = Path.GetFullPath("DataMaterials.xml");
            //string fileName = @"DataMaterials.xml";
            //FileInfo f = new FileInfo(fileName);
            //string path = f.FullName;

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, DataMaterials);
            }

            //DataSet dataSet = new DataSet();
            //dataSet.ReadXml("DataMaterials.xml");


        }

        private void RefreshTable()
        {
            //не лучшее решение
            MaterialsTable.DataSource = null;
            MaterialsTable.DataSource = DataMaterials;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveSonotrode.Filter = "(.txt)|*.txt";

            //var countString = string.Join(',', counts);
            //AddAll(ref countString);

            if (SaveSonotrode.ShowDialog() == DialogResult.OK)
            {
                StringBuilder info = new();
                SonotrodeBoxing(ref info);

                var fileName = SaveSonotrode.FileName;
                File.WriteAllText(fileName, info.ToString());

                //отправить в боксинг и сгенерировать .снтрд
                //потом выгрузить его
            }
        }

        private void SonotrodeBoxing(ref StringBuilder info)
        {
            //пока что общая логика

            //info.Append(Type.ToString() + "\n");

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(CalcSonotrode.GetType());

            foreach (PropertyDescriptor prop in properties)
            {
                info.Append(prop.Name + " = " + prop.GetValue(CalcSonotrode).ToString() + "\n");
            }

            var items = new List<string>(info.ToString().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
            items.Sort();
            info = new StringBuilder(string.Join("\n", items.ToArray()));

            info.Append("\n" + "Type: " + Type.ToString() + "\n");
            info.Append("\n" + EnabledMaterials());
        }

        private string EnabledMaterials()
        {

            StringBuilder materialsname = new();
            materialsname.Append("This sonotrode you can use for:" + "\n");

            foreach (var m in DataMaterials)
            {
                if (CalcSonotrode.AmplitudeDetail >= m.AmplitudeStart
                    && CalcSonotrode.AmplitudeDetail <= m.AmplitudeEnd)
                    materialsname.Append(m.Name + "\n");
            }

            return materialsname.ToString();

        }


        private void OGLModel_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = OGLModel.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(MatrixMode.Modelview);

            if (Status == SonotrodeStatus.Make)
            {
                if (Type != SonotrodeTypes.ConicRectangle)
                    CircularRender(gl);
                else
                    RectangleRender(gl);
            }
        }

        private void RectangleRender(OpenGL gl)
        {
            var scaleRender = 25;

            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINES);

            gl.LoadIdentity();
            gl.Translate(0f, 0.0f, ScaleZ);
            gl.Rotate(AngleX, AngleY, AngleZ); ;
            gl.Color(0.0f, 1.0f, 0.0f);

            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Vertex(Sonotrode3DModel.d1[0] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d1[1] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d1[2] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d1[3] / scaleRender);

            gl.End();

            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Vertex(Sonotrode3DModel.d2[0] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d2[1] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d2[2] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d2[3] / scaleRender);

            gl.End();

            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Vertex(Sonotrode3DModel.d3[0] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d3[1] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d3[2] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d3[3] / scaleRender);

            gl.End();

            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Vertex(Sonotrode3DModel.d4[0] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d4[1] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d4[2] / scaleRender);
            gl.Vertex(Sonotrode3DModel.d4[3] / scaleRender);

            gl.End();

            gl.Begin(OpenGL.GL_LINES);

            for (int i = 0; i <= 3; i++)
            {
                gl.Vertex(Sonotrode3DModel.d1[i] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d2[i] / scaleRender);

                gl.Vertex(Sonotrode3DModel.d2[i] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d3[i] / scaleRender);

                gl.Vertex(Sonotrode3DModel.d3[i] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d4[i] / scaleRender);
            }

            gl.End();
            gl.Flush();
        }

        private void CircularRender(OpenGL gl)
        {
            //gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE_LOOP);
            var scaleRender = 25;
            var polygons = 64;

            gl.LoadIdentity();
            gl.Translate(0f, 0.0f, ScaleZ);
            gl.Rotate(AngleX, AngleY, AngleZ);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(0.0f, 1.0f, 0.0f);

            for (int i = 1; i <= polygons; i++)
            {
                gl.Vertex(Sonotrode3DModel.d1[i - 1] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d1[i] / scaleRender);

                gl.Vertex(Sonotrode3DModel.d2[i - 1] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d2[i] / scaleRender);

                gl.Vertex(Sonotrode3DModel.d3[i - 1] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d3[i] / scaleRender);

                gl.Vertex(Sonotrode3DModel.d4[i - 1] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d4[i] / scaleRender);
            }

            for (int i = 0; i < polygons; i++)
            {
                gl.Vertex(Sonotrode3DModel.d1[0].X / scaleRender, Sonotrode3DModel.d1[0].Y / scaleRender, 0.0f);
                gl.Vertex(Sonotrode3DModel.d1[i] / scaleRender);

                gl.Vertex(Sonotrode3DModel.d4[0].X / scaleRender, Sonotrode3DModel.d4[0].Y / scaleRender, 0.0f);
                gl.Vertex(Sonotrode3DModel.d4[i] / scaleRender);
            }

            for (int i = 0; i < polygons; i++)
            {
                gl.Vertex(Sonotrode3DModel.d1[i] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d2[i] / scaleRender);

                gl.Vertex(Sonotrode3DModel.d2[i] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d3[i] / scaleRender);

                gl.Vertex(Sonotrode3DModel.d3[i] / scaleRender);
                gl.Vertex(Sonotrode3DModel.d4[i] / scaleRender);
            }

            gl.End();
            gl.Flush();
        }

        //private void _3DSonotrode_Load(object sender, EventArgs e)
        //{

        //}
        private float AngleX = 0.0f;
        private float AngleY = 0.0f;
        private float AngleZ = 0.0f;
        private float ScaleZ = -10.0f;

        private void ModelManipulation_Tick(object sender, EventArgs e)
        {
            //if (angle < 360) angle += 2.0f;
            //else angle = 0.0f;
        }

        private void OGLModel_Click(object sender, EventArgs e)
        {
            //if (!flag1)
            //    ModelManipulation.Start();
            //else
            //    ModelManipulation.Stop();

            //flag1 = !flag1;
        }
        private void SaveToOBJ_Click(object sender, EventArgs e)
        {
            //MakeCircularOBJ();

            SaveOBJ.Filter = "(.obj)|*.obj";
            string parameters = string.Empty;
            //var countString = string.Join(',', counts);
            //AddAll(ref countString);

            if (SaveOBJ.ShowDialog() == DialogResult.OK)
            {
                if (Type != SonotrodeTypes.ConicRectangle)
                    parameters = MakeCircularOBJ();
                else
                    parameters = MakeRectangleOBJ();

                var fileName = SaveOBJ.FileName;
                File.WriteAllText(fileName, parameters.ToString());

                //отправить в боксинг и сгенерировать .снтрд
                //потом выгрузить его
            }
        }
        private string MakeCircularOBJ()
        {
            StringBuilder objLines = new();

            //AddToSB(ref objLines, Pixels3D.d1);

            for (int i = 0; i < Sonotrode3DModel.d1.Count - 1; i++)
                objLines.AppendLine("v " + Sonotrode3DModel.d1[i].X + " " + Sonotrode3DModel.d1[i].Y + " " + Sonotrode3DModel.d1[i].Z);

            for (int i = 0; i < Sonotrode3DModel.d2.Count - 1; i++)
                objLines.AppendLine("v " + Sonotrode3DModel.d2[i].X + " " + Sonotrode3DModel.d2[i].Y + " " + Sonotrode3DModel.d2[i].Z);

            for (int i = 0; i < Sonotrode3DModel.d3.Count - 1; i++)
                objLines.AppendLine("v " + Sonotrode3DModel.d3[i].X + " " + Sonotrode3DModel.d3[i].Y + " " + Sonotrode3DModel.d3[i].Z);

            for (int i = 0; i < Sonotrode3DModel.d4.Count - 1; i++)
                objLines.AppendLine("v " + Sonotrode3DModel.d4[i].X + " " + Sonotrode3DModel.d4[i].Y + " " + Sonotrode3DModel.d4[i].Z);

            objLines.AppendLine("v " + Sonotrode3DModel.d1[0].X + " " + Sonotrode3DModel.d1[0].Y + " " + 0.0);
            objLines.AppendLine("v " + Sonotrode3DModel.d4[0].X + " " + Sonotrode3DModel.d4[0].Y + " " + 0.0);

            //var l1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 1 };
            //var l2 = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 9 };
            //var l3 = new int[] { 17, 18, 19, 20, 21, 22, 23, 24, 17 };
            //var l4 = new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 25 };

            var l1 = new List<int>(); var l2 = new List<int>();
            var l3 = new List<int>(); var l4 = new List<int>();
            //var l2 = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 9 };
            //var l3 = new int[] { 17, 18, 19, 20, 21, 22, 23, 24, 17 };
            //var l4 = new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 25 };

            for (int i = 1; i <= 64; i++)
                l1.Add(i);
            l1.Add(l1[0]);

            for (int i = 65; i <= 128; i++)
                l2.Add(i);
            l2.Add(l2[0]);

            for (int i = 129; i <= 192; i++)
                l3.Add(i);
            l3.Add(l3[0]);

            for (int i = 193; i <= 256; i++)
                l4.Add(i);
            l4.Add(l4[0]);

            for (int i = 0; i < 64; i++)
            {
                objLines.AppendLine("f " + l1[i] + " " + l1[i + 1] + " " + 257);

                objLines.AppendLine("f " + l1[i] + " " + l2[i] + " " + l2[i + 1]);
                objLines.AppendLine("f " + l1[i] + " " + l1[i + 1] + " " + l2[i + 1]);

                objLines.AppendLine("f " + l2[i] + " " + l3[i] + " " + l3[i + 1]);
                objLines.AppendLine("f " + l2[i] + " " + l2[i + 1] + " " + l3[i + 1]);

                objLines.AppendLine("f " + l3[i] + " " + l4[i] + " " + l4[i + 1]);
                objLines.AppendLine("f " + l3[i] + " " + l3[i + 1] + " " + l4[i + 1]);

                objLines.AppendLine("f " + l4[i] + " " + l4[i + 1] + " " + 258);
            }

            return objLines.ToString();
        }

        private string MakeRectangleOBJ()
        {
            StringBuilder objLines = new();

            for (int i = 0; i < Sonotrode3DModel.d1.Count; i++)
                objLines.AppendLine("v " + Sonotrode3DModel.d1[i].X + " " + Sonotrode3DModel.d1[i].Y + " " + Sonotrode3DModel.d1[i].Z);

            for (int i = 0; i < Sonotrode3DModel.d2.Count; i++)
                objLines.AppendLine("v " + Sonotrode3DModel.d2[i].X + " " + Sonotrode3DModel.d2[i].Y + " " + Sonotrode3DModel.d2[i].Z);

            for (int i = 0; i < Sonotrode3DModel.d3.Count; i++)
                objLines.AppendLine("v " + Sonotrode3DModel.d3[i].X + " " + Sonotrode3DModel.d3[i].Y + " " + Sonotrode3DModel.d3[i].Z);

            for (int i = 0; i < Sonotrode3DModel.d4.Count; i++)
                objLines.AppendLine("v " + Sonotrode3DModel.d4[i].X + " " + Sonotrode3DModel.d4[i].Y + " " + Sonotrode3DModel.d4[i].Z);

            var l1 = new int[] { 1, 2, 3, 4, 1 };
            var l2 = new int[] { 5, 6, 7, 8, 5 };
            var l3 = new int[] { 9, 10, 11, 12, 9 };
            var l4 = new int[] { 13, 14, 15, 16, 13 };

            for (int i = 0; i < 4; i++)
            {
                objLines.AppendLine("f " + l1[i] + " " + l2[i] + " " + l2[i + 1]);
                objLines.AppendLine("f " + l1[i] + " " + l1[i + 1] + " " + l2[i + 1]);

                objLines.AppendLine("f " + l2[i] + " " + l3[i] + " " + l3[i + 1]);
                objLines.AppendLine("f " + l2[i] + " " + l2[i + 1] + " " + l3[i + 1]);

                objLines.AppendLine("f " + l3[i] + " " + l4[i] + " " + l4[i + 1]);
                objLines.AppendLine("f " + l3[i] + " " + l3[i + 1] + " " + l4[i + 1]);
            }

            objLines.AppendLine("f " + l1[0] + " " + l1[1] + " " + l1[2]);
            objLines.AppendLine("f " + l1[0] + " " + l1[2] + " " + l1[3]);

            objLines.AppendLine("f " + l4[0] + " " + l4[1] + " " + l4[2]);
            objLines.AppendLine("f " + l4[0] + " " + l4[2] + " " + l4[3]);

            return objLines.ToString();
        }

        private void OGLModel_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    AngleX += 15.0f;
                    break;
                case Keys.Down:
                    AngleX -= 15.0f;
                    break;
                case Keys.Left:
                    AngleY += 15.0f;
                    break;
                case Keys.Right:
                    AngleY -= 15.0f;
                    break;
                case Keys.Z:
                    AngleZ += 15.0f;
                    break;
                case Keys.X:
                    AngleZ -= 15.0f;
                    break;
                case Keys.Oemplus:
                    ScaleZ += 1.0f;
                    break;
                case Keys.OemMinus:
                    ScaleZ -= 1.0f;
                    break;
            }
        }

        private void OGLModel_KeyDown(object sender, KeyEventArgs e)
        {
            //AngleX -= 15.0f;
        }

        private void ProcessWave_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            //if (e.KeyCode == Keys.Oemplus)
            //    ScaleC += 1;
            //else if (e.KeyCode == Keys.OemMinus)
            //    ScaleC -= 1;

        }

        private void ProcessWave_DoubleClick(object sender, EventArgs e)
        {
            //ScaleCoeff += 1;
        }

        private void ProcessWave_MouseUp(object sender, MouseEventArgs e)
        {
            //ScaleC += 1;
        }

        private void SonotrodeGenerator_KeyUp(object sender, KeyEventArgs e)
        {
            //ScaleC += 1;
        }

        private void SaveToSTL_Click(object sender, EventArgs e)
        {
            SaveSTL.Filter = "(.stl)|*.stl";
            string parameters = string.Empty;

            if (SaveSTL.ShowDialog() == DialogResult.OK)
            {
                if (Type != SonotrodeTypes.ConicRectangle)
                    parameters = MakeCircularSTL();
                else
                    parameters = MakeRectangleSTL();

                var fileName = SaveSTL.FileName;
                File.WriteAllText(fileName, parameters.ToString());

                //отправить в боксинг и сгенерировать .снтрд
                //потом выгрузить его
            }
        }

        private string MakeCircularSTL()
        {
            StringBuilder stlLines = new();

            //AddToSB(ref objLines, Pixels3D.d1);

            stlLines.AppendLine("solid Pizdec");

            for (int i = 1; i <= Sonotrode3DModel.d1.Count - 1; i++)
            {
                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[i].X + " " + Sonotrode3DModel.d1[i].Y + " " + Sonotrode3DModel.d1[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[i - 1].X + " " + Sonotrode3DModel.d1[i - 1].Y + " " + Sonotrode3DModel.d1[i - 1].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i - 1].X + " " + Sonotrode3DModel.d2[i - 1].Y + " " + Sonotrode3DModel.d2[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");

                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[i].X + " " + Sonotrode3DModel.d1[i].Y + " " + Sonotrode3DModel.d1[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i].X + " " + Sonotrode3DModel.d2[i].Y + " " + Sonotrode3DModel.d2[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i - 1].X + " " + Sonotrode3DModel.d2[i - 1].Y + " " + Sonotrode3DModel.d2[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");
            }

            for (int i = 1; i <= Sonotrode3DModel.d2.Count - 1; i++)
            {
                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i].X + " " + Sonotrode3DModel.d2[i].Y + " " + Sonotrode3DModel.d2[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i - 1].X + " " + Sonotrode3DModel.d2[i - 1].Y + " " + Sonotrode3DModel.d2[i - 1].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i - 1].X + " " + Sonotrode3DModel.d3[i - 1].Y + " " + Sonotrode3DModel.d3[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");

                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i].X + " " + Sonotrode3DModel.d2[i].Y + " " + Sonotrode3DModel.d2[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i].X + " " + Sonotrode3DModel.d3[i].Y + " " + Sonotrode3DModel.d3[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i - 1].X + " " + Sonotrode3DModel.d3[i - 1].Y + " " + Sonotrode3DModel.d3[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");
            }

            for (int i = 1; i <= Sonotrode3DModel.d3.Count - 1; i++)
            {
                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i].X + " " + Sonotrode3DModel.d3[i].Y + " " + Sonotrode3DModel.d3[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i - 1].X + " " + Sonotrode3DModel.d3[i - 1].Y + " " + Sonotrode3DModel.d3[i - 1].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[i - 1].X + " " + Sonotrode3DModel.d4[i - 1].Y + " " + Sonotrode3DModel.d4[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");

                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i].X + " " + Sonotrode3DModel.d3[i].Y + " " + Sonotrode3DModel.d3[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[i].X + " " + Sonotrode3DModel.d4[i].Y + " " + Sonotrode3DModel.d4[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[i - 1].X + " " + Sonotrode3DModel.d4[i - 1].Y + " " + Sonotrode3DModel.d4[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");
            }

            for (int i = 1; i <= Sonotrode3DModel.d1.Count - 1; i++)
            {
                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[0].X + " " + Sonotrode3DModel.d1[0].Y + " " + 0.0);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[i].X + " " + Sonotrode3DModel.d1[i].Y + " " + Sonotrode3DModel.d1[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[i - 1].X + " " + Sonotrode3DModel.d1[i - 1].Y + " " + Sonotrode3DModel.d1[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");
            }

            for (int i = 1; i <= Sonotrode3DModel.d4.Count - 1; i++)
            {
                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[0].X + " " + Sonotrode3DModel.d4[0].Y + " " + 0.0);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[i].X + " " + Sonotrode3DModel.d4[i].Y + " " + Sonotrode3DModel.d4[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[i - 1].X + " " + Sonotrode3DModel.d4[i - 1].Y + " " + Sonotrode3DModel.d4[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");
            }

            stlLines.AppendLine("endsolid Pizdec");

            stlLines = stlLines.Replace(',', '.');

            return stlLines.ToString();
        }

        private void Amplitude_TextChanged(object sender, EventArgs e)
        {

        }

        private bool CheckAmplitude(int a)
        {
            if (a < 20 || a > 60)
                return false;
            else
                return true;
        }

        private void Amplitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Diameter_TextChanged(object sender, EventArgs e)
        {
            //UpdateData.Enabled = true;
        }
        //сделать еще енум с типами?
        private void BlockButton_TextChanged(object sender, EventArgs e)
        {
            if (Type != SonotrodeTypes.ConicRectangle)
            {
                if (!String.IsNullOrWhiteSpace(Diameter.Text) && !String.IsNullOrWhiteSpace(Amplitude.Text))
                    UpdateData.Enabled = true;
                else
                    UpdateData.Enabled = false;
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(HeightR.Text) && !String.IsNullOrWhiteSpace(WidthR.Text) && !String.IsNullOrWhiteSpace(Amplitude.Text))
                    UpdateData.Enabled = true;
                else
                    UpdateData.Enabled = false;
            }
        }

        private void Diameter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private string NavigationDescription()
        {
            return "Rotate:" + "\n" + "Arrow keys and Z/X" + "\n" + "Scale:" + "\n" + "+ and -";
        }

        private void StretchingCompressionWave_Click(object sender, EventArgs e)
        {
            //cts.Cancel();
            //OnToken();
        }

        private void StressWave_CheckedChanged(object sender, EventArgs e)
        {
            //OnToken();
        }

        private void StressWave_Click(object sender, EventArgs e)
        {
            //cts.Cancel();
            //OnToken();
        }

        private void AmplitudeS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AmplitudeE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private string MakeRectangleSTL()
        {
            StringBuilder stlLines = new();

            stlLines.AppendLine("solid Pizdec");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[0].X + " " + Sonotrode3DModel.d1[0].Y + " " + Sonotrode3DModel.d1[0].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[1].X + " " + Sonotrode3DModel.d1[1].Y + " " + Sonotrode3DModel.d1[1].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[2].X + " " + Sonotrode3DModel.d1[2].Y + " " + Sonotrode3DModel.d1[2].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[0].X + " " + Sonotrode3DModel.d1[0].Y + " " + Sonotrode3DModel.d1[0].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[2].X + " " + Sonotrode3DModel.d1[2].Y + " " + Sonotrode3DModel.d1[2].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[3].X + " " + Sonotrode3DModel.d1[3].Y + " " + Sonotrode3DModel.d1[3].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            for (int i = 1; i <= Sonotrode3DModel.d1.Count - 1; i++)
            {
                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[i].X + " " + Sonotrode3DModel.d1[i].Y + " " + Sonotrode3DModel.d1[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[i - 1].X + " " + Sonotrode3DModel.d1[i - 1].Y + " " + Sonotrode3DModel.d1[i - 1].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i - 1].X + " " + Sonotrode3DModel.d2[i - 1].Y + " " + Sonotrode3DModel.d2[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");

                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[i].X + " " + Sonotrode3DModel.d1[i].Y + " " + Sonotrode3DModel.d1[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i].X + " " + Sonotrode3DModel.d2[i].Y + " " + Sonotrode3DModel.d2[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i - 1].X + " " + Sonotrode3DModel.d2[i - 1].Y + " " + Sonotrode3DModel.d2[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");
            }

            for (int i = 1; i <= Sonotrode3DModel.d2.Count - 1; i++)
            {
                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i].X + " " + Sonotrode3DModel.d2[i].Y + " " + Sonotrode3DModel.d2[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i - 1].X + " " + Sonotrode3DModel.d2[i - 1].Y + " " + Sonotrode3DModel.d2[i - 1].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i - 1].X + " " + Sonotrode3DModel.d3[i - 1].Y + " " + Sonotrode3DModel.d3[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");

                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[i].X + " " + Sonotrode3DModel.d2[i].Y + " " + Sonotrode3DModel.d2[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i].X + " " + Sonotrode3DModel.d3[i].Y + " " + Sonotrode3DModel.d3[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i - 1].X + " " + Sonotrode3DModel.d3[i - 1].Y + " " + Sonotrode3DModel.d3[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");
            }

            for (int i = 1; i <= Sonotrode3DModel.d3.Count - 1; i++)
            {
                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i].X + " " + Sonotrode3DModel.d3[i].Y + " " + Sonotrode3DModel.d3[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i - 1].X + " " + Sonotrode3DModel.d3[i - 1].Y + " " + Sonotrode3DModel.d3[i - 1].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[i - 1].X + " " + Sonotrode3DModel.d4[i - 1].Y + " " + Sonotrode3DModel.d4[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");

                stlLines.AppendLine("facet normal 0 0 0");
                stlLines.AppendLine("outer loop");
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[i].X + " " + Sonotrode3DModel.d3[i].Y + " " + Sonotrode3DModel.d3[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[i].X + " " + Sonotrode3DModel.d4[i].Y + " " + Sonotrode3DModel.d4[i].Z);
                stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[i - 1].X + " " + Sonotrode3DModel.d4[i - 1].Y + " " + Sonotrode3DModel.d4[i - 1].Z);
                stlLines.AppendLine("endloop");
                stlLines.AppendLine("endfacet");
            }

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[3].X + " " + Sonotrode3DModel.d1[3].Y + " " + Sonotrode3DModel.d1[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[0].X + " " + Sonotrode3DModel.d1[0].Y + " " + Sonotrode3DModel.d1[0].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[0].X + " " + Sonotrode3DModel.d2[0].Y + " " + Sonotrode3DModel.d2[0].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d1[3].X + " " + Sonotrode3DModel.d1[3].Y + " " + Sonotrode3DModel.d1[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[3].X + " " + Sonotrode3DModel.d2[3].Y + " " + Sonotrode3DModel.d2[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[0].X + " " + Sonotrode3DModel.d2[0].Y + " " + Sonotrode3DModel.d2[0].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[3].X + " " + Sonotrode3DModel.d2[3].Y + " " + Sonotrode3DModel.d2[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[0].X + " " + Sonotrode3DModel.d2[0].Y + " " + Sonotrode3DModel.d2[0].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[0].X + " " + Sonotrode3DModel.d3[0].Y + " " + Sonotrode3DModel.d3[0].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d2[3].X + " " + Sonotrode3DModel.d2[3].Y + " " + Sonotrode3DModel.d2[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[3].X + " " + Sonotrode3DModel.d3[3].Y + " " + Sonotrode3DModel.d3[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[0].X + " " + Sonotrode3DModel.d3[0].Y + " " + Sonotrode3DModel.d3[0].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[3].X + " " + Sonotrode3DModel.d3[3].Y + " " + Sonotrode3DModel.d3[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[0].X + " " + Sonotrode3DModel.d3[0].Y + " " + Sonotrode3DModel.d3[0].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[0].X + " " + Sonotrode3DModel.d4[0].Y + " " + Sonotrode3DModel.d4[0].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d3[3].X + " " + Sonotrode3DModel.d3[3].Y + " " + Sonotrode3DModel.d3[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[3].X + " " + Sonotrode3DModel.d4[3].Y + " " + Sonotrode3DModel.d4[3].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[0].X + " " + Sonotrode3DModel.d4[0].Y + " " + Sonotrode3DModel.d4[0].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[0].X + " " + Sonotrode3DModel.d4[0].Y + " " + Sonotrode3DModel.d4[0].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[1].X + " " + Sonotrode3DModel.d4[1].Y + " " + Sonotrode3DModel.d4[1].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[2].X + " " + Sonotrode3DModel.d4[2].Y + " " + Sonotrode3DModel.d4[2].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");

            stlLines.AppendLine("facet normal 0 0 0");
            stlLines.AppendLine("outer loop");
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[0].X + " " + Sonotrode3DModel.d4[0].Y + " " + Sonotrode3DModel.d4[0].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[2].X + " " + Sonotrode3DModel.d4[2].Y + " " + Sonotrode3DModel.d4[2].Z);
            stlLines.AppendLine("vertex " + Sonotrode3DModel.d4[3].X + " " + Sonotrode3DModel.d4[3].Y + " " + Sonotrode3DModel.d4[3].Z);
            stlLines.AppendLine("endloop");
            stlLines.AppendLine("endfacet");



            stlLines.AppendLine("endsolid Pizdec");

            stlLines = stlLines.Replace(',', '.');

            return stlLines.ToString();
        }

        //class GraphicThings
        //{
        //    public Bitmap myBitmap = new();
        //    Graphics g = Graphics.FromImage(myBitmap);

        //    public void GrapgicThings(PictureBox pw)
        //    {

        //    }
        //}
    }
}