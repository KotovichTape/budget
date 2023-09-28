using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budget
{
    internal class Data
    {
        public int cost;
        private bool _isIncome;
        private int ORIGINAL_VALUE;
        public string Name { get; set; }
        public int Cost
        {
            get
            {
                if (MainWindow.isSave)
                {
                    return ORIGINAL_VALUE;
                }
                else
                {
                    if (cost < 0)
                    {
                        return -cost;
                    }
                    else
                    {
                        return cost;
                    }
                }
            }
            set
            {
                ORIGINAL_VALUE = value;
                cost = value;
                if (ORIGINAL_VALUE > 0)
                {
                    isIncome = true;
                }
                else
                {
                    isIncome = false;
                }
            }

        }
        public string Type { get; set; }
        public bool isIncome
        {
            get
            {
                return _isIncome;
            }
            private set
            {
                _isIncome = value;
            }
        }

        public string Date;

        public Data(string _name, int _cost, string _type, string date)
        {
            Name = _name;
            Type = _type;
            Cost = _cost;
            Date = date;
        }
    } 
}
