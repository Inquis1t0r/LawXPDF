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
using Microsoft.WindowsAPICodePack.Dialogs;
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

           
            

            

        }

        private void debugExit(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }



        private void generatePDF(object sender, RoutedEventArgs e)
        {

            string strPath = Environment.GetFolderPath(
            System.Environment.SpecialFolder.DesktopDirectory);

            String fileName = strPath + "/Zdjęcia_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            Document doc = new Document();
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = strPath;
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
              //  MessageBox.Show("Wybrany folder: " + dialog.FileName);
            }

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg";
            foreach (string file in Directory.GetFiles(dialog.FileName, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(System.IO.Path.GetExtension(s).ToLower())))
            {
                var image = iTextSharp.text.Image.GetInstance(file);

                PdfPCell cell = new PdfPCell(image, true);
                cell.Border = iTextSharp.text.Rectangle.TOP_BORDER;
                table.AddCell(cell);

            }

            doc.Add(table);

            doc.Close();
            MessageBox.Show("Plik PDF zapisany na pulpicie.");
        }
    }
}
