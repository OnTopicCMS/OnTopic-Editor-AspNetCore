/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Sample OnTopic Site
\=============================================================================================================================*/
using Ignia.Topics.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnTopicTest {

  /*============================================================================================================================
  | CLASS: STARTUP
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Configures the application and sets up dependencies.
  /// </summary>
  public class Startup {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new instances of the <see cref="Startup"/> class. Accepts an <see cref="IConfiguration"/>.
    /// </summary>
    /// <param name="configuration">
    ///   The shared <see cref="IConfiguration"/> dependency.
    /// </param>
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    /*==========================================================================================================================
    | PROPERTY: CONFIGURATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a (public) reference to the application's <see cref="IConfiguration"/> service.
    /// </summary>
    public IConfiguration Configuration { get; }

    /*==========================================================================================================================
    | METHOD: CONFIGURE SERVICES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides configuration of services. This method is called by the runtime to bootstrap the server configuration.
    /// </summary>
    public void ConfigureServices(IServiceCollection services) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Cookie Policy
      \-----------------------------------------------------------------------------------------------------------------------*/
      services.Configure<CookiePolicyOptions>(options => {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: MVC
      \-----------------------------------------------------------------------------------------------------------------------*/
      services.AddMvc()

        //Set to use .NET Core 2.2
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)

        //Add OnTopic support
        .AddTopicSupport();


    }

    /*==========================================================================================================================
    | METHOD: CONFIGURE (APPLICATION)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides configuration the application. This method is called by the runtime to bootstrap the application
    ///   configuration, including the HTTP pipeline.
    /// </summary>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Error Pages
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }
      else {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      /*------------------------------------------------------------------------------------------------------------------------
     | Configure: Server defaults
     \-----------------------------------------------------------------------------------------------------------------------*/
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();
      app.UseMvcWithDefaultRoute();

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: MVC
      \-----------------------------------------------------------------------------------------------------------------------*/
      app.UseMvc(routes => {

        /*------------------------------------------------------------------------------------------------------------------------
        | Handle OnTopic Web namespace
        \-----------------------------------------------------------------------------------------------------------------------*/
        routes.MapAreaRoute(
          name: "TopicEditor",
          areaName: "Editor",
          template: "OnTopic/{action}/{*path}",
          defaults: new { controller = "Editor" }
        );

      });

    }

  } //Class
} //Namespace
