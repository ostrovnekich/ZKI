using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
namespace ZKI
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        string cezarEncrypt(int key, string message)
        {
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] newAlphabet = new char[alphabet.Length];
            int j = 0;
            for (int i = 0; i < alphabet.Length; i++) 
            {
                if (i + key < alphabet.Length)
                {
                    newAlphabet[i] = alphabet[i + key];
                }
                else 
                {
                    newAlphabet[i] = alphabet[j];
                    j++;
                }
            }

            char[] input = new char[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                for (int k = 0; k < alphabet.Length; k++)
                {
                    if (message[i] == alphabet[k])
                    {
                        input[i] = newAlphabet[k];
                    }
                }
            }
            string inputEncrypt = new string(input);
            return inputEncrypt;
        }
        int key;
        private void button1_Click(object sender, EventArgs e)
        {
            key = int.Parse(textBox1.Text);
        }
        string key2;
        private void button2_Click(object sender, EventArgs e)
        {
            key2 = cezarEncrypt(key, textBox2.Text);
            label3.Text = $"ключ для Виженера: {key2}";
        }

        string vizhenerEncrypt(string key, string message)
        {
            char[,] square = new char[26, 26];
            for (int i = 0; i < square.GetLength(0); i++)
            {
                for (int j = 0; j < square.GetLength(1); j++)
                {
                    square[i, j] = cezarEncrypt(i, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")[j];
                }
            }
            char[] row = new char[message.Length];
            int a = 0;
            for (int i = 0; i < message.Length; i++)
            {
                if (a >= key.Length)
                {
                    a = 0;
                }
                row[i] = key[a];
                a++;
            }
            string result = "";
            int col = 0;
            int line = 0;
            for (int i = 0; i < message.Length; i++)
            {
                for (int j = 0; j < square.GetLength(0); j++)
                {
                    if (message[i] == square[0, j])
                    {
                        line = j;
                    }
                    if (row[i] == square[0, j])
                    {
                        col = j;
                    }
                }
                result += square[col, line];
            }
            return result;
        }

        string vizhenerDecrypt(string key, string encryptedMessage)
        {
            char[,] square = new char[26, 26];
            for (int i = 0; i < square.GetLength(0); i++)
            {
                for (int j = 0; j < square.GetLength(1); j++)
                {
                    square[i, j] = cezarEncrypt(i, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")[j];
                }
            }

            char[] row = new char[encryptedMessage.Length];
            int a = 0;
            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                if (a >= key.Length)
                {
                    a = 0;
                }
                row[i] = key[a];
                a++;
            }

            string result = "";
            int col = 0;
            int line = 0;

            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                for (int j = 0; j < square.GetLength(0); j++)
                {
                    if (row[i] == square[j, 0])
                    {
                        line = j;
                    }
                    if (encryptedMessage[i] == square[line, j])
                    {
                        col = j;
                        break;
                    }
                }
                result += square[0, col];
            }

            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = vizhenerEncrypt(key2, richTextBox1.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(saveFileDialog.FileName, richTextBox2.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = vizhenerDecrypt(key2, richTextBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    if (File.Exists(filePath))
                    {
                        richTextBox2.Text = File.ReadAllText(filePath);
                    }
                }
            }

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}