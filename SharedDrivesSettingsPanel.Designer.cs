namespace MillerX.RemoteDesktopPlus
{
	partial class SharedDrivesSettingsPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( )
		{
			this.m_DriveListBox = new System.Windows.Forms.CheckedListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_SpeakerPicture = new System.Windows.Forms.PictureBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.m_SpeakerPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// m_DriveListBox
			// 
			this.m_DriveListBox.CheckOnClick = true;
			this.m_DriveListBox.ColumnWidth = 48;
			this.m_DriveListBox.Location = new System.Drawing.Point( 83, 23 );
			this.m_DriveListBox.MultiColumn = true;
			this.m_DriveListBox.Name = "m_DriveListBox";
			this.m_DriveListBox.Size = new System.Drawing.Size( 301, 49 );
			this.m_DriveListBox.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add( this.m_SpeakerPicture );
			this.groupBox1.Controls.Add( this.m_DriveListBox );
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point( 0, 0 );
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size( 403, 98 );
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Shared drives";
			// 
			// m_SpeakerPicture
			// 
			this.m_SpeakerPicture.Image = global::MillerX.RemoteDesktopPlus.Properties.Resources.Drive;
			this.m_SpeakerPicture.Location = new System.Drawing.Point( 20, 33 );
			this.m_SpeakerPicture.Name = "m_SpeakerPicture";
			this.m_SpeakerPicture.Size = new System.Drawing.Size( 32, 32 );
			this.m_SpeakerPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_SpeakerPicture.TabIndex = 5;
			this.m_SpeakerPicture.TabStop = false;
			// 
			// SharedDrivesSettingsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (247)))), ((int) (((byte) (243)))), ((int) (((byte) (247)))) );
			this.Controls.Add( this.groupBox1 );
			this.Name = "SharedDrivesSettingsPanel";
			this.Size = new System.Drawing.Size( 403, 98 );
			this.groupBox1.ResumeLayout( false );
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.m_SpeakerPicture)).EndInit();
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.CheckedListBox m_DriveListBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PictureBox m_SpeakerPicture;

	}
}
