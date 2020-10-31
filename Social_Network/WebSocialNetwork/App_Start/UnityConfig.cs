using BuisnesLogic.Concrete;
using BuisnesLogic.Interfaces;
using DAL.Concrete;
using DAL.Interfaces;
using DalNeo4j.Concrete;
using DalNeo4j.Interfaces;
using System;

using Unity;
using Unity.Injection;
using WebSocialNetwork.Models;
using WebSocialNetwork.Models.Concrete;
using WebSocialNetwork.Models.Interfaces;

namespace WebSocialNetwork
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<IAuthManager, AuthManager>()
                .RegisterType<IPostManager, PostManager>()
                .RegisterType<IUserManager, UserManager>(); // BusinessLogic Managers

            container.RegisterType<IUser, AppUser>()
                .RegisterType<IAppUserManager, AppUserManager>()
                .RegisterType<IAppAuthManager, AppAuthManager>()
                .RegisterType<IAppPostsManager, AppPostsManager>();// App classes

            container.RegisterType<IUsersDal, UsersDal>(new InjectionConstructor("mongodb://localhost:27017/"))
                .RegisterType<IPostsDal, PostsDal>(new InjectionConstructor("mongodb://localhost:27017/"));// MongoDB Dal
            

            container.RegisterType<IUsersDalNeo4j, UsersDalNeo4j>(new InjectionConstructor("http://localhost:7474/db/data/", "neo4j", "1234567890")); //Neo4j Dal

        }
    }
}