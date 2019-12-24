using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;

namespace SerialPort
{

    public partial class Form1 : Form
    {
        
        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = "C:\\Users\\youdong\\Desktop\\아톰-자바스크립트\\캡스톤\\로그위치기록";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //richTextBox1.Text = "Incomming Data: ";
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            serialPort1.Open();

            if (checkBox1.Checked == true)
            {
                MessageBox.Show("로그파일 기록");
                
            }

            else
            {
                MessageBox.Show("기록 안함");
            }
            
        }
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp = (System.IO.Ports.SerialPort)sender;

            string indata = sp.ReadLine();
            if (checkBox1.Checked == true)
            {
                WriteLog(indata);
            }
            // Show all the incoming data in the port's buffer
            //Console.WriteLine(port.ReadExisting());
            richTextBox1.Text = indata;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            MessageBox.Show("시리얼 데이터 Stop");
            richTextBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("파일 내용 삭제");
            WriteLog(" ");
            serialPort1.Close();
            serialPort1.Dispose();
            richTextBox1.Clear();
            string logFilePath = "C:\\Users\\youdong\\Desktop\\아톰-자바스크립트\\캡스톤\\로그위치기록";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            if (System.IO.File.Exists(logFilePath))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(logFilePath);
                }
                catch
                {
                    MessageBox.Show("error");
                    return;
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try {
                string logFilePath = "C:\\Users\\youdong\\Desktop\\아톰-자바스크립트\\캡스톤\\로그위치기록";
                logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
                using (StreamReader sr = new StreamReader(logFilePath))
                {
                    string line;
                    line = sr.ReadToEnd();
                    richTextBox2.Text = line;
                }
                
            }
            catch(OutOfMemoryException)
            {
                MessageBox.Show("메모리가 부족합니다");
            }
            catch(IOException)
            {
               // MessageBox.Show("I/O 에러");
                richTextBox2.Clear();
            }

        }

        private void serialPort설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Dlg = new Form2();
            Dlg.ShowDialog();
            serialPort1.PortName = Dlg.portName;
            serialPort1.BaudRate = Dlg.baudRate;
            serialPort1.DataBits = Dlg.dataBits;
            Dlg.Dispose();

            Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\youdong\Desktop\아톰-자바스크립트\캡스톤\google.html");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var x = richTextBox2.Text;
            //var x = "RECV:8008ad:-91:$GPRMC,011044.000,A,3514.6897,N,12841.6107,E,0.98,89.98,300119,,,A*58 RECV:801017:-24:$GPRMC,011044.000,A,3514.6897,N,12841.6107,E,0.98,89.98,300119,,,A*58";

            //ID: 8008ad 
            var GPSdata = x.Substring(x.LastIndexOf("8008ad"));
            var parsedata = GPSdata.Split(',');
            if (parsedata[2] == "A")
            {
                float latitude = float.Parse(parsedata[3]);
                float longitude = float.Parse(parsedata[5]);
                float speed = float.Parse(parsedata[7]);
                var lat = Math.Round((Math.Floor(latitude / 100) + (latitude - Math.Floor(latitude / 100) * 100) / 60), 6);
                var lon = Math.Round((Math.Floor(longitude / 100) + (longitude - Math.Floor(longitude / 100) * 100) / 60), 6);
                var spd = Math.Round((speed * 0.514444), 6);
                textBox1.Text = lat.ToString();
                textBox2.Text = lon.ToString();
                textBox3.Text = spd.ToString();
            }
            else
            {
                textBox1.Text = "N/A";
                textBox2.Text = "N/A";
                textBox3.Text = "N/A";
            }

            //ID: 801017
            var GPSdata1 = x.Substring(x.LastIndexOf("801017"));
            var parsedata1 = GPSdata1.Split(',');
            
            if(parsedata1[2] == "A")
            {
                float latitude1 = float.Parse(parsedata1[3]);
                float longitude1 = float.Parse(parsedata1[5]);
                float speed1 = float.Parse(parsedata1[7]);
                var lat1 = Math.Round((Math.Floor(latitude1 / 100) + (latitude1 - Math.Floor(latitude1 / 100) * 100) / 60), 6);
                var lon1 = Math.Round((Math.Floor(longitude1 / 100) + (longitude1 - Math.Floor(longitude1 / 100) * 100) / 60), 6);
                var spd1 = Math.Round((speed1 * 0.514444), 6);
                textBox4.Text = lat1.ToString();
                textBox5.Text = lon1.ToString();
                textBox6.Text = spd1.ToString();
            }
            else
            {
                textBox4.Text = "N/A";
                textBox5.Text = "N/A";
                textBox6.Text = "N/A";
            }
        }
    }
}
