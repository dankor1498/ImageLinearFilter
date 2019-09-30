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
using System.IO;
using System.Windows.Forms;

namespace LinearFilter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string patchImage;
        Mat imageInput;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void PerformFiltering(Mat image, Mat kernel)
        {
            CvInvoke.Filter2D(imageInput, image, kernel, new System.Drawing.Point(-1, -1), 0.0, BorderType.Default);
            CvInvoke.Imshow(this.LinearFilter.Name, image);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (imageInput == null || this.ImageName.Text == "")
            {
                System.Windows.MessageBox.Show("Помилка відкриття файлу зображення.");
                return;
            }
            Mat image = imageInput.Clone();
            int kernelInt = (int)((Slider)sender).Value;
            if(kernelInt == 0)
            {
                CvInvoke.Imshow(this.LinearFilter.Name, imageInput);
                return;
            }
            Mat kernel = Mat.Ones(kernelInt, kernelInt, DepthType.Cv32F, 1) / (float)(kernelInt * kernelInt);
            PerformFiltering(image, kernel);
        }

        private void ReviewClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OPF = new OpenFileDialog();
                if (OPF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.ImageName.Text = OPF.FileName;
                    patchImage = @OPF.FileName;
                }

                imageInput = CvInvoke.Imread(patchImage, ImreadModes.Color);

                if (imageInput.IsEmpty)
                {
                    System.Windows.MessageBox.Show("Помилка відкриття файлу зображення.");
                    return;
                }

                CvInvoke.Imshow(this.LinearFilter.Name, imageInput);
            }
            catch
            {
                System.Windows.MessageBox.Show("Помилка відкриття файлу зображення.");
            }
        }
    }
}

