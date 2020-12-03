using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;


namespace PolygonLoader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public PolygonLoaderViewModel VM = new PolygonLoaderViewModel();
        public bool fileCreating = false;
        Thread thread = null; 

        public MainWindow()
        {
            InitializeComponent();
            ui_ServiceComboBox.Items.Add(VM.OpenStreetMaps.Name);
            ui_ServiceComboBox.SelectedIndex = 0;
            DataContext = VM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fileCreating == false)
            {
                if (ui_ServiceComboBox.SelectedValue.ToString() == VM.OpenStreetMaps.Name)
                {
                    VM.PointsList = VM.OpenStreetMaps.GetPointsList(ui_AddressField.Text);
                }
            }
            else
                MessageBox.Show("Идёт сохранение файла с координатами");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (fileCreating == false)
            {
                fileCreating = true;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Книга Excel (*.xlsx)|*.xlsx";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (saveFileDialog.ShowDialog() == true)
                {
                    thread = new Thread(new ParameterizedThreadStart(SaveFile));
                    thread.Start(saveFileDialog.FileName);
                }
            }
            else
                MessageBox.Show("Идёт сохранение файла с координатами");
        }

        private void SaveFile(object Path)
        {
            var excelApp = new Excel.Application();
            excelApp.Visible = false;
            excelApp.Workbooks.Add();
            excelApp.DisplayAlerts = false;
            Excel.Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            var row = 1;
            workSheet.Cells[row, "A"] = "Широта";
            workSheet.Cells[row, "B"] = "Долгота";
            workSheet.Cells[row, "C"] = "Данные верны";

            foreach (var point in VM.PointsList)
            {
                row++;
                workSheet.Cells[row, "A"] = point.Latitude;
                workSheet.Cells[row, "B"] = point.Longitude;
                workSheet.Cells[row, "C"] = point.IsRight;
            }

            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();

            workSheet.SaveAs((string)Path);
            excelApp.Workbooks.Close();
            excelApp.Quit();

            MessageBox.Show("Файл с точками полигона создан!");
            fileCreating = false;

            thread.Abort();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (fileCreating == false)
            {
                var pointsWindow = new PointsCount();
                if (pointsWindow.IsActive == false)
                    pointsWindow.Show();
            }
            else
                MessageBox.Show("Идёт сохранение файла с координатами");
        }
    }
}
