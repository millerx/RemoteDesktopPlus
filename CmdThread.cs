using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MillerX.RemoteDesktopPlus
{
	/// <summary>
	/// Thread that supports synchronous command execution.
	/// </summary>
	public class CmdThread<T> : IDisposable
	{
		private struct CmdPair
		{
			public T Command;
			public object[] Parameters;
		}

		private enum QuitOptions
		{
			Run = 0,
			WaitForCmds = 1,
			StopNow = 2
		}

		/// <summary>
		/// .Net thread to run commands.
		/// </summary>
		private Thread m_thread;

		/// <summary>
		/// Queue of commands to run.
		/// </summary>
		private Queue<CmdPair> m_cmdQueue = new Queue<CmdPair>();

		/// <summary>
		/// Singals that a command is ready to be processed.
		/// </summary>
		private ManualResetEvent m_event = new ManualResetEvent( false );

		/// <summary>
		/// Object upon which to do simple mutex synchronization.
		/// </summary>
		private object m_sync = new object();

		/// <summary>
		/// Should the run thread quit?
		/// </summary>
		private QuitOptions m_quit = QuitOptions.Run;

		/// <summary>
		/// Constructor.
		/// </summary>
		public CmdThread( )
		{
			m_thread = new Thread( new ThreadStart( ThreadMain ) );
		}

		/// <summary>
		/// Disposes resources.
		/// </summary>
		/// <remarks>Safe to run from any thread.</remarks>
		public void Dispose( )
		{
			this.Wait();
			m_thread.Join();
			m_event.Close();
		}

		/// <summary>
		/// Starts the command thread.
		/// </summary>
		/// <remarks>Safe to run from any thread.</remarks>
		public void Start( )
		{
			if ( this.InvokeRequired &&
				m_thread.ThreadState == ThreadState.Unstarted )
			{
				m_thread.Start();
			}
		}

		/// <summary>
		/// Stops the command thread after all enqueued commands have been ran.
		/// </summary>
		public void Wait( )
		{
			Quit( QuitOptions.WaitForCmds );
		}

		/// <summary>
		/// Stops the command thread even if there are commands left to be executed.
		/// </summary>
		/// <remarks>Safe to run from any thread.</remarks>
		public void Stop( )
		{
			Quit( QuitOptions.StopNow );
		}

		private void Quit( QuitOptions opt )
		{
			lock ( m_sync )
			{
				m_quit = opt;
				m_event.Set();
			}
		}

		/// <summary>
		/// Are we in the command thread?
		/// </summary>
		public bool InvokeRequired
		{
			// I explicitly chose not to implement the rest of the ISynchronizeInvoke interface.
			get { return Thread.CurrentThread.ManagedThreadId != m_thread.ManagedThreadId; }
		}

		/// <summary>
		/// Queues a command to be run on the command thread.
		/// </summary>
		/// <param name="command">Command to run.</param>
		/// <param name="parameters">Parameters to the command.</param>
		/// <remarks>Safe to run from any thread.</remarks>
		public void Execute( T command, params object[] parameters )
		{
			lock ( m_sync )
			{
				m_cmdQueue.Enqueue( new CmdPair() {
					Command = command,
					Parameters = parameters } );

				m_event.Set();
			}
		}

		/// <summary>
		/// Main thread loop.
		/// </summary>
		private void ThreadMain( )
		{
			while ( true )
			{
				QuitOptions quit;
				int count;
				CmdPair cmd = new CmdPair();
				lock ( m_sync )
				{
					quit = m_quit;
					count = m_cmdQueue.Count;
					if ( count > 0 )
						cmd = m_cmdQueue.Dequeue();
					m_event.Reset();
				}

				if ( quit == QuitOptions.StopNow )
					break;
				else if ( quit == QuitOptions.WaitForCmds && count == 0 )
					break;

				// Checking count allows to execute a null command.
				// We only WaitOne after all commands have been processed because calling AutoResetEvent.Set
				// rapidly can let mutliple events through in a single pass.
				if ( count == 0 )
				{
					m_event.WaitOne();
				}
				else
				{
					try
					{
						CmdProc( cmd.Command, cmd.Parameters );
					}
					catch ( Exception )
					{
					}
				}
			}
		}

		/// <summary>
		/// Dispatcher for commands.  Invoked when there is one or more commands on the queue.
		/// </summary>
		/// <param name="command">Command to execute.</param>
		/// <param name="parameters">Parameters to the command.</param>
		protected virtual void CmdProc( T command, object[] cparams )
		{
		}
	}
}
