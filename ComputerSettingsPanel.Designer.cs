namespace MillerX.RemoteDesktopPlus
{
	partial class ComputerSettingsPanel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( )
		{
			this.components = new System.ComponentModel.Container();
			this.m_ComputerLabel = new System.Windows.Forms.Label();
			this.m_ComputerComboBox = new System.Windows.Forms.ComboBox();
			this.m_LogoPictureBox = new System.Windows.Forms.PictureBox();
			this.m_AliasLabel = new System.Windows.Forms.Label();
			this.m_AliasComboBox = new System.Windows.Forms.ComboBox();
			this.m_ToolTip = new System.Windows.Forms.ToolTip( this.components );
			((System.ComponentModel.ISupportInitialize) (this.m_LogoPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// m_ComputerLabel
			// 
			this.m_ComputerLabel.AutoSize = true;
			this.m_ComputerLabel.Location = new System.Drawing.Point( 12, 90 );
			this.m_ComputerLabel.Name = "m_ComputerLabel";
			this.m_ComputerLabel.Size = new System.Drawing.Size( 55, 13 );
			this.m_ComputerLabel.TabIndex = 0;
			this.m_ComputerLabel.Text = "&Computer:";
			// 
			// m_ComputerComboBox
			// 
			this.m_ComputerComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.m_ComputerComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.m_ComputerComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.m_ComputerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_ComputerComboBox.FormattingEnabled = true;
			this.m_ComputerComboBox.Location = new System.Drawing.Point( 82, 87 );
			this.m_ComputerComboBox.Name = "m_ComputerComboBox";
			this.m_ComputerComboBox.Size = new System.Drawing.Size( 229, 21 );
			this.m_ComputerComboBox.TabIndex = 1;
			this.m_ComputerComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler( this.m_ComputerComboBox_DrawItem );
			this.m_ComputerComboBox.SelectedIndexChanged += new System.EventHandler( this.m_ComputerComboBox_SelectedIndexChanged );
			this.m_ComputerComboBox.TextChanged += new System.EventHandler( this.m_ComputerComboBox_TextChanged );
			// 
			// m_LogoPictureBox
			// 
			this.m_LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_LogoPictureBox.Image = global::MillerX.RemoteDesktopPlus.Properties.Resources.UserLogo;
			this.m_LogoPictureBox.Location = new System.Drawing.Point( 0, 0 );
			this.m_LogoPictureBox.Name = "m_LogoPictureBox";
			this.m_LogoPictureBox.Size = new System.Drawing.Size( 403, 72 );
			this.m_LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_LogoPictureBox.TabIndex = 7;
			this.m_LogoPictureBox.TabStop = false;
			this.m_LogoPictureBox.Click += new System.EventHandler( this.m_LogoPictureBox_Click );
			// 
			// m_AliasLabel
			// 
			this.m_AliasLabel.AutoSize = true;
			this.m_AliasLabel.Location = new System.Drawing.Point( 12, 118 );
			this.m_AliasLabel.Name = "m_AliasLabel";
			this.m_AliasLabel.Size = new System.Drawing.Size( 32, 13 );
			this.m_AliasLabel.TabIndex = 2;
			this.m_AliasLabel.Text = "&Alias:";
			// 
			// m_AliasComboBox
			// 
			this.m_AliasComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.m_AliasComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.m_AliasComboBox.Location = new System.Drawing.Point( 82, 115 );
			this.m_AliasComboBox.Name = "m_AliasComboBox";
			this.m_AliasComboBox.Size = new System.Drawing.Size( 229, 21 );
			this.m_AliasComboBox.TabIndex = 3;
			// 
			// m_ToolTip
			// 
			this.m_ToolTip.Active = false;
			// 
			// ComputerSettingsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (247)))), ((int) (((byte) (243)))), ((int) (((byte) (247)))) );
			this.Controls.Add( this.m_AliasLabel );
			this.Controls.Add( this.m_ComputerLabel );
			this.Controls.Add( this.m_LogoPictureBox );
			this.Controls.Add( this.m_ComputerComboBox );
			this.Controls.Add( this.m_AliasComboBox );
			this.Name = "ComputerSettingsPanel";
			this.Size = new System.Drawing.Size( 403, 142 );
			((System.ComponentModel.ISupportInitialize) (this.m_LogoPictureBox)).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label m_ComputerLabel;
		private System.Windows.Forms.PictureBox m_LogoPictureBox;
		private System.Windows.Forms.ComboBox m_ComputerComboBox;
		private System.Windows.Forms.Label m_AliasLabel;
		private System.Windows.Forms.ComboBox m_AliasComboBox;
		private System.Windows.Forms.ToolTip m_ToolTip;
	}
}
