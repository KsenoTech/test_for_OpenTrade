using Grpc.Core;
using TranslationServiceCore;
using TranslationServiceLibrary;


namespace GrpcService.Services
{
    public class TranslationServiceGrpc : TranslationService.TranslationServiceBase
    {
        private readonly ITranslationService _translationService;

        public TranslationServiceGrpc(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        public override async Task<TranslateResponse> Translate(TranslateRequest request, ServerCallContext context)
        {
            var translations = await _translationService.TranslateAsync(request.Texts.ToList(), request.FromLang, request.ToLang);
            var response = new TranslateResponse();
            response.Translations.AddRange(translations);
            return response;
        }

        public override async Task<ServiceInfoResponse> GetServiceInfo(ServiceInfoRequest request, ServerCallContext context)
        {
            var info = await _translationService.GetServiceInfoAsync();
            return new ServiceInfoResponse { Info = info };
        }
    }
}
