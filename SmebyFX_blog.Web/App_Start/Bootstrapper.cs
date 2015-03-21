using System;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Practices.Unity;
using SmebyFX_blog.Core.Repositories;
using SmebyFX_blog.Core.Repositories.DbConfig;
using SmebyFX_blog.Core.Services;
using Unity.Mvc4;

namespace SmebyFX_blog.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialize()
        {
            var container = new UnityContainer();
            RegiserTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static void RegiserTypes(IUnityContainer container)
        {
            var endpointUrl = ConfigurationManager.ConnectionStrings["DocumentDbEndpointUrl"].ConnectionString;
            var authorizationKey = ConfigurationManager.AppSettings["DocumentDbAuthorizationKey"];

            var documentClient = new DocumentClient(new Uri(endpointUrl), authorizationKey);
            var documentDb = new DocumentDbInitialization(documentClient);

            container.RegisterType<PostRepository>(new InjectionFactory(x => new PostRepository(documentClient, documentDb.GetPostCollection())));

            container.RegisterType<PostService>();
        }
    }
}