namespace GrpcService.Interfaces
{
    public interface ITranslationService
    {
        Task<string> TranslateAsync(string text, string fromLanguage, string toLanguage);
        Task<string> GetServiceInfoAsync();
    }

}
