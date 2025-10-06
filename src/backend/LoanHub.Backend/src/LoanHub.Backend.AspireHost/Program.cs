internal class Program
{
	private static void Main(string[] args)
	{
		var builder = DistributedApplication.CreateBuilder(args);

		builder.AddProject<Projects.LoanHub_Backend_Web>("web");

		builder.Build().Run();
	}
}