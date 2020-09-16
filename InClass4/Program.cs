using System;

namespace InClass4
{
    class Program
    {
        static void Main(string[] args)
        {
            // creating three persons, one for each class
            var person1 = new Person("Michael", "Borkland");
            var person2 = new Person("Joe", "Biden");
            var person3 = new Person("Donald", "Trump");

            // create generic vehicle
            var vehicle = new Vehicle("1111", "blue", person1);
            Console.WriteLine(String.Format("Occupant in generic vehicle: {0} {1}", vehicle.person.firstName, vehicle.person.lastName));
            vehicle.move();

            // create car
            var car = new Car("2222", "red", person2, "Toyota", "Camry", "2015", 4);
            Console.WriteLine(String.Format("Occupant in car: {0} {1}", car.person.firstName, car.person.lastName));
            car.drive();

            // create bike
            var bike = new Bike("3333", "black", person3, "BikeBrand", "BikeModel", 10);
            Console.WriteLine(String.Format("Occupant of bike: {0} {1}", bike.person.firstName, bike.person.lastName));
            bike.ride();

            // change occupant of car
            var person4 = new Person("Kamala", "Harris");
            car.changeOwner(person4);
            Console.WriteLine(String.Format("New occupant in car: {0} {1}", car.person.firstName, car.person.lastName));
        }
    }
}
