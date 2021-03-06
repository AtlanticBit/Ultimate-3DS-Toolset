﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Ultimate_3DS_Toolset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string HexAsciiConvert(string hex)
        {

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= hex.Length - 2; i += 2)
            {

                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2),

                System.Globalization.NumberStyles.HexNumber))));

            }

            return sb.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //This save file dialog allows you to choose where to save the keyx bin file
           
            if(String.IsNullOrEmpty(textBox1.Text))
            {
                //lol
            } else {
                //This downloads the keyx from a certain webpage, then writes it to a bin file
                progressBar1.Value = 25;
                StreamWriter writer = new StreamWriter(textBox1.Text);
                WebClient downloader = new WebClient();
                StreamReader reader = new StreamReader(downloader.OpenRead("http://govanify.com/3DS_0x25_KEYX.html"));
                progressBar1.Value = 50;
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(' ');
                    if (line[0] == "keyX:")
                    {
                        string win = HexAsciiConvert(line[1]);
                        writer.Write(win);
                    }
                }
                progressBar1.Value = 90;
                writer.Close();
                progressBar1.Value = 100;
                MessageBox.Show("slot0x25keyX.bin successfully saved!", "SAVED");
                progressBar1.Value = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult rekt = saveFileDialog1.ShowDialog();
            if (rekt == DialogResult.OK)
            {
                textBox1.Text = saveFileDialog1.FileName;
            }
        }
    }
    static class StringExtensions
    {

        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

    }
}
