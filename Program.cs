// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
//using FfalconXR.Native;
using System.Runtime.InteropServices;
using FfalconXR;
//using FfalconXR.Native;
//using UnityEngine;//.CoreModule;
//using UnityEngine.UI;
using FfalconXR;

XRSDK myXRSDK = new XRSDK();

XRSDK.XRSDK_Init();

namespace FfalconXR
{
    //NativeModule.Instance.sensor_msg.ToString());

    public class XRSDK
    {
        //WindowsMessager myWindowsMessager = new WindowsMessager(new HardwareInfo());

        //public InputField m_input;
        //Update is called once per frame
        //public HardwareInfo myHardwareInfo = new HardwareInfo();

        //void Update()
        //    {
        //        m_input.text = myWindowsMessager.GetGlassesQualternion(new Quaternion()).ToString();//myHardwareInfo.DeltaGlassQuat.ToString();//typeof(WindowsMessager.GetGlassesQualternion()).ToString();//NativeModule.Instance.GetGlassesQualternion().ToString();
        //    }

        //void ResetInput()
        //{
        //    //NativeModule.Instance.ResetGlassQuat();
        //    //NativeModule.Instance.ResetMobileQuat();
        //    //m_input.text = NativeModule.Instance.GetGlassesQualternion().ToString();//清空inputField //NativeModule.Instance.GetGlassesQualternion().ToString();
        //}
        const String DLL_PATH = "XRSDK.dll";
        [DllImport(DLL_PATH, CharSet = CharSet.None)]
        public static extern IntPtr GetArSensor();

        [DllImport(DLL_PATH, CharSet = CharSet.Ansi)]
        public static extern bool Reset();

        [DllImport(DLL_PATH, CharSet = CharSet.Ansi)]
        public static extern bool XRSDK_Init();

        [DllImport(DLL_PATH, CharSet = CharSet.Ansi)]
        public static extern bool XRSDK_Shutdown();

        //[DllExport("ReadArSensors", CharSet = CharSet.None)]
        public String ReadArSensors()
        {
            IntPtr data = XRSDK.GetArSensor();
            XR_SensorMessage sensor_msg = new XR_SensorMessage();
            int size = Marshal.SizeOf(sensor_msg);
            try
            {
                byte[] buff = new byte[size];
                Marshal.Copy(data, buff, 0, (int)size);

                IntPtr structPtr = Marshal.AllocHGlobal(size);
                Marshal.Copy(buff, 0, structPtr, size);

                sensor_msg = (XR_SensorMessage)(Marshal.PtrToStructure(structPtr, typeof(XR_SensorMessage)));
                Marshal.FreeHGlobal(structPtr);
                return sensor_msg.Quat.x.ToString() + "," + sensor_msg.Quat.y.ToString() + "," + sensor_msg.Quat.z.ToString() + "," + sensor_msg.Quat.w.ToString();
                //return sensor_msg.Gyro.x.ToString() + "," + sensor_msg.Gyro.y.ToString() + "," + sensor_msg.Gyro.z.ToString();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public struct quat_data_struct
        {
            public float x;
            public float y;
            public float z;
            public float w;
        }

        public struct sensor_data_struct
        {
            public float x;
            public float y;
            public float z;
        }

        public struct XR_SensorMessage
        {
            public byte State;
            public byte Statel;
            public byte State2;
            public byte State3;
            public UInt32 timestamp;
            public sensor_data_struct Gyro;
            public sensor_data_struct Accel;
            public sensor_data_struct Mag;
            public quat_data_struct Quat;
        }

        public struct Quaternion
        {
            public float x;
            public float y;
            public float z;
            public float w;
        }
    }
}