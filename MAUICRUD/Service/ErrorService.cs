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
                if (App.Current is not null && App.Current.MainPage is not null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            });
        }
    }
}
