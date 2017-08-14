using System;
using System.IO;
using BT7274.DataServices;

namespace BT7274.MilitiaHeadquarters
{
    public static class Locator
    {
        public static LoadoutData LoadoutData { get; set; }

        public static string CurrentDirectory { get; } = Directory.GetCurrentDirectory();
    }
}