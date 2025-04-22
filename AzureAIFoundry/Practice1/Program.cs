using Azure.Identity;
using Azure.AI.Projects;
using Azure;
using Azure.AI.TextAnalytics;

string connectionString = "eastus2.api.azureml.ms;4914e4b9-a086-4a23-8558-00038f36080e;rg-Albert89111296002-5224_ai;albert89111296002-7242";
var options = new DefaultAzureCredentialOptions
{
    TenantId = "2dea6ebd-8596-4848-88b5-170e0696c393" // 正確的 tenant ID
};
var projectClient = new AIProjectClient(connectionString , new DefaultAzureCredential(options));


var connectionClient = projectClient.GetConnectionsClient();
ConnectionResponse connection = connectionClient.GetDefaultConnection(ConnectionType.AzureAIServices , true);

var apiAuth = connection.Properties as ConnectionPropertiesApiKeyAuth;
var credential = new AzureKeyCredential(apiAuth.Credentials.Key);
Uri endpoint = new Uri(apiAuth.Target);
var Client = new TextAnalyticsClient(endpoint, credential);

var text = "I hated the movie. It was so slow!";
DocumentSentiment sentiment = Client.AnalyzeSentiment(text);
Console.WriteLine("Text: " + text);
Console.WriteLine("Sentiment: " + sentiment.Sentiment);