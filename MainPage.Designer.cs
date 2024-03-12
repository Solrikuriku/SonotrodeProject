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
            WidthR = new TextBox();
            HeightR = new TextBox();
            ParametersGroup = new GroupBox();
            DiameterName = new Label();
            HeightName = new Label();
            Diameter = new TextBox();
            WidthName = new Label();
            AmplitudeName = new Label();
            UpdateData = new Button();
            ProcessWave = new PictureBox();
            MakeWave = new CheckBox();
            AlSound = new NumericUpDown();
            SpeedOfSoundName = new Label();
            SonotrodeParameters = new GroupBox();
            SonotrodeDescription = new Label();
            AnimationWave = new CheckBox();
            ParametersGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ProcessWave).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AlSound).BeginInit();
            SonotrodeParameters.SuspendLayout();
            SuspendLayout();
            // 
            // SonotrodeName
            // 
            SonotrodeName.AutoSize = true;
            SonotrodeName.Location = new Point(12, 18);
            SonotrodeName.Name = "SonotrodeName";
            SonotrodeName.Size = new Size(82, 20);
            SonotrodeName.TabIndex = 0;
            SonotrodeName.Text = "Sonotrode:";
            // 
            // SonotrodeType
            // 
            SonotrodeType.FormattingEnabled = true;
            SonotrodeType.Items.AddRange(new object[] { "Stepwise Circular", "Conical Circular", "Conical Rectangle" });
            SonotrodeType.Location = new Point(100, 15);
            SonotrodeType.Name = "SonotrodeType";
            SonotrodeType.Size = new Size(151, 28);
            SonotrodeType.TabIndex = 1;
            SonotrodeType.SelectedIndexChanged += SonotrodeType_SelectedIndexChanged;
            // 
            // Amplitude
            // 
            Amplitude.Location = new Point(100, 58);
            Amplitude.Name = "Amplitude";
            Amplitude.Size = new Size(151, 27);
            Amplitude.TabIndex = 2;
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
            ParametersGroup.Controls.Add(DiameterName);
            ParametersGroup.Controls.Add(HeightName);
            ParametersGroup.Controls.Add(Diameter);
            ParametersGroup.Controls.Add(WidthName);
            ParametersGroup.Controls.Add(HeightR);
            ParametersGroup.Controls.Add(WidthR);
            ParametersGroup.Location = new Point(289, 12);
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
            AmplitudeName.Location = new Point(12, 61);
            AmplitudeName.Name = "AmplitudeName";
            AmplitudeName.Size = new Size(82, 20);
            AmplitudeName.TabIndex = 9;
            AmplitudeName.Text = "Amplitude:";
            // 
            // UpdateData
            // 
            UpdateData.Location = new Point(132, 91);
            UpdateData.Name = "UpdateData";
            UpdateData.Size = new Size(94, 29);
            UpdateData.TabIndex = 10;
            UpdateData.Text = "Update";
            UpdateData.UseVisualStyleBackColor = true;
            UpdateData.Click += UpdateData_Click;
            // 
            // ProcessWave
            // 
            ProcessWave.Location = new Point(289, 153);
            ProcessWave.Name = "ProcessWave";
            ProcessWave.Size = new Size(465, 425);
            ProcessWave.TabIndex = 15;
            ProcessWave.TabStop = false;
            ProcessWave.Click += ProcessWave_Click;
            ProcessWave.Paint += ProcessWave_Paint;
            // 
            // MakeWave
            // 
            MakeWave.AutoSize = true;
            MakeWave.Location = new Point(597, 121);
            MakeWave.Name = "MakeWave";
            MakeWave.Size = new Size(107, 24);
            MakeWave.TabIndex = 16;
            MakeWave.Text = "Show Wave";
            MakeWave.UseVisualStyleBackColor = true;
            // 
            // AlSound
            // 
            AlSound.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            AlSound.Location = new Point(438, 120);
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
            SpeedOfSoundName.Location = new Point(289, 120);
            SpeedOfSoundName.Name = "SpeedOfSoundName";
            SpeedOfSoundName.Size = new Size(120, 20);
            SpeedOfSoundName.TabIndex = 12;
            SpeedOfSoundName.Text = "Speed Of Sound:";
            // 
            // SonotrodeParameters
            // 
            SonotrodeParameters.Controls.Add(SonotrodeDescription);
            SonotrodeParameters.Location = new Point(12, 142);
            SonotrodeParameters.Name = "SonotrodeParameters";
            SonotrodeParameters.Size = new Size(250, 436);
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
            // AnimationWave
            // 
            AnimationWave.AutoSize = true;
            AnimationWave.Location = new Point(710, 120);
            AnimationWave.Name = "AnimationWave";
            AnimationWave.Size = new Size(100, 24);
            AnimationWave.TabIndex = 20;
            AnimationWave.Text = "Animation";
            AnimationWave.UseVisualStyleBackColor = true;
            // 
            // SonotrodeGenerator
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 590);
            Controls.Add(AnimationWave);
            Controls.Add(SonotrodeParameters);
            Controls.Add(AlSound);
            Controls.Add(MakeWave);
            Controls.Add(ProcessWave);
            Controls.Add(SpeedOfSoundName);
            Controls.Add(UpdateData);
            Controls.Add(AmplitudeName);
            Controls.Add(ParametersGroup);
            Controls.Add(SonotrodeType);
            Controls.Add(SonotrodeName);
            Controls.Add(Amplitude);
            Name = "SonotrodeGenerator";
            Text = "Вменяемое название пока не придумано";
            FormClosing += SonotrodeGenerator_FormClosing;
            Load += SonotrodeGenerator_Load;
            ParametersGroup.ResumeLayout(false);
            ParametersGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ProcessWave).EndInit();
            ((System.ComponentModel.ISupportInitialize)AlSound).EndInit();
            SonotrodeParameters.ResumeLayout(false);
            SonotrodeParameters.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private CheckBox MakeWave;
        private NumericUpDown AlSound;
        private Label SpeedOfSoundName;
        private GroupBox SonotrodeParameters;
        private Label SonotrodeDescription;
        private CheckBox AnimationWave;
    }
}