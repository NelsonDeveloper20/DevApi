using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace ApiPortal_DataLake.Application.Filters
{
    public class ValidateGroupsAttribute: TypeFilterAttribute
    {
        public ValidateGroupsAttribute(params string[] groups): base(typeof(ValidateGroupsAttributeImp))
        {
            Arguments = new object[] { groups };
        }

        private class ValidateGroupsAttributeImp: IActionFilter
        {
            private string[] _groups;
            private readonly ILogger<ValidateGroupsAttributeImp> _logger;
            private readonly IJwt _jwt;

            public ValidateGroupsAttributeImp(
                ILogger<ValidateGroupsAttributeImp> logger,
                IJwt jwt,
                string[] groups
            )
            {
                this._logger = logger;
                this._jwt = jwt;
                this._groups = groups;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                // NotImplementedException();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                StringValues authorization;
                string content = "HTTP 401 Unauthorized";
                try
                {
                    bool hasValue = context.HttpContext.Request.Headers.TryGetValue(HeaderRequestConstant.AuthorizationJlr, out authorization);

                    if (!hasValue)
                    {
                        _logger.LogError("Error validate token: Not Header Authorization");
                        context.Result = new UnauthorizedObjectResult($"{content}: Not Header Authorization");
                    }
                    else
                    {
                        var token = this._jwt.ParseToken(authorization);
                        if (!this._jwt.ValidateToken(token))
                        {
                            _logger.LogError("Error validate token: Token invalid");
                            context.Result = new UnauthorizedObjectResult($"{content}: Token invalid");
                        }

                        //StringValues azureToken;
                        //bool hasAzureToken = context.HttpContext.Request.Headers.TryGetValue(HeaderRequestConstant.Authorization, out azureToken);
                        //if (hasAzureToken)
                        //{
                        //    var tokenAD = this._jwt.ParseToken(azureToken);
                        //    context.HttpContext.Items.Add(ClaimsConstant.AuthorizationAzureToken, tokenAD);
                        //}

                        /*StringValues azureTokenAd;
                        bool hasAzureTokenAd = context.HttpContext.Request.Headers.TryGetValue(HeaderRequestConstant.AzureTokenAD, out azureTokenAd);
                        if (hasAzureTokenAd)
                        {
                            context.HttpContext.Items.Add(ClaimsConstant.AzureTokenAD, azureTokenAd);
                        }

                        var groups = this._jwt.ReadsToken(token, ClaimsConstant.Rol);

                        if (groups != null && this._groups.Any())
                        {
                            // var validateGroups = JsonConvert.DeserializeObject<List<int>>(groups.ToString());
                            if (groups.ToList().FindIndex(g => this._groups.Contains(g)) == -1)
                            {
                                _logger.LogError($"Not authorization group: {JsonConvert.SerializeObject(_groups)}");
                                context.Result = new UnauthorizedObjectResult("HTTP 401.10 Unauthorized");
                            }
                        }*/
                    }

                }
                catch (Exception ex)
                {
                    this._logger.LogError($"Error validate token: {JsonConvert.SerializeObject(ex)}");
                    context.Result = new UnauthorizedObjectResult($"{content}: Invalid service");
                }
            }
        }
    }
}
