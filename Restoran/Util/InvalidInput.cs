using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restoran.CustomExceptions;
using Restoran.Model;

namespace Restoran.Util
{
    public class InvalidInput
    {

        public static int ValidatePositiveInteger(string message, string errorMessage)
        {
            int number = 0;
            bool isValid = false;

            do
            {
                isValid = true;
                try
                {
                    Console.Write(message);
                    number = int.Parse(Console.ReadLine());

                    if (number < 0)
                    {
                        Console.WriteLine(errorMessage);
                        isValid = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(errorMessage);
                    isValid = false;
                }
            } while (!isValid);
            return number;
        }

        public static decimal ValidatePositiveDecimal(string message, string errorMessage)
        {
            decimal number = decimal.Zero;
            bool isValid = false;

            do
            {
                isValid = true;
                try
                {
                    Console.Write(message);
                    number = decimal.Parse(Console.ReadLine());

                    if (number < 0)
                    {
                        Console.WriteLine(errorMessage);
                        isValid = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(errorMessage);
                    isValid = false;
                }
            } while (!isValid);
            return number;
        }

        public static void CheckForDuplicateMealName(string name, List<Meal> meals) 
        {
            foreach(Meal meal in meals)
            {
                if(meal.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    throw new DuplicateEntryException($"Jelo s nazivom '{name}' već postoji.");
                }
            }
        }

        public static void CheckForDuplicateEmployeer(string firstName, string lastName, List<Person> employees)
        {
            foreach(Person employee in employees)
            {
                if (employee.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && 
                    employee.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                {
                    throw new DuplicateEntryException($"Zaposlenik '{firstName} {lastName}' već postoji.");
                }
            }
        }

        public static void CheckForDuplicateRestaurant(string name, List<Restaurant> restaurants)
        {
            foreach(Restaurant restaurant in restaurants)
            {
                if(restaurant.Name.Equals (name, StringComparison.OrdinalIgnoreCase))
                {
                    throw new DuplicateEntryException($"Restoran '{name}' već postoji");
                }
            }
        }
    }
}
