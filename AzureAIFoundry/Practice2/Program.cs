using Azure.Identity;
using Azure.AI.Projects;
using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

string connectionString = "eastus2.api.azureml.ms;4914e4b9-a086-4a23-8558-00038f36080e;rg-albert89111296002-4953_ai;albert89111296002-7935";
var options = new DefaultAzureCredentialOptions
{
    TenantId = "2dea6ebd-8596-4848-88b5-170e0696c393" // 正確的 tenant ID
};
// Initialize the project client
var projectClient = new AIProjectClient(connectionString, new DefaultAzureCredential(options));

// Get an Azure OpenAI chat client
ChatClient chatClient = projectClient.GetAzureOpenAIChatClient("gpt-4o-mini");

// Get a chat completion based on a user-provided prompt
Console.WriteLine("Enter a question:");
var user_prompt = Console.ReadLine();
ChatCompletion completion = chatClient.CompleteChat(
    [
        new SystemChatMessage("You are a helpful AI assistant that answers questions."),
        new UserChatMessage(user_prompt),
    ]
);
Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");