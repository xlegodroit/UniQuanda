using System.Net;
using Microsoft.AspNetCore.Http.Features;
using UniQuanda.Presentation.API.Attributes;
using UniQuanda.Presentation.API.Extensions;
using UniQuanda.Presentation.API.Options;

namespace UniQuanda.Presentation.API.Middlewares
{
		public class RecaptchaMidldeware
		{
				private RequestDelegate _next;
				private RecaptchaOptions _recaptchaOptions;

				public RecaptchaMidldeware(RequestDelegate next, RecaptchaOptions recaptchaOptions)
				{
					_next = next;
					_recaptchaOptions = recaptchaOptions;
				}

				public async Task Invoke(HttpContext context)
				{
						var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
						var hasRecaptchaAttribute = endpoint?.Metadata.GetMetadata<RecaptchaAttribute>() is not null;
						if (!hasRecaptchaAttribute) {
								await _next(context);
								return;
						}

						var recaptchaToken = context.Request.Headers.GetRecaptcha();
						if (recaptchaToken is null) {
								await WriteRecaptchaError(context);
								return;
						}

						var isRecaptchaTokenValid = await VerifyToken(recaptchaToken);
						if (isRecaptchaTokenValid) {
								await _next(context);
						} else {
								await WriteRecaptchaError(context);
						}
				}

				private async Task<bool> VerifyToken(string token)
				{
					try
					{
							var url = "https://www.google.com/recaptcha/api/siteverify";
							using var http = new HttpClient();
							var body = new {
								secret = _recaptchaOptions.SecretKey,
								response = token,
							};
						
							var response = await http.PostAsJsonAsync(url, body);
							return response.StatusCode == HttpStatusCode.OK;
					}
					catch
					{
							return false;
					}
				}

				private static async Task WriteRecaptchaError(HttpContext context)
				{
						context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
						await context.Response.WriteAsync("Invalid Recaptcha");
				}
		}
}