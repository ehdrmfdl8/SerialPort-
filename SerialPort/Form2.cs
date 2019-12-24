using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPort
{
    public partial class Form2 : Form
    {
        public string portName;
        public int baudRate;
        public int dataBits;


        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            baudRate = int.Parse(comboBox1.Text);
            dataBits = int.Parse(comboBox2.Text);
            portName = comboBox5.Text;

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "9600";
            comboBox2.SelectedItem = "8";
            comboBox5.SelectedItem = "COM12";
        }
    }
}
