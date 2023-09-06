using System.IO.Ports;

class PortDataReceived
{
    public static void Main()
    {
        LightValueClass.UpdateTime = DateTime.Now;
        DateTime time = DateTime.Now;
        string filePath = $"VALUES\\Light Values {time.ToString("d T")}.txt";
        SerialPort mySerialPort = new SerialPort("COM8");
        mySerialPort.BaudRate = 9600;
        mySerialPort.Parity = Parity.None;
        mySerialPort.StopBits = StopBits.One;
        mySerialPort.DataBits = 8;
        mySerialPort.Handshake = Handshake.None;

        mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        mySerialPort.Open();

        Console.WriteLine("Press any key to exit...");
        Console.WriteLine();
        Console.ReadKey();
        mySerialPort.Close();
        File.WriteAllText(filePath, LightValueClass.LightValue);
    }

    public static class LightValueClass
    {
        public static string? LightValue { get; set; }
        public static DateTime UpdateTime { get; set; }
    }

    private static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;
        string indata = sp.ReadExisting();

        SaveDataToFile(indata);
        //
    }

    private static void SaveDataToFile(string data)
    {
        DateTime newUpdate = DateTime.Now;
        DateTime lastUpdate = LightValueClass.UpdateTime;

        int secondDif = (int)newUpdate.Subtract(lastUpdate).TotalSeconds;
        int minuteDif = (int)newUpdate.Subtract(lastUpdate).TotalMinutes;
        int hourDif = (int)newUpdate.Subtract(lastUpdate).TotalHours;
        int dayDif = (int)newUpdate.Subtract(lastUpdate).TotalDays;

        if (hourDif >= 1)
        {
            Console.WriteLine($"NEW HOUR\n{newUpdate}");
            Console.WriteLine($"{data}");
            LightValueClass.UpdateTime = newUpdate;
            LightValueClass.LightValue += $"[{newUpdate}]\n{data}\n";

        }
        else
        {
            Console.WriteLine($"{data}");
            LightValueClass.LightValue += $"{data}\n";
        }

    }
}