using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICRUD.Service
{
    public class ErrorService : IErrorService
    {
        public void HandleError(Exception ex)
        {
            // Log the exception (you can customize this based on your logging strategy)
            Console.WriteLine($"An error occurred: {ex.Message}");

            // Display the error message to the user
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            });
        }
    }
}
