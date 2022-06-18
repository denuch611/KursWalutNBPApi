using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.InteropServices;


namespace KursWalutNBPApi
{
    public partial class KursyWalut : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // height of ellipse
          int nHeightEllipse // width of ellipse
      );
        public class Rates
        {
            public double mid { get; set; }
            public string Currency { get; set; }
            public string Code { get; set; }
            public IList<Rates>? rates { get; set; }
        }
        public KursyWalut()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

            comboBox1.Items.Add("USD - dolar amerykański");
            comboBox1.Items.Add("AUD - dolar australijski");
            comboBox1.Items.Add("CAD - dolar kanadyjski");
            comboBox1.Items.Add("NZD - dolar nowozelandzki");
            comboBox1.Items.Add("EUR - euro");
            comboBox1.Items.Add("CHF - frank szwajcarski");
            comboBox1.Items.Add("GBP - funt szterling");
            comboBox1.Items.Add("UAH - hrywna ");
            comboBox1.Items.Add("JPY - jen(Japonia)");
            comboBox1.Items.Add("HUF - forint Węgry");
            comboBox1.Items.Add("CZK - korona czeska");
            comboBox1.Items.Add("DKK - korona duńska ");
            comboBox1.Items.Add("ISK - korona islandzka");
            comboBox1.Items.Add("NOK - korona norweska");
            comboBox1.Items.Add("SEK - korona szwedzka");
            comboBox1.Items.Add("NOK - korona norweska");
            comboBox1.Items.Add("SEK - korona szwedzka");

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox3.ReadOnly = true;
            textBox3.Enabled = false;
        }
        private async void button2_Click(object sender, EventArgs e)

        {
            comboBox2.Items.Add("Kalkulator");
            {

                var url = "http://api.nbp.pl/api/.";
                var table = "A";
                var code = comboBox1.Text.Substring(0, 3);
                var date1 = DateTime.Today;
                date1.Day.ToString();
                date1.DayOfWeek.ToString();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (date1.DayOfWeek.ToString() is "Saturday")
                {
                    var Rdate = DateTime.Today.AddDays(-1);
                    textBox3.Text = Rdate.ToString("yyyy-MM-dd");
                }
                else if (date1.DayOfWeek.ToString() is "Sunday")
                {
                    var Rdate = DateTime.Today.AddDays(-2);
                    textBox3.Text = Rdate.ToString("yyyy-MM-dd");
                }
                else
                {
                    var Rdate = DateTime.Today;
                    textBox3.Text = Rdate.ToString("yyyy-MM-dd");
                }

                    HttpResponseMessage response = await client.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/{table}/{code}/{textBox3.Text}/");
                    if (response.IsSuccessStatusCode)
                    {
                        var Waluta = await response.Content.ReadAsStringAsync();
                        var rates = JsonConvert.DeserializeObject<Rates>(Waluta);
                        foreach (var item in rates.rates)
                        {

                            if (string.IsNullOrEmpty(textBox6.Text))
                            {
                                textBox6.AppendText(item.mid.ToString());
                                comboBox2.Items.Clear();
                                comboBox3.Items.Clear();
                                comboBox2.Items.Add("ZŁOTY -> " + comboBox1.Text);
                                comboBox3.Items.Add(comboBox1.Text + " ->ZŁOTY");
                            }
                            else
                            {
                                textBox6.Clear();
                                comboBox2.Items.Clear();
                                comboBox3.Items.Clear();
                                comboBox2.Items.Add("ZŁOTY -> " + comboBox1.Text);
                                comboBox3.Items.Add(comboBox1.Text + " ->ZŁOTY");
                                textBox6.AppendText(item.mid.ToString());
                            }
                        }
                    }
                }
            }
        

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            if (comboBox3.Items.Count > 0)
            {
                double SetPrice = (double)Convert.ToDecimal(textBox2.Text);
                double SetValueToConvert = (double)Convert.ToDecimal(textBox6.Text);
                var suma = Math.Round(SetPrice * SetValueToConvert, 4);
                textBox1.AppendText(suma.ToString());
            }
            else if (comboBox2.Items.Count > 0)
            {
                double SetPrice = (double)Convert.ToDecimal(textBox2.Text);
                double SetValueToConvert = (double)Convert.ToDecimal(textBox6.Text);
                var suma = Math.Round(SetPrice / SetValueToConvert, 4);
                textBox1.AppendText(suma.ToString());
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            textBox1.Clear();
            textBox6.Clear();
            textBox3.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void KursyWalut_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void KursyWalut_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        Point lastPoint;
    }
    }

     