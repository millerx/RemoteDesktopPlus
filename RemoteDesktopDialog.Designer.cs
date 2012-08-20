namespace MillerX.RemoteDesktopPlus
{
	partial class RemoteDesktopDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( )
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ContextMenuStrip m_contextMenu;
            System.Windows.Forms.ToolStripMenuItem m_ctxAdvancedTools;
            System.Windows.Forms.ToolStripMenuItem m_ctxAdvEditComputerList;
            System.Windows.Forms.ToolStripMenuItem m_ctxAdvResetWindowPos;
            System.Windows.Forms.ToolStripMenuItem m_ctxAbout;
            this.m_ConnectButton = new System.Windows.Forms.Button();
            this.m_Timer = new System.Windows.Forms.Timer( this.components );
            this.m_AliasLabel = new System.Windows.Forms.Label();
            this.m_ComputerLabel = new System.Windows.Forms.Label();
            this.m_LogoPictureBox = new MillerX.RemoteDesktopPlus.LogoPictureBox();
            this.m_ComputerComboBox = new MillerX.RemoteDesktopPlus.ComputerComboBox();
            this.m_AliasComboBox = new System.Windows.Forms.ComboBox();
            this.m_ToolTip = new System.Windows.Forms.ToolTip( this.components );
            this.m_DrivesButton = new MillerX.RemoteDesktopPlus.SharedDrivesButton();
            this.m_AudioButton = new MillerX.RemoteDesktopPlus.AudioDestButton();
            this.m_ExpandAliasButton = new System.Windows.Forms.Button();
            m_contextMenu = new System.Windows.Forms.ContextMenuStrip( this.components );
            m_ctxAdvancedTools = new System.Windows.Forms.ToolStripMenuItem();
            m_ctxAdvEditComputerList = new System.Windows.Forms.ToolStripMenuItem();
            m_ctxAdvResetWindowPos = new System.Windows.Forms.ToolStripMenuItem();
            m_ctxAbout = new System.Windows.Forms.ToolStripMenuItem();
            m_contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.m_LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // m_contextMenu
            // 
            m_contextMenu.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            m_ctxAdvancedTools,
            m_ctxAbout} );
            m_contextMenu.Name = "m_contextMenu";
            m_contextMenu.Size = new System.Drawing.Size( 160, 70 );
            // 
            // m_ctxAdvancedTools
            // 
            m_ctxAdvancedTools.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            m_ctxAdvEditComputerList,
            m_ctxAdvResetWindowPos} );
            m_ctxAdvancedTools.Name = "m_ctxAdvancedTools";
            m_ctxAdvancedTools.Size = new System.Drawing.Size( 159, 22 );
            m_ctxAdvancedTools.Text = "Advanced Tools";
            // 
            // m_ctxAdvEditComputerList
            // 
            m_ctxAdvEditComputerList.Name = "m_ctxAdvEditComputerList";
            m_ctxAdvEditComputerList.Size = new System.Drawing.Size( 213, 22 );
            m_ctxAdvEditComputerList.Text = "Edit Computer List";
            m_ctxAdvEditComputerList.Click += new System.EventHandler( this.m_ctxAdvEditComputerList_Click );
            // 
            // m_ctxAdvResetWindowPos
            // 
            m_ctxAdvResetWindowPos.Name = "m_ctxAdvResetWindowPos";
            m_ctxAdvResetWindowPos.Size = new System.Drawing.Size( 213, 22 );
            m_ctxAdvResetWindowPos.Text = "Reset RD Window Position";
            m_ctxAdvResetWindowPos.Click += new System.EventHandler( this.m_ctxAdvResetWindowPos_Click );
            // 
            // m_ctxAbout
            // 
            m_ctxAbout.Name = "m_ctxAbout";
            m_ctxAbout.Size = new System.Drawing.Size( 159, 22 );
            m_ctxAbout.Text = "About...";
            m_ctxAbout.Click += new System.EventHandler( this.m_ctxAbout_Click );
            // 
            // m_ConnectButton
            // 
            this.m_ConnectButton.Location = new System.Drawing.Point( 235, 157 );
            this.m_ConnectButton.Name = "m_ConnectButton";
            this.m_ConnectButton.Size = new System.Drawing.Size( 75, 28 );
            this.m_ConnectButton.TabIndex = 6;
            this.m_ConnectButton.Text = "Connect";
            this.m_ConnectButton.UseVisualStyleBackColor = true;
            this.m_ConnectButton.Click += new System.EventHandler( this.m_ConnectButton_Click );
            // 
            // m_Timer
            // 
            this.m_Timer.Interval = 3000;
            this.m_Timer.Tick += new System.EventHandler( this.m_Timer_Tick );
            // 
            // m_AliasLabel
            // 
            this.m_AliasLabel.AutoSize = true;
            this.m_AliasLabel.Location = new System.Drawing.Point( 11, 119 );
            this.m_AliasLabel.Name = "m_AliasLabel";
            this.m_AliasLabel.Size = new System.Drawing.Size( 32, 13 );
            this.m_AliasLabel.TabIndex = 2;
            this.m_AliasLabel.Text = "&Alias:";
            // 
            // m_ComputerLabel
            // 
            this.m_ComputerLabel.AutoSize = true;
            this.m_ComputerLabel.Location = new System.Drawing.Point( 11, 91 );
            this.m_ComputerLabel.Name = "m_ComputerLabel";
            this.m_ComputerLabel.Size = new System.Drawing.Size( 55, 13 );
            this.m_ComputerLabel.TabIndex = 0;
            this.m_ComputerLabel.Text = "&Computer:";
            // 
            // m_LogoPictureBox
            // 
            this.m_LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_LogoPictureBox.Location = new System.Drawing.Point( 0, 0 );
            this.m_LogoPictureBox.Name = "m_LogoPictureBox";
            this.m_LogoPictureBox.Size = new System.Drawing.Size( 350, 72 );
            this.m_LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_LogoPictureBox.TabIndex = 12;
            this.m_LogoPictureBox.TabStop = false;
            // 
            // m_ComputerComboBox
            // 
            this.m_ComputerComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.m_ComputerComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_ComputerComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.m_ComputerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_ComputerComboBox.FormattingEnabled = true;
            this.m_ComputerComboBox.IntegralHeight = false;
            this.m_ComputerComboBox.Location = new System.Drawing.Point( 81, 88 );
            this.m_ComputerComboBox.Name = "m_ComputerComboBox";
            this.m_ComputerComboBox.Size = new System.Drawing.Size( 229, 21 );
            this.m_ComputerComboBox.TabIndex = 1;
            this.m_ComputerComboBox.ComputerChanged += new System.EventHandler<MillerX.RemoteDesktopPlus.DataEventArgs>( this.m_ComputerComboBox_ComputerChanged );
            this.m_ComputerComboBox.SelectedIndexChanged += new System.EventHandler( this.m_ComputerComboBox_SelectedIndexChanged );
            // 
            // m_AliasComboBox
            // 
            this.m_AliasComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.m_AliasComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_AliasComboBox.IntegralHeight = false;
            this.m_AliasComboBox.Location = new System.Drawing.Point( 81, 116 );
            this.m_AliasComboBox.Name = "m_AliasComboBox";
            this.m_AliasComboBox.Size = new System.Drawing.Size( 229, 21 );
            this.m_AliasComboBox.TabIndex = 3;
            // 
            // m_DrivesButton
            // 
            this.m_DrivesButton.Location = new System.Drawing.Point( 71, 157 );
            this.m_DrivesButton.Name = "m_DrivesButton";
            this.m_DrivesButton.Size = new System.Drawing.Size( 52, 45 );
            this.m_DrivesButton.TabIndex = 13;
            this.m_ToolTip.SetToolTip( this.m_DrivesButton, "Shared drives." );
            this.m_DrivesButton.UseVisualStyleBackColor = true;
            // 
            // m_AudioButton
            // 
            this.m_AudioButton.Location = new System.Drawing.Point( 12, 157 );
            this.m_AudioButton.Name = "m_AudioButton";
            this.m_AudioButton.Size = new System.Drawing.Size( 52, 45 );
            this.m_AudioButton.TabIndex = 4;
            this.m_AudioButton.UseVisualStyleBackColor = true;
            // 
            // m_ExpandAliasButton
            // 
            this.m_ExpandAliasButton.Enabled = false;
            this.m_ExpandAliasButton.Location = new System.Drawing.Point( 316, 100 );
            this.m_ExpandAliasButton.Name = "m_ExpandAliasButton";
            this.m_ExpandAliasButton.Size = new System.Drawing.Size( 23, 23 );
            this.m_ExpandAliasButton.TabIndex = 14;
            this.m_ExpandAliasButton.Text = "S";
            this.m_ExpandAliasButton.UseVisualStyleBackColor = true;
            this.m_ExpandAliasButton.Click += new System.EventHandler( this.m_ExpandAliasButton_Click );
            // 
            // RemoteDesktopDialog
            // 
            this.AcceptButton = this.m_ConnectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (247)))), ((int) (((byte) (243)))), ((int) (((byte) (247)))) );
            this.ClientSize = new System.Drawing.Size( 350, 210 );
            this.ContextMenuStrip = m_contextMenu;
            this.Controls.Add( this.m_ExpandAliasButton );
            this.Controls.Add( this.m_DrivesButton );
            this.Controls.Add( this.m_AliasLabel );
            this.Controls.Add( this.m_ComputerLabel );
            this.Controls.Add( this.m_AudioButton );
            this.Controls.Add( this.m_LogoPictureBox );
            this.Controls.Add( this.m_ComputerComboBox );
            this.Controls.Add( this.m_AliasComboBox );
            this.Controls.Add( this.m_ConnectButton );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RemoteDesktopDialog";
            this.Text = "Remote Desktop Plus";
            m_contextMenu.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize) (this.m_LogoPictureBox)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button m_ConnectButton;
		private System.Windows.Forms.Timer m_Timer;
        private System.Windows.Forms.Label m_AliasLabel;
        private System.Windows.Forms.Label m_ComputerLabel;
        private LogoPictureBox m_LogoPictureBox;
        private ComputerComboBox m_ComputerComboBox;
        private System.Windows.Forms.ComboBox m_AliasComboBox;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private AudioDestButton m_AudioButton;
        private SharedDrivesButton m_DrivesButton;
        private System.Windows.Forms.Button m_ExpandAliasButton;
	}
}

