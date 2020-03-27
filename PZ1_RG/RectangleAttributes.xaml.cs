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
    /// Interaction logic for RectangleAttributes.xaml
    /// </summary>
    public partial class RectangleAttributes : Window
    {
        private double positionX;
        private double positionY;

        // edit current rectangle
        private Rectangle editRectangle;

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

        public RectangleAttributes(double x, double y)
        {
            InitializeComponent();
            PositionX = x;
            PositionY = y;
            editRectangle = null;
        }
        public RectangleAttributes(Rectangle rectangle)
        {
            InitializeComponent();
            // copy current ellipse
            editRectangle = rectangle;
            FillColor.SelectedColor = ((SolidColorBrush)editRectangle.Fill).Color;
            BorderColor.SelectedColor = ((SolidColorBrush)editRectangle.Stroke).Color;
            tbWidth.Text = Convert.ToString(editRectangle.Width);
            tbWidth.IsEnabled = false;
            tbHeight.Text = Convert.ToString(editRectangle.Height);
            tbHeight.IsEnabled = false;
            tbBorderThickness.Text = Convert.ToString(editRectangle.StrokeThickness);
            tbBorderThickness.IsEnabled = false;
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtDraw_Click(object sender, RoutedEventArgs e)
        {
            //if ((tbHeight.Text.ToString()).Equals("") || (tbWidth.Text.ToString()).Equals("") || (tbBorderThickness.Text.ToString()).Equals(""))
            //    MessageBox.Show("All atributes must be writen");
            // field for validation
            bool _valid = true;            
            double _width;
            double _height;
            double _borderThickness;
            Color? fillColor = FillColor.SelectedColor;
            Color? borderColor = BorderColor.SelectedColor;

            // check all valids           
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

            // color check 
            if (fillColor == null)
            {
                _valid = false;
                FillColor.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FillColor.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            if (borderColor == null)
            {
                _valid = false;
                BorderColor.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                BorderColor.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            if (!Double.TryParse(tbBorderThickness.Text, out _borderThickness))
            {
                _valid = false;
                tbBorderThickness.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tbBorderThickness.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            // final check _valid
            if (!_valid)
            {
                MessageBox.Show("Invalid inputs. Check fields that are 'red'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // check if we are drawing new ellipse or editing existing one
                if (editRectangle == null)
                {
                    // draw new ellipse
                    Rectangle rectangle = new Rectangle();
                    // apply properties
                    rectangle.Width = _width;
                    rectangle.Height = _height;
                    rectangle.StrokeThickness = _borderThickness;
                    rectangle.Fill = new SolidColorBrush((Color)fillColor);
                    rectangle.Stroke = new SolidColorBrush((Color)borderColor);

                    // position top-left
                    Canvas.SetLeft(rectangle, PositionX);
                    Canvas.SetTop(rectangle, PositionY);

                    Canvas canvas = ((MainWindow)Application.Current.MainWindow).MyCanvas;
                    canvas.Children.Add(rectangle);

                    ((MainWindow)Application.Current.MainWindow).undoRedo.Add(rectangle);
                    ((MainWindow)Application.Current.MainWindow).Brojac++;
                }
                else // edit existing
                {
                    editRectangle.Width = _width;
                    editRectangle.Height = _height;
                    editRectangle.StrokeThickness = _borderThickness;
                    editRectangle.Fill = new SolidColorBrush((Color)fillColor);
                    editRectangle.Stroke = new SolidColorBrush((Color)borderColor);
                }

                this.Close();
            }
        }

    }

      
    
}
