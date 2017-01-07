

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var ngAnnontate = require('gulp-ng-annotate')

gulp.task('minify', function () {
    return gulp.src("wwwroot/js/*.js")
    .pipe(ngAnnontate())
    .pipe(uglify())
    .pipe(gulp.dest("wwwroot/lib/_app"))
});