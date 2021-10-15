namespace WindowsFormsApp1
{
    partial class MainForm
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
            this.cmdProcess = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdInspectRows = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdReview = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cboInspectRowIndices = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.OleDbLoadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdProcess
            // 
            this.cmdProcess.Location = new System.Drawing.Point(12, 24);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(75, 23);
            this.cmdProcess.TabIndex = 0;
            this.cmdProcess.Text = "Process";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(755, 263);
            this.dataGridView1.TabIndex = 1;
            // 
            // cmdInspectRows
            // 
            this.cmdInspectRows.Location = new System.Drawing.Point(249, 24);
            this.cmdInspectRows.Name = "cmdInspectRows";
            this.cmdInspectRows.Size = new System.Drawing.Size(75, 23);
            this.cmdInspectRows.TabIndex = 2;
            this.cmdInspectRows.Text = "Inspect rows";
            this.cmdInspectRows.UseVisualStyleBackColor = true;
            this.cmdInspectRows.Click += new System.EventHandler(this.cmdInspectRows_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OleDbLoadButton);
            this.panel1.Controls.Add(this.cmdReview);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cboInspectRowIndices);
            this.panel1.Controls.Add(this.cmdProcess);
            this.panel1.Controls.Add(this.cmdInspectRows);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 408);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 59);
            this.panel1.TabIndex = 3;
            // 
            // cmdReview
            // 
            this.cmdReview.Location = new System.Drawing.Point(330, 24);
            this.cmdReview.Name = "cmdReview";
            this.cmdReview.Size = new System.Drawing.Size(75, 23);
            this.cmdReview.TabIndex = 5;
            this.cmdReview.Text = "Review";
            this.cmdReview.UseVisualStyleBackColor = true;
            this.cmdReview.Click += new System.EventHandler(this.cmdReview_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Location = new System.Drawing.Point(668, 24);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(75, 23);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cboInspectRowIndices
            // 
            this.cboInspectRowIndices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInspectRowIndices.FormattingEnabled = true;
            this.cboInspectRowIndices.Location = new System.Drawing.Point(113, 24);
            this.cboInspectRowIndices.Name = "cboInspectRowIndices";
            this.cboInspectRowIndices.Size = new System.Drawing.Size(121, 21);
            this.cboInspectRowIndices.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer1.Size = new System.Drawing.Size(755, 408);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.TabIndex = 4;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(755, 141);
            this.dataGridView2.TabIndex = 2;
            // 
            // OleDbLoadButton
            // 
            this.OleDbLoadButton.Location = new System.Drawing.Point(426, 24);
            this.OleDbLoadButton.Name = "OleDbLoadButton";
            this.OleDbLoadButton.Size = new System.Drawing.Size(75, 23);
            this.OleDbLoadButton.TabIndex = 6;
            this.OleDbLoadButton.Text = "OleDb load";
            this.OleDbLoadButton.UseVisualStyleBackColor = true;
            this.OleDbLoadButton.Click += new System.EventHandler(this.OleDbLoadButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 467);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Sample";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdProcess;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cmdInspectRows;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox cboInspectRowIndices;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdReview;
        private System.Windows.Forms.Button OleDbLoadButton;
    }
}

