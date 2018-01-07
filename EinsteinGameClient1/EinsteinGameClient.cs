using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EinsteinGameClient1
{
    public partial class EinsteinGameClient : Form
    {
        public EinsteinGameClient()
        {
            InitializeComponent();
        }

        private void button1_Click(System.Object sender, System.EventArgs e)
        {
            EinsteinGameClient1.EinsteinGameServiceReference.EinsteinGameServiceClient client = new EinsteinGameClient1.EinsteinGameServiceReference.EinsteinGameServiceClient();
            string returnString = String.Empty;

            try
            {
                int number = Int32.Parse(textBox1.Text);
                EinsteinGameClient1.EinsteinGameServiceReference.EinsteinGameDto result = client.RunGame(number);
                returnString = string.Join(",", result.OutPut.ToArray()); 
                textBox2.Text = returnString;
                textBox2.ForeColor = System.Drawing.Color.Black;
            }
            catch
            {

                textBox2.Text = "Pleace insert a valid number";
                textBox2.ForeColor = System.Drawing.Color.Red;

            }
        }
    }
}
