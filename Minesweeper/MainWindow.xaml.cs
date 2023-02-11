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

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public enum GameLevel { Easy=10, Normal=20, Hard=40}

        private void cmbGameLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isload)
            {

           
            ComboBoxItem item = ((ComboBoxItem)cmbGameLevel.SelectedItem);
            string str = item.Content.ToString();
            int number = Convert.ToInt32(item.Tag);

            CreateGameArea(number);
            }
        }
        
       bool isload=false;
        private void CreateGameArea(int totalMine)
        {
            int count=10;
            if (totalMine == 10) { count = 9; }
            else if (totalMine == 20) { count = 15; }
            else if (totalMine == 45) { count = 19; }

            wrpPanel.Children.Clear();

            wrpPanel.Width = count * 25;

            for (int i = 0; i < count*count; i++)
            {
                Button btn = new Button();
                btn.Width = 25;
                btn.Height = 25;
                btn.Tag = false;

                btn.Click += Btn_Click;

                //wrpPanel.Children.Add(btn);
                if (i%2==0)
                {
                    btn.Background = new SolidColorBrush(Color.FromRgb(142, 204, 57));
                }
                else
                {
                    btn.Background = new SolidColorBrush(Color.FromRgb(167, 217, 72));
                }
                wrpPanel.Children.Add(btn);
                

            }
            placeMines(totalMine, count * count);
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn= (Button)sender;
            if ((bool)btn.Tag == true) 
            {
                btn.Background = Brushes.Red;
                MessageBox.Show("Game OVER");
                ShowAllMines();
                MessageBox.Show("");
                CreateGameArea(10);
            }
            else
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(229, 194, 159));
            }
        }

        private void placeMines( int totalMines, int totalPlace)
        {
            Random rnd = new Random();
            int counter = 0;
            
            do
            {
                Button btn = (Button)wrpPanel.Children[rnd.Next(0, totalPlace + 1)];

                if ((bool)btn.Tag==false)
                {
                    btn.Tag = true;
                    //btn.Background = Brushes.Red;
                    counter++;
                }
            } while (counter<totalMines);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            isload = true;
            CreateGameArea(10);
        }

        private void ShowAllMines()
        {
            foreach (Button btn in wrpPanel.Children)
            {
                if ((bool)btn.Tag ==true)
                {
                    btn.Background = Brushes.DarkRed;
                }

            }
        }
    }
}
