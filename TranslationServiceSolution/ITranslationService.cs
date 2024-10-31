using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationServiceCore
{
    public interface ITranslationService
    {
        /// <summary>
        /// Переводит список текстов с одного языка на другой.
        /// </summary>
        /// <param name="texts">Список строк для перевода.</param>
        /// <param name="fromLang">Язык исходного текста (например, "en" для английского).</param>
        /// <param name="toLang">Язык, на который нужно перевести (например, "ru" для русского).</param>
        /// <returns>Список переведённых строк.</returns>
        Task<List<string>> TranslateAsync(List<string> texts, string fromLang, string toLang);

        /// <summary>
        /// Возвращает информацию о сервисе перевода (API и тип кэша).
        /// </summary>
        /// <returns>Информация о сервисе.</returns>
        Task<string> GetServiceInfoAsync();
    }

}
