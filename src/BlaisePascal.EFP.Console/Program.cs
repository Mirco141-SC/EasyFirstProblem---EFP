using BlaisePascal.EFP.Domain;
using System.Text;

public class Program
{
    public static void Main()
    {
        //Setting the console encoding to UTF-8 to be able to use the currency character.
        Console.OutputEncoding = Encoding.UTF8;

        //Defining consts
        const decimal DeliveryCost = 5.0m;
        const char Currency = '€';

        string userInput; //ATTENTION! This variable get re-assigned and used for each input

        do
        {
            Console.WriteLine("Enter customer's name: ");
            userInput = Console.ReadLine();
        }
        while (userInput.IsWhiteSpace());
        string customerName = userInput;

        do
        {
            Console.WriteLine("\n\nInsert the number of books bought by the customer: ");
            userInput = Console.ReadLine();
        }
        while(userInput.IsWhiteSpace() /*&& int.Parse(userInput) < 0*/); //Disabled input check otherwise no "invalid order" case will exists
        int numberOfBooks = int.Parse(userInput);

        do
        {
            Console.WriteLine("\n\nInsert the price of a single book: ");
            userInput = Console.ReadLine();
        }
        while (userInput.IsWhiteSpace() /*&& decimal.Parse(userInput) < 0m*/); //Disabled input check otherwise no "invalid order" case will exists
        decimal bookUnitPrice = decimal.Parse(userInput);

        do
        {
            Console.WriteLine("\n\nIs the customer a student? [Y/N]");
            userInput = Console.ReadLine()?.ToLower();
        }
        while (userInput != "y" && userInput != "n");
        bool isStudent = userInput == "y";

        do
        {
            Console.WriteLine
                (
                "\n\nSelect the delivery type.\n" +
                "1. Delivery\n" +
                "2. Pick-up"
                );

            userInput = Console.ReadLine();
        }
        while (userInput != "1" && userInput != "2");
        int deliveryInput = int.Parse(userInput);

        DeliveryType deliveryType = deliveryInput == 1 ? DeliveryType.Delivery : DeliveryType.Pickup;

        //Calculating order's total spendings
        decimal subtotal = numberOfBooks * bookUnitPrice;
        decimal appliedDeliveryCost = 0m;

        if(deliveryType == DeliveryType.Delivery) 
        {
            appliedDeliveryCost = DeliveryCost;
        }

        decimal orderTotal = subtotal + appliedDeliveryCost;
        string status;

        switch (orderTotal)
        {
            case <= 0:
                status = "Invalid order.";
                break;
            case < 15:
                status = "That's one small order, thank you!";
                break;
            case >= 15:
                status = "Thank you for your order!";
                break;
        }

        //Printing order confirmation
        Console.WriteLine
            (
            "\n\n------- ORDER CONFIRMATION -------\n" +
            $"Customer: {customerName} (Student: {isStudent})\n" +
            $"Number of books bought: {numberOfBooks}\n" +
            $"Price of a single book: {bookUnitPrice} {Currency}\n" +
            $"Delivery type: {deliveryType.ToString()}\n" +
            $"-------------------------------------\n" +
            $"Subtotal: {subtotal} {Currency}\n" +
            $"Delivery cost: {appliedDeliveryCost} {Currency}\n" +
            $"TOTAL: {orderTotal} {Currency}\n" +
            "--------------------------------------\n\n"+
            $"Status: {status}"
            );
    }
}