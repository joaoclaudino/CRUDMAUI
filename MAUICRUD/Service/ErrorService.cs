namespace MauiCrud.Service
{
    public class ErrorService : IErrorService
    {
        public void HandleError(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Application.Current is not null && Application.Current.MainPage is not null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            });
        }
    }
}
