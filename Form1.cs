using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Windows.Forms;
using IronPdf;
using System.IO;

namespace splitPdf
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        public string splice(string item, int index)
        {
            string result = "";
            for (int i = index; i < item.Length; i++)
            {
                result += item[i];
            }
            return result;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string Path = "";
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    Path = openFile.FileName;
                }
                PdfSharp.Pdf.PdfDocument pdf = PdfReader.Open(Path, PdfDocumentOpenMode.Import);

                for (int i = 0; i < pdf.PageCount; i++)
                {


                    PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();
                    document.AddPage(pdf.Pages[i]);

                    MemoryStream stream = new MemoryStream();
                    document.Save(stream);
                    byte[] pdfBytes = stream.ToArray();

                    IronPdf.PdfDocument pdfDocument = new IronPdf.PdfDocument(pdfBytes);

                    string Text = pdfDocument.ExtractAllText();
                    string[] lines = Text.Split('\n');
                    foreach (string line in lines)
                    {
                        string item = "";
                        for (int j = 0; j < line.Length; j++)
                        {
                            item += line[j];
                            if (item == txtWord.Text)
                            {
                                string fileName = line.Substring(j, (line.Length - j));
                                fileName = fileName.TrimEnd('\r', '\n');
                                if (fileName[1] == '0')
                                {
                                    fileName = splice(fileName, 2);
                                }
                                PdfSharp.Pdf.PdfDocument FinalFile = new PdfSharp.Pdf.PdfDocument();
                                FinalFile.AddPage(pdf.Pages[i]);
                                FinalFile.Save(txtPath.Text + "\" + fileName.ToString() + ".pdf");
                            }
                        }
                    }
                }
                MessageBox.Show("Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
