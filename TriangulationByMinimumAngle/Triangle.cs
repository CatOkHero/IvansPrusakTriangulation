using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangulationByMinimumAngle
{
    public class Triangle
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public List<int> boudns { get; set; }

        public Triangle()
        {
                
        }

        public Triangle(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}]",A,B,C);
        }


        public bool DoesContain(int point)
        {
            if (point == A || point == B || point == C) return true;
            else return false;
        }

        public int this[int i]
        {
            get
            {
                if (i == 0) return A;
                else if (i == 1) return B;
                else return C;
            }
            set
            {
                if (i == 0) A = value;
                else if (i == 1) B = value;
                else C = value;
            }
        }
    }
}
