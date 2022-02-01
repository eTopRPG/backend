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

namespace TestGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region
        public int x, y;
        public bool[,] ArrayWall = new bool[85, 45];
        public Border[,] borders = new Border[85, 45];
        static string pathWall = @"Res\Wall.png";
        static string pathGround = @"Res\Ground.png";
        static string pathEnergy = @"Res\yEnergy.png";
        static string pathLava = @"Res\Lava.png";
        static string pathSkeletona = @"Res\Skeletona.png";
        ImageBrush imageBrushWall = new ImageBrush();
        ImageBrush imageBrushGround = new ImageBrush();
        ImageBrush imageBrushEnergy = new ImageBrush();
        ImageBrush imageBrushLava = new ImageBrush();
        ImageBrush imageBrushMonstr = new ImageBrush();
        Monstr[] monstrs = new Monstr[2];
        List<Monstr> monstrsList = new List<Monstr>();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            EnergyVal.Text = 100.ToString();
            imageBrushWall.ImageSource = new BitmapImage(new Uri(pathWall, UriKind.RelativeOrAbsolute)); 
            imageBrushGround.ImageSource = new BitmapImage(new Uri(pathGround, UriKind.RelativeOrAbsolute)); 
            imageBrushEnergy.ImageSource = new BitmapImage(new Uri(pathEnergy, UriKind.RelativeOrAbsolute)); 
            imageBrushLava.ImageSource = new BitmapImage(new Uri(pathLava, UriKind.RelativeOrAbsolute)); 
            imageBrushMonstr.ImageSource = new BitmapImage(new Uri(pathSkeletona, UriKind.RelativeOrAbsolute)); 
            x = 4;
            y = 4;
            Player.SetValue(Grid.RowProperty, y);
            Player.SetValue(Grid.ColumnProperty, x);
            
            ImportMap();
            //radar();
        }

        private void ImportMap()
        {
            string path = @"Res\greatRPG.csv";
            var lines = File.ReadAllLines(path);
            int lenX = 0;
            int lenY = 0;
            string[,] info = new string[84,45];
            foreach (var item in lines)
            {
                foreach (var item1 in item.Split(','))
                {
                    info[lenX, lenY] = item1;
                    lenX++;
                }
                lenX = 0;
                lenY++;
            }
            for (int i = 0; i < 84; i++)
            {
                for (int j = 0; j < 45; j++)
                {
                    switch (Convert.ToInt32(info[i, j]))
                    {
                        case 0: GeneratePole(i, j);
                            break;
                        case 1: GenerateWall(i, j);
                            break;
                        case 2: GenerateLAVA(i, j);
                            break;
                        case 3:
                            GenerateMonstr(i, j);
                            break;
                        default:
                            GeneratePole(i, j);
                            break;
                    }
                    //if (Convert.ToInt32(info[i, j]) == 1)
                    //{
                    //    GenerateWall(i, j);
                    //}
                    //else if (Convert.ToInt32(info[i, j]) == 0)
                    //{
                    //    ArrayWall[i, j] = false;
                    //    GeneratePole(i, j);
                    //}
                }
            }
            GenerateEnergy(3, 3);
            GenerateEnergy(1, 1);

        }

        private void GenerateMonstr(int x, int y)
        {
            Border border = new Border();
            Monstr monstr = new Monstr(100, 10, x, y);
            monstrs[0] = monstr;
            monstrsList.Add(monstr);
            border.ToolTip = monstr.addDamage(0).ToString();
            border.Background = imageBrushMonstr;
            //border.Background = Brushes.Black;
            border.Width = 100;
            border.Height = 100;
            GameMap.Children.Add(border);
            Panel.SetZIndex(border, 1);
            border.SetValue(Grid.RowProperty, y);
            border.SetValue(Grid.ColumnProperty, x);
            borders[x, y] = border;
            border.Visibility = Visibility.Collapsed;
            border.MouseDown += (s, e) => MessageBox.Show($"{Grid.GetRow(border)},{Grid.GetColumn(border)}");
        }
        private void GenerateLAVA(int x, int y)
        {
            Border border = new Border();
            border.Background = imageBrushLava;
            //border.Background = Brushes.Black;
            border.Width = 100;
            border.Height = 100;
            GameMap.Children.Add(border);
            Panel.SetZIndex(border, 1);
            border.SetValue(Grid.RowProperty, y);
            border.SetValue(Grid.ColumnProperty, x);
            borders[x, y] = border;
            border.Visibility = Visibility.Collapsed;
            border.MouseDown += (s, e) => MessageBox.Show($"{Grid.GetRow(border)},{Grid.GetColumn(border)}");
        }

        private void GenerateWall(int x, int y)
        {
            ArrayWall[x, y] = true;
            Border border = new Border();
            border.Background = imageBrushWall;
            //border.Background = Brushes.Black;
            border.Width = 100;
            border.Height = 100;
            GameMap.Children.Add(border);
            Panel.SetZIndex(border, 1);
            border.SetValue(Grid.RowProperty, y);
            border.SetValue(Grid.ColumnProperty, x);
            borders[x, y] = border;
            border.Visibility = Visibility.Collapsed;
            border.MouseDown += (s, e) => MessageBox.Show($"{Grid.GetRow(border)},{Grid.GetColumn(border)}");
        }
        private void GenerateEnergy(int x, int y)
        {
            ArrayWall[x, y] = true;
            Border border = new Border();
            border.Background = imageBrushEnergy;
            border.Width = 100;
            border.Height = 100;
            GameMap.Children.Add(border);
            Panel.SetZIndex(border, 2);
            border.SetValue(Grid.RowProperty, y);
            border.SetValue(Grid.ColumnProperty, x);
            borders[x, y] = border;
            border.Visibility = Visibility.Collapsed;
            border.MouseDown += (s, e) => MessageBox.Show($"{Grid.GetRow(border)},{Grid.GetColumn(border)}");
        }

        private void GeneratePole(int x, int y)
        {
            
            Border border = new Border();
            //border.Background = Brushes.LightGray;
            border.Background = imageBrushGround;
            border.Width = 100;
            border.Height = 100;
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
                case Key.E: 
                    interaction();
                    break;
                case Key.F:
                    attack();
                    break;
                default:
                    break;
            }
        }

        private void radar()
        {
            if (Convert.ToInt32(EnergyVal.Text) > 0)
            {
                EnergyMinus(2);
                for (int i = 0; i < 84; i++)
                {
                    for (int j = 0; j < 45; j++)
                    {
                        if (   (Grid.GetRow(borders[i, j]) - y) < 3 
                            && (Grid.GetRow(borders[i, j]) - y) > -3 
                            && (Grid.GetColumn(borders[i, j]) - x) < 3 
                            && (Grid.GetColumn(borders[i, j]) - x) > -3)
                        {
                            borders[i, j].Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void interaction()
        {
            if (Convert.ToInt32(EnergyVal.Text) > 0)
            {
                EnergyMinus(2);
                for (int i = 0; i < 84; i++)
                {
                    for (int j = 0; j < 45; j++)
                    {
                        if ((Grid.GetRow(borders[i, j]) - y) < 2 && (Grid.GetRow(borders[i, j]) - y) > - 2 && (Grid.GetColumn(borders[i, j]) - x) < 2 && (Grid.GetColumn(borders[i, j]) - x) > - 2)
                        {
                            
                            if (borders[i, j].Background == imageBrushEnergy)
                            {
                                EnergyMinus(-50);
                                borders[i, j].Background = imageBrushGround;
                            }
                        }
                    }
                }
            }
        }

        private void attack()
        {
            if (Convert.ToInt32(EnergyVal.Text) > 0)
            {
                EnergyMinus(2);
                for (int i = 0; i < 84; i++)
                {
                    for (int j = 0; j < 45; j++)
                    {
                        if ((Grid.GetRow(borders[i, j]) - y) < 2 && (Grid.GetRow(borders[i, j]) - y) > -2 && (Grid.GetColumn(borders[i, j]) - x) < 2 && (Grid.GetColumn(borders[i, j]) - x) > -2)
                        {
                            if (borders[i, j].Background == imageBrushMonstr)
                            {
                                if (monstrs[0].haveMonstr(i, j).ToString() == "false")
                                {
                                    for (int z = 0; z < monstrs.Length; z++)
                                    {
                                        borders[i, j].ToolTip = monstrs[z].addDamage(-20);
                                        TbHP.Text = (Convert.ToInt32(TbHP.Text) - monstrs[z].attack()).ToString();
                                        if (Convert.ToInt32(borders[i, j].ToolTip) < 1)
                                        {
                                            MessageBox.Show("You win!");
                                            borders[i, j].Background = imageBrushGround;
                                        }
                                    }
                                    
                                }
                                
                                
                            }
                        }
                    }
                }
            }
        }

        private void goX(int hod)
        {
            if (x <= 84 && x >= 0 && y >= 0 && y <= 45 && Convert.ToInt32(EnergyVal.Text) > 0)
            {
                x += hod;
                if (borders[x, y].Visibility == Visibility.Visible && borders[x, y].Background != imageBrushWall )
                {
                    if (borders[x, y].Background == imageBrushLava)
                    {
                        Player.Background = null;
                        TbHP.Text = "0";
                        EnergyVal.Text = "0";
                        MessageBox.Show("You lose!");
                    }
                    Player.SetValue(Grid.ColumnProperty, x);
                    MainScroll.ScrollToHorizontalOffset((x-4) * 100);
                    EnergyMinus(1);
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


        private void MainScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private void goY(int hod)
        {
            if (x <= 84 && x >= 0 && y >= 0 && y <= 45 && Convert.ToInt32(EnergyVal.Text) > 0)
            {
                y += hod;
                if (borders[x, y].Visibility == Visibility.Visible && borders[x, y].Background != imageBrushWall)
                {
                    Player.SetValue(Grid.RowProperty, y);
                    MainScroll.ScrollToVerticalOffset((y-4) * 100);
                    EnergyMinus(1);
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

        private void EnergyMinus(int val)
        {
            EnergyVal.Text = (Convert.ToInt32(EnergyVal.Text) - val).ToString();
        }
    }
}
