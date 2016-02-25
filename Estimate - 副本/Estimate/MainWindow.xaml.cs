using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;

namespace Estimate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static T FindChild<T>(DependencyObject parent, string childname)//查找控件
            where T : DependencyObject
        {
            if (parent == null) return null;
            T foundchild = null;
            int childrencount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrencount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                //如果子控件不是需查找的控件类型
                T childType = child as T;
                if (childType == null)
                {
                    //下一级递归查找
                    foundchild = FindChild<T>(child, childname);
                    //找到控件中断递归
                    if (foundchild != null) break;
                }
                else if (!string.IsNullOrEmpty(childname))
                {
                    var frameworkElement = child as FrameworkElement;
                    //如果控件名称符合参数条件
                    if (frameworkElement != null && frameworkElement.Name == childname)
                    {
                        foundchild = (T)child;
                        break;
                    }
                }
                else
                {
                    //查找到控件
                    foundchild = (T)child;
                    break;
                }
            }
            return foundchild;
        }


        private void Weigh_Click(object sender, RoutedEventArgs e)
        {
        //    Weigh weigh = MainWindow.FindChild<Weigh>(Application.Current.MainWindow, "Weigh");
        //    if (weigh != null)
        //    {
        //        foreach (var t in mainpenal.Children)
        //        {
        //            if (t.Title == "过磅单")
        //            {
        //                t.IsActive = true;
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Weigh newweige = new Weigh();
        //        LayoutDocument newreport = new LayoutDocument();
        //        newreport.Title = "过磅单";
        //        newreport.IsActive = true;
        //        newreport.Content = newweige;
        //        newweige.Name = "Weigh";
        //        mainpenal.Children.Add(newreport);
        //    }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Estimate.EstimateDataSet estimateDataSet = ((Estimate.EstimateDataSet)(this.FindResource("estimateDataSet")));
            // 将数据加载到表 estimate 中。可以根据需要修改此代码。
            Estimate.EstimateDataSetTableAdapters.estimateTableAdapter estimateDataSetestimateTableAdapter = new Estimate.EstimateDataSetTableAdapters.estimateTableAdapter();
            estimateDataSetestimateTableAdapter.Fill(estimateDataSet.estimate);
            System.Windows.Data.CollectionViewSource estimateViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("estimateViewSource")));
            estimateViewSource.View.MoveCurrentToFirst();
        }
    }
}
