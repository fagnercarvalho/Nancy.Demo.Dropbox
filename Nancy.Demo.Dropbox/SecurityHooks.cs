namespace Nancy.Demo.Dropbox
{
    using DropNet;
    using DropNet.Exceptions;
    using System;

    public class SecurityHooks
    {
        public static Func<NancyContext, Response> RequiresDropboxAuthentication(IDropNetClient dropNetClient)
        {
            return (ctx) =>
            {
                Response response = null;
                try
                {
                    var token = dropNetClient.GetAccessToken();
                    dropNetClient.UserLogin = token;
                }
                catch (DropboxRestException ex) when (ex.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest))
                {
                    response = new Response
                    {
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                }
                catch (DropboxRestException ex) when (ex.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))
                {
                    //
                }

                return response;
            };
        }
    }
}
