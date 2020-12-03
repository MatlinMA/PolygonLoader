using System;
using System.Collections.Generic;
using System.Windows;

namespace PolygonLoader
{
    /// <summary>
    /// Логика взаимодействия для PointsCount.xaml
    /// </summary>
    public partial class PointsCount : Window
    {
        public PointsCount()
        {
            InitializeComponent();
            DataContext = MainWindow.VM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int space;
            if (Int32.TryParse(ui_PointsCountTextBox.Text, out space))
            {
                var tempList = new List<Point>();

                if (space >= MainWindow.VM.PointsList.Count)
                {
                    tempList.Add(MainWindow.VM.PointsList[0]);
                    MainWindow.VM.PointsList = tempList;
                }
                else
                {
                    for (int i = 0; i < MainWindow.VM.PointsList.Count; i = i + space)
                        tempList.Add(MainWindow.VM.PointsList[i]);

                    MainWindow.VM.PointsList = tempList;
                }

                MessageBox.Show("Количество точек полигона обновлено");
            }
            else
                MessageBox.Show("Ошибка ввода параметра");
        }

        private void ui_PointsCountTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

    }
}
