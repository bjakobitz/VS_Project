namespace kave_project
{
    partial class Form1
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
            this.btn_run = new System.Windows.Forms.Button();
            this.lbl_eventDir = new System.Windows.Forms.Label();
            this.tb_eventDir = new System.Windows.Forms.TextBox();
            this.lbl_output_file = new System.Windows.Forms.Label();
            this.tb_output = new System.Windows.Forms.TextBox();
            this.btn_event_browse = new System.Windows.Forms.Button();
            this.btn_output_browse = new System.Windows.Forms.Button();
            this.pb_file_progress = new System.Windows.Forms.ProgressBar();
            this.lbl_time_lbl = new System.Windows.Forms.Label();
            this.lbl_time = new System.Windows.Forms.Label();
            this.dgv_counts = new System.Windows.Forms.DataGridView();
            this.command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pb_quick = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_counts)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(16, 113);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(75, 23);
            this.btn_run.TabIndex = 0;
            this.btn_run.Text = "Run";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // lbl_eventDir
            // 
            this.lbl_eventDir.AutoSize = true;
            this.lbl_eventDir.Location = new System.Drawing.Point(25, 13);
            this.lbl_eventDir.Name = "lbl_eventDir";
            this.lbl_eventDir.Size = new System.Drawing.Size(49, 13);
            this.lbl_eventDir.TabIndex = 1;
            this.lbl_eventDir.Text = "Event dir";
            // 
            // tb_eventDir
            // 
            this.tb_eventDir.Location = new System.Drawing.Point(80, 13);
            this.tb_eventDir.Name = "tb_eventDir";
            this.tb_eventDir.Size = new System.Drawing.Size(800, 20);
            this.tb_eventDir.TabIndex = 2;
            // 
            // lbl_output_file
            // 
            this.lbl_output_file.AutoSize = true;
            this.lbl_output_file.Location = new System.Drawing.Point(16, 46);
            this.lbl_output_file.Name = "lbl_output_file";
            this.lbl_output_file.Size = new System.Drawing.Size(58, 13);
            this.lbl_output_file.TabIndex = 3;
            this.lbl_output_file.Text = "Output File";
            // 
            // tb_output
            // 
            this.tb_output.Location = new System.Drawing.Point(80, 40);
            this.tb_output.Name = "tb_output";
            this.tb_output.Size = new System.Drawing.Size(800, 20);
            this.tb_output.TabIndex = 4;
            // 
            // btn_event_browse
            // 
            this.btn_event_browse.Location = new System.Drawing.Point(886, 13);
            this.btn_event_browse.Name = "btn_event_browse";
            this.btn_event_browse.Size = new System.Drawing.Size(75, 23);
            this.btn_event_browse.TabIndex = 5;
            this.btn_event_browse.Text = "Browse";
            this.btn_event_browse.UseVisualStyleBackColor = true;
            this.btn_event_browse.Click += new System.EventHandler(this.btn_event_browse_Click);
            // 
            // btn_output_browse
            // 
            this.btn_output_browse.Location = new System.Drawing.Point(886, 38);
            this.btn_output_browse.Name = "btn_output_browse";
            this.btn_output_browse.Size = new System.Drawing.Size(75, 23);
            this.btn_output_browse.TabIndex = 6;
            this.btn_output_browse.Text = "Browse";
            this.btn_output_browse.UseVisualStyleBackColor = true;
            this.btn_output_browse.Click += new System.EventHandler(this.btn_output_browse_Click);
            // 
            // pb_file_progress
            // 
            this.pb_file_progress.Location = new System.Drawing.Point(16, 77);
            this.pb_file_progress.Name = "pb_file_progress";
            this.pb_file_progress.Size = new System.Drawing.Size(377, 23);
            this.pb_file_progress.TabIndex = 7;
            // 
            // lbl_time_lbl
            // 
            this.lbl_time_lbl.AutoSize = true;
            this.lbl_time_lbl.Location = new System.Drawing.Point(400, 86);
            this.lbl_time_lbl.Name = "lbl_time_lbl";
            this.lbl_time_lbl.Size = new System.Drawing.Size(30, 13);
            this.lbl_time_lbl.TabIndex = 8;
            this.lbl_time_lbl.Text = "Time";
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(437, 85);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(0, 13);
            this.lbl_time.TabIndex = 9;
            // 
            // dgv_counts
            // 
            this.dgv_counts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_counts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.command,
            this.count});
            this.dgv_counts.Location = new System.Drawing.Point(16, 142);
            this.dgv_counts.Name = "dgv_counts";
            this.dgv_counts.Size = new System.Drawing.Size(1012, 356);
            this.dgv_counts.TabIndex = 10;
            // 
            // command
            // 
            this.command.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.command.HeaderText = "Command";
            this.command.Name = "command";
            this.command.ReadOnly = true;
            // 
            // count
            // 
            this.count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.count.HeaderText = "Count";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            // 
            // pb_quick
            // 
            this.pb_quick.Location = new System.Drawing.Point(97, 113);
            this.pb_quick.Name = "pb_quick";
            this.pb_quick.Size = new System.Drawing.Size(296, 23);
            this.pb_quick.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 510);
            this.Controls.Add(this.pb_quick);
            this.Controls.Add(this.dgv_counts);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.lbl_time_lbl);
            this.Controls.Add(this.pb_file_progress);
            this.Controls.Add(this.btn_output_browse);
            this.Controls.Add(this.btn_event_browse);
            this.Controls.Add(this.tb_output);
            this.Controls.Add(this.lbl_output_file);
            this.Controls.Add(this.tb_eventDir);
            this.Controls.Add(this.lbl_eventDir);
            this.Controls.Add(this.btn_run);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_counts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Label lbl_eventDir;
        private System.Windows.Forms.TextBox tb_eventDir;
        private System.Windows.Forms.Label lbl_output_file;
        private System.Windows.Forms.TextBox tb_output;
        private System.Windows.Forms.Button btn_event_browse;
        private System.Windows.Forms.Button btn_output_browse;
        private System.Windows.Forms.ProgressBar pb_file_progress;
        private System.Windows.Forms.Label lbl_time_lbl;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.DataGridView dgv_counts;
        private System.Windows.Forms.DataGridViewTextBoxColumn command;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.ProgressBar pb_quick;
    }
}

