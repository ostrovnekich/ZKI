using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace ZKI
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        int[] kvadrat = { 17, 24, 1, 8, 15, 23, 5, 7, 14, 16, 4, 6, 13, 20, 22, 10, 12, 19, 21, 3, 11, 18, 25, 2, 9 };
        char[] shifr = new char[25];
        char[] slovo1 = new char[25];
        string result = "";
        int pos;
        private void button1_Click(object sender, EventArgs e)
        {
            string slovo = textBox1.Text;
            for (int i = 0; i < slovo.Length; i++)
            {
                slovo1[i] = slovo[i];
            }
            for (int i = 0; i < kvadrat.Length; i++)
            {
                for (int j = 0; j < kvadrat.Length; j++)
                {
                    if (kvadrat[i] == j + 1)
                    {
                        shifr[kvadrat[i] - 1] = slovo1[i];
                    }
                }
            }
            string str = new string(shifr);
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != '\0')
                {
                    result += str[i];
                }
            }
            for (int i = 0; i < result.Length/2; i++)
            {
                richTextBox1.Text += result[i];
                pos = i;
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(saveFileDialog.FileName, str);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            richTextBox2.Clear();
            richTextBox1.Clear();
            string slovo = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    if (File.Exists(filePath))
                    {
                        slovo = File.ReadAllText(filePath);
                    }
                }
            }
            for (int i = 0; i < slovo.Length; i++)
            {
                if (slovo[i] != '\0')
                slovo1[i] = slovo[i];
            }
            for (int i = 0; i < kvadrat.Length; i++)
            {
                for (int j = 0; j < kvadrat.Length; j++)
                {
                    if (kvadrat[i] == j + 1)
                    {
                        shifr[i] = slovo1[kvadrat[i] - 1];
                    }
                }
            }
            string str = new string(shifr);
            richTextBox2.Text = str;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) && char.IsLower(e.KeyChar) && e.KeyChar >= 'а' && e.KeyChar <= 'я'))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = pos + 1; i < result.Length; i++)
            {
                richTextBox1.Text += result[i];
            }
        }
    }
}
