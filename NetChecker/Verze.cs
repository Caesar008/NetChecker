using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace NetChecker
{
    class Verze
    {
        internal string verze()
        {
            string result = "";
            result = "Installed versions .NET:\r\n";
            // Opens the registry key for the .NET Framework entry. 
            using (RegistryKey ndpKey =
                RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
                OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                // As an alternative, if you know the computers you will query are running .NET Framework 4.5  
                // or later, you can use: 
                // using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,  
                // RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {

                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", "");
                        string sp = versionKey.GetValue("SP", "").ToString();
                        string install = versionKey.GetValue("Install", "").ToString();
                        if (install == "") //no install info, must be later.
                            result += versionKeyName + "  " + name + "\r\n";
                        else
                        {
                            if (sp != "" && install == "1")
                            {
                                result += versionKeyName + "  " + name + "  SP" + sp + "\r\n";
                            }

                        }
                        if (name != "")
                        {
                            continue;
                        }
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (name != "")
                                sp = subKey.GetValue("SP", "").ToString();
                            install = subKey.GetValue("Install", "").ToString();
                            if (install == "") //no install info, must be later.
                                result += versionKeyName + "  " + name + "\r\n";
                            else
                            {
                                if (sp != "" && install == "1")
                                {
                                    result += "  " + subKeyName + "  " + name + "  SP" + sp + "\r\n";
                                }
                                else if (install == "1")
                                {
                                    result += "  " + subKeyName + "  " + name + "\r\n";
                                }

                            }

                        }

                    }
                }
            }

            using (RegistryKey ndpKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (true)
                {
                    result += "\r\n.NET 4.5 and newer:\r\nVersion: " + CheckFor45DotVersion(releaseKey) + "\r\n";
                }
            }
            return result;
        }

        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 528040)
            {
                return "4.8 or newer";
            }
            if (releaseKey >= 461808)
            {
                return "4.7.2";
            }
            if (releaseKey >= 461308)
            {
                return "4.7.1";
            }
            if (releaseKey >= 460798)
            {
                return "4.7";
            }
            if (releaseKey >= 394802)
            {
                return "4.6.2";
            }
            if (releaseKey >= 394254)
            {
                return "4.6.1";
            }
            if (releaseKey >= 393295)
            {
                return "4.6";
            }
            if (releaseKey >= 379893)
            {
                return "4.5.2";
            }
            if (releaseKey >= 378675)
            {
                return "4.5.1";
            }
            if (releaseKey >= 378389)
            {
                return "4.5";
            }
            // This line should never execute. A non-null release key should mean 
            // that 4.5 nebo novější is installed. 
            return "No version 4.5 or newer was not found";
        }
    }
}
