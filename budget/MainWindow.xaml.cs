using System;
using System.Collections.Generic;
using System.IO;
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

namespace budget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, CRUD
    {
        public static bool isSave = false;
        private List<string> types = new List<string>();
        List<Data> datas = new List<Data>();
        string PATH = "D:\\infodata.json";

        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists(PATH))
                File.Create(PATH);

            date_pick.DisplayDate = DateTime.Now.Date;
            date_pick.Text = date_pick.DisplayDate.ToString();
            if (!File.Exists(PATH))
            {
                File.Create(PATH);
            }
            if (JSONConv.getInfo<List<Data>>(PATH) == null)
            {
                datas = new List<Data>();
            }
            else
            {
                datas = JSONConv.getInfo<List<Data>>(PATH);
            }
            Update();
            type_pick.ItemsSource = types;
        }

        public void Create()
        {
            if (name.Text != "" && name.Text != "Имя записи")
            {
                if (type_pick.SelectedItem != null)
                {
                    if ((summ.Text != "" || summ.Text != "Сумма денег") && int.TryParse(summ.Text, out int number))
                    {
                        isSave = true;
                        var mon = Convert.ToInt32((string)summ.Text.Trim());
                        Data new_data = new Data(name.Text, mon, type_pick.SelectedItem.ToString(), date_pick.Text);
                        datas.Add(new_data);
                        JSONConv.saveInfo(PATH, datas);
                        isSave = false;
                        name.Text = "";
                        summ.Text = "";
                        type_pick.SelectedItem = null;
                        Update();
                    }
                    else
                    {
                        MessageBox.Show("Вы не ввели сумму денег или ввели ее неправильно!");
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали тип записи!");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели имя записи!");
            }
        }

        public void CreateType()
        {
            TypeCreate window = new TypeCreate();
            window.ShowDialog();
            if (window.Text != "")
            {
                types.Add(window.Text);
                type_pick.ItemsSource = null;
                type_pick.ItemsSource = types;
            }
        }

        public void Delete()
        {
            isSave = true;
            int index = datas.IndexOf((Data)data.SelectedItem);
            datas.RemoveAt(index);
            JSONConv.saveInfo(PATH, datas);
            isSave = false;
            Update();
        }

        public void Update()
        {
            data.ItemsSource = null;
            List<Data> sorted = new List<Data>();
            int summ = 0;
            foreach (var item in datas)
            {
                if (item.Date.ToString() == date_pick.Text)
                {
                    if (item.isIncome) { summ += item.Cost; }
                    else { summ -= item.Cost; item.cost *= -1; }
                    sorted.Add(item);
                }
            }
            if (summ < 0)
            {
                DataSumm.Text = "Итого(-):" + -summ;
            }
            else
            {
                DataSumm.Text = "Итого: " + summ;
            }
            data.ItemsSource = sorted;
        }

        public void UpdateData()
        {
            int index = datas.IndexOf(data.SelectedItem as Data);
            var mon = Convert.ToInt32((string)summ.Text.Trim());
            Data newdata = new Data(name.Text, mon, type_pick.SelectedItem.ToString(), date_pick.Text);
            data.ItemsSource = null;
            isSave = true;
            datas[index] = newdata;
            JSONConv.saveInfo(PATH, datas);
            isSave = false;
            data.ItemsSource = datas;
        }

        private void date_pick_CalendarClosed(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void date_pick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data.SelectedItem != null)
            {
                name.Text = (data.SelectedItem as Data).Name;
                summ.Text = (data.SelectedItem as Data).Cost.ToString();
                if (!types.Contains((data.SelectedItem as Data).Type))
                {
                    type_pick.ItemsSource = null;
                    types.Add((data.SelectedItem as Data).Type);
                    type_pick.ItemsSource = types;
                }
                type_pick.SelectedItem = types[types.Count - 1];
            }
        }

        private void create_type_Click(object sender, RoutedEventArgs e)
        {
            CreateType();
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void redact_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }
    }
}
