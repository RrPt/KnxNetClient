using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Knx
{
    class KnxTools
    {
        // Tools
        public static string BytesToString(byte[] receiveBytes)
        {
            if (receiveBytes==null) return "<nix>";
            String erg =  DateTime.Now.ToString("HH:mm:ss")+": ";
            for (int i = 0; i < receiveBytes.Length; i++) erg = erg + receiveBytes[i].ToString("X2") + " ";
            return erg;
        }


    }
}
