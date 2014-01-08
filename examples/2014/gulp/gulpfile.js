// A minimal Gulp build: TypeScript → JS, LESS → CSS, concat, uglify.
// Run 'gulp watch' during development; 'gulp' for a production build.
var gulp   = require('gulp');
var ts     = require('gulp-typescript');
var less   = require('gulp-less');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');

var tsProject = ts.createProject({ target: 'ES5', module: 'commonjs' });

gulp.task('scripts', function () {
  return gulp.src('src/**/*.ts')
    .pipe(ts(tsProject))
    .pipe(concat('app.js'))
    .pipe(uglify())
    .pipe(gulp.dest('dist'));
});

gulp.task('styles', function () {
  return gulp.src('src/styles/main.less')
    .pipe(less())
    .pipe(gulp.dest('dist/css'));
});

gulp.task('watch', function () {
  gulp.watch('src/**/*.ts',   ['scripts']);
  gulp.watch('src/**/*.less', ['styles']);
});

gulp.task('default', ['scripts', 'styles']);
