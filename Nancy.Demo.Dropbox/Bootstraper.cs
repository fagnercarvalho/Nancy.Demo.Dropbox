namespace Nancy.Demo.Dropbox
{
    using DropNet;
    using System.Configuration;
    using TinyIoc;

    public class Bootstraper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IDropNetClient>(new DropNetClient(
                ConfigurationManager.AppSettings["API_KEY"], 
                ConfigurationManager.AppSettings["API_SECRET"]));
        }
    }
}
