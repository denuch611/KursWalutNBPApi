using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursWalutNBPApi
{
    public partial class KursyWalut : Form
    {
        public KursyWalut()
        {
            InitializeComponent();
            comboBox1.Items.Add("USD - dolar amerykański");
            comboBox1.Items.Add("AUD - dolar australijski");
            comboBox1.Items.Add("CAD - dolar kanadyjski");
            comboBox1.Items.Add("NZD - dolar nowozelandzki");
            comboBox1.Items.Add("EUR - euro");
            comboBox1.Items.Add("CHF - frank szwajcarski");
            comboBox1.Items.Add("GBP - funt szterling");
            comboBox1.Items.Add("UAH - hrywna ");
            comboBox1.Items.Add("JPY - jen(Japonia)");
            comboBox1.Items.Add("NOK - korona norweska");
            comboBox1.Items.Add("SEK - korona szwedzka");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;


        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                textBox6.AppendText(comboBox1.Text);
                comboBox2.Items.Add("zł -> " + comboBox1.Text);
                comboBox2.Items.Add(comboBox1.Text + " -> zł");
            }
            else
            {
                textBox6.Clear();
                
                comboBox2.Items.Add("zł -> " + comboBox1.Text);
                comboBox2.Items.Add(comboBox1.Text + " ->zł");
                textBox6.AppendText(comboBox1.Text);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
