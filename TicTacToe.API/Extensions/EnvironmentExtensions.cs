namespace TicTacToe.API.Extensions
{
	public static class EnvironmentExtensions
	{
		private const string Deployment = "Deployment";

		public static bool IsDeployment(this IWebHostEnvironment environment)
		{
			return environment.EnvironmentName == Deployment;
		}
	}
}
