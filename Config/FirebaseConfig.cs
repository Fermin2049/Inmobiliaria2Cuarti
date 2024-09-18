using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

public static class FirebaseConfig
{
    public static void Initialize()
    {
        try
        {
            FirebaseApp.Create(
                new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(
                        Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "Config",
                            "inmobilirianet-bda045475369.json"
                        )
                    ),
                }
            );
        }
        catch (Exception ex)
        {
            // Log the error or handle it as needed
            Console.WriteLine($"Error initializing Firebase: {ex.Message}");
            throw;
        }
    }
}
