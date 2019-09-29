using System;
using System.Drawing;
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
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.CvEnum;
using System.Threading;

namespace LinearFilter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //PerformFiltering(@"C:\Users\DanKor1498\Desktop\apple.jpg", 3);
        }

        public void PerformFiltering(string fileName, int kernelSize)
        {
            Mat imageInput, imageOutput;
            imageInput = CvInvoke.Imread(fileName, ImreadModes.Color);
            imageOutput = imageInput;

            Mat kernel;
            System.Drawing.Point anchor = new System.Drawing.Point(-1, -1); 
            double delta = 0.0;

            if (imageInput.IsEmpty)
            {
                MessageBox.Show("Помилка відкриття файлу зображення.");
                return;
            }

            kernel = Mat.Ones(kernelSize, kernelSize, DepthType.Cv32F, 1) / (float)(kernelSize * kernelSize);
            CvInvoke.Filter2D(imageInput, imageOutput, kernel, anchor, delta, BorderType.Default);
            CvInvoke.Imshow(this.LinearFilter.Name, imageOutput);

            //for(int i = 3; i<10; i++)
            //{
            //    kernel = Mat.Ones(i, i, DepthType.Cv32F, 1) / (float)(i * i);
            //    CvInvoke.Filter2D(imageInput, imageOutput, kernel, anchor, delta, BorderType.Default);
            //    CvInvoke.Imshow(this.LinearFilter.Name, imageOutput);
            //    CvInvoke.WaitKey(500);
            //}
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PerformFiltering(@"C:\Users\DanKor1498\Desktop\apple.jpg", (int)((Slider)sender).Value);
        }
    }
}
