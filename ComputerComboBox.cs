using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
    class ComputerComboBox : ComboBox
    {
        private RecentComputerList m_computers = new RecentComputerList();
        private ToolTip m_ToolTip = new ToolTip();

        public ComputerComboBox( )
        {
            this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                m_ToolTip.Dispose();
            }

            base.Dispose( disposing );
        }

        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public new int SelectedIndex
        {
            get { return base.SelectedIndex; }
            set
            {
                base.SelectedIndex = value;
                // If the computer name and the alias are the same then it won't trigger the text changed event.
                OnTextChanged( EventArgs.Empty );
            }
        }

        public event EventHandler<DataEventArgs> ComputerChanged;
        protected void OnComputerChanged( ComputerName computer )
        {
            if ( ComputerChanged != null )
                ComputerChanged( this, new DataEventArgs( computer ) );
        }

        public void Populate( RecentComputerList computers )
        {
            m_computers = computers;

            BeginUpdate();
            this.Items.Clear();
            this.Items.AddRange( computers.ToArray() );
            this.MaxDropDownItems = Config.MaxComboItems;
            EndUpdate();
        }

        protected override void OnDrawItem( DrawItemEventArgs e )
        {
            e.DrawBackground();

            ComputerName computer = (ComputerName) this.Items[e.Index];
            Brush brush = GetComboBoxItemBrush( computer, e.State );
            e.Graphics.DrawString( computer.ToString(), e.Font, brush, e.Bounds.X, e.Bounds.Y );

            e.DrawFocusRectangle();
        }

        private Brush GetComboBoxItemBrush( ComputerName computer, DrawItemState state )
        {
            if ( (state & DrawItemState.Selected) != 0 )
                return SystemBrushes.HighlightText;
            else if ( computer.Alias != null )
                return UIItems.AliasBrush;
            else
                return SystemBrushes.ControlText;
        }

        protected override void OnTextChanged( EventArgs e )
        {
            ComputerName computer = m_computers.Find( this.Text );
            if ( computer != null )
            {
                if ( !string.IsNullOrEmpty( computer.Alias ) )
                {
                    m_ToolTip.SetToolTip( this, computer.Computer );
                    m_ToolTip.Active = true;
                    this.ForeColor = UIItems.AliasColor;
                }
                else
                {
                    m_ToolTip.Active = false;
                    this.ForeColor = SystemColors.WindowText;
                }

                OnComputerChanged( computer );
            }

            base.OnTextChanged( e );
        }
    }
}
