using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {

        private void btnSaveColorsToCSV_Click(object sender, RoutedEventArgs e)
        {
            // Színek kiírása fájlba CSV formátumban
            string filePath = "colors.csv";
            File.WriteAllLines(filePath, colorList);
        }

        private ObservableCollection<string> colorList = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();

            // Slider inicializálása
            InitializeSlider(sliderRed);
            InitializeSlider(sliderGreen);
            InitializeSlider(sliderBlue);

            // Listbox adatkötése az ObservableCollection-hez
            listBoxColors.ItemsSource = colorList;
        }

        private void InitializeSlider(Slider slider)
        {
            slider.Minimum = 0;
            slider.Maximum = 255;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Szín kikeverése a sliderek értékeiből
            Color mixedColor = Color.FromRgb((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);

            // Színminta frissítése
            rectangleColor.Fill = new SolidColorBrush(mixedColor);
        }

        private void btnAddColor_Click(object sender, RoutedEventArgs e)
        {
            // Szín hozzáadása a listához
            string colorString = $"({(int)sliderRed.Value},{(int)sliderGreen.Value},{(int)sliderBlue.Value})";
            colorList.Add(colorString);
        }

        private void btnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            // Színek kiírása fájlba
            string filePath = "colors.csv";
            File.WriteAllLines(filePath, colorList);
        }

        private void btnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            // Színek beolvasása fájlból
            string filePath = "colors.csv";
            if (File.Exists(filePath))
            {
                colorList.Clear();
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    colorList.Add(line);
                }
            }
        }

        private void listBoxColors_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Dupla kattintás eseménykezelése
            string selectedColorString = listBoxColors.SelectedItem as string;
            if (selectedColorString != null)
            {
                // Szín kiválasztása a listából
                var values = selectedColorString.Trim('(', ')').Split(',').Select(int.Parse).ToArray();
                sliderRed.Value = values[0];
                sliderGreen.Value = values[1];
                sliderBlue.Value = values[2];
            }
        }
    }
}
