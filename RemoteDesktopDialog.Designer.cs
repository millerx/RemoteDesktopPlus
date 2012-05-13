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
			System.Windows.Forms.ToolStripMenuItem m_ctxAdvOpenComputerList;
			System.Windows.Forms.ToolStripMenuItem m_ctxAdvResetWindowPos;
			System.Windows.Forms.ToolStripMenuItem m_ctxAbout;
			this.m_CancelButton = new System.Windows.Forms.Button();
			this.m_ConnectButton = new System.Windows.Forms.Button();
			this.m_ComputerSettingsPanel = new MillerX.RemoteDesktopPlus.ComputerSettingsPanel();
			this.m_AudioDestinationPanel = new MillerX.RemoteDesktopPlus.AudioDestinationSettingsPanel();
			this.m_SharedDrivesControl = new MillerX.RemoteDesktopPlus.SharedDrivesSettingsPanel();
			this.m_Timer = new System.Windows.Forms.Timer( this.components );
			m_contextMenu = new System.Windows.Forms.ContextMenuStrip( this.components );
			m_ctxAdvancedTools = new System.Windows.Forms.ToolStripMenuItem();
			m_ctxAdvOpenComputerList = new System.Windows.Forms.ToolStripMenuItem();
			m_ctxAdvResetWindowPos = new System.Windows.Forms.ToolStripMenuItem();
			m_ctxAbout = new System.Windows.Forms.ToolStripMenuItem();
			m_contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_contextMenu
			// 
			m_contextMenu.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            m_ctxAdvancedTools,
            m_ctxAbout} );
			m_contextMenu.Name = "m_contextMenu";
			m_contextMenu.Size = new System.Drawing.Size( 160, 48 );
			// 
			// m_ctxAdvancedTools
			// 
			m_ctxAdvancedTools.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            m_ctxAdvOpenComputerList,
            m_ctxAdvResetWindowPos} );
			m_ctxAdvancedTools.Name = "m_ctxAdvancedTools";
			m_ctxAdvancedTools.Size = new System.Drawing.Size( 159, 22 );
			m_ctxAdvancedTools.Text = "Advanced Tools";
			// 
			// m_ctxAdvOpenComputerList
			// 
			m_ctxAdvOpenComputerList.Name = "m_ctxAdvOpenComputerList";
			m_ctxAdvOpenComputerList.Size = new System.Drawing.Size( 213, 22 );
			m_ctxAdvOpenComputerList.Text = "Open Computer List File...";
			m_ctxAdvOpenComputerList.Click += new System.EventHandler( this.m_ctxAdvOpenComputerList_Click );
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
			// m_CancelButton
			// 
			this.m_CancelButton.Location = new System.Drawing.Point( 316, 157 );
			this.m_CancelButton.Name = "m_CancelButton";
			this.m_CancelButton.Size = new System.Drawing.Size( 75, 23 );
			this.m_CancelButton.TabIndex = 2;
			this.m_CancelButton.Text = "Cancel";
			this.m_CancelButton.UseVisualStyleBackColor = true;
			// 
			// m_ConnectButton
			// 
			this.m_ConnectButton.Location = new System.Drawing.Point( 235, 157 );
			this.m_ConnectButton.Name = "m_ConnectButton";
			this.m_ConnectButton.Size = new System.Drawing.Size( 75, 23 );
			this.m_ConnectButton.TabIndex = 1;
			this.m_ConnectButton.Text = "Connect";
			this.m_ConnectButton.UseVisualStyleBackColor = true;
			this.m_ConnectButton.Click += new System.EventHandler( this.m_ConnectButton_Click );
			// 
			// m_ComputerSettingsPanel
			// 
			this.m_ComputerSettingsPanel.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (247)))), ((int) (((byte) (243)))), ((int) (((byte) (247)))) );
			this.m_ComputerSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_ComputerSettingsPanel.Location = new System.Drawing.Point( 0, 0 );
			this.m_ComputerSettingsPanel.Name = "m_ComputerSettingsPanel";
			this.m_ComputerSettingsPanel.Size = new System.Drawing.Size( 403, 139 );
			this.m_ComputerSettingsPanel.TabIndex = 0;
			// 
			// m_AudioDestinationPanel
			// 
			this.m_AudioDestinationPanel.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (247)))), ((int) (((byte) (243)))), ((int) (((byte) (247)))) );
			this.m_AudioDestinationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_AudioDestinationPanel.Location = new System.Drawing.Point( 0, 186 );
			this.m_AudioDestinationPanel.Name = "m_AudioDestinationPanel";
			this.m_AudioDestinationPanel.Padding = new System.Windows.Forms.Padding( 3, 0, 3, 0 );
			this.m_AudioDestinationPanel.Size = new System.Drawing.Size( 403, 98 );
			this.m_AudioDestinationPanel.TabIndex = 3;
			// 
			// m_SharedDrivesControl
			// 
			this.m_SharedDrivesControl.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (247)))), ((int) (((byte) (243)))), ((int) (((byte) (247)))) );
			this.m_SharedDrivesControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_SharedDrivesControl.Location = new System.Drawing.Point( 0, 284 );
			this.m_SharedDrivesControl.Name = "m_SharedDrivesControl";
			this.m_SharedDrivesControl.Size = new System.Drawing.Size( 403, 98 );
			this.m_SharedDrivesControl.TabIndex = 4;
			// 
			// m_Timer
			// 
			this.m_Timer.Interval = 3000;
			this.m_Timer.Tick += new System.EventHandler( this.m_Timer_Tick );
			// 
			// RemoteDesktopDialog
			// 
			this.AcceptButton = this.m_ConnectButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (247)))), ((int) (((byte) (243)))), ((int) (((byte) (247)))) );
			this.ClientSize = new System.Drawing.Size( 403, 382 );
			this.ContextMenuStrip = m_contextMenu;
			this.Controls.Add( this.m_ComputerSettingsPanel );
			this.Controls.Add( this.m_CancelButton );
			this.Controls.Add( this.m_ConnectButton );
			this.Controls.Add( this.m_AudioDestinationPanel );
			this.Controls.Add( this.m_SharedDrivesControl );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "RemoteDesktopDialog";
			this.Text = "Remote Desktop Plus";
			m_contextMenu.ResumeLayout( false );
			this.ResumeLayout( false );

		}

		#endregion

		private MillerX.RemoteDesktopPlus.AudioDestinationSettingsPanel m_AudioDestinationPanel;
		private SharedDrivesSettingsPanel m_SharedDrivesControl;
		private System.Windows.Forms.Button m_CancelButton;
		private System.Windows.Forms.Button m_ConnectButton;
		private ComputerSettingsPanel m_ComputerSettingsPanel;
		private System.Windows.Forms.Timer m_Timer;
	}
}

