using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoEnhancer
{
    public static class Convertors
    {
        public static Photo Bitmap2Photo(Bitmap bmp)
        {
            var result = new Photo(bmp.Width, bmp.Height);

            for (var x = 0; x < bmp.Width; x++)
                for (var y = 0; y < bmp.Height; y++)
                {
                    var pixel = bmp.GetPixel(x, y);

                    //result[x, y].R = (double)pixel.R / 255;
                    //result[x, y].G = (double)pixel.G / 255;
                    //result[x, y].B = (double)pixel.B / 255;

                    result[x, y] = new Pixel(
                        (double)pixel.R / 255,
                        (double)pixel.G / 255,
                        (double)pixel.B / 255
                        );
                }

            return result;
        }

        public static Bitmap Photo2Bitmap(Photo photo)
        {
            var result = new Bitmap(photo.Width, photo.Height);

            for (var x = 0; x < photo.Width; x++)
                for (var y = 0; y < photo.Height; y++)
                {
                    result.SetPixel(x, y, Color.FromArgb(
                        (int)(photo[x, y].R * 255),
                        (int)(photo[x, y].G * 255),
                        (int)(photo[x, y].B * 255)
                        ));
                }

            return result;
        }

        public static double GetPixelHue(Pixel p)
        {
            Color color = Pixel2Color(p);
            return color.GetHue();
        }

        public static double GetPixelSaturation(Pixel p)
        {
            var color = Pixel2Color(p);
            return color.GetSaturation();
        }

        public static double GetPixelLightness(Pixel p)
        {
            var color = Pixel2Color(p);
            return color.GetBrightness();
        }

        public static Color Pixel2Color(Pixel p)
        {
            return Color.FromArgb((int)(p.R * 255), (int)(p.G * 255), (int)(p.B * 255));
        }

        public static Pixel HSL2Pixel(double hue, double saturation, double lightness)
        {
            double q;
            if (lightness < 0.5)
                q = lightness * (1 + saturation);
            else
                q = lightness + saturation - lightness * saturation;

            double p = 2 * lightness - q;

            double h = hue / 360;

            var t = new[] { h + 1.0 / 3, h, h - 1.0 / 3 };

            for (var i = 0; i < 3; i++)
                if (t[i] < 0)
                    t[i] += 1;
                else if (t[i] > 1)
                    t[i] -= 1;

            var rgb = new double[3];

            for (var i = 0; i < 3; i++)
                if (t[i] < 1.0 / 6)
                    rgb[i] = p + ((q - p) * 6 * t[i]);
                else if (t[i] < 0.5)
                    rgb[i] = q;
                else if (t[i] < 2.0 / 3)
                    rgb[i] = p + ((q - p) * (2.0 / 3 - t[i]) * 6);
                else
                    rgb[i] = p;

            return new Pixel(rgb[0], rgb[1], rgb[2]);
        }


    }
}