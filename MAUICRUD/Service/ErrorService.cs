namespace MauiCrud.Service
{
    public class ErrorService : IErrorService
    {
        public void HandleError(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");

            async void Action()
            {
                if (Application.Current is not null && Application.Current.MainPage is not null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }

            MainThread.BeginInvokeOnMainThread(Action);
        }
    }
}
