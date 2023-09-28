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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace budget
{
    /// <summary>
    /// Логика взаимодействия для TypeCreate.xaml
    /// </summary>
    public partial class TypeCreate : Window
    {
        public string Text;
        public TypeCreate()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (type_name.Text != "Тип записи...")
            {
                Text = type_name.Text;
                Close();
            }
            else
            {
                MessageBox.Show("Вы не ввели значение!");
            }
        }
    }
}
