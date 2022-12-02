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
using System.ComponentModel;
using System.Windows.Markup;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace Practicum_10
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += WindowClosing;
            application.Quit();
        }
        private void StartClearClick(object sender, RoutedEventArgs e)
        {
            const string message = "Вы уверены, что желаете очистить запись?";
            const string caption = "Form cleaning";
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ResultDoubleNumber.Clear();
                ResultNumber.Clear();
                InputDoubleNumber.Clear();
                InputNumber.Clear();
            }
        }

        Excel.Application application = new Excel.Application();

        private void GetCalculationClick(object sender, RoutedEventArgs e)
        {
            Excel.Workbook document = application.Workbooks.Add(Type.Missing);
            try
            {
                if (InputDoubleNumber.Text.Contains("arcsin"))
                {
                    var arcsinus = application.WorksheetFunction.Asin(Convert.ToDouble(InputDoubleNumber.Text.Substring(7, InputDoubleNumber.Text.Length - 8)));
                    ResultDoubleNumber.Text += Math.Round(arcsinus, 4);
                }
                if (InputDoubleNumber.Text.Contains("arccos"))
                {
                    var arccosinus = application.WorksheetFunction.Acos(Convert.ToDouble(InputDoubleNumber.Text.Substring(7, InputDoubleNumber.Text.Length - 8)));
                    ResultDoubleNumber.Text += Math.Round(arccosinus, 4);
                }
                if (Regex.IsMatch(InputNumber.Text, "[A-Z]"))
                {
                    var arabicNumbers = application.WorksheetFunction.Arabic(InputNumber.Text);
                    ResultNumber.Text += arabicNumbers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"User error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            const string message = "Вы уверены, что хотите закрыть приложение?";
            const string caption = "Form closing";
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
