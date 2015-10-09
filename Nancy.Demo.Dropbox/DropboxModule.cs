namespace Nancy.Demo.Dropbox
{
    using DropNet;
    using System.IO;
    using System.Linq;

    public class DropboxModule : NancyModule
    {
        public DropboxModule(IDropNetClient dropNetClient)
        {
            Get["/files"] = _ =>
            {
                this.RequiresDropboxAuthentication(dropNetClient);

                var metaData = dropNetClient.GetMetaData(list: true);

                return Response.AsJson(metaData.Contents
                    .Select(f => new {
                        Name = f.Name,
                        Modified = f.Modified,
                        Type = f.Is_Dir ? "Folder": "File"
                    }));
            };

            Post["/files"] = _ =>
            {
                var file = Request.Files.FirstOrDefault();

                if (file != null)
                {
                    var uploaded = dropNetClient.UploadFile("/", file.Name, ReadFile(file.Value));

                    return uploaded != null ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                } else {
                    return HttpStatusCode.BadRequest;
                }
            };

            Get["/login"] = _ =>
            {
                dropNetClient.GetToken();
                var url = dropNetClient.BuildAuthorizeUrl();
                return Response.AsRedirect(url, Responses.RedirectResponse.RedirectType.Permanent);
            };
        }

        public static byte[] ReadFile(Stream file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
