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
using System.Windows.Shapes;//Koristi se za generisanje 2d grafike


namespace PZ1_RG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields 
        private enum SelectedShape
        {
            None, Ellipse, Rectangle, Polygon, Image
        }
        private SelectedShape currntShape = SelectedShape.None;
        // Point start, end;
        PointCollection polygonDots;// { };
        Dictionary<int, Object> dicShape;
        private int brojac;
        private int brojac2;
        public List<Object> undoRedo;
        public List<Object> undoClear;
        bool clear = false;
        public int Brojac
        {
            get { return brojac;}
            set
            { brojac = value;}
        }
        public int BrojacClearUndo
        {
            get { return brojac2;}
            set { brojac2 = value;}
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            polygonDots = new PointCollection();
            dicShape = new Dictionary<int, Object>();
            undoRedo = new List<Object>();
            undoClear = new List<Object>();
            Brojac = 0;
            BrojacClearUndo = 0;
        }
        #region EllipseExecuteButton

        private void Ellipse_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }

        private void Ellipse_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clear = false;
            if (currntShape == SelectedShape.Ellipse)
            {
                currntShape = SelectedShape.None;
                btnEllipse.IsChecked = false;
                btnRectangle.IsChecked = false;
                btnPolygon.IsChecked = false;
                btnImage.IsChecked = false;
            }
            else
            {
                currntShape = SelectedShape.Ellipse;
                btnEllipse.IsChecked = true;
                btnRectangle.IsChecked = false;
                btnPolygon.IsChecked = false;
                btnImage.IsChecked = false;
            }
        }
        #endregion
        #region RectangleExecuteButton
        private void Rectangle_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Rectangle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clear = false;
            if (currntShape == SelectedShape.Rectangle)
            {
                currntShape = SelectedShape.None;
                btnEllipse.IsChecked = false;
                btnRectangle.IsChecked = false;
                btnPolygon.IsChecked = false;
                btnImage.IsChecked = false;
            }
            else
            {
                currntShape = SelectedShape.Rectangle;
                btnEllipse.IsChecked = false;
                btnRectangle.IsChecked = true;
                btnPolygon.IsChecked = false;
                btnImage.IsChecked = false;
            }
        }
        #endregion
        #region PolygonExecuteButton
        private void Polygon_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Polygon_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clear = false;
            if (currntShape == SelectedShape.Polygon)
            {
                currntShape = SelectedShape.None;
                btnEllipse.IsChecked = false;
                btnRectangle.IsChecked = false;
                btnPolygon.IsChecked = false;
                btnImage.IsChecked = false;
            }
            else
            {
                currntShape = SelectedShape.Polygon;
                btnEllipse.IsChecked = false;
                btnRectangle.IsChecked = false;
                btnPolygon.IsChecked = true;
                btnImage.IsChecked = false;
            }
        }
        #endregion
        #region ImageExecuteButton

        private void Image_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Image_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clear = false;
            if (currntShape == SelectedShape.Image)
            {
                currntShape = SelectedShape.None;
                btnEllipse.IsChecked = false;
                btnRectangle.IsChecked = false;
                btnPolygon.IsChecked = false;
                btnImage.IsChecked = false;
            }
            else
            {
                currntShape = SelectedShape.Image;
                btnEllipse.IsChecked = false;
                btnRectangle.IsChecked = false;
                btnPolygon.IsChecked = false;
                btnImage.IsChecked = true;
            }
        }
        #endregion
        
        #region Undo
        private void Undo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Undo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (undoRedo.Count > 0 && Brojac > 0)
            {
                MyCanvas.Children.RemoveAt(Brojac - 1);
                Brojac--;                
            }
            if (clear)
            {
                foreach (var item in undoClear)
                {
                    clear = false;
                    MyCanvas.Children.Add((item as UIElement));
                }
                //vrati u klasicne
                undoRedo = undoClear;
                Brojac = BrojacClearUndo;
                //prebrisi ih
                BrojacClearUndo = 0;
                undoClear = new List<Object>();

            }
           
        }
        #endregion

        #region Redo
        private void Redo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Redo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (undoRedo.Count > Brojac)
            {
                MyCanvas.Children.Add(undoRedo[Brojac] as UIElement);
                Brojac++;
            }
            clear = false;
        }
        #endregion
        
        #region Clear
        private void Clear_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // reset all
            MyCanvas.Children.Clear();
            //ubaci u pomocne
            BrojacClearUndo = Brojac;            
            undoClear = undoRedo;
            clear = true;
            //prebrisi klasicne
            Brojac = 0;
            undoRedo = new List<Object>();

            polygonDots.Clear();
            btnEllipse.IsChecked = false;
            btnRectangle.IsChecked = false;
            btnPolygon.IsChecked = false;
            btnImage.IsChecked = false;
            currntShape = SelectedShape.None;
        }
        #endregion

        #region MouseMovenment
        private void MyCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // when left-click, get position where to place new shape (to use top-left start position)
            Point point = e.GetPosition((Canvas)sender);

            // checkin which button is selected
            switch (currntShape)
            {
                // ellipse
                case SelectedShape.Ellipse:
                    // we pass x and y position to constructor of window
                    EllipseAttributes ellipseWindow = new EllipseAttributes(point.X, point.Y);
                    ellipseWindow.Show();
                    btnEllipse.IsChecked = false;
                    currntShape = SelectedShape.None;
                    break;

                // rectangle
                case SelectedShape.Rectangle:
                    // we pass x and y position to constructor of window
                    RectangleAttributes rectangleWindow = new RectangleAttributes(point.X, point.Y);
                    rectangleWindow.Show();
                    btnRectangle.IsChecked = false;
                    currntShape = SelectedShape.None;
                    break;

                // polygon
                case SelectedShape.Polygon:
                    // for each click we add new point (position) to the point list
                    polygonDots.Add(point);
                    break;

                // image
                case SelectedShape.Image:
                    ImageSize imageWindow = new ImageSize(point.X, point.Y);
                    imageWindow.Show();
                    btnImage.IsChecked = false;
                    currntShape = SelectedShape.None;
                    break;

                default:
                    break;
            }
        }
        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // edit existing shape with left button
            // get it's position first
            Point point = e.GetPosition((Canvas)sender);

            // check which shape is clicked and pass to correct window
            if (currntShape == SelectedShape.Polygon)
            {
                if (polygonDots.Count > 1)
                {
                    PolygonAttributes polygonWindow = new PolygonAttributes(polygonDots, point);
                    polygonWindow.Show();
                    btnPolygon.IsChecked = false;
                    currntShape = SelectedShape.None;
                }
                else
                {
                    currntShape = SelectedShape.None;
                    polygonDots.Clear();
                    btnPolygon.IsChecked = false;
                }
            }
            else
            {
                if (e.OriginalSource is Rectangle)
                {
                    RectangleAttributes rectangleWindow = new RectangleAttributes((Rectangle)e.OriginalSource);
                    rectangleWindow.Show();
                    btnRectangle.IsChecked = false;
                    currntShape = SelectedShape.None;
                }

                if (e.OriginalSource is Ellipse)
                {
                    EllipseAttributes ellipseWindow = new EllipseAttributes((Ellipse)e.OriginalSource);
                    ellipseWindow.Show();
                    btnEllipse.IsChecked = false;
                    currntShape = SelectedShape.None;
                }

                if (e.OriginalSource is Polygon)
                {
                    PolygonAttributes polygonWindow = new PolygonAttributes((Polygon)e.OriginalSource);
                    polygonWindow.Show();
                    btnPolygon.IsChecked = false;
                    currntShape = SelectedShape.None;
                }

                if (e.OriginalSource is Image)
                {
                    ImageSize imageWindow = new ImageSize((Image)e.OriginalSource);
                    imageWindow.Show();
                    btnImage.IsChecked = false;
                    currntShape = SelectedShape.None;
                }
            }
        }
        #endregion
    }
}
