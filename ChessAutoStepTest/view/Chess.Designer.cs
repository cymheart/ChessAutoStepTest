namespace ChessAutoStepTest
{
    partial class Chess
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.setting = new System.Windows.Forms.Panel();
            this.cmdRecord = new System.Windows.Forms.Panel();
            this.listBoxRecord = new System.Windows.Forms.ListBox();
            this.board = new ChessAutoStepTest.view.BoardControl();
            this.btnPlay = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.setting.SuspendLayout();
            this.cmdRecord.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdRecord, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.22222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.77778F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(779, 582);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.44836F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.55164F));
            this.tableLayoutPanel2.Controls.Add(this.setting, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.board, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(773, 391);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // setting
            // 
            this.setting.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.setting.Controls.Add(this.btnPlay);
            this.setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setting.Location = new System.Drawing.Point(590, 0);
            this.setting.Margin = new System.Windows.Forms.Padding(0);
            this.setting.Name = "setting";
            this.setting.Size = new System.Drawing.Size(183, 391);
            this.setting.TabIndex = 1;
            // 
            // cmdRecord
            // 
            this.cmdRecord.Controls.Add(this.listBoxRecord);
            this.cmdRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdRecord.Location = new System.Drawing.Point(0, 397);
            this.cmdRecord.Margin = new System.Windows.Forms.Padding(0);
            this.cmdRecord.Name = "cmdRecord";
            this.cmdRecord.Size = new System.Drawing.Size(779, 185);
            this.cmdRecord.TabIndex = 1;
            // 
            // listBoxRecord
            // 
            this.listBoxRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxRecord.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxRecord.FormattingEnabled = true;
            this.listBoxRecord.ItemHeight = 21;
            this.listBoxRecord.Items.AddRange(new object[] {
            " "});
            this.listBoxRecord.Location = new System.Drawing.Point(0, 0);
            this.listBoxRecord.Name = "listBoxRecord";
            this.listBoxRecord.Size = new System.Drawing.Size(779, 185);
            this.listBoxRecord.TabIndex = 0;
            // 
            // board
            // 
            this.board.BackColor = System.Drawing.SystemColors.Control;
            this.board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board.Location = new System.Drawing.Point(3, 3);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(584, 385);
            this.board.TabIndex = 2;
            this.board.SizeChanged += new System.EventHandler(this.board_SizeChanged);
            this.board.Paint += new System.Windows.Forms.PaintEventHandler(this.board_Paint);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(39, 190);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "开始游戏";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // Chess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 582);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Chess";
            this.Text = "ChessAutoStep";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.setting.ResumeLayout(false);
            this.cmdRecord.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel setting;
        private System.Windows.Forms.Panel cmdRecord;
        private System.Windows.Forms.ListBox listBoxRecord;
        private view.BoardControl board;
        private System.Windows.Forms.Button btnPlay;
    }
}

