using System;
using System.Windows.Forms;

namespace OOP2
{
    partial class MainForm
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
            this.CtrlDGVHall = new System.Windows.Forms.DataGridView();
            this.CtrlLblNum = new System.Windows.Forms.Label();
            this.CtrlLblHelp = new System.Windows.Forms.Label();
            this.CtrlButStart = new System.Windows.Forms.Button();
            this.CtrlButStop = new System.Windows.Forms.Button();
            this.CtrlLblBounds = new System.Windows.Forms.Label();
            this.CtrlLblLeftBoundary = new System.Windows.Forms.Label();
            this.CtrlLblRightBoundary = new System.Windows.Forms.Label();
            this.CtrlLblThreshold = new System.Windows.Forms.Label();
            this.CtrlNUDLeftBoundary = new System.Windows.Forms.NumericUpDown();
            this.CtrlNUDRightBoundary = new System.Windows.Forms.NumericUpDown();
            this.CtrlNUDThreshold = new System.Windows.Forms.NumericUpDown();
            this.CtrlTBATMs = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlDGVHall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlNUDLeftBoundary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlNUDRightBoundary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlNUDThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlTBATMs)).BeginInit();
            this.SuspendLayout();
            // 
            // CtrlDGVHall
            // 
            this.CtrlDGVHall.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CtrlDGVHall.Location = new System.Drawing.Point(12, 88);
            this.CtrlDGVHall.Name = "CtrlDGVHall";
            this.CtrlDGVHall.Size = new System.Drawing.Size(496, 339);
            this.CtrlDGVHall.TabIndex = 0;
            // 
            // CtrlLblNum
            // 
            this.CtrlLblNum.AutoSize = true;
            this.CtrlLblNum.Location = new System.Drawing.Point(12, 9);
            this.CtrlLblNum.Name = "CtrlLblNum";
            this.CtrlLblNum.Size = new System.Drawing.Size(133, 13);
            this.CtrlLblNum.TabIndex = 2;
            this.CtrlLblNum.Text = "Количество банкоматов:";
            // 
            // CtrlLblHelp
            // 
            this.CtrlLblHelp.AutoSize = true;
            this.CtrlLblHelp.Location = new System.Drawing.Point(467, 9);
            this.CtrlLblHelp.Name = "CtrlLblHelp";
            this.CtrlLblHelp.Size = new System.Drawing.Size(50, 13);
            this.CtrlLblHelp.TabIndex = 3;
            this.CtrlLblHelp.Text = "Помощь";
            this.CtrlLblHelp.Click += new System.EventHandler(this.CtrlLblHelp_Click);
            // 
            // CtrlButStart
            // 
            this.CtrlButStart.Location = new System.Drawing.Point(433, 31);
            this.CtrlButStart.Name = "CtrlButStart";
            this.CtrlButStart.Size = new System.Drawing.Size(75, 23);
            this.CtrlButStart.TabIndex = 4;
            this.CtrlButStart.Text = "Старт";
            this.CtrlButStart.UseVisualStyleBackColor = true;
            this.CtrlButStart.Click += new System.EventHandler(this.CtrlButStart_Click);
            // 
            // CtrlButStop
            // 
            this.CtrlButStop.Location = new System.Drawing.Point(433, 60);
            this.CtrlButStop.Name = "CtrlButStop";
            this.CtrlButStop.Size = new System.Drawing.Size(75, 23);
            this.CtrlButStop.TabIndex = 5;
            this.CtrlButStop.Text = "Стоп";
            this.CtrlButStop.UseVisualStyleBackColor = true;
            this.CtrlButStop.Click += new System.EventHandler(this.CtrlButStop_Click);
            // 
            // CtrlLblBounds
            // 
            this.CtrlLblBounds.AutoSize = true;
            this.CtrlLblBounds.Location = new System.Drawing.Point(168, 9);
            this.CtrlLblBounds.Name = "CtrlLblBounds";
            this.CtrlLblBounds.Size = new System.Drawing.Size(124, 13);
            this.CtrlLblBounds.TabIndex = 7;
            this.CtrlLblBounds.Text = "Запрашиваемые цены:";
            // 
            // CtrlLblLeftBoundary
            // 
            this.CtrlLblLeftBoundary.AutoSize = true;
            this.CtrlLblLeftBoundary.Location = new System.Drawing.Point(170, 33);
            this.CtrlLblLeftBoundary.Name = "CtrlLblLeftBoundary";
            this.CtrlLblLeftBoundary.Size = new System.Drawing.Size(23, 13);
            this.CtrlLblLeftBoundary.TabIndex = 9;
            this.CtrlLblLeftBoundary.Text = "От:";
            // 
            // CtrlLblRightBoundary
            // 
            this.CtrlLblRightBoundary.AutoSize = true;
            this.CtrlLblRightBoundary.Location = new System.Drawing.Point(170, 64);
            this.CtrlLblRightBoundary.Name = "CtrlLblRightBoundary";
            this.CtrlLblRightBoundary.Size = new System.Drawing.Size(25, 13);
            this.CtrlLblRightBoundary.TabIndex = 10;
            this.CtrlLblRightBoundary.Text = "До:";
            // 
            // CtrlLblThreshold
            // 
            this.CtrlLblThreshold.AutoSize = true;
            this.CtrlLblThreshold.Location = new System.Drawing.Point(306, 9);
            this.CtrlLblThreshold.Name = "CtrlLblThreshold";
            this.CtrlLblThreshold.Size = new System.Drawing.Size(104, 13);
            this.CtrlLblThreshold.TabIndex = 11;
            this.CtrlLblThreshold.Text = "Порог отключения:";
            // 
            // CtrlNUDLeftBoundary
            // 
            this.CtrlNUDLeftBoundary.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CtrlNUDLeftBoundary.Location = new System.Drawing.Point(199, 31);
            this.CtrlNUDLeftBoundary.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CtrlNUDLeftBoundary.Name = "CtrlNUDLeftBoundary";
            this.CtrlNUDLeftBoundary.Size = new System.Drawing.Size(72, 20);
            this.CtrlNUDLeftBoundary.TabIndex = 13;
            this.CtrlNUDLeftBoundary.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CtrlNUDLeftBoundary.ValueChanged += new System.EventHandler(this.CtrlNUDLeftBoundary_ValueChanged);
            // 
            // CtrlNUDRightBoundary
            // 
            this.CtrlNUDRightBoundary.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CtrlNUDRightBoundary.Location = new System.Drawing.Point(199, 62);
            this.CtrlNUDRightBoundary.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.CtrlNUDRightBoundary.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CtrlNUDRightBoundary.Name = "CtrlNUDRightBoundary";
            this.CtrlNUDRightBoundary.Size = new System.Drawing.Size(72, 20);
            this.CtrlNUDRightBoundary.TabIndex = 14;
            this.CtrlNUDRightBoundary.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CtrlNUDRightBoundary.ValueChanged += new System.EventHandler(this.CtrlNUDRightBoundary_ValueChanged);
            // 
            // CtrlNUDThreshold
            // 
            this.CtrlNUDThreshold.Location = new System.Drawing.Point(309, 31);
            this.CtrlNUDThreshold.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.CtrlNUDThreshold.Name = "CtrlNUDThreshold";
            this.CtrlNUDThreshold.Size = new System.Drawing.Size(101, 20);
            this.CtrlNUDThreshold.TabIndex = 15;
            this.CtrlNUDThreshold.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.CtrlNUDThreshold.ValueChanged += new System.EventHandler(this.CtrlNUDThreshold_ValueChanged);
            // 
            // CtrlTBATMs
            // 
            this.CtrlTBATMs.Location = new System.Drawing.Point(15, 31);
            this.CtrlTBATMs.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.CtrlTBATMs.Name = "CtrlTBATMs";
            this.CtrlTBATMs.Size = new System.Drawing.Size(73, 20);
            this.CtrlTBATMs.TabIndex = 16;
            this.CtrlTBATMs.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 439);
            this.Controls.Add(this.CtrlTBATMs);
            this.Controls.Add(this.CtrlNUDThreshold);
            this.Controls.Add(this.CtrlNUDRightBoundary);
            this.Controls.Add(this.CtrlNUDLeftBoundary);
            this.Controls.Add(this.CtrlLblThreshold);
            this.Controls.Add(this.CtrlLblRightBoundary);
            this.Controls.Add(this.CtrlLblLeftBoundary);
            this.Controls.Add(this.CtrlLblBounds);
            this.Controls.Add(this.CtrlButStop);
            this.Controls.Add(this.CtrlButStart);
            this.Controls.Add(this.CtrlLblHelp);
            this.Controls.Add(this.CtrlLblNum);
            this.Controls.Add(this.CtrlDGVHall);
            this.Name = "MainForm";
            this.Text = "Зал баблометов";
            ((System.ComponentModel.ISupportInitialize)(this.CtrlDGVHall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlNUDLeftBoundary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlNUDRightBoundary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlNUDThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CtrlTBATMs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CtrlDGVHall;
        private System.Windows.Forms.Label CtrlLblNum;
        private System.Windows.Forms.Label CtrlLblHelp;


        private void CtrlLblHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("4.	 Смоделировать работу зала с банкоматами. " +
                "В зале N (задается в программе) банкоматов. Люди заходят в зал случайным образом," +
                " интервалы времени между появлением покупателей распределены по нормальному закону. " +
                "Когда клиенты подходят к банкоматам, они занимают" +
                " очередь в случайный банкомат. Длительность обслуживания постоянна. Визуализировать работу " +
                "зала(как минимум кол - во людей в зале и на каждом банкомате)." +
                " Поведение каждого клиента и каждого банкомата реализуется в отдельном" +
                " потоке(соответствующим образом должны быть описаны классы клиента и " +
                "банкомата). Когда клиент становится к банкомату (вызывается соответствующий метод " +
                "«Взять бабло»), поток клиента блокируется до момента, пока банкомат не освободится" +
                "(синхронизация с банкоматом с помощью нужного EventWaitHandle). Задержки выдачи денег " +
                "реализуется с помощью Thread.Sleep(). " +
                "При желании можно воспользоваться классами из пространства имен System.Threading.Tasks.", "Информация");
        }

        private Button CtrlButStart;
        private Button CtrlButStop;
        private Label CtrlLblBounds;
        private Label CtrlLblLeftBoundary;
        private Label CtrlLblRightBoundary;
        private Label CtrlLblThreshold;
        private NumericUpDown CtrlNUDLeftBoundary;
        private NumericUpDown CtrlNUDRightBoundary;
        private NumericUpDown CtrlNUDThreshold;
        private NumericUpDown CtrlTBATMs;
    }
}

