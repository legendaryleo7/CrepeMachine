namespace BlazorApp.Data;

public class CrepeMachine
{
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    public decimal TemperatureOneCelsius { get; set; } = 0;

    public decimal TemperatureTwoCelsius { get; set; } = 0;

    public decimal PowerOne { get; set; } = 0;

    public decimal PowerTwo {get; set;} = 0;

}
