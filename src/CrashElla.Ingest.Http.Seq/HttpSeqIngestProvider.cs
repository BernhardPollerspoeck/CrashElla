using CrashElla.Core;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;
using CrashElla.Core.Data;
using System.Text.RegularExpressions;

namespace CrashElla.Ingest.Http.Seq;

public partial class HttpSeqIngestProvider(IOptions<HttpSeqIngestProviderConfiguration> configuration) : IIngestProvider
{
	public void Ingest(LogEntry entry, IReadOnlyDictionary<string, object> customProperties)
	{
		Task.Run(async () =>
		{
			try
			{
				//TODO: check if we need to log (minimum ingest level will be told by api on first log)

				var message = FormatMessage(entry.MessageTemplate, entry.Parameters ?? [], out var templatedArguments);

				var logId = BitConverter.ToString(BitConverter.GetBytes(Random.Shared.NextDouble()));
				var logEvent = new Dictionary<string, object>
				{
					{ "@i", logId },
					{ "@m" ,message },
					{ "@t", DateTime.UtcNow },
					{ "@l", entry.Level.ToString() },
					{ "@mt", entry.MessageTemplate },
				};
				if (entry.Exception is not null)
				{
					logEvent["@x"] = JsonSerializer.Serialize(new SerializableException(entry.Exception));
				}
				foreach (var property in customProperties)
				{
					logEvent[property.Key] = property.Value;
				}
				foreach (var argument in templatedArguments)
				{
					logEvent[argument.Key] = argument.Value;
				}

				var json = JsonSerializer.Serialize(logEvent);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				using var client = new HttpClient();
				// Setze den API-Schlüssel im Header
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-Seq-ApiKey", configuration.Value.ApiKey);

				var response = await client.PostAsync($"{configuration.Value.IngestUri}/ingest/clef", content);
				var responseContent = await response.Content.ReadAsStringAsync();
			}
			catch (Exception ex)
			{
				//TODO: log this propperly
				Console.WriteLine($"Error sending log event: {ex.Message}");
			}
		});
	}

	private static string FormatMessage(string messageTemplate, object[] args, out Dictionary<string, object> argumentDictionary)
	{
		argumentDictionary = [];
		var regex = FormatArgumentFinderRegex();
		var matches = regex.Matches(messageTemplate);

		var formattedMessage = messageTemplate;
		for (var i = 0; i < matches.Count; i++)
		{
			var match = matches[i];
			var argumentName = match.Groups[1].Value;

			if (i < args.Length)
			{
				formattedMessage = formattedMessage.Replace(match.Value, args[i].ToString());
				argumentDictionary[argumentName] = args[i];
			}
		}

		return formattedMessage;
	}

	[GeneratedRegex(@"\{([^\}]+)\}")]
	private static partial Regex FormatArgumentFinderRegex();
}

