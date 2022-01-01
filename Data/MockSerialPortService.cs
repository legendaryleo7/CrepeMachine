using BlazorApp.Data;

public class MockReadSerialPortService
{
    public string SerialPortValue { get; set; } = string.Empty;

    private readonly CrepeMachine _crepeMachine;

    private Timer _timer;

    private int i = 0;

    public MockReadSerialPortService(CrepeMachine crepeMachine)
    {
        _crepeMachine = crepeMachine;
        InitCrepeMachineMockValues();
        _timer = new Timer(state => { GenerateMockValues(); }, null, 0, 3000);
    }

    private void InitCrepeMachineMockValues()
    {
        _crepeMachine.TemperatureOneCelsius = 25.64M;
        _crepeMachine.TemperatureTwoCelsius = 29.28M;
        _crepeMachine.PowerOne = 1.00M;
        _crepeMachine.PowerTwo = 0M;
    }

    private void GenerateMockValues()
    {
        var random = new Random();

        if (i % 3 == 0)
        {
            _crepeMachine.TemperatureOneCelsius -= (decimal)random.NextDouble();
            _crepeMachine.TemperatureTwoCelsius -= (decimal)random.NextDouble();
        }
        else
        {
            _crepeMachine.TemperatureOneCelsius += (decimal)random.NextDouble();
            _crepeMachine.TemperatureTwoCelsius += (decimal)random.NextDouble();
        }

        _crepeMachine.PowerOne = (decimal)random.NextDouble();
        _crepeMachine.PowerTwo = (decimal)random.NextDouble();

        if (_crepeMachine.TemperatureOneCelsius > 300)
        {
            _crepeMachine.TemperatureOneCelsius = 25M;
        }

        if (_crepeMachine.TemperatureTwoCelsius > 300)
        {
            _crepeMachine.TemperatureTwoCelsius = 25;
        }

        i++;
    }
}