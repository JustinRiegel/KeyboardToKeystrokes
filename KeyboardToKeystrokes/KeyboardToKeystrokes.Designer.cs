

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
            inputLogGroup.SuspendLayout();
            SuspendLayout();
            // 
            // startListeningButton
            // 
            startListeningButton.Location = new Point(12, 12);
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
            stopListeningButton.Location = new Point(157, 12);
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
            inputLogTextBox.Size = new Size(442, 311);
            inputLogTextBox.TabIndex = 2;
            inputLogTextBox.TabStop = false;
            inputLogTextBox.WordWrap = false;
            // 
            // inputLogGroup
            // 
            inputLogGroup.Controls.Add(inputLogTextBox);
            inputLogGroup.Location = new Point(12, 52);
            inputLogGroup.Name = "inputLogGroup";
            inputLogGroup.Size = new Size(460, 347);
            inputLogGroup.TabIndex = 3;
            inputLogGroup.TabStop = false;
            inputLogGroup.Text = "Input Log";
            // 
            // openMappingManagerButton
            // 
            openMappingManagerButton.Location = new Point(18, 431);
            openMappingManagerButton.Name = "openMappingManagerButton";
            openMappingManagerButton.Size = new Size(118, 23);
            openMappingManagerButton.TabIndex = 4;
            openMappingManagerButton.Text = "Open Mapping Manager";
            openMappingManagerButton.UseVisualStyleBackColor = true;
            openMappingManagerButton.Click += openMappingManagerButton_Click;
            // 
            // KeyboardToKeystrokes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(717, 682);
            Controls.Add(openMappingManagerButton);
            Controls.Add(inputLogGroup);
            Controls.Add(stopListeningButton);
            Controls.Add(startListeningButton);
            Name = "KeyboardToKeystrokes";
            Text = "Form1";
            inputLogGroup.ResumeLayout(false);
            inputLogGroup.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button startListeningButton;
        private Button stopListeningButton;
        private TextBox inputLogTextBox;
        private GroupBox inputLogGroup;
        private Button openMappingManagerButton;
    }
}