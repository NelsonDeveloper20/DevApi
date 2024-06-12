using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

namespace ApiPortal_DataLake.Application.Filters
{
    public class ValidateTokenAttribute: ActionFilterAttribute
    {
        private readonly ILogger<ValidateTokenAttribute> _logger;
        private readonly IJwt _jwt;

        public ValidateTokenAttribute(ILogger<ValidateTokenAttribute> logger, IJwt jwt)
        {
            this._logger = logger;
            _jwt = jwt;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringValues authorization;
            string content = "HTTP 401 Unauthorized";
            try
            {
                bool hasValue = context.HttpContext.Request.Headers.TryGetValue(HeaderRequestConstant.AuthorizationJlr, out authorization);

                if (!hasValue)
                {
                    context.Result = new UnauthorizedObjectResult($"{content}: Invalid service");
                }
                else
                {
                    var token = this._jwt.ParseToken(authorization);
                    if (!this._jwt.ValidateToken(token))
                    {
                        context.Result = new UnauthorizedObjectResult($"{content}: Token invalid");
                    }

                    //StringValues azureToken;
                    //bool hasAzureToken = context.HttpContext.Request.Headers.TryGetValue(HeaderRequestConstant.Authorization, out azureToken);
                    //if (hasAzureToken)
                    //{
                    //    var tokenAD = this._jwt.ParseToken(azureToken);
                    //    context.HttpContext.Items.Add(ClaimsConstant.AuthorizationAzureToken, tokenAD);
                    //}

                    StringValues azureTokenAd;
                    bool hasAzureTokenAd = context.HttpContext.Request.Headers.TryGetValue(HeaderRequestConstant.AzureTokenAD, out azureTokenAd);
                    if (hasAzureTokenAd)
                    {
                        context.HttpContext.Items.Add(ClaimsConstant.AzureTokenAD, azureTokenAd);
                    }
                }

            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error validate token: {JsonConvert.SerializeObject(ex)}");
                context.Result = new UnauthorizedObjectResult($"{content}: Invalid service");
            }

            base.OnActionExecuting(context);
        }
    }
}
