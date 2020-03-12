using Microsoft.AspNetCore.Http;
using Olive;
using PeopleService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public static class Extensions
    {
        public static Task LogOn(this UserInfo @this)
        {
            // if it's a smart phone we should persist logout time for a year
            var isSmartPhone = Context.Current.Request().IsSmartPhone();
            var timeOut = isSmartPhone ?
                Config.Get("Authentication:Cookie:TimeoutMobile", defaultValue: 540).Minutes() :
                Config.Get("Authentication:Cookie:Timeout", defaultValue: 540).Minutes();

            return new Olive.Security.GenericLoginInfo
            {
                DisplayName = @this.DisplayName,
                Email = @this.Email,
                ID = @this.ID.ToString(),
                Roles = @this.Roles.Split(',').Trim().ToArray(),
                Timeout = timeOut
            }.LogOn(remember: isSmartPhone);
        }

        public static Domain.Feature[] SubItems(this Domain.Feature @this)
        {
            if (@this == null)
                return Domain.Feature.All.Where(x => x.Parent == null).ToArray();
            else return @this.GetAllChildren().Cast<Domain.Feature>().ToArray();
        }

        public static bool IsSmartPhone(this HttpRequest @this)
        {
            var agent = @this.Headers["User-Agent"].ToString("|").ToLowerOrEmpty();
            return agent.ContainsAny(new[] { "iphone", "android" });
        }
    }
}