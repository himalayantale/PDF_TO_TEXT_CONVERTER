using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Text.RegularExpressions;


namespace eBook_Reader
{
    public partial class Form1 : Form
    {
        string filename; 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Start Reading eBook //
            if (textBox1.Text == "")
            {
                
                MessageBox.Show(" Please Enter a location","PDF to TEXT Converter");
            }
            
            else if (File.Exists(filename))
            {
                try
                {
                    StringBuilder text = new StringBuilder();
                    PdfReader pdfReader = new PdfReader(filename);
                    progressBar1.Maximum = pdfReader.NumberOfPages;
                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                        text.Append(System.Environment.NewLine);
                        text.Append("\n Page Number:" + page);
                        text.Append(System.Environment.NewLine);
                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                        text.Append(currentText);
                        pdfReader.Close();
                        progressBar1.Value++;

                    }
                    pdftext.Text += text.ToString();
                    progressBar1.Value = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: "+ ex.Message, "Error");
                }
            }
            else
            {
                pdftext.Text = "Error: eBook does not exist at the specified location";
            }
            // End Reading eBook //
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "eBook (*.pdf)|*.pdf";
            openFileDialog1.ShowDialog();
            
            string filelocation = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
            textBox1.Text = filelocation;
            filename = filelocation;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void creditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveAsTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if ((result == DialogResult.OK) && (saveFileDialog1.FileName.Length > 0))
            {
                string filelocation = saveFileDialog1.InitialDirectory + saveFileDialog1.FileName;
                System.IO.File.WriteAllText(filelocation, pdftext.Text);
                MessageBox.Show("File Saved!", "PDF to TEXT Converter");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //code to search a phrase
            string search = textBox2.Text;
            //var regex = new Regex(search);
            if (Regex.IsMatch(pdftext.Text, search))
            {
                MessageBox.Show("Word/Phrase Found!", "Search Result");
                //Code to handle matched word.
            }
            else
            {
                MessageBox.Show("Word/Phrase Not Found!", "Search Result");
            }

        }
       
       
        
    }
}
