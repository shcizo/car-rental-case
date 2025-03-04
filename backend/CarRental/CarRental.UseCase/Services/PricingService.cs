
using CarRental.Core.Models;

namespace CarRental.UseCase.Services;

public class PricingService
{
    public static decimal CalculatePrice(int distance, CarType carType, Setting settings, TimeSpan rentalDuration)
    {
        var basePrice = CalculateBasePrice(carType, settings, rentalDuration);
        var distancePrice = CalculateDistancePrice(distance, carType, settings);
        
        return basePrice + distancePrice;
    }

    private static decimal CalculateBasePrice(CarType carType, Setting settings, TimeSpan rentalDuration)
    {
        var daysCharged = decimal.Ceiling(Convert.ToDecimal(rentalDuration.TotalDays));
        return carType switch
        {
            CarType.SmallCar => daysCharged * settings.BaseDayFee,
            CarType.WagonCar => daysCharged * settings.BaseDayFee * 1.3m,
            CarType.TruckCar => daysCharged * settings.BaseDayFee * 1.3m,
            _ => 0
        };
    }

    private static decimal CalculateDistancePrice(int distance, CarType carType, Setting settings)
    {
        return carType switch
        {
            CarType.WagonCar => settings.BaseKmFee * distance,
            CarType.TruckCar => settings.BaseKmFee * distance * 1.5m,
            _ => 0
        };
    }
}