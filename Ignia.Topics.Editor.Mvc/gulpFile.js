/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/*==============================================================================================================================
| DEPENDENCIES
\-----------------------------------------------------------------------------------------------------------------------------*/
const   {src, dest, parallel}   = require('gulp');

const   gulpif                  = require('gulp-if'),
        concat                  = require('gulp-concat'),
        merge                   = require('merge2');

const   sass                    = require('gulp-sass'),
        postCss                 = require("gulp-postcss"),
        cssNano                 = require("cssnano"),
        sourceMaps              = require("gulp-sourcemaps"),
        jshint                  = require('gulp-jshint'),
        uglify                  = require('gulp-uglify');

/*==============================================================================================================================
| VARIABLES
\-----------------------------------------------------------------------------------------------------------------------------*/
var     environment             = 'development',
        outputDir               = 'wwwroot',
        isProduction            = false;

/*==============================================================================================================================
| SOURCE FILE PATHS
>-------------------------------------------------------------------------------------------------------------------------------
| Paths to files referenced in the build process. Path names may use any "magic" glob characters, as documented at
| https://github.com/isaacs/node-glob.
>-------------------------------------------------------------------------------------------------------------------------------
| ### NOTE: JJC021715: These paths are only intended for source files. Destination files will not use glob "magic", and will
| be conditional based on the outputDir. As a result, they will likely be hardcoded into each task's dest() method.
\-----------------------------------------------------------------------------------------------------------------------------*/
const files = {
  scss                          : 'Shared/Styles/**/[!_]*.scss',
  js                            : 'Shared/Scripts/*.js',
  jsVendor                      : [ 'node_modules/jquery/dist/jquery.min.js',
                                    'node_modules/jquery-ui-dist/jquery-ui.min.js',
                                    'node_modules/jquery-validation/dist/jquery.validate.min.js',
                                    'node_modules/popper.js/dist/umd/popper.min.js',
                                    'node_modules/bootstrap/dist/js/bootstrap.min.js',
                                    'node_modules/jquery-tokeninput/dist/js/jquery-tokeninput.min.js',
                                    'Shared/Scripts/ExtJS/ext-base.js',
                                    'Shared/Scripts/ExtJS/ext-all.js',
                                    'Shared/Scripts/ExtJS/ext-ExtendTextField.js',
                                    'node_modules/jquery-ui-timepicker-addon/dist/jquery-ui-timepicker-addon.min.js',
                                    'node_modules/jquery.are-you-sure/jquery.are-you-sure.js'
                                  ],
  cssVendor                     : [ 'node_modules/bootstrap/dist/css/bootstrap.min.css',
                                    'node_modules/jquery-ui-dist/jquery-ui.min.css',
                                    'node_modules/jquery-tokeninput/dist/css/token-input.min.css',
                                    'node_modules/jquery-ui-timepicker-addon/dist/jquery-ui-timepicker-addon.min.css'
                                  ]
};



/*==============================================================================================================================
| DEPENDENCIES
>-------------------------------------------------------------------------------------------------------------------------------
| Paths to third-party dependencies that need to be copied into the project. This is exclusively for pre-compiled client-side
| files, such as JavaScript (excluding TypeScript), CSS (excluding SCSS), images, and the occasional font.
\-----------------------------------------------------------------------------------------------------------------------------*/
const dependencies = {
  'Scripts': {
    'jQuery'                    : 'node_modules/jquery/dist/*.*',
    'jQueryUI'                  : 'node_modules/jquery-ui-dist/jquery-ui.*.js',
    'jQueryValidate'            : 'node_modules/jquery-validation/dist/jquery.validate*.js',
    'Bootstrap'                 : 'node_modules/bootstrap/dist/js/bootstrap.min.*',
    'Popper'                    : 'node_modules/popper.js/dist/umd/popper.min*',
    'TokenInput'                : 'node_modules/jquery-tokeninput/dist/js/*.js',
    'ExtJS'                     : 'Shared/Scripts/ExtJS/*.js',
    'CkEditor'                  : 'Shared/Scripts/CkEditor/**/*.js',
    'TrentRichardson'           : 'node_modules/jquery-ui-timepicker-addon/dist/*.js',
    'PaperCut'                  : 'node_modules/jquery.are-you-sure/*.js'
  },
  'Styles': {
    'Bootstrap'                 : 'node_modules/bootstrap/dist/css/bootstrap.min.*',
    'jQueryUI'                  : 'node_modules/jquery-ui-dist/jquery-ui.*.css',
    'ExtJS'                     : 'Shared/Scripts/ExtJS/Resources/**/*',
    'TokenInput'                : 'node_modules/jquery-tokeninput/dist/css/token-input.min.css',
    'TrentRichardson'           : 'node_modules/jquery-ui-timepicker-addon/dist/*.css'
  },
  'Fonts': {
  }
};

