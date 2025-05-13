using System.Collections.Generic;
using Restoran.Model;
using Restoran.Util;

internal class Program
{

    private static void Main(string[] args)
    {
        
        List<Category> categories = new List<Category>();
        List<Ingredient> ingredients = new List<Ingredient>();
        List<Meal> meals = new List<Meal>();
        List<Restaurant> restaurants = new List<Restaurant>();
        List<Order> orders = new List<Order>();
        List<Person> employees = new List<Person>();

        for (int i = 0; i < DataInput.NUMBER_OF_CATEGORIES; i++)
        {
            Console.WriteLine($"Unesite podatke za {i + 1}. kategoriju");
            Category category = DataInput.NextCategory();
            categories.Add(category);
        }


        for (int i = 0; i < DataInput.NUMBER_OF_INGREDIANTS; i++)
        {
            Console.WriteLine($"Unesite podatke za {i + 1}. sastojak");
            Ingredient ingredient = DataInput.NextIngredient(categories);
            ingredients.Add(ingredient);
        }
        ingredients = ingredients.OrderBy(i => i.Name).ToList();  

        for (int i = 0; i < DataInput.NUMBER_OF_MEALS; i++)
        {
            Console.WriteLine($"Unesite podatke za {i + 1}. jelo");
            Meal meal = DataInput.NextMeal(categories, ingredients, meals);
            meals.Add(meal);
        }

        for(int i = 0; i < DataInput.NUMBER_OF_EMPLOYEES; i++)
        {
            Console.WriteLine($"Unesite podatke za {i + 1}. zaposlenika");
            Person person = DataInput.NextPerson(employees);
            employees.Add(person);
        }



        for (int i = 0; i < DataInput.NUMBER_OF_RESTAURANTS; i++)
        {
            Console.WriteLine($"Unesite podatke za {i + 1}. restoran");
            Restaurant restaurant = DataInput.NextRestaurant(meals, employees, restaurants);
            restaurants.Add(restaurant);
        }

        for (int i = 0; i < DataInput.NUMBER_OF_ORDERS; i++)
        {
            Console.WriteLine($"Unesite podatke za {i + 1}. narudžbu");
            Order order = DataInput.NextOrder(restaurants);
            orders.Add(order);
        }

        var restaurantWithMostEmployees = restaurants.OrderByDescending(r => r.Chefs.Count + r.Waiters.Count + r.Deliverers.Count).FirstOrDefault();
        Console.WriteLine($"Restoran s najviše zaposlenika je {restaurantWithMostEmployees.Name}");

        FindMostExpensiveOrder(orders);
        FindTopDeliverer(orders);
        FindEmployeeWithHighestSalary(employees);
        FindEmployeeWithLongestContract(employees);
        FindHighestAndLowestCalorieMeal(meals);

    }

    public static void FindMostExpensiveOrder(List<Order> orders)
    {
        List<Restaurant> restaurants = new List<Restaurant>();
        decimal maxPrice = decimal.Zero;

        foreach (Order order in orders)
        {
            if (order.GetTotalPrice() > maxPrice)
            {
                maxPrice = order.GetTotalPrice();
                restaurants.Clear();
                restaurants.Add(order.Restaurant);
            }
            else if (order.GetTotalPrice() == maxPrice)
            {
                if (!restaurants.Contains(order.Restaurant))
                {
                    restaurants.Add(order.Restaurant);
                }
            }
        }

        Console.WriteLine("Restoran/i s najskupljom narudžbom su: ");
        foreach(Restaurant restaurant in restaurants)
        {
            Console.WriteLine(restaurant.Name);
        }
    }

    public static void FindTopDeliverer(List<Order> orders)
    {
        List<Deliverer> deliverers = new List<Deliverer>();
        int topDeliveries = 0;

        foreach (Order order in orders)
        {
            if (order.Deliverer.DeliveryCount > topDeliveries)
            {
                topDeliveries = order.Deliverer.DeliveryCount;
                deliverers.Clear();
                deliverers.Add(order.Deliverer);
            }
            else if (order.Deliverer.DeliveryCount == topDeliveries)
            {
                if (!deliverers.Contains(order.Deliverer))
                {
                    deliverers.Add(order.Deliverer);
                }
            }
        }
        Console.WriteLine("Dostavljač/i s najviše narudžbi su: ");
        foreach (Deliverer deliverer in deliverers)
        {
            Console.WriteLine(deliverer.FirstName + " " + deliverer.LastName);
        }
    }

    public static void FindEmployeeWithHighestSalary(List<Person> employees)
    {
        Person employeeWithHighestSalary = null;
        decimal highestSalary = decimal.Zero;

        foreach (Person employee in employees)
        {
            decimal salary = decimal.Zero;

            if (employee is Chef chef)
            {
                salary = chef.Contract.Salary;
            }
            else if (employee is Waiter waiter)
            {
                salary = waiter.Contract.Salary;
            }
            else if (employee is Deliverer deliverer)
            {
                salary = deliverer.Contract.Salary;
            }

            if (salary > highestSalary)
            {
                highestSalary = salary;
                employeeWithHighestSalary = employee;
            }
        }
        Console.WriteLine($"Zaposlenik s največom plaćom je {employeeWithHighestSalary.FirstName} " +
            $"{employeeWithHighestSalary.LastName} s plaćom {highestSalary}");
    }

    public static void FindEmployeeWithLongestContract(List<Person> employees)
    {
        Person employeeWithLongestContract = null;
        double longestContract = 0;

        foreach(Person employee in employees)
        {
            double contract = 0;
            if(employee is Chef chef)
            {
                contract = (chef.Contract.EndDate - chef.Contract.StartDate).TotalDays;
            }
            else if(employee is Waiter waiter)
            {
                contract = (waiter.Contract.EndDate - waiter.Contract.StartDate).TotalDays;
            }
            else if (employee is Deliverer deliverer)
            {
                contract = (deliverer.Contract.EndDate - deliverer.Contract.StartDate).TotalDays;
            }

            if(contract > longestContract)
            {
                longestContract = contract;
                employeeWithLongestContract = employee;
            }
        }

        Console.WriteLine($"Zaposlenik s najdužim ugovorm je {employeeWithLongestContract.FirstName} " +
            $"{employeeWithLongestContract.LastName} s ukupno dana {longestContract}");
    }

    private static void FindHighestAndLowestCalorieMeal(List<Meal> meals)
    {
        Meal highCalorieMeal = meals[0];
        Meal lowCalorieMeal = meals[0];

        decimal minKcal = decimal.Zero;
        decimal maxKcal = decimal.Zero;

        foreach( Meal meal in meals ) 
        {
            decimal mealKcal = decimal.Zero;

            if(mealKcal > maxKcal)
            {
                maxKcal = mealKcal;
                highCalorieMeal = meal;
            }

            if (mealKcal < minKcal)
            {
                minKcal = mealKcal;
                lowCalorieMeal = meal;
            }
        }

        Console.WriteLine($"Jelo s najmanje kalorija je {lowCalorieMeal.Name}");
        Console.WriteLine($"Jelo s najviše kalorija je {highCalorieMeal.Name}");
    }



}