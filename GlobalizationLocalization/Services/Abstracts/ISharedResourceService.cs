using Microsoft.Extensions.Localization;

namespace GlobalizationLocalization.Services.Abstracts;
public interface ISharedResourceService
{
    LocalizedString GetLocalizedString(string key);
}
