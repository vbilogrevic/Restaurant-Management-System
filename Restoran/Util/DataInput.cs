using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Restoran.CustomExceptions;
using Restoran.Model;

namespace Restoran.Util
{
    public class DataInput
    {

        public const int NUMBER_OF_CATEGORIES = 3;
        public const int NUMBER_OF_INGREDIANTS = 5; 
        public const int NUMBER_OF_MEALS = 3;
        public const int NUMBER_OF_RESTAURANTS = 3;
        public const int NUMBER_OF_ORDERS = 3;
        public const int NUMBER_OF_EMPLOYEES = 5;

        public static long idBrojac = 0;
        public const int MINIMUM_SALARY = 1400;

        public static Category NextCategory()
        {
            Console.Write("Naziv: ");
            string name = Console.ReadLine();

            Console.Write("Opis: ");
            string description = Console.ReadLine();

            return new Category(idBrojac++, name, description);
        }

        public static Ingredient NextIngredient(List<Category> categories)
        {
            Console.Write("Naziv: ");
            string name = Console.ReadLine();

            Category tempCategory;
            Console.WriteLine("Kategorije");
            for (int j = 0; j < categories.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {categories[j].Name}, {categories[j].Description}");
            }


            int index;
            do
            {
                index = InvalidInput.ValidatePositiveInteger("Odaberite kategoriju: ",
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;
                if (index < 0 || index > categories.Count)
                {
                    Console.WriteLine("Unijeli ste neispravnu vrijednost!");
                }
            } while (index < 0 || index > categories.Count);
            tempCategory = categories[index];

            decimal kcal = InvalidInput.ValidatePositiveDecimal("Kalorije: ", 
                Messages.INVALID_BIGDECIMAL_INPUT_AND_NEGATIVE_ERROR);

            Console.Write("Način pripreme: ");
            string tempPreperationMethod = Console.ReadLine();

            Ingredient ingredient = new Ingredient(idBrojac++, name, tempCategory, kcal, tempPreperationMethod);
            return ingredient;
        }

        public static Meal NextMeal(List<Category> categories, List<Ingredient> ingredients, List<Meal> meals)
        {
            int typeOfMeal;
            do
            {
                typeOfMeal = InvalidInput.ValidatePositiveInteger("Odaberite\n1. Vegan\n2. Vegetarian\n3. Meat\nOdaberite: ", 
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);
                if (typeOfMeal < 1 || typeOfMeal > 3)
                {
                    Console.WriteLine("Pogrešan unos, ponovite!");
                }
            } while (typeOfMeal < 1 || typeOfMeal > 3);

            string name;
            while(true)
            {
                try
                {
                    Console.Write("Naziv: ");
                    name = Console.ReadLine();

                    InvalidInput.CheckForDuplicateMealName(name, meals);
                    break;
                }
                catch(DuplicateEntryException e)
                {
                    Console.WriteLine($"Pogreška: {e.Message} Pokušajte ponovo.");
                }
            }

            Category tempCategory;
            Console.WriteLine("Kategorije");
            for (int j = 0; j < categories.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {categories[j].Name}, {categories[j].Description}");
            }

            int index;
            do
            {
                index = InvalidInput.ValidatePositiveInteger("Odaberite kategoriju: ",
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;
                if (index < 0 || index > categories.Count)
                {
                    Console.WriteLine("Unijeli ste neispravnu vrijednost!");
                }
            } while (index < 0 || index > categories.Count);
            tempCategory = categories[index];

            Console.WriteLine("Namirnice");
            for (int j = 0; j < ingredients.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {ingredients[j].Name}");
            }

            int numberOfIngredients = InvalidInput.ValidatePositiveInteger("Koliko sastojaka želite odabrati: ", 
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);

            List<Ingredient> tempIngredients = new List<Ingredient>();

            for (int j = 0; j < numberOfIngredients; j++)
            {
                int indexIngredient;

                do
                {
                    indexIngredient = InvalidInput.ValidatePositiveInteger($"Unesite {j + 1}. sastojak: ",
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;
                    if(indexIngredient < 0 || indexIngredient > ingredients.Count)
                    {
                        Console.WriteLine("Unijeli ste neispravnu vrijednost!");
                    }
                } while (indexIngredient < 0 || indexIngredient > ingredients.Count);

                tempIngredients.Add(ingredients[indexIngredient]);
            }

            decimal price;
            while(true)
            {
                try
                {
                    price = InvalidInput.ValidatePositiveDecimal("Cijena: ", 
                        Messages.INVALID_BIGDECIMAL_INPUT_AND_NEGATIVE_ERROR);
                    if (price > 45)
                    {
                        throw new InvalidValueException("Cijena jela ne može biti veća od 45");
                    }
                    break;
                }
                catch (InvalidValueException e) {
                    Console.WriteLine("Pogreška: " + e.Message);
                }
            }

            
            switch (typeOfMeal)
            {
                case 1: 
                    int kcal = InvalidInput.ValidatePositiveInteger("Kalorije: ", 
                        Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);
                    return new VeganMeal(idBrojac++, name, tempCategory, tempIngredients, price, kcal);

                case 2: 
                    Console.Write("Sadrži li jelo orašaste plodove (da / ne): ");
                    string nutsString = Console.ReadLine();
                    bool containsNuts = nutsString.Equals("da", StringComparison.OrdinalIgnoreCase);
                    return new VegetarianMeal(idBrojac++, name, tempCategory, tempIngredients, price, containsNuts);

                case 3: 
                    Console.Write("Vrsta mesa: ");
                    string meatType = Console.ReadLine();
                    return new MeatMeal(idBrojac++, name, tempCategory, tempIngredients, price, meatType);

                default:
                    throw new InvalidOperationException("Nepoznat odabir vrste jela.");
            }
        }
        

        public static Person NextPerson(List<Person> employees)
        {
            int typeOfEmployee;
            do
            {
                typeOfEmployee = InvalidInput.ValidatePositiveInteger("Odaberite vrstu zaposlenika\n1. Kuhar\n2. Konobar\n3. Dostavljač\nOdaberite: ", 
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);
                if (typeOfEmployee < 1 || typeOfEmployee > 3)
                {
                    Console.WriteLine("Pogrešan unos, ponovite!");
                }
            } while (typeOfEmployee < 1 || typeOfEmployee > 3);


            string firstName, lastName;

            while(true)
            {
                try
                {
                    Console.Write("Ime: ");
                    firstName = Console.ReadLine();
                    Console.Write("Prezime: ");
                    lastName = Console.ReadLine();

                    InvalidInput.CheckForDuplicateEmployeer(firstName, lastName, employees);
                    break;
                }
                catch(DuplicateEntryException e)
                {
                    Console.WriteLine("Pogreška, " + e.Message);
                }
            }


            decimal salary;
            while(true)
            {
                try
                {
                    salary = InvalidInput.ValidatePositiveDecimal("Plaća: ", 
                        Messages.INVALID_BIGDECIMAL_INPUT_AND_NEGATIVE_ERROR);
                    if (salary < MINIMUM_SALARY)
                    {
                        throw new InvalidValueException($"Plaća ne može biti manje od {MINIMUM_SALARY}");
                    }
                    break;
                }
                catch (InvalidValueException e)
                {
                    Console.WriteLine("Pogreška: " + e.Message);
                }
            }


            string startDateString, endDateString;

            DateTime startDate;
            while (true)
            {
                Console.Write("Datum početka rada (dd.MM.yyyy): ");
                startDateString = Console.ReadLine();

                if (DateTime.TryParseExact(startDateString, "dd.MM.yyyy", null,
                     System.Globalization.DateTimeStyles.None, out startDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Neispravan format datuma. Pokušajte ponovo (format: dd.MM.yyyy)");
                }
            }

            DateTime endDate;
            while (true)
            {
                Console.Write("Datum početka rada (dd.MM.yyyy): ");
                endDateString = Console.ReadLine();

                if (DateTime.TryParseExact(endDateString, "dd.MM.yyyy", null,
                     System.Globalization.DateTimeStyles.None, out endDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Neispravan format datuma. Pokušajte ponovo (format: dd.MM.yyyy)");
                }
            }

            int typeOfContract;
            do
            {
                typeOfContract = InvalidInput.ValidatePositiveInteger("Vrsta ugovora\n1. Full Time\n2. Part time\nOdaberite: ", 
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);
                if (typeOfContract < 1 || typeOfContract > 2)
                {
                    Console.WriteLine("Pogrešan unos, ponovite");
                }
            } while (typeOfContract < 1 || typeOfContract > 2);

            ContractType contractType;

            switch (typeOfContract)
            {
                case 1:
                    contractType = ContractType.FullTime; break;
                case 2:
                    contractType = ContractType.PartTime; break;
                default:
                    throw new InvalidOperationException("Nepoznata vrsta ugovora.");

            }

            Model.Contract contract = new Model.Contract(salary, startDate, endDate, contractType);

            return typeOfEmployee switch
            {
                1 => new Chef(firstName, lastName, contract),
                2 => new Waiter(firstName, lastName, contract),
                3 => new Deliverer(firstName, lastName, contract),
                _ => throw new InvalidOperationException("Nepoznat odabir")
            };
        }

        public static Restaurant NextRestaurant(List<Meal> meals, List<Person> employees, List<Restaurant> restaurants)
        {
            string name;

            while(true)
            {
                try
                {
                    Console.Write("Naziv: ");
                    name = Console.ReadLine();
                    InvalidInput.CheckForDuplicateRestaurant(name, restaurants);
                    break;
                }
                catch(DuplicateEntryException e)
                {
                    Console.WriteLine("Pogreška, " + e.Message);
                }
            }

            Console.Write("Ulica: ");
            string street = Console.ReadLine();

            Console.Write("Broj adrese: ");
            string houseNumber = Console.ReadLine();

            Console.Write("Grad: ");
            string city = Console.ReadLine();

            Console.Write("Poštanski broj: ");
            string postalCode = Console.ReadLine();

            Address address = new Address(idBrojac++, street, houseNumber, city, postalCode);

            Console.WriteLine("Jela");
            for (int j = 0; j < meals.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {meals[j].Name}");
            }
            int numberOfMeals = InvalidInput.ValidatePositiveInteger("Koliko jela želite odabrati: ", 
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);

            List<Meal> tempMeals = new List<Meal>();

            for (int j = 0; j < numberOfMeals; j++)
            {
                int indexMeal;

                do
                {
                    indexMeal = InvalidInput.ValidatePositiveInteger($"Unesite {j + 1}. jelo: ",
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;

                    if(indexMeal < 0 || indexMeal > meals.Count)
                    {
                        Console.WriteLine("Unijeli ste neispravnu vrijednost!");
                    }
                } while (indexMeal < 0 || indexMeal > meals.Count);

                tempMeals.Add(meals[indexMeal]);
            }

            Console.WriteLine("Kuhari");
            for (int j = 0; j < NUMBER_OF_EMPLOYEES; j++)
            {
                if (employees[j] is Chef chef)
                {
                    Console.WriteLine($"{j + 1}. {chef.FirstName} {chef.LastName}");
                }
            }
            int numberOfChefs = InvalidInput.ValidatePositiveInteger("Koliko kuhara želite odabrati: ", 
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);

            List<Chef> tempChefs = new List<Chef>();

            for (int j = 0; j < numberOfChefs; j++)
            {
                int indexChef = InvalidInput.ValidatePositiveInteger($"Unesite {j + 1}. kuhara: ",
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;

                tempChefs.Add((Chef)employees[indexChef]);
            }

            Console.WriteLine("Konobari");
            for (int j = 0; j < NUMBER_OF_EMPLOYEES; j++)
            {
                if (employees[j] is Waiter waiter) {
                    Console.WriteLine($"{j + 1}. {waiter.FirstName} {waiter.LastName}");
                }
            }
            int numberOfWaiters = InvalidInput.ValidatePositiveInteger("Koliko konobara želite odabrati: ", 
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);
            List<Waiter> tempWaiters = new List<Waiter>();
            for (int j = 0; j < numberOfWaiters; j++)
            {
                int indexWaiter = InvalidInput.ValidatePositiveInteger($"Unesite {j + 1}. konobara: ", 
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;
                tempWaiters.Add((Waiter)employees[indexWaiter]);
            }

            Console.WriteLine("Dostavljači");
            for (int j = 0; j < NUMBER_OF_EMPLOYEES; j++)
            {
                if (employees[j] is Deliverer deliverer)
                {
                    Console.WriteLine($"{j + 1}. {deliverer.FirstName} {deliverer.LastName}");
                }
            }
            int numberOfDeliverers = InvalidInput.ValidatePositiveInteger("Koliko dostavljača želite odabrati: ", 
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);
            List<Deliverer> tempDeliverers = new List<Deliverer>();
            for (int j = 0; j < numberOfDeliverers; j++)
            {
                int indexDeliverer = InvalidInput.ValidatePositiveInteger($"Unesite {j + 1}. dostavljača: ", 
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;
                tempDeliverers.Add((Deliverer)employees[indexDeliverer]);
            }

            Restaurant restaurant = new Restaurant(idBrojac++, name, address, tempMeals, tempChefs, tempWaiters, tempDeliverers);
            return restaurant;
        }

        public static Order NextOrder(List<Restaurant> restaurants)
        {
            Console.WriteLine("Restorani");
            for (int j = 0; j < restaurants.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {restaurants[j].Name}");
            }
            int restaurantIndex;

            do
            {
                restaurantIndex = InvalidInput.ValidatePositiveInteger("Odaberite restoran: ",
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;

                if (restaurantIndex < 0 || restaurantIndex > restaurants.Count)
                {
                    Console.WriteLine("Unijeli ste neispravnu vrijednost!");
                }

            } while (restaurantIndex < 0 || restaurantIndex > restaurants.Count);
            Restaurant tempRestaurant = restaurants[restaurantIndex];


            Console.WriteLine("Jela");
            for (int j = 0; j < tempRestaurant.Meals.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {tempRestaurant.Meals[j].Name}");
            }
            int numberOfMeals = InvalidInput.ValidatePositiveInteger("Koliko jela želite naručiti: ", 
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR);

            List<Meal> tempMeals = new List<Meal>();

            for (int j = 0; j < numberOfMeals; j++)
            {
                int mealIndex;

                do
                {
                    mealIndex = InvalidInput.ValidatePositiveInteger($"Unesite {j + 1}. jelo: ",
                    Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;

                    if(mealIndex < 0 || mealIndex > tempRestaurant.Meals.Count)
                    {
                        Console.WriteLine("Unijeli ste neispravnu vrijednost!");
                    }

                } while (mealIndex < 0 || mealIndex > tempRestaurant.Meals.Count);

                tempMeals.Add(tempRestaurant.Meals[mealIndex]);
            }

            Console.WriteLine("Dostavljači");
            for (int j = 0; j < tempRestaurant.Deliverers.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {tempRestaurant.Deliverers[j].FirstName} {tempRestaurant.Deliverers[j].LastName}");
            }
            int delivererIndex = InvalidInput.ValidatePositiveInteger("Odaberite dostavljača: ", 
                Messages.INVALID_INT_INPUT_AND_NEGATIVE_ERROR) - 1;
            Deliverer deliverer = tempRestaurant.Deliverers[delivererIndex];
            deliverer.incrementDeliveryCount();

            DateTime tempDateTime = DateTime.Now;

            Order order = new Order(idBrojac++, tempRestaurant, tempMeals, deliverer, tempDateTime);
            return order;
        }
    }
}
