using CodeChallenge.Config;


public class Program
{
    public static void Main(string[] args)
    {
        var app = new App().Configure(args);
        app.Run(); // Ensure the application runs
    }
}
