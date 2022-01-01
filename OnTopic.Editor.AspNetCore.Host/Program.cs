/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Sample OnTopic Site
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.FileProviders;
using OnTopic.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore;
using OnTopicTest;

    /*==========================================================================================================================
    | CONFIGURE SERVICES
    \-------------------------------------------------------------------------------------------------------------------------*/
    var builder                 = WebApplication.CreateBuilder(args);

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Cookie Policy
      \-----------------------------------------------------------------------------------------------------------------------*/
      builder.Services.Configure<CookiePolicyOptions>(options => {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: MVC
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mvcBuilder = builder.Services.AddControllersWithViews()

        //Add OnTopic support
        .AddTopicSupport()

        //Add OnTopic editor support
        .AddTopicEditor();

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Runtime View Compilation
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (builder.Environment.IsDevelopment()) {
        mvcBuilder.AddRazorRuntimeCompilation();
        builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options => {
          var libraryPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "OnTopic.Editor.AspNetCore"));
          var pluginsPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "OnTopic.Editor.AspNetCore.Attributes"));
          options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
          options.FileProviders.Add(new PhysicalFileProvider(pluginsPath));
        });
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Register: Activators
      \-----------------------------------------------------------------------------------------------------------------------*/
      var activator = new SampleActivator(builder.Configuration.GetConnectionString("OnTopic"), builder.Environment);

      builder.Services.AddSingleton<IControllerActivator>(activator);
      builder.Services.AddSingleton<IViewComponentActivator>(activator);

    /*==========================================================================================================================
    | CONFIGURE APPLICATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    var app                     = builder.Build();

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Error Pages
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (app.Environment.IsDevelopment()) {
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
        app.MapControllers();
        app.MapTopicEditorRoute();