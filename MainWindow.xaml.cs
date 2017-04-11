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

namespace DateCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TimeSpan interval = new TimeSpan(0);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AppLoaded(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now.Date;
            dpBeginDateRange.SelectedDate = date;
            dpEndDateRange.SelectedDate = date;
            dpBeginDate.SelectedDate = date;
            lbResultIntervalCalc.Content = date.ToShortDateString();
        }

    

        private void CalcDaysClicked(object sender, RoutedEventArgs e)
        {
            DateTime dateB = (DateTime)dpBeginDateRange.SelectedDate;
            DateTime dateE = (DateTime)dpEndDateRange.SelectedDate;
            interval = dateE - dateB; // Получаем разницу между датами

            SelectedIndexCB(cbTimeInterval.SelectedIndex);
        }

        private void CalcDateClicked(object sender, RoutedEventArgs e)
        {
            DateTime dateB = (DateTime)dpBeginDate.SelectedDate;
            try
            {
                double days = Double.Parse(Interval.Text.ToString());
                lbResultIntervalCalc.Content = dateB.AddDays(days).ToShortDateString();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message,"Внимание!",MessageBoxButton.OK,MessageBoxImage.Warning);
            }

        }

        private void cbTimeInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedIndexCB(cbTimeInterval.SelectedIndex);
        }

        private void SelectedIndexCB(int index)
        {
            switch (index)
            {
                case 0: lbResultDateCalc.Content = interval.Days +
                    CalculateSuffix(Math.Abs(interval.Days % 100), new string[] { " день", " дня", " дней" });
                    break;
                case 1: lbResultDateCalc.Content = interval.TotalHours +
                    CalculateSuffix(Math.Abs(interval.Days % 100), new string[] { " час", " часа", " часов" });
                    break;
                case 2: lbResultDateCalc.Content = interval.TotalMinutes +
                    CalculateSuffix(Math.Abs(interval.Days % 100), new string[] { " минута", " минуты", " минут" });
                    break;
                case 3: lbResultDateCalc.Content = interval.TotalSeconds +
                    CalculateSuffix(Math.Abs(interval.Days % 100), new string[] { " секунда", " секунды", " секунд" });
                    break;

            }
        }


        private string CalculateSuffix(int lastTwoDigits, string[] suffix)
        {
            // Выделяем последнюю цифру для правильного вывода дней, дня, день
            int lastDigit = Math.Abs(lastTwoDigits % 10);
            // Выделяем последне две цифры для обработки исключения 
            if (lastTwoDigits > 10 && lastTwoDigits < 15)  // ислючение для чисел 11, 12, 13, 14.
            {
                return (suffix[2]);
            }
            else
            {
                switch (lastDigit) // правило для обычных случаев
                {
                    case 1: return (suffix[0]);
                    case 2:
                    case 3:
                    case 4: return (suffix[1]);
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 0: return (suffix[2]);
                    default: return "";
                }
            }
            
        }
       
    }
}
