//using System;
//using FlickrNet;


//namespace Closet.Web.Infrastructure.ImageUploader
//{
//    public class FlickrManager
//    {       
//        public const string ApiKey = "cc08605624883c548f7fd6c8b8eed713";
//        public const string SharedSecret = "396d24e278293b56";

//        public static Flickr GetInstance()
//        {
//            return new Flickr(ApiKey, SharedSecret);
//        }

//        //public static Flickr GetAuthInstance()
//        //{
//        //    var f = new Flickr(ApiKey, SharedSecret);
//        //    if (OAuthToken != null)
//        //    {
//        //        f.OAuthAccessToken = OAuthToken.Token;
//        //        f.OAuthAccessTokenSecret = OAuthToken.TokenSecret;
//        //    }
//        //    return f;
//        //}

//        //public static OAuthAccessToken OAuthToken
//        //{
//        //    get
//        //    {
//        //        if (HttpContext.Current.Request.Cookies["OAuthToken"] == null)
//        //        {
//        //            return null;
//        //        }
//        //        var values = HttpContext.Current.Request.Cookies["OAuthToken"].Values;
//        //        return new OAuthAccessToken
//        //        {
//        //            FullName = values["FullName"],
//        //            Token = values["Token"],
//        //            TokenSecret = values["TokenSecret"],
//        //            UserId = values["UserId"],
//        //            Username = values["Username"]
//        //        };
//        //    }
//        //    set
//        //    {
//        //        // Stores the authentication token in a cookie which will expire in 1 hour
//        //        var cookie = new HttpCookie("OAuthToken")
//        //        {
//        //            Expires = DateTime.UtcNow.AddHours(1),
//        //        };
//        //        cookie.Values["FullName"] = value.FullName;
//        //        cookie.Values["Token"] = value.Token;
//        //        cookie.Values["TokenSecret"] = value.TokenSecret;
//        //        cookie.Values["UserId"] = value.UserId;
//        //        cookie.Values["Username"] = value.Username;
//        //        HttpContext.Current.Response.AppendCookie(cookie);
//        //    }
//        //}
        
//    }
//}
