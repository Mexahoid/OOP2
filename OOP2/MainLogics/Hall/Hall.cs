using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2
{
    class Hall
    {
        private List<ATM> _atms;

        public Hall(int ATMCount, int Threshold, int LeftBoundary, int RightBoundary)
        {
            for (int i = 0; i < ATMCount; i++)
            {
                _atms.Add(new ATM(Threshold));
            }
        }
    }
}
