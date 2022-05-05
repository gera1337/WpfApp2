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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int PageSize = 10;

        public MainWindow()
        {
            InitializeComponent();
            List<string> sortlist = new List<string>() { "наименование", "размер", "скидки", "приоритет агента"};
            first.ItemsSource=sortlist;
            //List<string> sortlistt = new List<string>() { "ЗАО", "МКК", "МФО", "ОАО","ООО","ПАО" };
            second.ItemsSource = newwpfEntities.funcbd().AgentType.ToList();
            DataGridListAgent.CanUserAddRows = false;
            CountPage.Text = (newwpfEntities.funcbd().final.Count() / PageSize).ToString();
            drawDataGrid();
        }

        public void drawDataGrid()
        {
            List<final> finallist = newwpfEntities.funcbd().final.ToList();

            if(second.SelectedIndex != -1)
            {
                int idAgent = int.Parse(second.SelectedValue.ToString());
                finallist = finallist.Where(x => x.AgentTypeID == idAgent).ToList();
            }

            DataGridListAgent.ItemsSource = finallist.Skip((int.Parse(CurrentPage.Text) - 1) * PageSize).Take(PageSize).ToList(); ;
        }

            private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        private void back(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(CurrentPage.Text) - 1) > 0)
            {
                CurrentPage.Text = (int.Parse(CurrentPage.Text) - 1).ToString();
                drawDataGrid();
                
            }
            else
            {
                MessageBox.Show("вернитесь на страницу вперед");
            }
        }

        private void next(object sender, RoutedEventArgs e)
        {

            if ((int.Parse(CurrentPage.Text) + 1) <= int.Parse(CountPage.Text))
            {
                CurrentPage.Text = (int.Parse(CurrentPage.Text) + 1).ToString();
                drawDataGrid();
                
            }
            else
            {
                MessageBox.Show("дальше нет страниц");
            }
        }

        private void second_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (second.SelectedIndex != 1) 
            {
                CurrentPage.Text = "1";
                int agentTypeID = int.Parse(second.SelectedValue.ToString());
                CountPage.Text = (newwpfEntities.funcbd().final.Where(x=>x.AgentTypeID== agentTypeID).Count() / PageSize).ToString();
            }
            
        }

        private void zxc_Click(object sender, RoutedEventArgs e)
        {
            Window1 zxc=new Window1();
            zxc.Show();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Window2 edit=new Window2();
            edit.Show();
        }
    }
}
