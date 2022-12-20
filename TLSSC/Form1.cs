using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace TLSSC
{
    public partial class Form1 : Form
    {

        SerialPort port;
        string getvalue;
        int[] data = new int[50000];
        int countdata = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach(string port in ports)
            {
                comboBox1.Items.Add(port);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread mastertread;
            mastertread = new Thread(runit);
            mastertread.Start();
            port = new SerialPort(comboBox1.Text, 9600);
            port.Open();
        //    countdata = 0;
        }

        void runit()
        {
            while (true)
            {
                try
                {
                    
                    if (port.IsOpen == true)
                    {
                        int i = 0;
                        while ( i<=50000)
                        {
                            getvalue = port.ReadLine();
                            data[i] = Convert.ToInt32(getvalue);
                            countdata++;
                            chart1.Invoke((MethodInvoker)(() => chart1.Series["R"].Points.AddXY(Convert.ToInt32(i), Convert.ToInt32(getvalue))));
                            i++;
                        }
                        
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("{0} Exception caught.", ex);
                }
            }
        }
        
        string path = @"C:\Users\user\Desktop\PROJECT\testing\test.csv";

        private void button2_Click(object sender, EventArgs e)
        {
            //var file = 
            
      /*      countdata = 0;
            for (int j = 0; j < 100; j++)
            {
                data[j] = 150 - j;
                countdata++;
            }*/
            // Set the variable "delimiter" to ", ".
            
            string delimiter = ", ";

            for (int i = 0; i < countdata; i++)
            {            
                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    string createText = "Time" + delimiter + "RGB" + Environment.NewLine;
                    File.WriteAllText(path, createText);
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                string appendText = (i+1) + "," + data[i] + Environment.NewLine;
                File.AppendAllText(path, appendText);
                
            }
            
            // add(data, k, "file.csv");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            port.Close();
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
            // restart the program
            Application.Restart();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            /*
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\\";      
            saveFileDialog1.Title = "Save text Files";
            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            
            {
                string name = saveFileDialog1.FileName;
                string delimiter = ", ";

                /*for (int i = 0; i < countdata; i++)
                {
                    // This text is added only once to the file.
                    if (!File.Exists(name))
                    {
                        // Create a file to write to.
                        string createText = "Time" + delimiter + "RGB" + Environment.NewLine;
                        File.WriteAllText(name, createText);
                    }

                    // This text is always added, making the file longer over time
                    // if it is not deleted.
                    string appendText = (i + 1) + "," + data[i] + Environment.NewLine;
                    File.AppendAllText(name, appendText);

                }
            }
            */

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV|*.csv";
            saveFileDialog1.Title = "Save an .csv file";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                string delimiter = ", ";

                for (int i = 0; i < countdata; i++)
                {
                    // This text is added only once to the file.

                    if (!File.Exists(saveFileDialog1.FileName))
                    {
                        // Create a file to write to.
                        string createText = "Time" + delimiter + "RGB" + Environment.NewLine;
                        File.WriteAllText(saveFileDialog1.FileName, createText);
                    }


                    // This text is always added, making the file longer over time
                    // if it is not deleted.
                    string appendText = (i + 1) + "," + data[i] + Environment.NewLine;
                    File.AppendAllText(saveFileDialog1.FileName, appendText);

                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = getvalue;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
