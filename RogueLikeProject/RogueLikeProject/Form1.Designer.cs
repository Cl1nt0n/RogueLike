
namespace RogueLikeProject
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerShot = new System.Windows.Forms.Timer(this.components);
            this.playerHealthBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.labelBountyCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.BackColor = System.Drawing.Color.White;
            this.pictureBoxMap.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(800, 800);
            this.pictureBoxMap.TabIndex = 0;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMap_Paint);
            this.pictureBoxMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_MouseClick);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 7;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerShot
            // 
            this.timerShot.Interval = 2;
            this.timerShot.Tick += new System.EventHandler(this.timerShot_Tick);
            // 
            // playerHealthBar
            // 
            this.playerHealthBar.BackColor = System.Drawing.Color.Maroon;
            this.playerHealthBar.ForeColor = System.Drawing.Color.Red;
            this.playerHealthBar.Location = new System.Drawing.Point(40, 818);
            this.playerHealthBar.MarqueeAnimationSpeed = 1000;
            this.playerHealthBar.Name = "playerHealthBar";
            this.playerHealthBar.Size = new System.Drawing.Size(294, 23);
            this.playerHealthBar.TabIndex = 4;
            this.playerHealthBar.Value = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 818);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "HP";
            // 
            // labelBountyCount
            // 
            this.labelBountyCount.AutoSize = true;
            this.labelBountyCount.Location = new System.Drawing.Point(799, 818);
            this.labelBountyCount.Name = "labelBountyCount";
            this.labelBountyCount.Size = new System.Drawing.Size(13, 13);
            this.labelBountyCount.TabIndex = 6;
            this.labelBountyCount.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 882);
            this.Controls.Add(this.labelBountyCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playerHealthBar);
            this.Controls.Add(this.pictureBoxMap);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerShot;
        private System.Windows.Forms.ProgressBar playerHealthBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBountyCount;
    }
}

