using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public static class EncryptionDIExtensions
    {
        public static IServiceCollection AddEncryption(this IServiceCollection collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            return collection.AddSingleton<IEncryptionService,EncryptionService>();
        }
    }
}
