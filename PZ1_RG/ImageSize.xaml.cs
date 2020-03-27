using Microsoft.Win32;
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

namespace PZ1_RG
{
    /// <summary>
    /// Interaction logic for ImageSize.xaml
    /// </summary>
    public partial class ImageSize : Window
    {
        private double positionX;
        private double positionY;
        private Image editImage;
        public double PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }
        public double PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }

        public ImageSize(double x, double y)
        {
            InitializeComponent();
            PositionX = x;
            PositionY = y;
            editImage = null;
        }

        public ImageSize(Image image)
        {
            InitializeComponent();
            // copy current image
            editImage = image;
            tbHeight.Text = Convert.ToString(editImage.Height);
            tbHeight.IsEnabled = false;
            tbWidth.Text = Convert.ToString(editImage.Width);
            tbWidth.IsEnabled = false;
            btDraw.Content = String.Format("Change Image");
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtDraw_Click(object sender, RoutedEventArgs e)
        {
            // field for validation
            bool _valid = true;
            double _width;
            double _height;
            // check all valids
            // size is int?
            if (!Double.TryParse(tbWidth.Text, out _width))
            {
                _valid = false;
                tbWidth.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tbWidth.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            if (!Double.TryParse(tbHeight.Text, out _height))
            {
                _valid = false;
                tbHeight.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tbHeight.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            // final check _valid
            if (!_valid)
            {
                MessageBox.Show("Invalid inputs. Check fields that are 'red'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // new image add
                if (editImage == null)
                {//open new dialog
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files(*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";
                    openFileDialog.Title = "Choose a picture to insert.";
                    //choose image
                    if (openFileDialog.ShowDialog() == true)
                    {
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                        image.Width = _width;
                        image.Height = _height;

                        Canvas.SetLeft(image, PositionX);
                        Canvas.SetTop(image, PositionY);
                        Canvas canvas = ((MainWindow)Application.Current.MainWindow).MyCanvas;
                        canvas.Children.Add(image);

                        ((MainWindow)Application.Current.MainWindow).undoRedo.Add((image));
                        ((MainWindow)Application.Current.MainWindow).Brojac++;

                    }
                }
                else // edit existing image
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files(*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";
                    openFileDialog.Title = "Choose a picture to insert.";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        editImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    }
                }
            }

            this.Close();
        }
    }


}
