﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string? input);
        string Decrypt(string? input);
    }
}
