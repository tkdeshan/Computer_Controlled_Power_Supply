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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;

namespace CCPS
{
    public partial class ccpsSetup : Form
    {
        string Voltage;
        string Current;
        string Power;
        String Resistor;
        public string vol;
        public string vol1;
        string x1;
        public String setVol;
        public String resistorPower;
        public String maxVol;
        double[] arrayvol = new double[300];
        double[] arraycur = new double[300];
        int con = 0;
        int i=0 ;
        int k=0;
        double pow;
        double resVal;
        double resVol;
        public ccpsSetup()
        {
            InitializeComponent();
            timer1.Start();   
        }

        private void ccpsSetup_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            groupBox7.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox6.Enabled= false;
            groupBox5.Enabled= false;

            btnClose.Visible= false;
            btnOpen.Visible = true;

            comboBoxPort.Text = "SELECT";
            comboBoxBaudRate.Text = "SELECT";

            string[] portLists = SerialPort.GetPortNames();
            comboBoxPort.Items.AddRange(portLists);          

            progressBar1.Value = 0;

            chart1.Titles.Add("Current (mA) Vs Voltage (V)");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBoxPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBoxBaudRate.Text);
                serialPort1.Open();

                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = false;
                groupBox5.Enabled = true;

                btnClose.Visible = true;
                btnOpen.Visible = false;

                progressBar1.Value = 100;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();

                    btnOpen.Enabled = true;
                    btnClose.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    groupBox7.Enabled = false;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = false;
                    groupBox6.Enabled = false;
                    groupBox5.Enabled = false;

                    btnClose.Visible = false;
                    btnOpen.Visible = true;

                    progressBar1.Value = 0;

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void ccpsSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Write("VC#");
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = true;
                    groupBox3.Enabled = true;
                    groupBox4.Enabled = false;
                    groupBox6.Enabled= false;
                    groupBox7.Enabled = false;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Write("CC#");
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = true;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = true;                   
                    groupBox6.Enabled = false;
                    groupBox7.Enabled = false;

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("RC#");
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = true;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;             
                groupBox6.Enabled = false;
                groupBox7.Enabled = true;        
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("DC#");
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                groupBox6.Enabled = true;
                groupBox7.Enabled = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            serialPort1.Write("S");
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox6.Enabled = false;
            groupBox7.Enabled = false;
            button6.Enabled = true;
            btn_ok.Enabled = true;

            label13.Text = " ";
            lblVoltage.Text = " ";
            lblCurrent.Text = " ";
            lblPower.Text = " ";
            textBoxVoltageCV.Text = " ";
            textBox1.Text = " ";
            txt_max_voltage.Text = " ";

            chart1.Series["ChartArea1"].Points.Clear();                

            for (int n = 0; n < arrayvol.Length; n++)
            {
                arrayvol[n] = 0;
            }



            for (int m = 0; m < arrayvol.Length; m++)
            {
                arraycur[m] = 0;
            }

            i = 0;
               
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

           if (serialPort1.IsOpen)
            {
                try
                {                  
                    lblVoltage.Text = Voltage;                 
                    lblCurrent.Text = Current;
                    lblPower.Text = Power;
                    label13.Text = Resistor;

                    if (con == 1)
                    {
                        double myvol = double.Parse(Voltage);
                        double mycurrent = double.Parse(Current);

                        arrayvol[i] = myvol;
                        arraycur[i] = mycurrent * 1000;
                        i++;
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
                      
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            timer1.Stop();
        }

        private void btnVltSetCV_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    if (Double.Parse(textBoxVoltageCV.Text) < 2 || Double.Parse(textBoxVoltageCV.Text) > 12 || String.IsNullOrEmpty(textBoxVoltageCV.Text))
                    {
                        MessageBox.Show("Plese Enter Correct format");
                    }
                    else
                    {
                        x1 = textBoxVoltageCV.Text;
                        serialPort1.Write(x1);
                        //textBoxVoltageCV.Text = " ";
                    }                        
                }
                
                
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Voltage = serialPort1.ReadLine();
            Current = serialPort1.ReadLine();
            Power = serialPort1.ReadLine();
            Resistor = serialPort1.ReadLine();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            if (serialPort1.IsOpen)
            {
                try
                {
                    if (Double.Parse(textBox1.Text) == 0.25 || Double.Parse(textBox1.Text) == 0.50 || Double.Parse(textBox1.Text) == 1.00 || String.IsNullOrEmpty(textBox1.Text))
                    {
                        resistorPower = textBox1.Text;
                        serialPort1.Write(resistorPower);                        
                    }
                    else
                    {
                        MessageBox.Show("Plese Enter Valid Power!");
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            btn_ok.Enabled = false;
           
            if (serialPort1.IsOpen)
            {
                try
                {
                    if (Double.Parse(txt_max_voltage.Text) < 2 || Double.Parse(txt_max_voltage.Text) > 12 ||  String.IsNullOrEmpty(txt_max_voltage.Text))
                    {                        
                        MessageBox.Show("Plese Enter Valid Voltage!");
                    }
                    else
                    {
                        maxVol = txt_max_voltage.Text;
                        serialPort1.Write(maxVol);
                        con = 1;
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void btn_graph_Click(object sender, EventArgs e)
        {
            con= 0;
          
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 0.2;
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.CornflowerBlue;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.CornflowerBlue;        
            chart1.Series["ChartArea1"].Color = Color.Red;
            
            for (k = 0; k < i; k++)
            {
                chart1.Series["ChartArea1"].Points.AddXY(arrayvol[k], arraycur[k]);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}    