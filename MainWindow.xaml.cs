using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NUnit;

namespace LawXPDF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FileStream fs = new FileStream("Chapter1_Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();



            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DirectoryInfo d = new DirectoryInfo(filepath);

            foreach (var file in Directory.GetFiles(filepath, "*.jpg"))
            {
                var image = iTextSharp.text.Image.GetInstance(file);
                doc.Add(image);
            }
            doc.Close();

        }
    }
}
