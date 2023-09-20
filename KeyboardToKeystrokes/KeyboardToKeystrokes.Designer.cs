

namespace KeyboardToKeystrokes
{
    partial class KeyboardToKeystrokes
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
            _inputDevice?.Dispose();
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startListeningButton = new Button();
            stopListeningButton = new Button();
            inputLogTextBox = new TextBox();
            inputLogGroup = new GroupBox();
            openMappingManagerButton = new Button();
            inputDeviceCombobox = new ComboBox();
            inputDeviceSelectorLabel = new Label();
            listeningGroupBox = new GroupBox();
            selectInputDeviceButton = new Button();
            refreshInputDeviceListButton = new Button();
            inputLogGroup.SuspendLayout();
            listeningGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // startListeningButton
            // 
            startListeningButton.Location = new Point(6, 22);
            startListeningButton.Name = "startListeningButton";
            startListeningButton.Size = new Size(118, 23);
            startListeningButton.TabIndex = 0;
            startListeningButton.Text = "Start Listening";
            startListeningButton.UseVisualStyleBackColor = true;
            startListeningButton.Click += startListeningButton_Click;
            // 
            // stopListeningButton
            // 
            stopListeningButton.Enabled = false;
            stopListeningButton.Location = new Point(198, 22);
            stopListeningButton.Name = "stopListeningButton";
            stopListeningButton.Size = new Size(118, 23);
            stopListeningButton.TabIndex = 1;
            stopListeningButton.Text = "Stop Listening";
            stopListeningButton.UseVisualStyleBackColor = true;
            stopListeningButton.Click += stopListeningButton_Click;
            // 
            // inputLogTextBox
            // 
            inputLogTextBox.Location = new Point(6, 22);
            inputLogTextBox.MaxLength = 100;
            inputLogTextBox.Multiline = true;
            inputLogTextBox.Name = "inputLogTextBox";
            inputLogTextBox.ScrollBars = ScrollBars.Vertical;
            inputLogTextBox.Size = new Size(304, 311);
            inputLogTextBox.TabIndex = 2;
            inputLogTextBox.TabStop = false;
            inputLogTextBox.WordWrap = false;
            // 
            // inputLogGroup
            // 
            inputLogGroup.Controls.Add(inputLogTextBox);
            inputLogGroup.Location = new Point(6, 51);
            inputLogGroup.Name = "inputLogGroup";
            inputLogGroup.Size = new Size(316, 347);
            inputLogGroup.TabIndex = 3;
            inputLogGroup.TabStop = false;
            inputLogGroup.Text = "Input Log";
            // 
            // openMappingManagerButton
            // 
            openMappingManagerButton.Location = new Point(6, 404);
            openMappingManagerButton.Name = "openMappingManagerButton";
            openMappingManagerButton.Size = new Size(161, 23);
            openMappingManagerButton.TabIndex = 4;
            openMappingManagerButton.Text = "Open Mapping Manager";
            openMappingManagerButton.UseVisualStyleBackColor = true;
            openMappingManagerButton.Click += openMappingManagerButton_Click;
            // 
            // inputDeviceCombobox
            // 
            inputDeviceCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            inputDeviceCombobox.FormattingEnabled = true;
            inputDeviceCombobox.Location = new Point(94, 13);
            inputDeviceCombobox.Name = "inputDeviceCombobox";
            inputDeviceCombobox.Size = new Size(246, 23);
            inputDeviceCombobox.TabIndex = 5;
            // 
            // inputDeviceSelectorLabel
            // 
            inputDeviceSelectorLabel.AutoSize = true;
            inputDeviceSelectorLabel.Location = new Point(12, 16);
            inputDeviceSelectorLabel.Name = "inputDeviceSelectorLabel";
            inputDeviceSelectorLabel.Size = new Size(76, 15);
            inputDeviceSelectorLabel.TabIndex = 6;
            inputDeviceSelectorLabel.Text = "Input Device:";
            // 
            // listeningGroupBox
            // 
            listeningGroupBox.Controls.Add(startListeningButton);
            listeningGroupBox.Controls.Add(stopListeningButton);
            listeningGroupBox.Controls.Add(inputLogGroup);
            listeningGroupBox.Controls.Add(openMappingManagerButton);
            listeningGroupBox.Enabled = false;
            listeningGroupBox.Location = new Point(12, 120);
            listeningGroupBox.Name = "listeningGroupBox";
            listeningGroupBox.Size = new Size(328, 438);
            listeningGroupBox.TabIndex = 7;
            listeningGroupBox.TabStop = false;
            listeningGroupBox.Text = "Listening actions";
            // 
            // selectInputDeviceButton
            // 
            selectInputDeviceButton.Enabled = false;
            selectInputDeviceButton.Location = new Point(223, 42);
            selectInputDeviceButton.Name = "selectInputDeviceButton";
            selectInputDeviceButton.Size = new Size(118, 23);
            selectInputDeviceButton.TabIndex = 8;
            selectInputDeviceButton.Text = "Use this device";
            selectInputDeviceButton.UseVisualStyleBackColor = true;
            selectInputDeviceButton.Click += selectInputDeviceButton_Click;
            // 
            // refreshInputDeviceListButton
            // 
            refreshInputDeviceListButton.Location = new Point(12, 42);
            refreshInputDeviceListButton.Name = "refreshInputDeviceListButton";
            refreshInputDeviceListButton.Size = new Size(118, 23);
            refreshInputDeviceListButton.TabIndex = 9;
            refreshInputDeviceListButton.Text = "Refresh device list";
            refreshInputDeviceListButton.UseVisualStyleBackColor = true;
            refreshInputDeviceListButton.Click += refreshInputDeviceListButton_Click;
            // 
            // KeyboardToKeystrokes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 570);
            Controls.Add(refreshInputDeviceListButton);
            Controls.Add(selectInputDeviceButton);
            Controls.Add(listeningGroupBox);
            Controls.Add(inputDeviceSelectorLabel);
            Controls.Add(inputDeviceCombobox);
            Name = "KeyboardToKeystrokes";
            Text = "KeyboardToKeystrokes";
            inputLogGroup.ResumeLayout(false);
            inputLogGroup.PerformLayout();
            listeningGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startListeningButton;
        private Button stopListeningButton;
        private TextBox inputLogTextBox;
        private GroupBox inputLogGroup;
        private Button openMappingManagerButton;
        private ComboBox inputDeviceCombobox;
        private Label inputDeviceSelectorLabel;
        private GroupBox listeningGroupBox;
        private Button selectInputDeviceButton;
        private Button refreshInputDeviceListButton;
    }
}