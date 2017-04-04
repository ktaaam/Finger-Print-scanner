using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DummyFinger
{
    public class FingerPrintArgs : EventArgs
    {
        public FingerPrintArgs(byte[] print)
        {
            this.print = print;
        }

        public byte[] getPrint()
        {
            return this.print;
        }

        byte[] print;
    }

    public class FingerPrintEvent:Form
    {
        public delegate void FingerPrintEventHandler(object sender, FingerPrintArgs e);

        public event FingerPrintEventHandler FingerPrintDetect;

        public FingerPrintEvent()
        {
            this.KeyDown += new KeyEventHandler(Detect_finger);
        }

        protected void SendFingerPrintEvent(byte[] print)
        {
            FingerPrintArgs args = new FingerPrintArgs(print);
            FingerPrintDetect(this, args);
        }

        private void Detect_finger(object sender, KeyEventArgs e)
        {
            byte[] output = BitConverter.GetBytes(e.KeyValue);
            //int output = e.KeyValue;
            SendFingerPrintEvent(output);
        }

    }
}
