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
using System.Security.Cryptography;
using System.Diagnostics.Tracing;

namespace DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string sKey;  // записываем ключ
        private void button1_Click(object sender, EventArgs e)
        {
            sKey = textBox1.Text;

            //условие выбора файла которое необходимо будет зашифровать
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "des files |*.des";   //формат сохранения

                // место выбора где будет сохранен файл
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // переменная куда будет записываться зашифрованный текст из файла
                    string destination = saveFileDialog1.FileName;
                    //само шифрование
                    EncryptFile(source, destination, sKey);

                }

            }

        }
        private void EncryptFile(string sourse, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(sourse, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypt = new FileStream(destination, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                // вектор инициализации
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

                // ICryptoTransform базовые операции криптографических преобразование
                // CreateDecryptor создает симметрические объекты шифровования с тек. ключами 
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypt, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];

                //   fsInput производит чтение блока байт 
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);

                // cryptoStream записывает последовательность байтов в текущий cryptoStream
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();

               }
            // если ключ будет больше или меньше 8 символов или ключ будет не верный 
            catch
            {
                MessageBox.Show("ошибка");
                return;
            }
            fsInput.Close();
            fsEncrypt.Close();
        }

        private void DecryptFile(string sourse, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(sourse, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypt = new FileStream(destination, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                // вектор инициализации
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

                // ICryptoTransform базовые операции криптографических преобразование
                // CreateDecryptor создает симметрические объекты шифровования с тек. ключами 
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypt, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];

                //   fsInput производит чтение блока байт 
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);

                // cryptoStream записывает последовательность байтов в текущий cryptoStream
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();

            }
            // если ключ будет больше или меньше 8 символов или ключ будет не верный 
            catch
            {
                MessageBox.Show("ошибка");
                return;
            }
            fsInput.Close();
            fsEncrypt.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sKey = textBox1.Text;

            //позволяет находить файл с разрешением
            openFileDialog1.Filter = "des files |*.des";
            //условие выбора файла которое необходимо будет зашифровать
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt files |*.txt";   //формат сохранения

                // место выбора где будет сохранен файл
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // переменная куда будет записываться зашифрованный текст из файла
                    string destination = saveFileDialog1.FileName;
                    //само шифрование
                    DecryptFile(source, destination, sKey);

                }

            }
        }
    }

}
