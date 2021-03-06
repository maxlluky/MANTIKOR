using SharpPcap;
using System;
using System.Reflection;

class Menu
{
    public ICaptureDevice captureDevice;
    private static string deviceDescription = "(not configured)";

    public void printFrontend(TargetList pTargetList, Attack pAttack)
    {
        Console.Clear();

        string logo = @"
░█▀▄▀█ ─█▀▀█ ░█▄─░█ ▀▀█▀▀ ▀█▀ ░█─▄▀ ░█▀▀▀█ ░█▀▀█ 
░█░█░█ ░█▄▄█ ░█░█░█ ─░█── ░█─ ░█▀▄─ ░█──░█ ░█▄▄▀ 
░█──░█ ░█─░█ ░█──▀█ ─░█── ▄█▄ ░█─░█ ░█▄▄▄█ ░█─░█
";

        Console.WriteLine(logo);

        Console.WriteLine("MANTIKOR {0} & SharpPcap {1}\n", Assembly.GetExecutingAssembly().GetName().Version, SharpPcap.Version.VersionString);

        Console.WriteLine("Use the numbers to navigate!");
        Console.WriteLine("[1] Configure Network Adapter => {0}", deviceDescription);
        Console.WriteLine("[2] Define new Target  Targets => {0}", pTargetList.getLength());
        Console.WriteLine("[3] Print/Edit Target-List\n");
        Console.WriteLine("[4] Start Attack : Threads => {0}", pAttack.getThreadCount());
        Console.WriteLine("[5] Force Stop\n");
    }

    public void configureNetworkAdapter()
    {
        var devices = CaptureDeviceList.Instance;

        if (devices.Count < 1)
        {
            Console.WriteLine("No devices were found on this machine");
            return;
        }

        int i = 0;

        foreach (var dev in devices)
        {
            dev.Open();
            Console.WriteLine("{0}) {1} {2}", i, dev.Description, dev.MacAddress);
            dev.Close();
            i++;
        }

        Console.WriteLine();
        Console.Write("Please choose an Adapter: ");

        captureDevice = devices[int.Parse(Console.ReadLine())];
        captureDevice.Open();
        deviceDescription = captureDevice.Description;
    }
}
