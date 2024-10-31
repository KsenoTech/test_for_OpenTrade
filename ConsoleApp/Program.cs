using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using TranslationServiceLibrary;


namespace TranslationServiceSolution
{
    public class Program
    {
        static async Task Main(string[] args)
        {
             var channel = GrpcChannel.ForAddress("http://localhost:5222");
            var client = new TranslationService.TranslationServiceClient(channel);

            // Пример вызова GetServiceInfo
            var infoResponse = await client.GetServiceInfoAsync(new ServiceInfoRequest());
            Console.WriteLine("Service Info: " + infoResponse.Info);
            
            // Пример вызова Translate
            var translateResponse = await client.TranslateAsync(new TranslateRequest
            {
                Texts = { "Hello, world!" },
                FromLang = "en",
                ToLang = "es"
            });

            foreach (var translation in translateResponse.Translations)
            {
                Console.WriteLine("Translation: " + translation);
            }
        }
    }

}
