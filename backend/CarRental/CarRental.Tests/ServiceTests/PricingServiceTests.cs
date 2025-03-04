using CarRental.Core.Models;
using CarRental.UseCase.Services;
using FluentAssertions;

namespace CarRental.Tests.ServiceTests;

[TestFixture]
public class PricingServiceTests
{
    [SetUp]
    public void Setup()
    {
        _settings = new Setting
        {
            BaseDayFee = 100, // Example base fee per day
            BaseKmFee = 2, // Example per km fee
        };
    }

    private Setting _settings;

    [Test]
    public void CalculatePrice_SmallCar_OneDayRental_ReturnsBaseDayFee()
    {
        // Arrange
        var rentalDuration = TimeSpan.FromHours(24); // 1 full day
        const int distance = 100;
        const CarType carType = CarType.SmallCar;

        // Act
        var price = PricingService.CalculatePrice(distance, carType, _settings, rentalDuration);

        // Assert
        price.Should().Be(_settings.BaseDayFee);
    }

    [Test]
    public void CalculatePrice_WagonCar_TwoDaysRental_ReturnsCorrectPrice()
    {
        // Arrange
        var rentalDuration = TimeSpan.FromHours(48); // 2 full days
        var distance = 0;
        var carType = CarType.WagonCar;

        // Act
        var price = PricingService.CalculatePrice(distance, carType, _settings, rentalDuration);

        // Assert
        price.Should().Be(_settings.BaseKmFee * _settings.BaseDayFee * 1.3m);
    }

    [Test]
    public void CalculatePrice_TruckCar_WithDistance_ReturnsCorrectPrice()
    {
        // Arrange
        var rentalDuration = TimeSpan.FromHours(24); // 1 full day
        var distance = 50;
        var carType = CarType.TruckCar;

        // Act
        var price = PricingService.CalculatePrice(distance, carType, _settings, rentalDuration);

        // Assert
        price.Should().Be(_settings.BaseDayFee * 1.3m + _settings.BaseKmFee * distance * 1.5m);
    }

    [Test]
    public void CalculatePrice_WagonCar_WithDistance_ReturnsCorrectPrice()
    {
        // Arrange
        var rentalDuration = TimeSpan.FromHours(24); // 1 full day
        var distance = 30;
        var carType = CarType.WagonCar;

        // Act
        var price = PricingService.CalculatePrice(distance, carType, _settings, rentalDuration);

        // Assert
        price.Should().Be(_settings.BaseDayFee * 1.3m + _settings.BaseKmFee * distance);
    }

    [Test]
    public void CalculatePrice_SmallCar_ZeroDistance_ZeroDays_ReturnsZero()
    {
        // Arrange
        var rentalDuration = TimeSpan.Zero;
        var distance = 0;
        var carType = CarType.SmallCar;

        // Act
        var price = PricingService.CalculatePrice(distance, carType, _settings, rentalDuration);

        // Assert
        price.Should().Be(0);
    }
}