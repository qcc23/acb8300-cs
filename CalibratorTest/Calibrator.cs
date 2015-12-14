using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FakeColourFinder
{
    public class Calibrator
    {
        [DllImport("LG_ACB8300.dll", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern int LG_Calibrator_DeviceOpen();

        [DllImport("LG_ACB8300.dll", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern int LG_Calibrator_SetMonitorType(int i);

        [DllImport("LG_ACB8300.dll", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern int LG_Calibrator_GetXYZ(ref double a, ref double b, ref double c, uint repeats);

        public static bool Initialise()
        {
            if (LG_Calibrator_DeviceOpen() != 0)
            {
                return false;
            }

            LG_Calibrator_SetMonitorType(0);
            return true;
        }

        public static double GetBrightness()
        {
            double dummy = 0.0;
            double rawY = 0.0;

            LG_Calibrator_GetXYZ(ref dummy, ref rawY, ref dummy, 5);

            return rawY;
        }

        public static SensorReading GetReading(double maxBrightness, uint readings=3)
        {
            double rawX = 0.0;
            double rawY = 0.0;
            double rawZ = 0.0;

            LG_Calibrator_GetXYZ(ref rawX, ref rawY, ref rawZ, readings);

            return new SensorReading(rawX, rawY, rawZ, maxBrightness);
        }
    }
}
