using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace Votp
{
    public partial class Program
    {
        public class PlaceholdLocalizator : IViewLocalizer
        {
            public LocalizedHtmlString this[string name] => new LocalizedHtmlString(name, name);

            public LocalizedHtmlString this[string name, params object[] arguments] => this[name];

            public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            {
                throw new NotImplementedException();
            }

            public LocalizedString GetString(string name)
            {
                return new LocalizedString(name, name);
            }

            public LocalizedString GetString(string name, params object[] arguments)
            {
                return GetString(name);
            }
        }
    }
}