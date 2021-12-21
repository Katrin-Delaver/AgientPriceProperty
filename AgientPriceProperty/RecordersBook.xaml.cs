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
            var listTimePay = _context.TimeWork.ToList().Select(x => new
            {
                IdTableNumber = x.tabelNumberPerson,
                FIO = x.Employeer.FIO,
                StartTime = x.timeStart,
                FinishTime = x.timeFinish,
                SumTimeWork = x.timeFinish.Hours - x.timeStart.Hours,
                Pay = x.timeFinish > TimeSpan.Parse("17:00:00") ? 0 : (x.timeFinish.Hours - x.timeStart.Hours) * priceHour
            }).ToList();

            timeWorkEmployeer.ItemsSource = listTimePay;

            var listP = listTimePay.Select(x => new
            {
                IdTableNumber = x.IdTableNumber,
                FIO = x.FIO,
                StartTime = x.StartTime,
                FinishTime = x.FinishTime,
                PTimeWork = x.FinishTime > TimeSpan.Parse("17:00:00") ? x.FinishTime.Hours - TimeSpan.Parse("17:00:00").Hours : 0,
                Pay = x.FinishTime > TimeSpan.Parse("17:00:00") ? (x.FinishTime.Hours - TimeSpan.Parse("17:00:00").Hours) * doublePriceHour : 0
            });

            PWorkEmployeer.ItemsSource = listP;


            var genericList = listTimePay.Join(listP,
                x => x.IdTableNumber,
                y => y.IdTableNumber,
                (x, y) => new
                {
                    TableNumber = x.IdTableNumber,
                    Pay = listTimePay.Where(c => c.IdTableNumber == x.IdTableNumber).Sum(u => u.Pay) + listP.Where(c => c.IdTableNumber == y.IdTableNumber).Sum(u => u.Pay)
                }).Distinct();

            //var listTime = listTimePay.Select(x => new { TabelNumber = x.IdTableNumber, Pay = listTimePay.Where(y => y.IdTableNumber == x.IdTableNumber).Sum(u => (int)u.Pay)}).Distinct();
            dataPay.ItemsSource = genericList.ToList();
        }

    }
}
