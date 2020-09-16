using System;

public class Bike : Vehicle
{
    public string make { get; }
    public string type { get; }
    public int numberOfGears { get; }

    public Bike(string registrationNumber, string color, Person person, string make, string type,
        int numberOfGears) : base(registrationNumber, color, person)
    {
        this.make = make;
        this.type = type;
        this.numberOfGears = numberOfGears;
    }

    public void ride()
    {
        move();
        Console.WriteLine("Riding...");
    }
}