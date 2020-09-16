using System;

public class Vehicle
{
    public string registrationNumber { get; }
    public string color { get; }
    public Person person { get; }

    public Vehicle(string registrationNumber, string color, Person person)
    {
        this.registrationNumber = registrationNumber;
        this.color = color;
        this.person = person;
    }

    public void move()
    {
        Console.WriteLine("Moving...");
    }

    public void changeOwner(Person newPerson)
    {
        person.firstName = newPerson.firstName;
        person.lastName = newPerson.lastName;
    }
}