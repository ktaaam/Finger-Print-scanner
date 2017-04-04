using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class FingerPrint
    {
        private int id;
        private string fp;

        public FingerPrint(int id, string fp)
        {
            this.id = id;
            this.fp = fp;
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string fingerPrint
        {
            get
            {
                return this.fp;
            }
            set
            {
                this.fp = value;
            }
        }
    }
}
