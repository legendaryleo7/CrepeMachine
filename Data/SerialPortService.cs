using BlazorApp.Data;

public class ReadSerialPortService
{
    public string SerialPortValue { get; set; } = string.Empty;

    private readonly CrepeMachine _crepeMachine;

    public ReadSerialPortService(CrepeMachine crepeMachine)
    {
        _crepeMachine = crepeMachine;
        Console.WriteLine($"{ string.Join(", ", System.IO.Ports.SerialPort.GetPortNames())}");
        
        try {
            System.IO.Ports.SerialPort mySerialPort = new System.IO.Ports.SerialPort(Environment.GetEnvironmentVariable("SerialPort") ?? "COM4");
            mySerialPort.BaudRate = 57600;

            if (!mySerialPort.IsOpen)
            {
                mySerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceivedHandler);
                mySerialPort.Open();
            }
        } catch (Exception e)
        {
            Console.WriteLine("Could not open serial connection. Error Message: {0}", e.Message);
            Console.WriteLine("Available serial ports: {0}", string.Join(", ", System.IO.Ports.SerialPort.GetPortNames()));
        }

        
    }

    private void DataReceivedHandler(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    {
        System.IO.Ports.SerialPort sp = (System.IO.Ports.SerialPort)sender;
        try
        {
            string indata = sp.ReadLine();
            var chunks = indata.Split(',');
            _crepeMachine.LastUpdated = DateTime.Now;
            _crepeMachine.TemperatureOneCelsius = decimal.Parse(chunks[1]);
            _crepeMachine.TemperatureTwoCelsius = decimal.Parse(chunks[0]);
            _crepeMachine.PowerOne = decimal.Parse(chunks[6]);
            _crepeMachine.PowerTwo = decimal.Parse(chunks[7]);
            
            SerialPortValue = indata.ToString();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }

    public Task<string> GetSerialValue()
    {
        return Task.FromResult(SerialPortValue);
    }
}