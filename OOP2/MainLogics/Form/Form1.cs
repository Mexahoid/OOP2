using System;
using System.Threading;
using System.Windows.Forms;

namespace OOP2
{
    public partial class MainForm : Form
    {
        #region Поля

        /// <summary>
        /// Удовлетворяет ли верхняя граница.
        /// </summary>
        private bool _right = true;

        /// <summary>
        /// Удовлетворяет ли нижняя граница.
        /// </summary>
        private bool _left = true;

        /// <summary>
        /// Экземпляр зала с банкоматами.
        /// </summary>
        private Hall _hall;

        /// <summary>
        /// Поток выполнения зала с банкоматами.
        /// </summary>
        private Thread _hallThread;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void CtrlButStart_Click(object sender, EventArgs e)
        {
            CtrlDGVHall.ColumnCount = (int)CtrlNUDATMs.Value;
            CtrlDGVHall.RowCount = 3;
            Client.lb = (int)CtrlNUDLeftBoundary.Value;
            Client.rb = (int)CtrlNUDRightBoundary.Value;
            if (_right && _left)
            {
                _hall = new Hall((int)CtrlNUDATMs.Value, OnDataChange);
                _hallThread = new Thread(_hall.StartClientThread);
                _hallThread.Start(SynchronizationContext.Current);
            }
        }

        private void CtrlButStop_Click(object sender, EventArgs e)
        {
            _hallThread.Abort();
            _hall = null;
            CtrlDGVHall.ColumnCount = 0;
        }

        private void CtrlNUDLeftBoundary_ValueChanged(object sender, EventArgs e)
        {
            CtrlNUDRightBoundary.Minimum = CtrlNUDRightBoundary.Value;

            if (CtrlNUDRightBoundary.Value % 10 != 0)
            {
                MessageBox.Show("Нельзя вводить цены, не кратные 10");
                _left = false;
            }
            else
                _left = true;
        }

        private void CtrlNUDRightBoundary_ValueChanged(object sender, EventArgs e)
        {
            if (CtrlNUDRightBoundary.Value % 10 != 0)
            {
                MessageBox.Show("Нельзя вводить цены, не кратные 10");
                _right = false;
            }
            else
                _right = true;
        }

        private void CtrlNUDThreshold_ValueChanged(object sender, EventArgs e)
        {
            ATM.threshold = (int)CtrlNUDThreshold.Value;
        }

        private void OnDataChange(string[,] Arr)
        {
            for (int i = 0; i < (int)CtrlNUDATMs.Value; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CtrlDGVHall.Rows[j].Cells[i].Value = Arr[j, i];
                }
            }
        }
    }
}
