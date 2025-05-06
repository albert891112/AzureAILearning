using Azure;
using Azure.AI.Inference;
using Azure.AI.OpenAI;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;

namespace chatApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            // Get config settings
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            string project_connection = configuration["PROJECT_CONNECTION"];
            string model_deployment = configuration["MODEL_DEPLOYMENT"];

            var options = new DefaultAzureCredentialOptions
            {
                TenantId = "2dea6ebd-8596-4848-88b5-170e0696c393" // 正確的 tenant ID
            };

            //Initialize the Azure Project client
            var projectClient = new AIProjectClient(project_connection, new DefaultAzureCredential(options));

            //Get the chat client 
            ChatClient chatClient = projectClient.GetAzureOpenAIChatClient(model_deployment);

            //Initailize system prompt with system message
            var prompt = new List<ChatMessage>() 
            { 
                new SystemChatMessage("You are a helpful AI assistant that answers questions.") 
            };


            Console.WriteLine("Enter prompt : ");
            var user_Input = Console.ReadLine();

            var ChatOption = new ChatCompletionsOptions
            {
                Model = model_deployment,
            };

            while (user_Input != "quit")
            {

                // Get a chat completion
                prompt.Add(new UserChatMessage(user_Input));
                ChatCompletion completion = chatClient.CompleteChat(prompt);
                var completionText = completion.Content[0].Text;
                Console.WriteLine(completionText);
                prompt.Add(new AssistantChatMessage(completionText));
                Console.WriteLine("Enter prompt : ");
                user_Input = Console.ReadLine();

            }
        }
    }
}



