namespace Nancy.Demo.Dropbox
{
    using DropNet;
    using Nancy.Extensions;

    public static class NancyExtensions
    {
        public static void RequiresDropboxAuthentication(this INancyModule module, IDropNetClient dropNetClient)
        {
            module.AddBeforeHookOrExecute(SecurityHooks.RequiresDropboxAuthentication(dropNetClient), "Authentication failed.");
        }
    }
}
