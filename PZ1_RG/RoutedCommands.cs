using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PZ1_RG
{
    public class RoutedCommands
    {
        /// <summary>
        /// Shortcut: Alt + 1
        /// </summary>
        public static readonly RoutedUICommand Ellipse = new RoutedUICommand("Ellipse", "Ellipse", typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D1, ModifierKeys.Alt)
            });

        /// <summary>
        /// Shortcut: Alt + 2
        /// </summary>
        public static readonly RoutedUICommand Rectangle = new RoutedUICommand("Rectangle", "Rectangle", typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D2, ModifierKeys.Alt)
            });

        /// <summary>
        /// Shortcut: Alt + 3
        /// </summary>
        public static readonly RoutedUICommand Polygon = new RoutedUICommand("Polygon", "Polygon", typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D3, ModifierKeys.Alt)
            });

        /// <summary>
        /// Shortcut: Alt + 4
        /// </summary>
        public static readonly RoutedUICommand Image = new RoutedUICommand("Image", "Image", typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D4, ModifierKeys.Alt)
            });

        /// <summary>
        /// Shortcut: Ctrl + Y
        /// </summary>
        public static readonly RoutedUICommand Redo = new RoutedUICommand("Redo", "Redo", typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Y, ModifierKeys.Control)
            });

        /// <summary>
        /// Shortcut: Ctrl + Z
        /// </summary>
        public static readonly RoutedUICommand Undo = new RoutedUICommand("Undo", "Undo", typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Z, ModifierKeys.Control)
            });

        /// <summary>
        /// Shortcut: Alt + X
        /// </summary>
        public static readonly RoutedUICommand Clear = new RoutedUICommand("Clear", "Clear", typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.X, ModifierKeys.Alt)
            });
    }
}
