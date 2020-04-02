using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Glider_WPF_1._0.UserControlReport
{
    /// <summary>
    /// Логика взаимодействия для ReportUserControl.xaml
    /// </summary>
    public partial class ReportUserControl : UserControl
    {
        public ObservableCollection<Report> Reports { get; set; }
        string Login { get; set; }
        public ReportUserControl(string Login )
        {
            InitializeComponent();
            this.Login = Login;
            Reports = new ObservableCollection<Report>(GliderDataContext.Instance.Report.ToList());
            foreach (Report retort in Reports)
            {
                if (retort.Login == Login)
                    Reports.Add(retort);
            }
            DataContext = this;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (Notes_txt.Text != "")
            {
                Report report = new Report();
                report.Revenue = Revenue_txt.Text;
                report.Order = Order_txt.Text;
                report.Checks = Checks_txt.Text;
                report.ChecksAmount = ChecksAmount_txt.Text;
                report.Notes = Notes_txt.Text;
                report.DateReport = DateTime.Now;
                report.Login = this.Login;
                GliderDataContext gliderDataContext = GliderDataContext.Instance;
                gliderDataContext.Report.Add(report);
                gliderDataContext.SaveChanges();
                Reports.Add(report);
            }
            else
            {
                MessageBox.Show("Fill in all lines");
            }
            Revenue_txt.Clear();
            Order_txt.Clear();
            Checks_txt.Clear();
            ChecksAmount_txt.Clear();
            Notes_txt.Clear();



        }

        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (Report rep in ItemsControlReports.Items.SourceCollection)
            {
                if (rep.Done == true)
                {
                    GliderDataContext gliderDataContext = GliderDataContext.Instance;
                    gliderDataContext.Report.Remove(rep);
                    gliderDataContext.SaveChanges();
                }
            }
            ItemsControlReports.ItemsSource = null;
            Reports.Clear();
            foreach (Report retort in Reports)
            {
                if (retort.Login == Login)
                    Reports.Add(retort);
            }
            DataContext = this;
        }
    }
}
