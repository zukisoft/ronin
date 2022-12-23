namespace zuki.ronin.ui
{
	partial class RulingsView
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
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_webbrowser = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// m_webbrowser
			// 
			this.m_webbrowser.AllowWebBrowserDrop = false;
			this.m_webbrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_webbrowser.IsWebBrowserContextMenuEnabled = false;
			this.m_webbrowser.Location = new System.Drawing.Point(6, 6);
			this.m_webbrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.m_webbrowser.Name = "m_webbrowser";
			this.m_webbrowser.ScriptErrorsSuppressed = true;
			this.m_webbrowser.Size = new System.Drawing.Size(388, 388);
			this.m_webbrowser.TabIndex = 0;
			this.m_webbrowser.Visible = false;
			this.m_webbrowser.WebBrowserShortcutsEnabled = false;
			this.m_webbrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnDocumentCompleted);
			// 
			// RulingsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_webbrowser);
			this.DoubleBuffered = true;
			this.Name = "RulingsView";
			this.Size = new System.Drawing.Size(400, 400);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser m_webbrowser;
	}
}
