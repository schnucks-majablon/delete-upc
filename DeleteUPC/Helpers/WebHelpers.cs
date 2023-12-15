using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace DeleteUPC.Helpers
{
    public class WebHelpers
    {

        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static string GetUserlogin
        {
            get
            {



                var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.IndexOf("email") >= 0);



                string result = string.Empty;

                // ajax calls will not find a claim/User, so it will have "Not Authenticated" as the login
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == true)
                    result = "Authenticated";
                else
                    result = "Not Authenticated";

                if (claim != null)
                    return claim.Value.Substring(0, claim.Value.IndexOf("@"));
                else
                    return result;

            }
        }


        public static string GetUserGroups
        {
            get
            {


                // var claimsArray = _httpContextAccessor.HttpContext.User.Claims.ToArray();
                int ReserveUPCAuthority = 1;



                if (_httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type.IndexOf("groups") >= 0 && (c.Value.ToUpper() == "MASS - ADMIN")).Any())
                    ReserveUPCAuthority = 1;
                if (_httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type.IndexOf("groups") >= 0 && (c.Value.ToUpper() == "MASS - COE")).Any())
                    ReserveUPCAuthority = 1;
                if (_httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type.IndexOf("groups") >= 0 && (c.Value.ToUpper() == "MASS - RESERVEUPC")).Any())
                    ReserveUPCAuthority = 1;

                if (_httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type.IndexOf("roles") >= 0 && (c.Value.ToUpper() == "MASS - RESERVEUPC")).Any())
                    ReserveUPCAuthority = 1;



                //for (int i = 0; i < claimsArray.Length; i++)
                //{
                //    string claimType = claimsArray[i].Type.ToLower();
                //    string claimValue = claimsArray[i].Value;

                //    if (claimType == "groups")
                //    {


                //        switch (claimValue)
                //        {
                //            case "MASS - Admin":
                //                if (ReserveUPCAuthority < 1)
                //                {
                //                    ReserveUPCAuthority = 1;
                //                }
                //                break;
                //            case "MASS - COE":
                //                if (ReserveUPCAuthority < 1)
                //                {
                //                    ReserveUPCAuthority = 1;
                //                }
                //                break;

                //            case "MASS - ReserveUPC":
                //                if (ReserveUPCAuthority < 1)
                //                {
                //                    ReserveUPCAuthority = 1;
                //                }
                //                break;

                //            case "MASS - View Only":

                //                if (ReserveUPCAuthority < 1)
                //                {
                //                    ReserveUPCAuthority = 0;
                //                }

                //                break;

                //        }


                //    }


                //}


                return ReserveUPCAuthority.ToString();
            }

        }



        public static string GetUserName
        {
            get
            {
                var UserName = _httpContextAccessor.HttpContext.User.Identity.Name;

                //////////////////////////////////////////////////////

                return UserName;

            }
        }



        public static string GetItemNbr(string item)
        {
            if (item != null && item.Length >= 6)
                return item.Substring(3, 7);
            else
                return "";
        }

        public static string GetUPC(string item)
        {
            return item.Substring(12, 13);
        }
        public static string GetDescription(string item)
        {
            return item.Substring(28);
        }

        public static string GetVendorNbr(string brand)
        {
            if (brand != null && brand.Length >= 6)
                return brand.Substring(0, 6);
            else
                return "";
        }

        public static string GetUserRole
        {
            get
            {
                string role = string.Empty;
                if (_httpContextAccessor != null)
                {
                    if (_httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type.IndexOf("groups") >= 0 && (c.Value.ToUpper() == "MASS - DELETEUPC")).Any())
                        role = "MASS - DELETEUPC";
                    else if (_httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type.IndexOf("groups") >= 0 && (c.Value.ToUpper() == "MASS - COE")).Any())
                        role = "MASS - COE";

                    return role;
                }
                else
                    return "NullUserLogin";
            }
        }

    }
}
