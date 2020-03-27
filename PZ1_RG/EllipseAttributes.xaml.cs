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
    /// Interaction logic for EllipseAttributes.xaml
    /// </summary>
    public partial class EllipseAttributes : Window
    {
        private double positionX;
        private double positionY;
        private Ellipse editEllipse;
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
        public EllipseAttributes(double x, double y)
        {
            InitializeComponent();

            PositionX = x;
            PositionY = y;
            editEllipse = null;
            
        }
        public EllipseAttributes(Ellipse ellipse)
        {
            InitializeComponent();
            //kopiranje trenutne elipse
            editEllipse = ellipse;
            FillColor.SelectedColor = ((SolidColorBrush)editEllipse.Fill).Color;
            BorderColor.SelectedColor = ((SolidColorBrush)editEllipse.Stroke).Color;
            tbWidth.Text = Convert.ToString(editEllipse.Width);
            tbWidth.IsEnabled = false;
            tbHeight.Text = Convert.ToString(editEllipse.Height);
            tbHeight.IsEnabled = false;
            tbBorderThickness.Text = Convert.ToString(editEllipse.StrokeThickness);
            tbBorderThickness.IsEnabled = false;

        }
       // Color mborderColor = Colors.Black;

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtDraw_Click(object sender, RoutedEventArgs e)
        {
            bool _valid = true;
            double _width;
            double _height;
            double _borderThickness;
            // allowing value null for color (?)
            // so we can compare it later
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

            // color check null?
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
                if (editEllipse == null)
                {
                    // draw new ellipse
                    Ellipse ellipse = new Ellipse();
                    // apply properties
                    ellipse.Width = _width;
                    ellipse.Height = _height;
                    ellipse.StrokeThickness = _borderThickness;
                    ellipse.Fill = new SolidColorBrush((Color)fillColor);
                    ellipse.Stroke = new SolidColorBrush((Color)borderColor);

                    // position top-left
                    Canvas.SetLeft(ellipse, PositionX);
                    Canvas.SetTop(ellipse, PositionY);

                    Canvas canvas = ((MainWindow)Application.Current.MainWindow).MyCanvas;
                    canvas.Children.Add(ellipse);

                    ((MainWindow)Application.Current.MainWindow).undoRedo.Add(ellipse);
                    ((MainWindow)Application.Current.MainWindow).Brojac++;
                }
                else // edit existing
                {
                    editEllipse.Width = _width;
                    editEllipse.Height = _height;
                    editEllipse.StrokeThickness = _borderThickness;
                    editEllipse.Fill = new SolidColorBrush((Color)fillColor);
                    editEllipse.Stroke = new SolidColorBrush((Color)borderColor);
                }

                this.Close();
            }



        }

        //private void FillColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        //{

        //}

        //private void BorderColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        //{
        //    mborderColor = BorderColor.SelectedColor.Value;
        //    //if (m_Selected != null)
        //    //{
        //    //    m_Selected.Stroke = new SolidColorBrush(mborderColor);
        //    //}
        //}
    }
}
