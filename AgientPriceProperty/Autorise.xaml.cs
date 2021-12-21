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

namespace AgientPriceProperty
{
    /// <summary>
    /// Логика взаимодействия для Autorise.xaml
    /// </summary>
    public partial class Autorise : Page
    {
        PriceActiveEntities _context;
        public Autorise(PriceActiveEntities _context)
        {
            InitializeComponent();
            this._context = _context;
        }
        private void Input(object sender, RoutedEventArgs e)
        {
            int inputTableEmployeer = Convert.ToInt32(inputTabel.Text);
            string inputPasswordEmployer = inputPassword.Text;

            Employeer checkAutorise = _context.Employeer.ToList().Find(x => x.tabelNumber == inputTableEmployeer && x.password.Equals(inputPasswordEmployer));

            if (checkAutorise != null)
            {
                MessageBox.Show("Вы авторизованы");
                _context.TimeWork.Add(new TimeWork() { timeStart = DateTime.Now.TimeOfDay, dateWork = DateTime.Now.Date, timeFinish = DateTime.Now.TimeOfDay, tabelNumberPerson = checkAutorise.tabelNumber });
                _context.SaveChanges();
                if (checkAutorise.Position1.title.Equals("Бухгалтер"))
                {
                    MessageBox.Show($"Здравствуйте бухгалер : {checkAutorise.FIO}");
                    NavigationService.Navigate(new RecordersBook(_context));
                }
            }
            else
            {
                MessageBox.Show("Проверьте даные");
            }
        }
    }
}
