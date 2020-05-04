using System;
using Emgu.CV;
using System.IO;

namespace BasicSobel
{
    class Program
    {
        static void Main(string[] args)
        {
            var img = CvInvoke.Imread(Path.Join("resources", "ZeroSweater.jpg"));
            var gray = new Mat(img.Rows, img.Cols, Emgu.CV.CvEnum.DepthType.Cv16S, 1);
            var gradX = new Mat(gray.Rows, gray.Cols, Emgu.CV.CvEnum.DepthType.Cv16S, 1);
            var gradY = new Mat(gray.Rows, gray.Cols, Emgu.CV.CvEnum.DepthType.Cv16S, 1);
            var absGradX = new Mat(gray.Rows, gray.Cols, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
            var absGradY = new Mat(gray.Rows, gray.Cols, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
            var sobelGrad = new Mat(gray.Rows, gray.Cols, Emgu.CV.CvEnum.DepthType.Cv8U, 1);

            CvInvoke.CvtColor(img, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            CvInvoke.GaussianBlur(gray, gray, new System.Drawing.Size(3, 3), 0);            

            CvInvoke.Sobel(gray, gradX, Emgu.CV.CvEnum.DepthType.Cv8U, 1, 0, 3);
            CvInvoke.Sobel(gray, gradY, Emgu.CV.CvEnum.DepthType.Cv8U, 0, 1, 3);

            CvInvoke.ConvertScaleAbs(gradX, absGradX, 1, 0);
            CvInvoke.ConvertScaleAbs(gradY, absGradY, 1, 0);

            CvInvoke.AddWeighted(absGradX, .5, absGradY, .5, 0, sobelGrad);

            CvInvoke.Imshow("sobel x", absGradX);
            CvInvoke.Imshow("sobel Y", absGradY);
            CvInvoke.Imshow("sobel", sobelGrad);

            CvInvoke.Imwrite("sobelX.jpg", absGradX);
            CvInvoke.Imwrite("sobelY.jpg", absGradY);
            CvInvoke.Imwrite("sobel.jpg", sobelGrad);

            CvInvoke.Imshow("gray", gray);
            CvInvoke.WaitKey(0);            
        }
    }
}
