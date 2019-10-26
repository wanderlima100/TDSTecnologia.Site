﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TDSTecnologia.Site.Infrastructure.Repository
{
    public class UtilImagem
    {
        public static string ConverterByteArrayParaStringBase64(byte[] imagem)
        {
            return imagem != null ? "data:image/png;base64," + Convert.ToBase64String(imagem, 0, imagem.Length) : null;
        }

        public static byte[] ConverterParaByte(IFormFile imagem)
        {
            if (imagem != null && imagem.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                imagem.OpenReadStream().CopyTo(ms);
                return ms.ToArray();
            }
            return null;
        }
    }
}
