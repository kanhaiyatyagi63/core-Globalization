
using GlobalizationLocalization.Resources;
using GlobalizationLocalization.Services.Abstracts;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GlobalizationLocalization.Services;

public class SharedResourceService : ISharedResourceService
{
    private readonly IStringLocalizer _localizer;

    public SharedResourceService(IStringLocalizerFactory factory)
    {
        var type = typeof(SharedResource);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        _localizer = factory.Create("SharedResource", assemblyName.Name);
    }

    public LocalizedString GetLocalizedString(string key)
    {
        return _localizer[key];
    }
}
