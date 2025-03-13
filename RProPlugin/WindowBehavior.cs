using System.Windows;
using System.Windows.Input;

namespace RProPlugin;

public static class WindowBehavior
{
    public static readonly DependencyProperty IsDraggableProperty =
        DependencyProperty.RegisterAttached("IsDraggable", typeof(bool), typeof(WindowBehavior),
            new PropertyMetadata(false, OnIsDraggableChanged));

    public static bool GetIsDraggable(UIElement element)
    {
        return (bool)element.GetValue(IsDraggableProperty);
    }

    public static void SetIsDraggable(UIElement element, bool value)
    {
        element.SetValue(IsDraggableProperty, value);
    }

    private static void OnIsDraggableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            if ((bool)e.NewValue)
            {
                element.MouseLeftButtonDown += OnMouseLeftButtonDown;
            }
            else
            {
                element.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            }
        }
    }

    private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is UIElement element && Window.GetWindow(element) is Window window)
        {
            window.DragMove();
        }
    }

    public static readonly DependencyProperty EnableResizeProperty =
        DependencyProperty.RegisterAttached("EnableResize", typeof(bool), typeof(WindowBehavior),
            new PropertyMetadata(false, OnEnableResizeChanged));

    public static bool GetEnableResize(UIElement element)
    {
        return (bool)element.GetValue(EnableResizeProperty);
    }

    public static void SetEnableResize(UIElement element, bool value)
    {
        element.SetValue(EnableResizeProperty, value);
    }

    private static void OnEnableResizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element)
        {
            if ((bool)e.NewValue)
            {
                element.MouseLeftButtonDown += OnResizeMouseLeftButtonDown;
                element.MouseMove += OnResizeMouseMove;
                element.MouseLeftButtonUp += OnResizeMouseLeftButtonUp;
            }
            else
            {
                element.MouseLeftButtonDown -= OnResizeMouseLeftButtonDown;
                element.MouseMove -= OnResizeMouseMove;
                element.MouseLeftButtonUp -= OnResizeMouseLeftButtonUp;
            }
        }
    }

    private static bool isResizing;
    private static Point startPoint;
    private static Size startSize;

    private static void OnResizeMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement element && Window.GetWindow(element) is Window window)
        {
            isResizing = true;
            startPoint = e.GetPosition(window);
            startSize = new Size(window.Width, window.Height);
            element.CaptureMouse();
            e.Handled = true;
        }
    }

    private static void OnResizeMouseMove(object sender, MouseEventArgs e)
    {
        if (isResizing && sender is FrameworkElement element && Window.GetWindow(element) is Window window)
        {
            Point currentPoint = e.GetPosition(window);
            double diffX = currentPoint.X - startPoint.X;
            double diffY = currentPoint.Y - startPoint.Y;

            window.Width = startSize.Width + diffX;
            window.Height = startSize.Height + diffY;
            e.Handled = true;
        }
    }

    private static void OnResizeMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement element)
        {
            isResizing = false;
            element.ReleaseMouseCapture();
            e.Handled = true;
        }
    }
} 