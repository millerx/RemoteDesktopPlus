namespace MillerX.RemoteDesktopPlus
{
    partial class SharedDrivesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container();
            this.m_DriveListBox = new System.Windows.Forms.CheckedListBox();
            this.m_timer = new System.Windows.Forms.Timer( this.components );
            this.m_DynamicDrivesCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_DriveListBox
            // 
            this.m_DriveListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_DriveListBox.CheckOnClick = true;
            this.m_DriveListBox.ColumnWidth = 48;
            this.m_DriveListBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_DriveListBox.Location = new System.Drawing.Point( 0, 0 );
            this.m_DriveListBox.MultiColumn = true;
            this.m_DriveListBox.Name = "m_DriveListBox";
            this.m_DriveListBox.Size = new System.Drawing.Size( 226, 45 );
            this.m_DriveListBox.TabIndex = 1;
            this.m_DriveListBox.LostFocus += new System.EventHandler( this.m_DriveListBox_LostFocus );
            // 
            // m_timer
            // 
            this.m_timer.Tick += new System.EventHandler( this.m_timer_Tick );
            // 
            // m_DynamicDrivesCheckbox
            // 
            this.m_DynamicDrivesCheckbox.Location = new System.Drawing.Point( 1, 46 );
            this.m_DynamicDrivesCheckbox.Name = "m_DynamicDrivesCheckbox";
            this.m_DynamicDrivesCheckbox.Size = new System.Drawing.Size( 225, 20 );
            this.m_DynamicDrivesCheckbox.TabIndex = 2;
            this.m_DynamicDrivesCheckbox.Text = "Drives that I plug in later";
            this.m_DynamicDrivesCheckbox.UseVisualStyleBackColor = false;
            this.m_DynamicDrivesCheckbox.LostFocus += new System.EventHandler( this.m_DynamicDrivesCheckbox_LostFocus );
            // 
            // SharedDrivesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size( 226, 65 );
            this.ControlBox = false;
            this.Controls.Add( this.m_DynamicDrivesCheckbox );
            this.Controls.Add( this.m_DriveListBox );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SharedDrivesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.CheckedListBox m_DriveListBox;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.CheckBox m_DynamicDrivesCheckbox;
    }
}