using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using rabapp.Model.Common;
using rabapp.Model.Quiz.ViewModels;

namespace rabapp.Service.Common.Helper
{
    public sealed class CookieHelper
    {
        private HttpRequest _request;
        private HttpResponse _response;
        private readonly string _cryptographyKey = SiteConfigurationReader.GetAppSettingsString(Constants.CommonConstants.CryptographyKey);

        public CookieHelper(HttpRequest request,
        HttpResponse response)
        {
            _request = request;
            _response = response;
        }

        public CookieHelper(HttpRequest request)
            : this(request, null)
        {
        }

        public CookieHelper(HttpResponse response)
            : this(null, response)
        {
        }

        public CookieHelper()
        {
            HttpContext context = HttpContext.Current;

            _request = context.Request;
            _response = context.Response;
        }
        [DebuggerStepThrough()]
        public void Set(string key,
        string value,
        DateTime expire)
        {
            SetValue(CryptographyHelper.Encrypt(BuildFullKey(key), _cryptographyKey), CryptographyHelper.Encrypt(value, _cryptographyKey), expire);
        }

        [DebuggerStepThrough()]
        public void Set(string key,
        string value)
        {
            SetValue(CryptographyHelper.Encrypt(BuildFullKey(key), _cryptographyKey), CryptographyHelper.Encrypt(value, _cryptographyKey));
        }

        [DebuggerStepThrough()]
        private void SetValue(string key,
        string value,
        DateTime expire)
        {

            HttpCookie cookie = new HttpCookie(HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value));
            cookie.Expires = expire;

            SetCookie(cookie);
        }

        [DebuggerStepThrough()]
        private void SetValue(string key,
        string value)
        {
            SetCookie(new HttpCookie(HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)));
        }

        [DebuggerStepThrough()]
        private void SetCookie(HttpCookie cookie)
        {
            _response.Cookies.Set(cookie);
        }


        public void SetLoginCookie(string userName, string userData, bool isPermanentCookie)
        {
            if (_response != null)
            {

                var userAuthTicket = new FormsAuthenticationTicket(1, userName, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), isPermanentCookie, userData, FormsAuthentication.FormsCookiePath);
                string encUserAuthTicket = FormsAuthentication.Encrypt(userAuthTicket);
                var userAuthCookie = new HttpCookie(Constants.CookieKeys.UserAuthentication, encUserAuthTicket);
                if (userAuthTicket.IsPersistent) userAuthCookie.Expires = userAuthTicket.Expiration;
                userAuthCookie.Path = FormsAuthentication.FormsCookiePath;
                if (_request.Url.Host.ToLower() != "localhost")
                {
                    userAuthCookie.Domain = SiteConfigurationReader.GetAppSettingsString(Constants.CommonConstants.UserAuthCookieDomain);
                }
                _response.Cookies.Add(userAuthCookie);
            }
        }

        public void RememberMe(string userName, string password)
        {
            if (_response != null)
            {
                string data = userName + "," + password;
                data = CryptographyHelper.Encrypt(data, _cryptographyKey);
                HttpCookie userAuthCookie = new HttpCookie(Constants.CommonConstants.RememberMeCookieName, data);
                userAuthCookie.Expires = DateTime.UtcNow.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(userAuthCookie);
                _response.Cookies.Add(userAuthCookie);
            }
        }

        public void ForgetMe()
        {
            if (_response != null)
            {
                _response.Cookies.Remove(Constants.CommonConstants.RememberMeCookieName);
                _request.Cookies.Remove(Constants.CommonConstants.RememberMeCookieName);
                if (_response.Cookies[Constants.CommonConstants.RememberMeCookieName] != null)
                {
                    _response.Cookies[Constants.CommonConstants.RememberMeCookieName].Expires = DateTime.UtcNow.AddDays(-1);
                }
                if (_request.Cookies[Constants.CommonConstants.RememberMeCookieName] != null)
                {
                    _request.Cookies[Constants.CommonConstants.RememberMeCookieName].Expires = DateTime.UtcNow.AddDays(-1);
                }
            }
        }

        public void GetUserDataFromRememberMeCookie(ref string userName, ref string password)
        {
            if (_request != null)
            {
                HttpCookie userDataCookie = _request.Cookies.Get(Constants.CommonConstants.RememberMeCookieName);
                if (userDataCookie != null)
                {
                    string userData = userDataCookie.Value;
                    if (!String.IsNullOrEmpty(userData))
                    {
                        userData = CryptographyHelper.Decrypt(userData, _cryptographyKey);
                        string[] values = userData.Split(',').ToArray();
                        if (values != null && values.Length >= 2)
                        {
                            userName = values[0];
                            password = values[1];
                        }
                    }
                }
            }
        }


        public string GetUserDataFromLoginCookie()
        {
            if (_response != null)
            {
                if (HttpContext.Current.User != null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        if (HttpContext.Current.User.Identity is FormsIdentity)
                        {
                            FormsIdentity formIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
                            FormsAuthenticationTicket userAuthTicket = formIdentity.Ticket;
                            if (!String.IsNullOrEmpty(userAuthTicket.UserData))
                            {
                                return CryptographyHelper.Decrypt(userAuthTicket.UserData, _cryptographyKey);
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        [DebuggerStepThrough()]
        public string Get(string key)
        {
            string value = GetValue(key);

            if (value != null)
            {
                return CryptographyHelper.Decrypt(value, _cryptographyKey);
            }

            return null;
        }

        [DebuggerStepThrough()]
        private string GetValue(string key)
        {
            HttpCookie cookie = GetCookie(CryptographyHelper.Encrypt(BuildFullKey(key), _cryptographyKey));

            if (cookie == null)
            {
                return null;
            }

            if (String.IsNullOrEmpty(cookie.Value))
            {
                return null;
            }

            return HttpUtility.UrlDecode(cookie.Value);
        }

        [DebuggerStepThrough()]
        private HttpCookie GetCookie(string key)
        {
            return _request.Cookies[HttpUtility.UrlEncode(key)];
        }

        [DebuggerStepThrough()]
        public static string BuildFullKey(string localKey)
        {
            const string COOKIE_KEY = "Web.UI.Helper";

            if (localKey.IndexOf(COOKIE_KEY) > -1)
            {
                return localKey;
            }
            else
            {
                return COOKIE_KEY + localKey;
            }
        }

        public void RemoveAll()
        {
            string[] myCookies = _request.Cookies.AllKeys;
            string[] except = { Constants.CookieKeys.Currency, Constants.CookieKeys.Culture, Constants.CookieKeys.Marketplace };

            foreach (string cookie in myCookies)
            {
                if (!Enumerable.Contains(except, cookie))
                {
                    HttpCookie userAuthCookie = new HttpCookie(cookie, "");
                    if (_request.Url.Host.ToLower() != "localhost")
                    {
                        userAuthCookie.Domain = SiteConfigurationReader.GetAppSettingsString(Constants.CommonConstants.UserAuthCookieDomain);
                    }
                    userAuthCookie.Expires = DateTime.UtcNow.AddDays(-2d);
                    _response.Cookies.Add(userAuthCookie);
                }

            }
        }

        public static UserViewModel GetSavedUserNameAndPassword()
        {
            CookieHelper newCookieHelper = new CookieHelper();
            string userName = string.Empty;
            string password = string.Empty;
            newCookieHelper.GetUserDataFromRememberMeCookie(ref userName, ref password);
            UserViewModel newUserViewModel = new UserViewModel();
            newUserViewModel.Email = userName;
            newUserViewModel.Password = password;
            return newUserViewModel;
        }
    }
}
