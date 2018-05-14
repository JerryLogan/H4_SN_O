using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace H4_SN_O
{
    public partial class Form1 : Form
    {
        String lineline;
        String[] template_content_262 = new string[262];
        String[] SN_split_16 = new String[16];
        String SN_str;
        String desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        String folder_path;


        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e) //select template
        {
            int line_count =0;
            folder_path = desktop_path + @"\H4_SN_0";
            DirectoryInfo di = Directory.CreateDirectory(folder_path);
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr_txt = new StreamReader(openFileDialog1.FileName);
                //line_count = 0;


                while ((lineline = sr_txt.ReadLine()) != null)
                {
                    //Console.WriteLine(line_count + "\t" + lineline);
                    template_content_262[line_count] = lineline;
                    textBox1.Text += template_content_262[line_count];
                    textBox1.Text += "\r\n";
                    Console.WriteLine(line_count + "\t" + lineline);
                    line_count++;
                }
            }
            //Console.WriteLine(line_count);
        }

        private void button2_Click(object sender, EventArgs e) //gen
        {
            textBox2.Clear();
            textBox3.Clear();
            #region get SN via barzode
            SN_str = textBox_SN_1.Text;
            byte[] bytes = Encoding.ASCII.GetBytes(SN_str); // string to dec
            
            String[] SN_split_16 = new String[16];
            for (int i = 0; i < 16; i++)
            {
                SN_split_16[i] = bytes[i].ToString("X");
                //Console.WriteLine((SN_split_16[i])+"\t");
            }
            #endregion

            #region modify txt
            for (int i = 0; i < 262; i++)
            {
                textBox3.Text += template_content_262[i];
                textBox3.Text += "\r\n";

                if (i == 50 || i == 52 || i == 54 || i == 56 ||
                    i == 58 || i == 60 || i == 62 || i == 64 ||
                    i == 66 || i == 68 || i == 70 || i == 72 ||
                    i == 74 || i == 76 || i == 78 || i == 80)
                {
                    String addr = "";
                    String data_str = "";


                    switch (i)
                    {
                        //year
                        case 50:
                            addr = "30"; data_str = SN_split_16[0];
                            break;

                        //week
                        case 52:
                            addr = "32"; data_str = SN_split_16[1];
                            break;
                        case 54:
                            addr = "34"; data_str = SN_split_16[2];
                            break;

                        //foxlink
                        case 56:
                            addr = "36"; data_str = SN_split_16[3];
                            break;
                        //Arena Model Number
                        case 58:
                            addr = "38"; data_str = SN_split_16[4];
                            break;
                        case 60:
                            addr = "3A"; data_str = SN_split_16[5];
                            break;
                        case 62:
                            addr = "3C"; data_str = SN_split_16[6];
                            break;
                        case 64:
                            addr = "3E"; data_str = SN_split_16[7];
                            break;

                        //Arena
                        case 66:
                            addr = "40"; data_str = SN_split_16[8];
                            break;
                        case 68:
                            addr = "42"; data_str = SN_split_16[9];
                            break;
                        //flag

                        case 70:
                            addr = "44"; data_str = SN_split_16[10];
                            break;

                        //serial number
                        case 72:    //萬
                            addr = "46"; data_str = SN_split_16[11];
                            break;
                        case 74:    //千
                            addr = "48"; data_str = SN_split_16[12];
                            break;
                        case 76:    //百
                            addr = "4A"; data_str = SN_split_16[13];
                            break;
                        case 78:    //十
                            addr = "4C"; data_str = SN_split_16[14];
                            break;
                        case 80:    //個 
                            addr = "4E"; data_str = SN_split_16[15];
                            break;
                    }
                    textBox2.Text += addr + "\t";
                    textBox2.Text += data_str;
                    textBox2.Text += "\t00000000";
                    textBox2.Text += "\r\n";
                }
                else
                {
                    textBox2.Text += template_content_262[i];
                    textBox2.Text += "\r\n";
                }
            }
            #endregion
            System.IO.File.WriteAllText(folder_path + @"\" + SN_str + ".txt", textBox2.Text);
        }
    }
}
