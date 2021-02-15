/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       OnTopic Editor
\=============================================================================================================================*/

/*==============================================================================================================================
| DEPENDENCIES
\-----------------------------------------------------------------------------------------------------------------------------*/
const   {src, dest, parallel}   = require('gulp');

const   sass                    = require('gulp-sass'),
        postCss                 = require("gulp-postcss"),
        cssNano                 = require("cssnano"),
        sourceMaps              = require("gulp-sourcemaps"),
        jshint                  = require('gulp-jshint'),
        uglify                  = require('gulp-uglify');

/*==============================================================================================================================
| VARIABLES
\-----------------------------------------------------------------------------------------------------------------------------*/
var     outputDir               = 'wwwroot';

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
  js                            : 'Shared/Scripts/**/*.js',
  vendor                        : {
    js                          :   [ 'node_modules/jquery-tokeninput/dist/js/jquery-tokeninput.min.js',
                                      'node_modules/jquery-ui-timepicker-addon/dist/jquery-ui-timepicker-addon.min.js'                                                                          ],
    css                         :   [ 'node_modules/jquery-tokeninput/dist/css/token-input.min.css',
                                      'node_modules/jquery-tokeninput/dist/css/token-input-facebook.min.css',
                                      'node_modules/jquery-ui-timepicker-addon/dist/jquery-ui-timepicker-addon.min.css'
                                    ]
                                  }
};

/*==============================================================================================================================
| METHOD: GET OUTPUT DIR
\-----------------------------------------------------------------------------------------------------------------------------*/
var     getOutputDir            = (contentType) => outputDir.concat("/Shared/", contentType, "/");

/*==============================================================================================================================
| FACTORY: SCSS
>-------------------------------------------------------------------------------------------------------------------------------
| Compiles the SCSS files, including views, and moves them to the build directory.
\-----------------------------------------------------------------------------------------------------------------------------*/
var scssFactory = (source, destination) =>
  src(source, { base: 'Shared/Styles' })
  //.pipe(autoPrefixer({ browsers: ['last 2 versions', 'safari 5', 'ie 8', 'ie 9', 'opera 12.1', 'ios 6', 'android 4'] }))
  //.pipe(sassUnicode())
  .pipe(sourceMaps.init())
  .pipe(sass())
  .on("error", sass.logError)
  .pipe(postCss([
    cssNano()
  ]))
  .pipe(sourceMaps.write('.'))
  .pipe(dest(destination || getOutputDir('Styles')));

/*==============================================================================================================================
| FACTORY: JAVASCRIPT FILES
>-------------------------------------------------------------------------------------------------------------------------------
| Minimizes JavaScript files as part of production process.
\-----------------------------------------------------------------------------------------------------------------------------*/
var jsFactory = (source, destination, filename) =>
  src(source)
    //.pipe(jshint('.jshintrc'))
    .pipe(sourceMaps.init())
    .pipe(jshint())
    .pipe(jshint.reporter('default'))
    .pipe(uglify())
    .pipe(sourceMaps.write('.'))
    .pipe(dest(destination));

/*==============================================================================================================================
| FACTORY: VENDOR FILES
>-------------------------------------------------------------------------------------------------------------------------------
| Consolidates third-party files sourced from npm as part of production process.
\-----------------------------------------------------------------------------------------------------------------------------*/
var vendorFilesFactory = (source, destination, filename) =>
  src(source)
    .pipe(sourceMaps.init())
    .pipe(sourceMaps.write('.'))
    .pipe(dest(destination));

/*==============================================================================================================================
| DEFINE TASKS
>-------------------------------------------------------------------------------------------------------------------------------
| Using the above factory methods, define available tasks
\-----------------------------------------------------------------------------------------------------------------------------*/
var scssTask                    = () => scssFactory(files.scss);
var jsTask                      = () => jsFactory(files.js, getOutputDir('Scripts'), 'Scripts.js');
var jsVendorTask                = () => vendorFilesFactory(files.vendor.js, getOutputDir('Scripts'), 'Vendor.js');
var cssVendorTask               = () => vendorFilesFactory(files.vendor.css, getOutputDir('Styles'), 'Vendor.css');

/*==============================================================================================================================
| EXPORT TASKS
>-------------------------------------------------------------------------------------------------------------------------------
| Exports the above defined tasks for use by gulp.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.js                      = jsTask;
exports.scss                    = scssTask;
exports.jsVendor                = jsVendorTask;
exports.cssVendor               = cssVendorTask;

/*==============================================================================================================================
| TASK: BUILD
>-------------------------------------------------------------------------------------------------------------------------------
| Composite task that will call all build-related tasks.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.build = parallel(scssTask, cssVendorTask, jsTask, jsVendorTask);

/*==============================================================================================================================
| TASK: DEFAULT
>-------------------------------------------------------------------------------------------------------------------------------
| The default task when Gulp runs, assuming no task is specified. Assuming the environment variable isn't explicitly defined
| otherwise, will run on development-oriented tasks.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.default = parallel(scssTask, cssVendorTask, jsTask, jsVendorTask);