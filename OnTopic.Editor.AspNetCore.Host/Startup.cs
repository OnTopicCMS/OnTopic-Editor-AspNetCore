/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Sample OnTopic Site
\=============================================================================================================================*/
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using OnTopic.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore;

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
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) {
      Configuration = configuration;
      HostingEnvironment = webHostEnvironment;
    }

    /*==========================================================================================================================
    | PROPERTY: CONFIGURATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a (public) reference to the application's <see cref="IConfiguration"/> service.
    /// </summary>
    public IConfiguration Configuration { get; }

    /*==========================================================================================================================
    | PROPERTY: HOSTING ENVIRONMENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a (public) reference to the application's <see cref="IWebHostEnvironment"/> service.
    /// </summary>
    public IWebHostEnvironment HostingEnvironment { get; }

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
      var mvcBuilder = services.AddControllersWithViews()

        //Add OnTopic support
        .AddTopicSupport()

        //Add OnTopic editor support
        .AddTopicEditor();

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Runtime View Compilation
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (HostingEnvironment.IsDevelopment()) {
        mvcBuilder.AddRazorRuntimeCompilation();
        services.Configure<MvcRazorRuntimeCompilationOptions>(options => {
          var libraryPath = Path.GetFullPath(Path.Combine(HostingEnvironment.ContentRootPath, "..", "OnTopic.Editor.AspNetCore"));
          options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
        });
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Watch Razor Class Library (RCLs) views
      >-------------------------------------------------------------------------------------------------------------------------
      | ### HACK JJC20190523: Due to a limitation of Visual Studio, updates in views in RCLs don't trigger a dynamic recompile.
      | This thus requires rebuilding and reloading the application after every change in a view—not very practical! The
      | following works around this limitation. It is not necessary in production environments.
      \-----------------------------------------------------------------------------------------------------------------------*/
      //services.Configure<RazorViewEngineOptions>(options => options.FileProviders.Add(
      //  new PhysicalFileProvider(Path.Combine(HostingEnvironment.ContentRootPath, "..\\OnTopic.Editor.AspNetCore"))
      //));

      /*------------------------------------------------------------------------------------------------------------------------
      | Register: Activators
      \-----------------------------------------------------------------------------------------------------------------------*/
      var activator = new SampleActivator(Configuration.GetConnectionString("OnTopic"), HostingEnvironment);

      services.AddSingleton<IControllerActivator>(activator);
      services.AddSingleton<IViewComponentActivator>(activator);

    }

    /*==========================================================================================================================
    | METHOD: CONFIGURE (APPLICATION)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides configuration the application. This method is called by the runtime to bootstrap the application
    ///   configuration, including the HTTP pipeline.
    /// </summary>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

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
      //app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: MVC
      \-----------------------------------------------------------------------------------------------------------------------*/
      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
        endpoints.MapTopicEditorRoute();
      });

    }

  } //Class
} //Namespace