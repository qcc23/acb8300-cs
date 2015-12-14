using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeColourFinder
{
    public class SensorReading
    {
        public struct XYZStruct
        {
            public double X;
            public double Y;
            public double Z;
        }

        public struct YyxStruct
        {
            public double Y;
            public double y;
            public double x;
        }

        public struct RGBStruct
        {
            public double R;
            public double G;
            public double B;
        }
        
        public XYZStruct XYZ;
        public YyxStruct Yyx;
        public RGBStruct RGB;
        private double _maxBrightness = 100.0;
        private double _gamma = 2.2;  // The gamma curve the display is operating at.


        /// <summary>
        /// Create a SensorReading with the raw XYZ values supplied by the sensor, in addition to the measured maximum brightness for normalisation.
        /// 
        /// The LG XYZ values are converted to and from Yyx to normalise them to standard XYZ. The input XYZ are replaced with the normalised XYZ.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        public SensorReading(double X, double Y, double Z, double maxBrightness)
        {
            XYZ = new XYZStruct();
            Yyx = new YyxStruct();
            RGB = new RGBStruct();

            _maxBrightness = maxBrightness;

            XYZtoYyx(X, Y, Z, ref Yyx);
            YyxtoXYZ(ref Yyx, ref XYZ);
            XYZtoRGB(ref XYZ, ref RGB);
        }

        /// <summary>
        /// This is slightly modified to handle the output of the LG sensor. The brightness is scaled from 0-100 where 100 is the measured maximum brightness.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="Yyx"></param>
        private void XYZtoYyx(double X, double Y, double Z, ref YyxStruct Yyx)
        {
            Yyx.Y = (Math.Min(Y, _maxBrightness) / _maxBrightness) * 100.0; // normalise max brightness to 100

            // Too dark for meaningful results. Force to blackest thing ever.
            if (X < 0.1 && Y < 0.1 && Z < 0.1)
            {
                Yyx.Y = 0.0;
                Yyx.y = 0.0;
                Yyx.x = 0.0;
            }
            else
            {
                Yyx.y = Y / (X + Y + Z);
                Yyx.x = X / (X + Y + Z);
            }
        }

        private void YyxtoXYZ(ref YyxStruct Yyx, ref XYZStruct XYZ)
        {
            if (Yyx.y == 0.0 && Yyx.Y == 0.0 && Yyx.x == 0.0)
            {
                // Avoid divide by zero, just make it zero.
                XYZ.X = 0.0;
                XYZ.Y = 0.0;
                XYZ.Z = 0.0;
            }
            else
            {
                XYZ.X = Yyx.x * (Yyx.Y / Yyx.y);
                XYZ.Y = Yyx.Y;
                XYZ.Z = (1 - Yyx.x - Yyx.y) * (Yyx.Y / Yyx.y);
            }
        }

        private double FromGamma(double level, double gamma)
        {
            // Calculate the linear value from a measured gamma-adjusted value
            return Math.Pow(level, 1 / gamma);
        }

        private double To255(double value)
        {
            // Convert a number in the range 0.0-1.0 to 0-255, but allowing values in excess of 255.
            return Math.Max(0, value * 255);
        }

        private void XYZtoRGB(ref XYZStruct XYZ, ref RGBStruct RGB)
        {
            double x = XYZ.X / 100; //X from 0 to  95.047      (Observer = 2°, Illuminant = D65)
            double y = XYZ.Y / 100; // Y from 0 to 100.000
            double z = XYZ.Z / 100; //Z from 0 to 108.883

            double r = x * 3.2406 + y * -1.5372 + z * -0.4986;
            double g = x * -0.9689 + y * 1.8758 + z * 0.0415;
            double b = x * 0.0557 + y * -0.2040 + z * 1.0570;

            RGB.R = To255(FromGamma(r, _gamma));
            RGB.G = To255(FromGamma(g, _gamma));
            RGB.B = To255(FromGamma(b, _gamma));
        }
    }
}
