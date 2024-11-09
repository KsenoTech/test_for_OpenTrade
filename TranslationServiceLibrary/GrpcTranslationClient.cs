using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationServiceCore;

namespace TranslationServiceLibrary
{
    public class GrpcTranslationClient : ITranslationService
    {
        private readonly TranslationService.TranslationServiceGrpcClient _client;

        public GrpcTranslationClient(TranslationService.TranslationServiceClient client)
        {
            _client = client;
        }
        public GrpcTranslationClient(string grpcAddress)
        {
            var channel = GrpcChannel.ForAddress(grpcAddress);
            _client = new TranslationService.TranslationServiceGrpcClient(channel);
        }

        public Task<List<string>> TranslateAsync(List<string> texts, string fromLang, string toLang)
        {
            // Вызовите gRPC метод для перевода
            throw new NotImplementedException();
        }

        public Task<string> GetServiceInfoAsync()
        {
            return Task.FromResult("gRPC Translation Service Client");
        }
    }
}