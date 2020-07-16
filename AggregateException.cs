/*
  AggregateException instances are flattened and handled in just one loop.
 */
using System;
using System.Threading.Tasks;

public class ByDenisRafi
{
    public static void Main()
    {
        var task1 = Task.Factory.StartNew(() => {
            var Hello = Task.Factory.StartNew(() => {
                var Denis = Task.Factory.StartNew(() => {
                    throw new CustomException("Denis");
                }, TaskCreationOptions.AttachedToParent);
                throw new CustomException("Hello");
            }, TaskCreationOptions.AttachedToParent);
        });
        try
        {
          task1.Wait();
        }
        catch (AggregateException ae)
        {
            foreach (var e in ae.Flatten().InnerExceptions) 
            {
                if (e is CustomException)
                {
                    Console.WriteLine(e.Message);
                }
                else
                {
                  throw;
                }
            }
        }
    }
}

public class CustomException : Exception
{
    public CustomException(String message) : base(message)
    {
    }
}
