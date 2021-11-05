using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    class SaturationFilter: PixelFilter
    {
        public SaturationFilter() : base(new SaturationParameters()) { }
        public override string ToString()
        {
            return "Насыщенность";
        }
        public override Pixel ProcessPixel(Pixel originalPixel,
            IParameters parameters)
        {
            var origH = Convertors.GetPixelHue(originalPixel);
            var origS = Convertors.GetPixelSaturation(originalPixel);
            var origL = Convertors.GetPixelLightness(originalPixel);

            var newS =origS * (parameters as SaturationParameters).Coefficient;
            if (newS < 1 & newS>0)
                return Convertors.HSL2Pixel(origH, newS, origL);
            else
                return Convertors.HSL2Pixel(origH, Pixel.Trim(newS), origL); 


        }
    }
}
