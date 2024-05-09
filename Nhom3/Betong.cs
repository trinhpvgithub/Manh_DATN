using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom3
{
    internal class Betong
    {
        string max = "";
        double rb = 0;
        double rbt = 0;
        double e = 0;

        public string Max
        {
            get
            {
                return max;
            }

            set
            {
                max = value;
            }
        }

        public double Rb
        {
            get
            {
                return rb;
            }

            set
            {
                rb = value;
            }
        }

        public double Rbt
        {
            get
            {
                return rbt;
            }

            set
            {
                rbt = value;
            }
        }

        public double E
        {
            get
            {
                return e;
            }

            set
            {
                e = value;
            }
        }
    }
}
