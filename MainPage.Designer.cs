namespace SonotrodeProject
{
    partial class SonotrodeGenerator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            SonotrodeName = new Label();
            SonotrodeType = new ComboBox();
            Amplitude = new TextBox();
            Width = new TextBox();
            Height = new TextBox();
            ParametersGroup = new GroupBox();
            DiameterName = new Label();
            HeightName = new Label();
            Diameter = new TextBox();
            WidthName = new Label();
            AmplitudeName = new Label();
            UpdateData = new Button();
            AlSound = new TrackBar();
            SpeedOfSoundName = new Label();
            TestBoxForValues = new RichTextBox();
            SoundValues = new TextBox();
            ProcessWave = new PictureBox();
            MakeWave = new CheckBox();
            richTextBox1 = new RichTextBox();
            ParametersGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AlSound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ProcessWave).BeginInit();
            SuspendLayout();
            // 
            // SonotrodeName
            // 
            SonotrodeName.AutoSize = true;
            SonotrodeName.Location = new Point(12, 9);
            SonotrodeName.Name = "SonotrodeName";
            SonotrodeName.Size = new Size(82, 20);
            SonotrodeName.TabIndex = 0;
            SonotrodeName.Text = "Sonotrode:";
            // 
            // SonotrodeType
            // 
            SonotrodeType.FormattingEnabled = true;
            SonotrodeType.Items.AddRange(new object[] { "Stepwise Circular", "Conical Circular", "Conical Rectangle" });
            SonotrodeType.Location = new Point(100, 6);
            SonotrodeType.Name = "SonotrodeType";
            SonotrodeType.Size = new Size(151, 28);
            SonotrodeType.TabIndex = 1;
            SonotrodeType.SelectedIndexChanged += SonotrodeType_SelectedIndexChanged;
            // 
            // Amplitude
            // 
            Amplitude.Location = new Point(100, 49);
            Amplitude.Name = "Amplitude";
            Amplitude.Size = new Size(151, 27);
            Amplitude.TabIndex = 2;
            // 
            // Width
            // 
            Width.Location = new Point(290, 26);
            Width.Name = "Width";
            Width.Size = new Size(125, 27);
            Width.TabIndex = 3;
            // 
            // Height
            // 
            Height.Location = new Point(290, 60);
            Height.Name = "Height";
            Height.Size = new Size(125, 27);
            Height.TabIndex = 4;
            // 
            // ParametersGroup
            // 
            ParametersGroup.Controls.Add(DiameterName);
            ParametersGroup.Controls.Add(HeightName);
            ParametersGroup.Controls.Add(Diameter);
            ParametersGroup.Controls.Add(WidthName);
            ParametersGroup.Controls.Add(Height);
            ParametersGroup.Controls.Add(Width);
            ParametersGroup.Location = new Point(312, 6);
            ParametersGroup.Name = "ParametersGroup";
            ParametersGroup.Size = new Size(465, 100);
            ParametersGroup.TabIndex = 5;
            ParametersGroup.TabStop = false;
            ParametersGroup.Text = "Parameters";
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
            // AmplitudeName
            // 
            AmplitudeName.AutoSize = true;
            AmplitudeName.Location = new Point(12, 52);
            AmplitudeName.Name = "AmplitudeName";
            AmplitudeName.Size = new Size(82, 20);
            AmplitudeName.TabIndex = 9;
            AmplitudeName.Text = "Amplitude:";
            // 
            // UpdateData
            // 
            UpdateData.Location = new Point(132, 82);
            UpdateData.Name = "UpdateData";
            UpdateData.Size = new Size(94, 29);
            UpdateData.TabIndex = 10;
            UpdateData.Text = "Update";
            UpdateData.UseVisualStyleBackColor = true;
            UpdateData.Click += UpdateData_Click;
            // 
            // AlSound
            // 
            AlSound.Location = new Point(427, 122);
            AlSound.Maximum = 5300;
            AlSound.Minimum = 4900;
            AlSound.Name = "AlSound";
            AlSound.Size = new Size(169, 56);
            AlSound.TabIndex = 11;
            AlSound.TickFrequency = 50;
            AlSound.Value = 5100;
            AlSound.Scroll += AlSound_Scroll;
            // 
            // SpeedOfSoundName
            // 
            SpeedOfSoundName.AutoSize = true;
            SpeedOfSoundName.Location = new Point(312, 122);
            SpeedOfSoundName.Name = "SpeedOfSoundName";
            SpeedOfSoundName.Size = new Size(120, 20);
            SpeedOfSoundName.TabIndex = 12;
            SpeedOfSoundName.Text = "Speed Of Sound:";
            // 
            // TestBoxForValues
            // 
            TestBoxForValues.Location = new Point(12, 194);
            TestBoxForValues.Name = "TestBoxForValues";
            TestBoxForValues.Size = new Size(239, 442);
            TestBoxForValues.TabIndex = 13;
            TestBoxForValues.Text = "";
            // 
            // SoundValues
            // 
            SoundValues.Location = new Point(602, 122);
            SoundValues.Name = "SoundValues";
            SoundValues.Size = new Size(125, 27);
            SoundValues.TabIndex = 14;
            // 
            // ProcessWave
            // 
            ProcessWave.Location = new Point(544, 184);
            ProcessWave.Name = "ProcessWave";
            ProcessWave.Size = new Size(599, 452);
            ProcessWave.TabIndex = 15;
            ProcessWave.TabStop = false;
            ProcessWave.Paint += ProcessWave_Paint;
            // 
            // MakeWave
            // 
            MakeWave.AutoSize = true;
            MakeWave.Location = new Point(822, 144);
            MakeWave.Name = "MakeWave";
            MakeWave.Size = new Size(107, 24);
            MakeWave.TabIndex = 16;
            MakeWave.Text = "Show Wave";
            MakeWave.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(291, 190);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(219, 446);
            richTextBox1.TabIndex = 17;
            richTextBox1.Text = "";
            // 
            // SonotrodeGenerator
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 648);
            Controls.Add(richTextBox1);
            Controls.Add(MakeWave);
            Controls.Add(ProcessWave);
            Controls.Add(SoundValues);
            Controls.Add(TestBoxForValues);
            Controls.Add(SpeedOfSoundName);
            Controls.Add(AlSound);
            Controls.Add(UpdateData);
            Controls.Add(AmplitudeName);
            Controls.Add(ParametersGroup);
            Controls.Add(SonotrodeType);
            Controls.Add(SonotrodeName);
            Controls.Add(Amplitude);
            Name = "SonotrodeGenerator";
            Text = "Вменяемое название пока не придумано";
            ParametersGroup.ResumeLayout(false);
            ParametersGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AlSound).EndInit();
            ((System.ComponentModel.ISupportInitialize)ProcessWave).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label SonotrodeName;
        private ComboBox SonotrodeType;
        private TextBox Amplitude;
        private TextBox Width;
        private TextBox Height;
        private GroupBox ParametersGroup;
        private Label DiameterName;
        private TextBox Diameter;
        private Label HeightName;
        private Label WidthName;
        private Label AmplitudeName;
        private Button UpdateData;
        private TrackBar AlSound;
        private Label SpeedOfSoundName;
        private RichTextBox TestBoxForValues;
        private TextBox SoundValues;
        private PictureBox ProcessWave;
        private CheckBox MakeWave;
        private RichTextBox richTextBox1;
    }
}