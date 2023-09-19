namespace KeyboardToKeystrokes
{
    partial class MappingManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            midiNoteMappingAssignmentButton = new Button();
            keyboardMappingAssignmentButton = new Button();
            cancelMappingButton = new Button();
            finishedMappingButton = new Button();
            addMappingButton = new Button();
            mappingsGroup = new GroupBox();
            mappingsListBox = new ListBox();
            deleteMappingButton = new Button();
            mappingsGroup.SuspendLayout();
            SuspendLayout();
            // 
            // midiNoteMappingAssignmentButton
            // 
            midiNoteMappingAssignmentButton.Location = new Point(12, 12);
            midiNoteMappingAssignmentButton.Name = "midiNoteMappingAssignmentButton";
            midiNoteMappingAssignmentButton.Size = new Size(175, 61);
            midiNoteMappingAssignmentButton.TabIndex = 1;
            midiNoteMappingAssignmentButton.Text = "Listen for MIDI note";
            midiNoteMappingAssignmentButton.UseVisualStyleBackColor = true;
            midiNoteMappingAssignmentButton.Click += midiNoteMappingAssignmentButton_Click;
            midiNoteMappingAssignmentButton.KeyDown += midiNoteMappingAssignmentButton_KeyDown;
            // 
            // keyboardMappingAssignmentButton
            // 
            keyboardMappingAssignmentButton.Location = new Point(193, 12);
            keyboardMappingAssignmentButton.Name = "keyboardMappingAssignmentButton";
            keyboardMappingAssignmentButton.Size = new Size(175, 61);
            keyboardMappingAssignmentButton.TabIndex = 2;
            keyboardMappingAssignmentButton.Text = "Listen for Keystroke";
            keyboardMappingAssignmentButton.UseVisualStyleBackColor = true;
            keyboardMappingAssignmentButton.Click += keyboardMappingAssignmentButton_Click;
            keyboardMappingAssignmentButton.KeyDown += keyboardMappingAssignmentButton_KeyDown;
            keyboardMappingAssignmentButton.KeyPress += keyboardMappingAssignmentButton_KeyPress;
            // 
            // cancelMappingButton
            // 
            cancelMappingButton.Location = new Point(374, 121);
            cancelMappingButton.Name = "cancelMappingButton";
            cancelMappingButton.Size = new Size(175, 27);
            cancelMappingButton.TabIndex = 3;
            cancelMappingButton.Text = "Cancel Mapping";
            cancelMappingButton.UseVisualStyleBackColor = true;
            cancelMappingButton.Click += cancelMappingButton_Click;
            // 
            // finishedMappingButton
            // 
            finishedMappingButton.Location = new Point(372, 349);
            finishedMappingButton.Name = "finishedMappingButton";
            finishedMappingButton.Size = new Size(175, 27);
            finishedMappingButton.TabIndex = 4;
            finishedMappingButton.Text = "Done";
            finishedMappingButton.UseVisualStyleBackColor = true;
            finishedMappingButton.Click += finishedMappingButton_Click;
            // 
            // addMappingButton
            // 
            addMappingButton.Enabled = false;
            addMappingButton.Location = new Point(374, 88);
            addMappingButton.Name = "addMappingButton";
            addMappingButton.Size = new Size(175, 27);
            addMappingButton.TabIndex = 5;
            addMappingButton.Text = "Add mapping";
            addMappingButton.UseVisualStyleBackColor = true;
            addMappingButton.Click += addMappingButton_Click;
            // 
            // mappingsGroup
            // 
            mappingsGroup.Controls.Add(mappingsListBox);
            mappingsGroup.Location = new Point(12, 79);
            mappingsGroup.Name = "mappingsGroup";
            mappingsGroup.Size = new Size(356, 359);
            mappingsGroup.TabIndex = 6;
            mappingsGroup.TabStop = false;
            mappingsGroup.Text = "Mappings";
            // 
            // mappingsListBox
            // 
            mappingsListBox.FormattingEnabled = true;
            mappingsListBox.ItemHeight = 15;
            mappingsListBox.Location = new Point(6, 22);
            mappingsListBox.Name = "mappingsListBox";
            mappingsListBox.Size = new Size(344, 319);
            mappingsListBox.TabIndex = 0;
            // 
            // deleteMappingButton
            // 
            deleteMappingButton.Location = new Point(374, 154);
            deleteMappingButton.Name = "deleteMappingButton";
            deleteMappingButton.Size = new Size(175, 27);
            deleteMappingButton.TabIndex = 7;
            deleteMappingButton.Text = "Delete Mapping";
            deleteMappingButton.UseVisualStyleBackColor = true;
            deleteMappingButton.Click += deleteMappingButton_Click;
            // 
            // MappingManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(559, 450);
            Controls.Add(deleteMappingButton);
            Controls.Add(mappingsGroup);
            Controls.Add(addMappingButton);
            Controls.Add(finishedMappingButton);
            Controls.Add(cancelMappingButton);
            Controls.Add(keyboardMappingAssignmentButton);
            Controls.Add(midiNoteMappingAssignmentButton);
            Name = "MappingManagerForm";
            Text = "Note to Keyboard Mapping";
            mappingsGroup.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button midiNoteMappingAssignmentButton;
        private Button keyboardMappingAssignmentButton;
        private Button cancelMappingButton;
        private Button finishedMappingButton;
        private Button addMappingButton;
        private GroupBox mappingsGroup;
        private Button deleteMappingButton;
        private ListBox mappingsListBox;
    }
}