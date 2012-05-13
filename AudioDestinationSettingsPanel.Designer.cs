namespace MillerX.RemoteDesktopPlus
{
	partial class AudioDestinationSettingsPanel
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
			this.m_Slider = new System.Windows.Forms.TrackBar();
			this.m_LeaveAtRemoteComputerLabel = new System.Windows.Forms.Label();
			this.m_DoNotPlayLabel = new System.Windows.Forms.Label();
			this.m_BringToComputerLabel = new System.Windows.Forms.Label();
			this.m_SpeakerPicture = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize) (this.m_Slider)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.m_SpeakerPicture)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_Slider
			// 
			this.m_Slider.LargeChange = 1;
			this.m_Slider.Location = new System.Drawing.Point( 72, 19 );
			this.m_Slider.Maximum = 2;
			this.m_Slider.Name = "m_Slider";
			this.m_Slider.Size = new System.Drawing.Size( 314, 45 );
			this.m_Slider.TabIndex = 0;
			this.m_Slider.Value = 1;
			// 
			// m_LeaveAtRemoteComputerLabel
			// 
			this.m_LeaveAtRemoteComputerLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_LeaveAtRemoteComputerLabel.Location = new System.Drawing.Point( 79, 54 );
			this.m_LeaveAtRemoteComputerLabel.Name = "m_LeaveAtRemoteComputerLabel";
			this.m_LeaveAtRemoteComputerLabel.Size = new System.Drawing.Size( 90, 31 );
			this.m_LeaveAtRemoteComputerLabel.TabIndex = 1;
			this.m_LeaveAtRemoteComputerLabel.Text = "Leave at remote computer";
			// 
			// m_DoNotPlayLabel
			// 
			this.m_DoNotPlayLabel.Location = new System.Drawing.Point( 199, 54 );
			this.m_DoNotPlayLabel.Name = "m_DoNotPlayLabel";
			this.m_DoNotPlayLabel.Size = new System.Drawing.Size( 67, 31 );
			this.m_DoNotPlayLabel.TabIndex = 2;
			this.m_DoNotPlayLabel.Text = "Do Not Play";
			// 
			// m_BringToComputerLabel
			// 
			this.m_BringToComputerLabel.Location = new System.Drawing.Point( 320, 54 );
			this.m_BringToComputerLabel.Name = "m_BringToComputerLabel";
			this.m_BringToComputerLabel.Size = new System.Drawing.Size( 64, 31 );
			this.m_BringToComputerLabel.TabIndex = 3;
			this.m_BringToComputerLabel.Text = "Bring to this computer";
			// 
			// m_SpeakerPicture
			// 
			this.m_SpeakerPicture.Image = global::MillerX.RemoteDesktopPlus.Properties.Resources.Speaker;
			this.m_SpeakerPicture.Location = new System.Drawing.Point( 20, 33 );
			this.m_SpeakerPicture.Name = "m_SpeakerPicture";
			this.m_SpeakerPicture.Size = new System.Drawing.Size( 29, 31 );
			this.m_SpeakerPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_SpeakerPicture.TabIndex = 4;
			this.m_SpeakerPicture.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add( this.m_BringToComputerLabel );
			this.groupBox1.Controls.Add( this.m_DoNotPlayLabel );
			this.groupBox1.Controls.Add( this.m_LeaveAtRemoteComputerLabel );
			this.groupBox1.Controls.Add( this.m_SpeakerPicture );
			this.groupBox1.Controls.Add( this.m_Slider );
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point( 3, 0 );
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size( 397, 98 );
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Remote audio";
			// 
			// AudioDestinationSettingsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (247)))), ((int) (((byte) (243)))), ((int) (((byte) (247)))) );
			this.Controls.Add( this.groupBox1 );
			this.Name = "AudioDestinationSettingsPanel";
			this.Padding = new System.Windows.Forms.Padding( 3, 0, 3, 0 );
			this.Size = new System.Drawing.Size( 403, 98 );
			((System.ComponentModel.ISupportInitialize) (this.m_Slider)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.m_SpeakerPicture)).EndInit();
			this.groupBox1.ResumeLayout( false );
			this.groupBox1.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.TrackBar m_Slider;
		private System.Windows.Forms.Label m_LeaveAtRemoteComputerLabel;
		private System.Windows.Forms.Label m_DoNotPlayLabel;
		private System.Windows.Forms.Label m_BringToComputerLabel;
		private System.Windows.Forms.PictureBox m_SpeakerPicture;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
