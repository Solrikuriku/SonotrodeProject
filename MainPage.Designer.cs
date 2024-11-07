namespace SonotrodeProject
{
    partial class SonotrodeGenerator
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SonotrodeGenerator));
            SonotrodeName = new Label();
            SonotrodeType = new ComboBox();
            Amplitude = new TextBox();
            WidthR = new TextBox();
            HeightR = new TextBox();
            ParametersGroup = new GroupBox();
            ProcessType = new GroupBox();
            StretchingCompressionWave = new RadioButton();
            StressWave = new RadioButton();
            DiameterName = new Label();
            HeightName = new Label();
            Diameter = new TextBox();
            WidthName = new Label();
            AlSound = new NumericUpDown();
            SpeedOfSoundName = new Label();
            AmplitudeName = new Label();
            UpdateData = new Button();
            ProcessWave = new PictureBox();
            SonotrodeParameters = new GroupBox();
            SonotrodeDescription = new Label();
            SaveSonotrodeInfo = new Button();
            SonotrodeMenu = new TabControl();
            Calculate = new TabPage();
            MaterialsInfo = new GroupBox();
            MaterialDescription = new Label();
            Materials = new TabPage();
            MaterialsTable = new DataGridView();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            amplitudeStartDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            amplitudeEndDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            mainMaterialsBindingSource6 = new BindingSource(components);
            AddInfo = new GroupBox();
            rndm = new Label();
            RangeAmplitude = new Label();
            MName = new Label();
            MaterialName = new TextBox();
            mainMaterialsBindingSource3 = new BindingSource(components);
            AddMaterial = new Button();
            AmplitudeS = new TextBox();
            mainMaterialsBindingSource4 = new BindingSource(components);
            AmplitudeE = new TextBox();
            mainMaterialsBindingSource5 = new BindingSource(components);
            SonotrodeModel = new TabPage();
            Info3D = new Label();
            SaveToSTL = new Button();
            SaveToOBJ = new Button();
            OGLModel = new SharpGL.OpenGLControl();
            mainMaterialsBindingSource = new BindingSource(components);
            mainMaterialsBindingSource1 = new BindingSource(components);
            mainMaterialsBindingSource2 = new BindingSource(components);
            MenuMaterials = new ContextMenuStrip(components);
            DeleteMaterials = new ToolStripMenuItem();
            SaveSonotrode = new SaveFileDialog();
            OpenSonotrode = new OpenFileDialog();
            ModelManipulation = new System.Windows.Forms.Timer(components);
            SaveOBJ = new SaveFileDialog();
            SaveSTL = new SaveFileDialog();
            ParametersGroup.SuspendLayout();
            ProcessType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AlSound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ProcessWave).BeginInit();
            SonotrodeParameters.SuspendLayout();
            SonotrodeMenu.SuspendLayout();
            Calculate.SuspendLayout();
            MaterialsInfo.SuspendLayout();
            Materials.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaterialsTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource6).BeginInit();
            AddInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource5).BeginInit();
            SonotrodeModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OGLModel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource2).BeginInit();
            MenuMaterials.SuspendLayout();
            SuspendLayout();
            // 
            // SonotrodeName
            // 
            SonotrodeName.AutoSize = true;
            SonotrodeName.Location = new Point(10, 19);
            SonotrodeName.Name = "SonotrodeName";
            SonotrodeName.Size = new Size(82, 20);
            SonotrodeName.TabIndex = 0;
            SonotrodeName.Text = "Sonotrode:";
            // 
            // SonotrodeType
            // 
            SonotrodeType.FormattingEnabled = true;
            SonotrodeType.Items.AddRange(new object[] { "Stepwise Circular", "Conical Circular", "Conical Rectangle" });
            SonotrodeType.Location = new Point(98, 16);
            SonotrodeType.Name = "SonotrodeType";
            SonotrodeType.Size = new Size(151, 28);
            SonotrodeType.TabIndex = 1;
            SonotrodeType.SelectedIndexChanged += SonotrodeType_SelectedIndexChanged;
            // 
            // Amplitude
            // 
            Amplitude.Location = new Point(98, 59);
            Amplitude.Name = "Amplitude";
            Amplitude.Size = new Size(151, 27);
            Amplitude.TabIndex = 2;
            Amplitude.TextChanged += Amplitude_TextChanged;
            Amplitude.KeyPress += Amplitude_KeyPress;
            // 
            // WidthR
            // 
            WidthR.Location = new Point(290, 26);
            WidthR.Name = "WidthR";
            WidthR.Size = new Size(125, 27);
            WidthR.TabIndex = 3;
            // 
            // HeightR
            // 
            HeightR.Location = new Point(290, 60);
            HeightR.Name = "HeightR";
            HeightR.Size = new Size(125, 27);
            HeightR.TabIndex = 4;
            // 
            // ParametersGroup
            // 
            ParametersGroup.Controls.Add(ProcessType);
            ParametersGroup.Controls.Add(DiameterName);
            ParametersGroup.Controls.Add(HeightName);
            ParametersGroup.Controls.Add(Diameter);
            ParametersGroup.Controls.Add(WidthName);
            ParametersGroup.Controls.Add(HeightR);
            ParametersGroup.Controls.Add(WidthR);
            ParametersGroup.Controls.Add(AlSound);
            ParametersGroup.Controls.Add(SpeedOfSoundName);
            ParametersGroup.Location = new Point(266, 9);
            ParametersGroup.Name = "ParametersGroup";
            ParametersGroup.Size = new Size(809, 120);
            ParametersGroup.TabIndex = 5;
            ParametersGroup.TabStop = false;
            ParametersGroup.Text = "Parameters";
            // 
            // ProcessType
            // 
            ProcessType.Controls.Add(StretchingCompressionWave);
            ProcessType.Controls.Add(StressWave);
            ProcessType.Location = new Point(439, 61);
            ProcessType.Name = "ProcessType";
            ProcessType.Size = new Size(364, 53);
            ProcessType.TabIndex = 19;
            ProcessType.TabStop = false;
            ProcessType.Text = "Process";
            // 
            // StretchingCompressionWave
            // 
            StretchingCompressionWave.AutoSize = true;
            StretchingCompressionWave.Location = new Point(104, 23);
            StretchingCompressionWave.Name = "StretchingCompressionWave";
            StretchingCompressionWave.Size = new Size(189, 24);
            StretchingCompressionWave.TabIndex = 1;
            StretchingCompressionWave.TabStop = true;
            StretchingCompressionWave.Text = "Stretching-Compression";
            StretchingCompressionWave.UseVisualStyleBackColor = true;
            StretchingCompressionWave.Click += StretchingCompressionWave_Click;
            // 
            // StressWave
            // 
            StressWave.AutoSize = true;
            StressWave.Location = new Point(14, 23);
            StressWave.Name = "StressWave";
            StressWave.Size = new Size(68, 24);
            StressWave.TabIndex = 0;
            StressWave.TabStop = true;
            StressWave.Text = "Stress";
            StressWave.UseVisualStyleBackColor = true;
            StressWave.CheckedChanged += StressWave_CheckedChanged;
            StressWave.Click += StressWave_Click;
            // 
            // DiameterName
            // 
            DiameterName.AutoSize = true;
            DiameterName.Location = new Point(6, 29);
            DiameterName.Name = "DiameterName";
            DiameterName.Size = new Size(74, 20);
            DiameterName.TabIndex = 6;
            DiameterName.Text = "Diameter:";
            // 
            // HeightName
            // 
            HeightName.AutoSize = true;
            HeightName.Location = new Point(227, 63);
            HeightName.Name = "HeightName";
            HeightName.Size = new Size(57, 20);
            HeightName.TabIndex = 8;
            HeightName.Text = "Height:";
            // 
            // Diameter
            // 
            Diameter.Location = new Point(86, 26);
            Diameter.Name = "Diameter";
            Diameter.Size = new Size(125, 27);
            Diameter.TabIndex = 5;
            Diameter.TextChanged += Diameter_TextChanged;
            Diameter.KeyPress += Diameter_KeyPress;
            // 
            // WidthName
            // 
            WidthName.AutoSize = true;
            WidthName.Location = new Point(232, 29);
            WidthName.Name = "WidthName";
            WidthName.Size = new Size(52, 20);
            WidthName.TabIndex = 7;
            WidthName.Text = "Width:";
            // 
            // AlSound
            // 
            AlSound.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            AlSound.Location = new Point(565, 26);
            AlSound.Maximum = new decimal(new int[] { 5300, 0, 0, 0 });
            AlSound.Minimum = new decimal(new int[] { 4900, 0, 0, 0 });
            AlSound.Name = "AlSound";
            AlSound.Size = new Size(150, 27);
            AlSound.TabIndex = 18;
            AlSound.TextAlign = HorizontalAlignment.Center;
            AlSound.Value = new decimal(new int[] { 4900, 0, 0, 0 });
            // 
            // SpeedOfSoundName
            // 
            SpeedOfSoundName.AutoSize = true;
            SpeedOfSoundName.Location = new Point(439, 29);
            SpeedOfSoundName.Name = "SpeedOfSoundName";
            SpeedOfSoundName.Size = new Size(120, 20);
            SpeedOfSoundName.TabIndex = 12;
            SpeedOfSoundName.Text = "Speed Of Sound:";
            // 
            // AmplitudeName
            // 
            AmplitudeName.AutoSize = true;
            AmplitudeName.Location = new Point(10, 62);
            AmplitudeName.Name = "AmplitudeName";
            AmplitudeName.Size = new Size(82, 20);
            AmplitudeName.TabIndex = 9;
            AmplitudeName.Text = "Amplitude:";
            // 
            // UpdateData
            // 
            UpdateData.Location = new Point(10, 100);
            UpdateData.Name = "UpdateData";
            UpdateData.Size = new Size(110, 29);
            UpdateData.TabIndex = 10;
            UpdateData.Text = "Update";
            UpdateData.UseVisualStyleBackColor = true;
            UpdateData.Click += UpdateData_Click;
            // 
            // ProcessWave
            // 
            ProcessWave.Location = new Point(266, 135);
            ProcessWave.Name = "ProcessWave";
            ProcessWave.Size = new Size(502, 526);
            ProcessWave.TabIndex = 15;
            ProcessWave.TabStop = false;
            ProcessWave.Click += ProcessWave_Click;
            ProcessWave.Paint += ProcessWave_Paint;
            ProcessWave.DoubleClick += ProcessWave_DoubleClick;
            ProcessWave.MouseUp += ProcessWave_MouseUp;
            ProcessWave.PreviewKeyDown += ProcessWave_PreviewKeyDown;
            // 
            // SonotrodeParameters
            // 
            SonotrodeParameters.Controls.Add(SonotrodeDescription);
            SonotrodeParameters.Location = new Point(10, 135);
            SonotrodeParameters.Name = "SonotrodeParameters";
            SonotrodeParameters.Size = new Size(250, 526);
            SonotrodeParameters.TabIndex = 19;
            SonotrodeParameters.TabStop = false;
            SonotrodeParameters.Text = "Info";
            // 
            // SonotrodeDescription
            // 
            SonotrodeDescription.AutoSize = true;
            SonotrodeDescription.Location = new Point(16, 38);
            SonotrodeDescription.Name = "SonotrodeDescription";
            SonotrodeDescription.Size = new Size(18, 20);
            SonotrodeDescription.TabIndex = 0;
            SonotrodeDescription.Text = "...";
            // 
            // SaveSonotrodeInfo
            // 
            SaveSonotrodeInfo.Location = new Point(139, 100);
            SaveSonotrodeInfo.Name = "SaveSonotrodeInfo";
            SaveSonotrodeInfo.Size = new Size(110, 29);
            SaveSonotrodeInfo.TabIndex = 22;
            SaveSonotrodeInfo.Text = "Save";
            SaveSonotrodeInfo.UseVisualStyleBackColor = true;
            SaveSonotrodeInfo.Click += button2_Click;
            // 
            // SonotrodeMenu
            // 
            SonotrodeMenu.Controls.Add(Calculate);
            SonotrodeMenu.Controls.Add(Materials);
            SonotrodeMenu.Controls.Add(SonotrodeModel);
            SonotrodeMenu.Location = new Point(12, 12);
            SonotrodeMenu.Name = "SonotrodeMenu";
            SonotrodeMenu.SelectedIndex = 0;
            SonotrodeMenu.Size = new Size(1099, 710);
            SonotrodeMenu.TabIndex = 23;
            // 
            // Calculate
            // 
            Calculate.Controls.Add(MaterialsInfo);
            Calculate.Controls.Add(UpdateData);
            Calculate.Controls.Add(SaveSonotrodeInfo);
            Calculate.Controls.Add(Amplitude);
            Calculate.Controls.Add(SonotrodeName);
            Calculate.Controls.Add(SonotrodeType);
            Calculate.Controls.Add(SonotrodeParameters);
            Calculate.Controls.Add(AmplitudeName);
            Calculate.Controls.Add(ProcessWave);
            Calculate.Controls.Add(ParametersGroup);
            Calculate.Location = new Point(4, 29);
            Calculate.Name = "Calculate";
            Calculate.Padding = new Padding(3);
            Calculate.Size = new Size(1091, 677);
            Calculate.TabIndex = 0;
            Calculate.Text = "Calculate";
            Calculate.UseVisualStyleBackColor = true;
            // 
            // MaterialsInfo
            // 
            MaterialsInfo.Controls.Add(MaterialDescription);
            MaterialsInfo.Location = new Point(774, 135);
            MaterialsInfo.Name = "MaterialsInfo";
            MaterialsInfo.Size = new Size(301, 526);
            MaterialsInfo.TabIndex = 23;
            MaterialsInfo.TabStop = false;
            MaterialsInfo.Text = "Materials";
            // 
            // MaterialDescription
            // 
            MaterialDescription.AutoSize = true;
            MaterialDescription.Location = new Point(19, 36);
            MaterialDescription.Name = "MaterialDescription";
            MaterialDescription.Size = new Size(18, 20);
            MaterialDescription.TabIndex = 1;
            MaterialDescription.Text = "...";
            // 
            // Materials
            // 
            Materials.Controls.Add(MaterialsTable);
            Materials.Controls.Add(AddInfo);
            Materials.Location = new Point(4, 29);
            Materials.Name = "Materials";
            Materials.Padding = new Padding(3);
            Materials.Size = new Size(1091, 677);
            Materials.TabIndex = 1;
            Materials.Text = "Materials";
            Materials.UseVisualStyleBackColor = true;
            // 
            // MaterialsTable
            // 
            MaterialsTable.AutoGenerateColumns = false;
            MaterialsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MaterialsTable.Columns.AddRange(new DataGridViewColumn[] { nameDataGridViewTextBoxColumn, amplitudeStartDataGridViewTextBoxColumn, amplitudeEndDataGridViewTextBoxColumn });
            MaterialsTable.DataSource = mainMaterialsBindingSource6;
            MaterialsTable.Location = new Point(19, 17);
            MaterialsTable.Name = "MaterialsTable";
            MaterialsTable.RowHeadersVisible = false;
            MaterialsTable.RowHeadersWidth = 51;
            MaterialsTable.RowTemplate.Height = 29;
            MaterialsTable.Size = new Size(567, 645);
            MaterialsTable.TabIndex = 6;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.Width = 125;
            // 
            // amplitudeStartDataGridViewTextBoxColumn
            // 
            amplitudeStartDataGridViewTextBoxColumn.DataPropertyName = "AmplitudeStart";
            amplitudeStartDataGridViewTextBoxColumn.HeaderText = "AmplitudeStart";
            amplitudeStartDataGridViewTextBoxColumn.MinimumWidth = 6;
            amplitudeStartDataGridViewTextBoxColumn.Name = "amplitudeStartDataGridViewTextBoxColumn";
            amplitudeStartDataGridViewTextBoxColumn.Width = 125;
            // 
            // amplitudeEndDataGridViewTextBoxColumn
            // 
            amplitudeEndDataGridViewTextBoxColumn.DataPropertyName = "AmplitudeEnd";
            amplitudeEndDataGridViewTextBoxColumn.HeaderText = "AmplitudeEnd";
            amplitudeEndDataGridViewTextBoxColumn.MinimumWidth = 6;
            amplitudeEndDataGridViewTextBoxColumn.Name = "amplitudeEndDataGridViewTextBoxColumn";
            amplitudeEndDataGridViewTextBoxColumn.Width = 125;
            // 
            // mainMaterialsBindingSource6
            // 
            mainMaterialsBindingSource6.DataSource = typeof(MainMaterials);
            // 
            // AddInfo
            // 
            AddInfo.Controls.Add(rndm);
            AddInfo.Controls.Add(RangeAmplitude);
            AddInfo.Controls.Add(MName);
            AddInfo.Controls.Add(MaterialName);
            AddInfo.Controls.Add(AddMaterial);
            AddInfo.Controls.Add(AmplitudeS);
            AddInfo.Controls.Add(AmplitudeE);
            AddInfo.Location = new Point(632, 17);
            AddInfo.Name = "AddInfo";
            AddInfo.Size = new Size(424, 194);
            AddInfo.TabIndex = 5;
            AddInfo.TabStop = false;
            AddInfo.Text = "Add Info";
            // 
            // rndm
            // 
            rndm.AutoSize = true;
            rndm.Location = new Point(140, 100);
            rndm.Name = "rndm";
            rndm.Size = new Size(15, 20);
            rndm.TabIndex = 7;
            rndm.Text = "-";
            // 
            // RangeAmplitude
            // 
            RangeAmplitude.AutoSize = true;
            RangeAmplitude.Location = new Point(22, 97);
            RangeAmplitude.Name = "RangeAmplitude";
            RangeAmplitude.Size = new Size(54, 20);
            RangeAmplitude.TabIndex = 6;
            RangeAmplitude.Text = "Range:";
            // 
            // MName
            // 
            MName.AutoSize = true;
            MName.Location = new Point(22, 45);
            MName.Name = "MName";
            MName.Size = new Size(52, 20);
            MName.TabIndex = 5;
            MName.Text = "Name:";
            // 
            // MaterialName
            // 
            MaterialName.DataBindings.Add(new Binding("Text", mainMaterialsBindingSource3, "Name", true));
            MaterialName.Location = new Point(80, 45);
            MaterialName.Name = "MaterialName";
            MaterialName.Size = new Size(319, 27);
            MaterialName.TabIndex = 1;
            // 
            // mainMaterialsBindingSource3
            // 
            mainMaterialsBindingSource3.DataSource = typeof(MainMaterials);
            // 
            // AddMaterial
            // 
            AddMaterial.Location = new Point(82, 143);
            AddMaterial.Name = "AddMaterial";
            AddMaterial.Size = new Size(94, 29);
            AddMaterial.TabIndex = 4;
            AddMaterial.Text = "Add";
            AddMaterial.UseVisualStyleBackColor = true;
            AddMaterial.Click += AddMaterial_Click;
            // 
            // AmplitudeS
            // 
            AmplitudeS.DataBindings.Add(new Binding("Text", mainMaterialsBindingSource4, "AmplitudeStart", true));
            AmplitudeS.Location = new Point(82, 97);
            AmplitudeS.Name = "AmplitudeS";
            AmplitudeS.Size = new Size(52, 27);
            AmplitudeS.TabIndex = 2;
            AmplitudeS.KeyPress += AmplitudeS_KeyPress;
            // 
            // mainMaterialsBindingSource4
            // 
            mainMaterialsBindingSource4.DataSource = typeof(MainMaterials);
            // 
            // AmplitudeE
            // 
            AmplitudeE.DataBindings.Add(new Binding("Text", mainMaterialsBindingSource5, "AmplitudeEnd", true));
            AmplitudeE.Location = new Point(161, 97);
            AmplitudeE.Name = "AmplitudeE";
            AmplitudeE.Size = new Size(52, 27);
            AmplitudeE.TabIndex = 3;
            AmplitudeE.KeyPress += AmplitudeE_KeyPress;
            // 
            // mainMaterialsBindingSource5
            // 
            mainMaterialsBindingSource5.DataSource = typeof(MainMaterials);
            // 
            // SonotrodeModel
            // 
            SonotrodeModel.Controls.Add(Info3D);
            SonotrodeModel.Controls.Add(SaveToSTL);
            SonotrodeModel.Controls.Add(SaveToOBJ);
            SonotrodeModel.Controls.Add(OGLModel);
            SonotrodeModel.Location = new Point(4, 29);
            SonotrodeModel.Name = "SonotrodeModel";
            SonotrodeModel.Padding = new Padding(3);
            SonotrodeModel.Size = new Size(1091, 677);
            SonotrodeModel.TabIndex = 2;
            SonotrodeModel.Text = "3D Model";
            SonotrodeModel.UseVisualStyleBackColor = true;
            // 
            // Info3D
            // 
            Info3D.AutoSize = true;
            Info3D.Location = new Point(948, 17);
            Info3D.Name = "Info3D";
            Info3D.Size = new Size(12, 20);
            Info3D.TabIndex = 3;
            Info3D.Text = ".";
            // 
            // SaveToSTL
            // 
            SaveToSTL.Location = new Point(964, 124);
            SaveToSTL.Name = "SaveToSTL";
            SaveToSTL.Size = new Size(94, 29);
            SaveToSTL.TabIndex = 2;
            SaveToSTL.Text = "Save .stl";
            SaveToSTL.UseVisualStyleBackColor = true;
            SaveToSTL.Click += SaveToSTL_Click;
            // 
            // SaveToOBJ
            // 
            SaveToOBJ.Location = new Point(964, 171);
            SaveToOBJ.Name = "SaveToOBJ";
            SaveToOBJ.Size = new Size(94, 29);
            SaveToOBJ.TabIndex = 1;
            SaveToOBJ.Text = "Save .obj";
            SaveToOBJ.UseVisualStyleBackColor = true;
            SaveToOBJ.Click += SaveToOBJ_Click;
            // 
            // OGLModel
            // 
            OGLModel.DrawFPS = false;
            OGLModel.Location = new Point(19, 17);
            OGLModel.Margin = new Padding(4, 5, 4, 5);
            OGLModel.Name = "OGLModel";
            OGLModel.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            OGLModel.RenderContextType = SharpGL.RenderContextType.DIBSection;
            OGLModel.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            OGLModel.Size = new Size(910, 642);
            OGLModel.TabIndex = 0;
            OGLModel.OpenGLDraw += OGLModel_OpenGLDraw;
            OGLModel.Click += OGLModel_Click;
            OGLModel.KeyDown += OGLModel_KeyDown;
            OGLModel.KeyUp += OGLModel_KeyUp;
            // 
            // MenuMaterials
            // 
            MenuMaterials.ImageScalingSize = new Size(20, 20);
            MenuMaterials.Items.AddRange(new ToolStripItem[] { DeleteMaterials });
            MenuMaterials.Name = "MenuMaterials";
            MenuMaterials.Size = new Size(123, 28);
            // 
            // DeleteMaterials
            // 
            DeleteMaterials.Name = "DeleteMaterials";
            DeleteMaterials.Size = new Size(122, 24);
            DeleteMaterials.Text = "&Delete";
            DeleteMaterials.Click += DeleteMaterials_Click;
            // 
            // OpenSonotrode
            // 
            OpenSonotrode.FileName = "openFileDialog1";
            // 
            // ModelManipulation
            // 
            ModelManipulation.Tick += ModelManipulation_Tick;
            // 
            // SonotrodeGenerator
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1116, 731);
            Controls.Add(SonotrodeMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SonotrodeGenerator";
            Text = "Sonotrode Project";
            FormClosing += SonotrodeGenerator_FormClosing;
            Load += SonotrodeGenerator_Load;
            KeyUp += SonotrodeGenerator_KeyUp;
            ParametersGroup.ResumeLayout(false);
            ParametersGroup.PerformLayout();
            ProcessType.ResumeLayout(false);
            ProcessType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AlSound).EndInit();
            ((System.ComponentModel.ISupportInitialize)ProcessWave).EndInit();
            SonotrodeParameters.ResumeLayout(false);
            SonotrodeParameters.PerformLayout();
            SonotrodeMenu.ResumeLayout(false);
            Calculate.ResumeLayout(false);
            Calculate.PerformLayout();
            MaterialsInfo.ResumeLayout(false);
            MaterialsInfo.PerformLayout();
            Materials.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MaterialsTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource6).EndInit();
            AddInfo.ResumeLayout(false);
            AddInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource3).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource4).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource5).EndInit();
            SonotrodeModel.ResumeLayout(false);
            SonotrodeModel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OGLModel).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainMaterialsBindingSource2).EndInit();
            MenuMaterials.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label SonotrodeName;
        private ComboBox SonotrodeType;
        private TextBox Amplitude;
        private TextBox WidthR;
        private TextBox HeightR;
        private GroupBox ParametersGroup;
        private Label DiameterName;
        private TextBox Diameter;
        private Label HeightName;
        private Label WidthName;
        private Label AmplitudeName;
        private Button UpdateData;
        private PictureBox ProcessWave;
        private NumericUpDown AlSound;
        private Label SpeedOfSoundName;
        private GroupBox SonotrodeParameters;
        private Label SonotrodeDescription;
        private Button button1;
        private Button SaveSonotrodeInfo;
        private TabControl SonotrodeMenu;
        private TabPage Calculate;
        private TabPage Materials;
        private TabPage SonotrodeModel;
        private ContextMenuStrip MenuMaterials;
        private ToolStripMenuItem DeleteMaterials;
        private Button AddMaterial;
        private TextBox AmplitudeE;
        private TextBox AmplitudeS;
        private TextBox MaterialName;
        private BindingSource mainMaterialsBindingSource;
        private BindingSource mainMaterialsBindingSource2;
        private BindingSource mainMaterialsBindingSource1;
        private SaveFileDialog SaveSonotrode;
        private OpenFileDialog OpenSonotrode;
        private GroupBox MaterialsInfo;
        private Label MaterialDescription;
        private SharpGL.OpenGLControl OGLModel;
        private System.Windows.Forms.Timer ModelManipulation;
        private Button SaveToOBJ;
        private SaveFileDialog SaveOBJ;
        private GroupBox AddInfo;
        private Label rndm;
        private Label RangeAmplitude;
        private Label MName;
        private Button SaveToSTL;
        private SaveFileDialog SaveSTL;
        private Label Info3D;
        private GroupBox ProcessType;
        private RadioButton StressWave;
        private RadioButton StretchingCompressionWave;
        private BindingSource mainMaterialsBindingSource3;
        private BindingSource mainMaterialsBindingSource4;
        private BindingSource mainMaterialsBindingSource5;
        private DataGridView MaterialsTable;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn amplitudeStartDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn amplitudeEndDataGridViewTextBoxColumn;
        private BindingSource mainMaterialsBindingSource6;
    }
}