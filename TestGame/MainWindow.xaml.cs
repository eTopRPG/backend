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

namespace TestGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int x, y;
        public bool[,] ArrayWall = new bool[85, 45];
        public Border[,] borders = new Border[85, 45];
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 85; i++)
            {
                for (int j = 0; j < 45; j++)
                {
                    ArrayWall[i, j] = false;
                    GeneratePole(i, j);
                }
            }
            x = 0;
            y = 0;
            GenerateWall(5, 5);
            GenerateWall(4, 5);
            GenerateWall(4, 6);
            GenerateWall(4, 7);
            GenerateWall(5, 7);
            Player.SetValue(Grid.RowProperty, y);
            Player.SetValue(Grid.ColumnProperty, x);
        }

        private void GenerateWall(int x, int y)
        {
            ArrayWall[x, y] = true;
            Border border = new Border();
            border.Background = Brushes.Black;
            border.Width = 80;
            border.Height = 80;
            GameMap.Children.Add(border);
            Panel.SetZIndex(border, 1);
            border.SetValue(Grid.RowProperty, y);
            border.SetValue(Grid.ColumnProperty, x);
            borders[x, y] = border;
            border.Visibility = Visibility.Collapsed;
            border.MouseDown += (s, e) => MessageBox.Show($"{Grid.GetRow(border)},{Grid.GetColumn(border)}");
        }

        private void GeneratePole(int x, int y)
        {
            
            Border border = new Border();
            border.Background = Brushes.LightGray;
            border.Width = 80;
            border.Height = 80;
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.BorderBrush = Brushes.Gray;
            GameMap.Children.Add(border);
            Panel.SetZIndex(border, 0);
            border.SetValue(Grid.RowProperty, y);
            border.SetValue(Grid.ColumnProperty, x);
            borders[x, y] = border;
            border.Visibility = Visibility.Collapsed;
            border.MouseDown += (s, e) => MessageBox.Show($"{Grid.GetRow(border)},{Grid.GetColumn(border)}");
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.Key)
            {
                case Key.W:
                    goY(-1);
                    break;
                case Key.A:
                    goX(-1);
                    break;
                case Key.S:
                    goY(1);
                    break;
                case Key.D:
                    goX(1);
                    break;
                case Key.Space:
                    radar();
                    break;
                default:
                    break;
            }
        }

        private void radar()
        {
            for (int i = 0; i < 85; i++)
            {
                for (int j = 0; j < 45; j++)
                {
                    if ((Grid.GetRow(borders[i, j]) - y) < 3 && (Grid.GetRow(borders[i, j]) - y) > -3 && (Grid.GetColumn(borders[i, j]) - x) < 3 && (Grid.GetColumn(borders[i, j]) - x) > -3)
                    {
                        borders[i, j].Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void goX(int hod)
        {
            x += hod;
            if (x <= 84 && x >= 0 && y >= 0 && y <= 44)
            {
                if (borders[x, y].Visibility == Visibility.Visible && borders[x, y].Background == Brushes.LightGray/*ArrayWall[x, y] == false/*!(y == Grid.GetRow(Wall) && x == Grid.GetColumn(Wall))*/ )
                {
                    Player.SetValue(Grid.ColumnProperty, x);
                }
                else
                {
                    x -= hod;
                }
            }
            else
            {
                x -= hod;
            }          
        }

        private void goY(int hod)
        {
            y += hod;
            if (x <= 84 && x >= 0 && y >= 0 && y <= 44)
            {
                if (borders[x, y].Visibility == Visibility.Visible && borders[x, y].Background == Brushes.LightGray/*ArrayWall[x, y] == false/*!(y == Grid.GetRow(Wall) && x == Grid.GetColumn(Wall))*/ )
                {
                    Player.SetValue(Grid.RowProperty, y);
                }
                else
                {
                    y -= hod;
                }
            }
            else
            {
                y -= hod;
            }
        }
    }
}
