using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2
{
    public partial class MainForm : Form
    {
        private bool _right = true;
        private bool _left = true;
        private Hall _hall;
        private SynchronizationContext SC;
        private Thread hallThread;

        public MainForm()
        {
            InitializeComponent();
            SC = SynchronizationContext.Current;
            CtrlDGVHall.ColumnCount = 3;
            CtrlDGVHall.RowCount = 3;
        }

        private void CtrlButStart_Click(object sender, EventArgs e)
        {
            Client.lb = (int)CtrlNUDLeftBoundary.Value;
            Client.rb = (int)CtrlNUDRightBoundary.Value;
            if (_right && _left)
            {
                _hall = new Hall((int)CtrlNUDATMs.Value);
                Console.WriteLine("Новый таск");
                _hall.ReloadData += OnDataChange;
                hallThread = new Thread(_hall.StartClientThread);
                hallThread.Start(SC);
                //_taskHall = Task.Factory.StartNew(_hall.StartClientThread);
            }
        }

        private void CtrlButStop_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Sas");
            MessageBox.Show("sas");
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
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CtrlDGVHall.Rows[j].Cells[i].Value = Arr[j, i];
                }
            }
        }
    }
}