/*==============================================================================================================================
| SET ENVIRONMENT
>-------------------------------------------------------------------------------------------------------------------------------
| Looks for an environment variable and conditionally set local context accordingly.
\-----------------------------------------------------------------------------------------------------------------------------*/
environment                     = process.env.BUILD_ENVIRONMENT || environment;

// Environment: Development
if (environment === 'development') {
  isProduction                  = false;
}

// Environment: Production
else {
  isProduction                  = true;
}

/*==============================================================================================================================
| TASK: SCSS
>-------------------------------------------------------------------------------------------------------------------------------
| Compiles the SCSS files, including views, and moves them to the build directory.
\-----------------------------------------------------------------------------------------------------------------------------*/
function scssTask() {
  return src(files.scss, {base: 'Shared/Styles'})
    //.pipe(autoPrefixer({ browsers: ['last 2 versions', 'safari 5', 'ie 8', 'ie 9', 'opera 12.1', 'ios 6', 'android 4'] }))
    //.pipe(sassUnicode())
    .pipe(sourceMaps.init())
    .pipe(sass())
    .on("error", sass.logError)
    .pipe(postCss([
      cssNano()
    ]))
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + '/Shared/Styles/'));
}

/*==============================================================================================================================
| TASK: JAVASCRIPT FILES
>-------------------------------------------------------------------------------------------------------------------------------
| Minimizes JavaScript files as part of production process.
\-----------------------------------------------------------------------------------------------------------------------------*/
function jsTask() {
  return src(files.js, { base: 'Shared/Scripts' })
    //.pipe(jshint('.jshintrc'))
    .pipe(sourceMaps.init())
    .pipe(jshint())
    .pipe(jshint.reporter('default'))
    .pipe(concat('Scripts.js'))
    .pipe(uglify())
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + '/Shared/Scripts/'));
}

/*==============================================================================================================================
| TASK: JAVASCRIPT VENDOR FILES
>-------------------------------------------------------------------------------------------------------------------------------
| Consolidates third-party JavaScript files sourced from npm as part of production process.
\-----------------------------------------------------------------------------------------------------------------------------*/
function jsVendorTask() {
  return src(files.jsVendor)
    .pipe(sourceMaps.init())
    .pipe(concat('Vendor.js'))
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + '/Shared/Scripts/'));
}

/*==============================================================================================================================
| TASK: STYLESHEET VENDOR FILES
>-------------------------------------------------------------------------------------------------------------------------------
| Consolidates third-party Stylesheet files sourced from npm as part of production process.
\-----------------------------------------------------------------------------------------------------------------------------*/
function cssVendorTask() {
  return src(files.cssVendor)
    .pipe(sourceMaps.init())
    .pipe(concat('Vendor.css'))
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + '/Shared/Styles/'));
}

/*==============================================================================================================================
| TASK: DEPENDENCIES
>-------------------------------------------------------------------------------------------------------------------------------
| Copies static dependencies from their source folders and into their appropriate build folders.
\-----------------------------------------------------------------------------------------------------------------------------*/
function dependenciesTask() {
  var streams = [];
  for (var contentType in dependencies) {
    for (var dependency in dependencies[contentType]) {
      streams.push(
        src(dependencies[contentType][dependency])
          .pipe(dest(outputDir.concat('/Shared/', contentType, '/Vendor/', dependency)))
      );
    }
  }
  return merge(streams);
}

/*==============================================================================================================================
| EXPORT TASKS
>-------------------------------------------------------------------------------------------------------------------------------
| Exports the above defined tasks for use by gulp.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.scss                    = scssTask;
exports.cssVendor               = cssVendorTask;
exports.js                      = jsTask;
exports.jsVendor                = jsVendorTask;
exports.dependencies            = dependenciesTask;

/*==============================================================================================================================
| TASK: BUILD
>-------------------------------------------------------------------------------------------------------------------------------
| Composite task that will call all build-related tasks.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.build = parallel(dependenciesTask, scssTask, cssVendorTask, jsTask, jsVendorTask);

/*==============================================================================================================================
| TASK: DEFAULT
>-------------------------------------------------------------------------------------------------------------------------------
| The default task when Gulp runs, assuming no task is specified. Assuming the environment variable isn't explicitly defined
| otherwise, will run on development-oriented tasks.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.default = parallel(dependenciesTask, scssTask, cssVendorTask, jsTask, jsVendorTask);
