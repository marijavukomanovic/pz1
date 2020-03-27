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
    /// Interaction logic for PolygonAttributes.xaml
    /// </summary>
    public partial class PolygonAttributes : Window
    {
        private Point shapePosition;
        private PointCollection polygonPoints;

        private Polygon editPolygon;

        public Point ShapePosition
        {
            get { return shapePosition; }
            set { shapePosition = value; }
        }
        public PolygonAttributes(PointCollection points, Point shapePosition)
        {
            InitializeComponent();
            polygonPoints = points;
            ShapePosition = shapePosition;
            editPolygon = null;
        }
        public PolygonAttributes(Polygon polygon)
        {
            InitializeComponent();
            editPolygon = polygon;
            FillColor.SelectedColor = ((SolidColorBrush)editPolygon.Fill).Color;
            BorderColor.SelectedColor = ((SolidColorBrush)editPolygon.Stroke).Color;
            tbBorderThickness.Text = Convert.ToString(editPolygon.StrokeThickness);
            tbBorderThickness.IsEnabled = false;
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void BorderColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        //{

        //}

        //private void FillColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        //{

        //}

        private void BtDraw_Click(object sender, RoutedEventArgs e)
        {
            //if (/*(tbHeight.Text.ToString()).Equals("") || (tbWidth.Text.ToString()).Equals("") ||*/ (tbBorderThickness.Text.ToString()).Equals(""))
            //    MessageBox.Show("All atributes must be writen");
            // field for validation
            bool _valid = true;            
            double _borderThickness;
            // allowing value null for color (?)
            // so we can compare it later
            Color? fillColor = FillColor.SelectedColor;
            Color? borderColor = BorderColor.SelectedColor;
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
                if (editPolygon == null)
                {
                    Polygon polygon = new Polygon();
                    polygon.Stroke = new SolidColorBrush((Color)borderColor);
                    polygon.Points = new PointCollection();
                    polygon.Fill = new SolidColorBrush((Color)fillColor);
                    polygon.StrokeThickness = _borderThickness;

                    // CONVERT POINTS TO ABSOLUTE COORDS
                    double maxY = 0;
                    double maxX = 0;

                    foreach (var point in polygonPoints)
                    {
                        if (point.X > maxX)
                        {
                            maxX = point.X;
                        }
                        if (point.Y > maxY)
                        {
                            maxY = point.Y;
                        }
                    }
                    PointCollection _tmpPoints = new PointCollection();
                    foreach (var point in polygonPoints)
                    {
                        _tmpPoints.Add(new Point(point.X - maxX, point.Y - maxY));
                    }
                    double extremeY = 0;
                    double extremeX = 0;

                    foreach (var point in _tmpPoints)
                    {
                        if (Math.Abs(point.X) > extremeX)
                        {
                            extremeX = Math.Abs(point.X);
                        }
                        if (Math.Abs(point.Y) > extremeY)
                        {
                            extremeY = Math.Abs(point.Y);
                        }
                    }

                    PointCollection finalPolygonPoints = new PointCollection();
                    foreach (var point in _tmpPoints)
                    {
                        finalPolygonPoints.Add(new Point(point.X + extremeX, point.Y + extremeY));
                    }

                    foreach (var point in finalPolygonPoints)
                    {
                        polygon.Points.Add(point);
                    }

                    Canvas.SetLeft(polygon, ShapePosition.X);
                    Canvas.SetTop(polygon, ShapePosition.Y);

                    Canvas canvas = ((MainWindow)(Application.Current.MainWindow)).MyCanvas;
                    canvas.Children.Add(polygon);

                    ((MainWindow)(Application.Current.MainWindow)).undoRedo.Add(polygon);
                    ((MainWindow)(Application.Current.MainWindow)).Brojac++;
                }
                else
                {

                    editPolygon.Stroke = new SolidColorBrush((Color)borderColor);
                    editPolygon.Fill = new SolidColorBrush((Color)fillColor);
                    editPolygon.StrokeThickness = _borderThickness;

                }

                this.Close();
            }
        }
    }
}

