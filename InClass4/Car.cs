using System;

public class Car : Vehicle
{
    public string make { get; }
    public string type { get; }
    public string modelYear { get; }
    public int numberOfDoors { get; }

    public Car(string registrationNumber, string color, Person person, string make, string type,
        string modelYear, int numberOfDoors) : base(registrationNumber, color, person)
    {
        this.make = make;
        this.type = type;
        this.modelYear = modelYear;
        this.numberOfDoors = numberOfDoors;
    }

    public void drive()
    {
        move();
        Console.WriteLine("Driving...");
    }
}