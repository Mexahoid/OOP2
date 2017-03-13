using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2
{
    public partial class MainForm : Form
    {
        private bool _right = false;
        private bool _left = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void CtrlButStart_Click(object sender, EventArgs e)
        {
            Client.lb = (int)CtrlNUDLeftBoundary.Value;
            Client.rb = (int)CtrlNUDRightBoundary.Value;
        }

        private void CtrlButStop_Click(object sender, EventArgs e)
        {

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
    }
}
