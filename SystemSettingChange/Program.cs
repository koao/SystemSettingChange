using System;
using CoreAudioApi;
using System.Management;

namespace SystemSettingChange
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                Console.WriteLine("Please enter a number");
                Console.WriteLine("1.DateTime 2.Volume 3.Brightness 9.Exit");

                string selectNum = Console.ReadLine();

                switch(selectNum)
                {
                    case "1":
                        Console.WriteLine("Please enter the date and time to set up");
                        try
                        {
                            DateTime dt = DateTime.Parse(Console.ReadLine());
                            SetSystemDateTime(dt);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("EXCEPTION:{0}", ex.Message);
                        }
                        break;
                    case "2":
                        Console.WriteLine("Please enter the volume values between 0 and 100");
                        try
                        {
                            int value = Int32.Parse(Console.ReadLine());
                            SetSystemVolume(value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("EXCEPTION:{0}", ex.Message);
                        }

                        break;
                    case "3":
                        Console.WriteLine("Please enter the brightness values between 0 and 100");
                        try
                        {
                            int value = Int32.Parse(Console.ReadLine());
                            SetSystemBrightness(value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("EXCEPTION:{0}", ex.Message);
                        }
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

            }
        }

        public static void SetSystemDateTime(DateTime dt)
        {
            Microsoft.VisualBasic.DateAndTime.Today = dt;
            Microsoft.VisualBasic.DateAndTime.TimeOfDay = dt;
        }

        public static void SetSystemVolume(int value)
        {
            MMDevice device;
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)value / 100.0f);
        }

        public static void SetSystemBrightness(int level)
        {
            ManagementClass WmiMonitorBrightnessMethods = new ManagementClass("root/wmi", "WmiMonitorBrightnessMethods", null);

            foreach (ManagementObject mo in WmiMonitorBrightnessMethods.GetInstances())
            {
                ManagementBaseObject inParams = mo.GetMethodParameters("WmiSetBrightness");
                inParams["Brightness"] = level;
                inParams["Timeout"] = 5;
                mo.InvokeMethod("WmiSetBrightness", inParams, null);
            }
        }
    }
}
