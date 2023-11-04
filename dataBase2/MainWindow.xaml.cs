using System;
using System.Collections.Generic;
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
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text.Json;

namespace dataBase2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<People_database> people_database = new List<People_database>();
        Salary_database salary_Database;
        Dictionary<int, Salary_database> salaryDatabaseDictionary = new Dictionary<int, Salary_database>();
        string path1 = "C:\\Users\\elise\\source\\repos\\database2\\database2\\Info\\People database.txt";
        string path2 = "C:\\Users\\elise\\source\\repos\\database2\\database2\\Info\\Salary database.txt";
        public MainWindow()
        {
            InitializeComponent();
            outputOnDisplay();
        }
        
        public void vivod()
        {
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string selectedSurname = comboBox.SelectedItem.ToString();

                int personnelNumber = people_database.FirstOrDefault(x => x.SurName == selectedSurname)?.Personnel_number ?? -1;

                if (personnelNumber != -1 && salaryDatabaseDictionary.ContainsKey(personnelNumber))
                {
                    Salary_database salaryDatabase = salaryDatabaseDictionary[personnelNumber];
                    JanuaryLabel.Content = salaryDatabase.MonthlySalaries[0];
                    FebruaryLabel.Content = salaryDatabase.MonthlySalaries[1];
                    MarchLabel.Content = salaryDatabase.MonthlySalaries[2];
                    AprilLabel.Content = salaryDatabase.MonthlySalaries[3];
                    MayLabel.Content = salaryDatabase.MonthlySalaries[4];
                    JuneLabel.Content = salaryDatabase.MonthlySalaries[5];
                    JulyLabel.Content = salaryDatabase.MonthlySalaries[6];
                    AugustLabel.Content = salaryDatabase.MonthlySalaries[7];
                    SeptemberLabel.Content = salaryDatabase.MonthlySalaries[8];
                    OctoberLabel.Content = salaryDatabase.MonthlySalaries[9];
                    NovemberLabel.Content = salaryDatabase.MonthlySalaries[10];
                    DecemberLabel.Content = salaryDatabase.MonthlySalaries[11];

                    textBoxSurName.Text = people_database[personnelNumber].SurName;
                    textBoxJanuary.Text = salaryDatabase.MonthlySalaries[0].ToString();
                    textBoxFebruary.Text = salaryDatabase.MonthlySalaries[1].ToString();
                    textBoxMarch.Text = salaryDatabase.MonthlySalaries[2].ToString();
                    textBoxApril.Text = salaryDatabase.MonthlySalaries[3].ToString();
                    textBoxMay.Text = salaryDatabase.MonthlySalaries[4].ToString();
                    textBoxJune.Text = salaryDatabase.MonthlySalaries[5].ToString();
                    textBoxJuly.Text = salaryDatabase.MonthlySalaries[6].ToString();
                    textBoxAugust.Text = salaryDatabase.MonthlySalaries[7].ToString();
                    textBoxSeptember.Text = salaryDatabase.MonthlySalaries[8].ToString();
                    textBoxOctober.Text = salaryDatabase.MonthlySalaries[9].ToString();
                    textBoxNovember.Text = salaryDatabase.MonthlySalaries[10].ToString();
                    textBoxDecember.Text = salaryDatabase.MonthlySalaries[11].ToString();
                }

            }
        }
        public void outputOnDisplay()
        {
            string[] lines1 = File.ReadAllLines(path1);
            string[] lines2 = File.ReadAllLines(path2);

            foreach (string s in lines1)
            {
                string[] lines = s.Split(";");
                int personnel_number = int.Parse(lines[0]);
                string surname = string.Format(lines[1]);
                people_database.Add(new People_database(personnel_number, surname));
                comboBox.Items.Add(surname);
            }
            foreach (string s in lines2)
            {
                string[] lines = s.Split(";");
                int personnel_number = int.Parse(lines[0]);
                salary_Database = new Salary_database(personnel_number);
                for (int i = 0; i < 12; i++)
                {
                    salary_Database.MonthlySalaries.Add(int.Parse(lines[i + 1]));

                }
                salaryDatabaseDictionary[personnel_number] = salary_Database;
            }
        }

    private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vivod();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            comboBox.Items.Clear();

            if (string.IsNullOrEmpty(textBoxSurName.Text) || string.IsNullOrEmpty( textBoxJanuary.Text) || string.IsNullOrEmpty(textBoxFebruary.Text) ||
                string.IsNullOrEmpty(textBoxMarch.Text) || string.IsNullOrEmpty(textBoxApril.Text) || string.IsNullOrEmpty(textBoxMay.Text) ||
                string.IsNullOrEmpty(textBoxJune.Text) || string.IsNullOrEmpty(textBoxJuly.Text) || string.IsNullOrEmpty(textBoxAugust.Text) ||
                string.IsNullOrEmpty(textBoxSeptember.Text) || string.IsNullOrEmpty(textBoxOctober.Text) || string.IsNullOrEmpty(textBoxNovember.Text) ||
                string.IsNullOrEmpty (textBoxDecember.Text))
            {
                MessageBox.Show("Заполните данные");
                return;
            }
            int id = 0;
            if (people_database.Count > 0)
            {
                id = people_database.Max(p => p.Personnel_number) + 1;
            }
            string text = $"{id};{textBoxSurName.Text}";
            string textSalary = $"{id};{textBoxJanuary.Text};{textBoxFebruary.Text};{textBoxMarch.Text};{textBoxApril.Text};{textBoxMay.Text};{textBoxJune.Text};" +
                $"{textBoxJuly.Text};{textBoxAugust.Text};{textBoxSeptember.Text};{textBoxOctober.Text};{textBoxNovember.Text};{textBoxDecember.Text}";
            List<string> lines1 = new List<string>(File.ReadAllLines(path1));
            List<string> lines2 = new List<string>(File.ReadAllLines(path2));
            if (!File.Exists(path1))
            {
                File.WriteAllTextAsync(path1, text, Encoding.UTF8);
            }
            if (!File.Exists(path2))
            {
                File.WriteAllTextAsync(path2, textSalary, Encoding.UTF8);
            }
            lines1.Add(text);
            lines2.Add(textSalary);
            //File.WriteAllLines(path1, lines1, Encoding.UTF8);
            //File.WriteAllLines(path2, lines2, Encoding.UTF8);
            File.WriteAllText("C:\\Users\\elise\\source\\repos\\dataBase2\\dataBase2\\Info\\People.json", JsonSerializer.Serialize(lines1),Encoding.UTF8);
            File.WriteAllText("C:\\Users\\elise\\source\\repos\\dataBase2\\dataBase2\\Info\\Salary.json", JsonSerializer.Serialize(lines1),Encoding.UTF8);
            outputOnDisplay();
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = comboBox.SelectedIndex;
            comboBox.Items.Clear();
            List<string> lines1 = new List<string>(File.ReadAllLines(path1));
            List<string> lines2 = new List<string>(File.ReadAllLines(path2));
            if (selectedIndex != null)
            {
                string text = $"{selectedIndex};{textBoxSurName.Text}";
                string textSalary = $"{selectedIndex};{textBoxJanuary.Text};{textBoxFebruary.Text};{textBoxMarch.Text};{textBoxApril.Text};{textBoxMay.Text};{textBoxJune.Text};" +
                    $"{textBoxJuly.Text};{textBoxAugust.Text};{textBoxSeptember.Text};{textBoxOctober.Text};{textBoxNovember.Text};{textBoxDecember.Text}";
                lines1[selectedIndex] = text;
                lines2[selectedIndex] = textSalary;
                File.WriteAllLines(path1, lines1);
                File.WriteAllLines(path2, lines2);
                File.WriteAllText("C:\\Users\\elise\\source\\repos\\dataBase2\\dataBase2\\Info\\People.json", JsonSerializer.Serialize(lines1), Encoding.UTF8);
                File.WriteAllText("C:\\Users\\elise\\source\\repos\\dataBase2\\dataBase2\\Info\\Salary.json", JsonSerializer.Serialize(lines1), Encoding.UTF8);

                people_database.Clear();
                outputOnDisplay();
            }

        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = comboBox.SelectedIndex;
            List<string> lines1 = new List<string>(File.ReadAllLines(path1));
            List<string> lines2 = new List<string>(File.ReadAllLines(path2));
            if (selectedIndex != null)
            {
                comboBox.Items.Clear();
                people_database.RemoveAt(selectedIndex);
                salaryDatabaseDictionary.Remove(selectedIndex);
                lines1.RemoveAt(selectedIndex);
                lines2.RemoveAt(selectedIndex);
                File.WriteAllLines(path1, lines1);
                File.WriteAllLines(path2, lines2);
                File.WriteAllText("C:\\Users\\elise\\source\\repos\\dataBase2\\dataBase2\\Info\\People.json", JsonSerializer.Serialize(lines1), Encoding.UTF8);
                File.WriteAllText("C:\\Users\\elise\\source\\repos\\dataBase2\\dataBase2\\Info\\Salary.json", JsonSerializer.Serialize(lines1), Encoding.UTF8);

                JanuaryLabel.Content = null;
                FebruaryLabel.Content = null;
                MarchLabel.Content = null;
                AprilLabel.Content = null;
                MayLabel.Content = null;
                JuneLabel.Content = null;
                JulyLabel.Content = null;
                AugustLabel.Content = null;
                SeptemberLabel.Content = null;
                OctoberLabel.Content = null;
                NovemberLabel.Content = null;
                DecemberLabel.Content = null;
                outputOnDisplay();

            }
        }
    }
}
