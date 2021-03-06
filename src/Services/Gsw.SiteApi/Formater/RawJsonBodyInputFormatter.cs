﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Gsw.SiteApi.Formater
{
    public class RawRequestBodyFormatter : InputFormatter
    {
        const string MEDIA_TEXT =  "text/plain";
        const string MEDIA_JSON = "application/json";
        public RawRequestBodyFormatter()
        {
            SupportedMediaTypes.Add(MEDIA_TEXT);
            SupportedMediaTypes.Add(MEDIA_JSON);
        }

        public override Boolean CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) || contentType == MEDIA_TEXT )
                return true;

            return false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;


            if (string.IsNullOrEmpty(contentType) || contentType == MEDIA_TEXT)
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = await reader.ReadToEndAsync();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }

            return await InputFormatterResult.FailureAsync();
        }
    }
}
