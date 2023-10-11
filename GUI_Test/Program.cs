using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Form form1 = new Form();
            //Form form2 = new Form();
            //form1.Text = "Form passed to Run()";
            //form2.Text = "Second form";
            //form1.Cursor = Cursors.WaitCursor;
            //Application.Run(form1);
            //form2.Show();
            //MessageBox.Show("Application.Run() has returned control back to Main.Bye,bye!", "TwoForms");
            Form f = new Form();
            f.Load += new EventHandler(f_Load);
            Application.Run(f);
        }
        private static void f_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Hello");
        }
    }
}
