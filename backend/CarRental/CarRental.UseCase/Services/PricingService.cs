using CarRental.Core.Models;

namespace CarRental.UseCase.Services;

public class PricingService
{
    private const decimal WagonConstant = 1.3m;
    private const decimal SmallCarConstant = 1m;
    private const decimal TruckConstant = 1.5m;
    public static decimal CalculatePrice(int distance, CarType carType, Setting settings, TimeSpan rentalDuration)
    {
        var basePrice = CalculateBasePrice(carType, settings, rentalDuration);
        var distancePrice = CalculateDistancePrice(distance, carType, settings);

        return basePrice + distancePrice;
    }

    private static decimal CalculateBasePrice(CarType carType, Setting settings, TimeSpan rentalDuration)
    {
        var daysCharged = decimal.Ceiling(Convert.ToDecimal(rentalDuration.TotalDays));
        var multiplier = carType switch
        {
            CarType.SmallCar => SmallCarConstant,
            CarType.StationWagon =>  WagonConstant,
            CarType.Truck => TruckConstant,
            _ => 0,
        };
        
        return daysCharged * settings.BaseDayFee * multiplier;
    }

    private static decimal CalculateDistancePrice(int distance, CarType carType, Setting settings)
    {
        var multiplier = carType switch
        {
            CarType.StationWagon => 1m,
            CarType.Truck => TruckConstant,
            _ => 0,
        };
        
        return settings.BaseKmFee * distance * multiplier;
    }
}