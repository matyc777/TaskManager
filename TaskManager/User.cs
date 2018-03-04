using System;

using System.Linq;
using System.Diagnostics;
using System.Management;

namespace TaskManager
{
    class User
    { 
        public static string GetProcessOwner(int processId)
        {
            var query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectCollection processList;

            using (var searcher = new ManagementObjectSearcher(query))
            {
                processList = searcher.Get();
            }

            foreach (var mo in processList.OfType < ManagementObject>())
            {
                object[] argList = { string.Empty, string.Empty };
                var returnVal = Convert.ToInt32(mo.InvokeMethod("GetOwner", argList));

                if (returnVal == 0)
                {
                    return argList[0].ToString();
                }
            }
            return "NO OWNER";
        }

        public static string ProsessPriority(Process process)
        {
            try
            {
                return process.BasePriority.ToString();
            }
            catch
            {
                return "Access is denied";
            }
        }

        public static string ProcessName(Process process)
        {
            try
            {
                return process.ProcessName.ToString();
            }
            catch
            {
                return "Access is denied";
            }
        }
        public static string ProcessId(Process process)
        {
            try
            {
                return process.Id.ToString();
            }
            catch
            {
                return "Access is denied";
            }
        }
        public static string ProcessMainWindowHandle(Process process)
        {
            try
            {
                return process.MainWindowHandle.ToString();
            }
            catch
            {
                return "Access is denied";
            }
        }
        public static string WorkingSet(Process process)
        {
            try
            {
                return (process.WorkingSet64 / 1048576).ToString();
            }
            catch
            {
                return "Access is denied";
            }
        }
        public static string StartTime(Process process)
        {
            try
            {
                return process.StartTime.ToString();
            }
            catch
            {
                return "Access is denied";
            }
        }
        public static string ThreadsC(Process process)
        {
            try
            {
                return process.Threads.Count.ToString();
            }
            catch
            {
                return "Access is denied";
            }
        }
        public static string ModulesC(Process process)
        {
            try
            {
                return process.Modules.Count.ToString();
            }
            catch
            {
                return "Access is denied";
            }
        }
    }
}