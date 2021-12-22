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
    /// Логика взаимодействия для RecordersBook.xaml
    /// </summary>
    public partial class RecordersBook : Page
    {
        PriceActiveEntities _context;
        int priceHour = 200;
        int doublePriceHour = 400;
        public RecordersBook(PriceActiveEntities _context)
        {
            InitializeComponent();
            this._context = _context;
            CheckTimeAndPay();
        }

        public void CheckTimeAndPay()
        {
            List<TimeWork> timeWorks = _context.TimeWork.ToList();
            TimeSpan timeF = new TimeSpan(17, 0, 0);
            foreach (var item in timeWorks)
            {
                
                if (item.timeFinish > timeF)
                {
                    item.moreHour = (item.timeFinish - timeF).Hours;
                    item.money += item.moreHour * 400;
                }
                if (item.timeStart > timeF)
                {
                    item.moreHour -= (item.timeStart - timeF).Hours;
                    item.money -= (item.timeStart - timeF).Hours * 400;
                }
                
                item.money += (item.timeFinish - item.timeStart).Hours * 200;
            }

            timeWorkEmployeer.ItemsSource = timeWorks;

            //dataPay.ItemsSource = list;
        }

    }
}
