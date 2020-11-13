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

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "des files |*.des";   

             
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                   
                    string destination = saveFileDialog1.FileName;
                    
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
                
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

                
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypt, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];

              
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);

                
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();

               }
            
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
               
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

               
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypt, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
 
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);

              
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();

            }
        
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

            openFileDialog1.Filter = "des files |*.des";
          
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt files |*.txt";   

              
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    
                    string destination = saveFileDialog1.FileName;
                    
                    DecryptFile(source, destination, sKey);

                }

            }
        }
    }

}
