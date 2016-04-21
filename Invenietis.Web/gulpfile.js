/// <binding Clean='clean:all' />
/// <binding Build='build:all' />

"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    less = require("gulp-less"),
    watch = require('gulp-watch'),
    through = require('through2'),
    bundle = require('gulp-bundle-aspnet'),
    plumber = require('gulp-plumber'),
    runSequence = require('gulp-run-sequence'),
    rename = require('gulp-rename');

var paths = {
    webroot: "./wwwroot/"
};

paths.jsFolder = paths.webroot + "js/";
paths.js = paths.jsFolder + "**/*.js";
paths.minJs = paths.jsFolder + "**/*.min.js";

paths.cssFolder = paths.webroot + "css/";
paths.css = paths.cssFolder + "**/*.css";
paths.minCss = paths.cssFolder + "**/*.min.css";

paths.lessFolder = paths.webroot + "less/";
paths.less = paths.lessFolder + "**/*.less";
paths.excludedLess = paths.lessFolder + "**/inc/*.less";

var onError = function (err) {
    console.log(err);
};

gulp.task("clean:js", function (cb) {
    rimraf(paths.minJs, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.minCss, cb);
});

gulp.task("clean:all", ["clean:js", "clean:css"]);

gulp.task('less', function () {
    return gulp.src([paths.less, "!" + paths.excludedLess])
      .pipe(less())
      .pipe(gulp.dest(paths.cssFolder));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
            .pipe(cssmin())
            .pipe(rename({ suffix: '.min' }))
            .pipe(gulp.dest(paths.cssFolder));
});

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs])
            .pipe(uglify())
            .pipe(rename({ suffix: '.min' }))
            .pipe(gulp.dest(paths.jsFolder));
});

gulp.task("min", ["min:js", "min:css"]);

gulp.task('bundles', function () {
    return gulp.src('./bundles.json')
        .pipe(bundle())
});

gulp.task('build:js', function (cb) {
    return runSequence(
        'clean:js',
        ['min:js', 'bundles'],
        cb
    );
});

gulp.task('build:css', function (cb) {
    return runSequence(
        ['clean:css', 'less'],
        'min:css',
        cb
    );
});

gulp.task("build:all", function (cb) {
    return runSequence(
        ['clean', 'less'],
        ['min', 'bundles'],
        cb
    );
});

gulp.task('watch:less', function () {
    return gulp.src([paths.less, "!" + paths.excludedLess])
        .pipe(watch(paths.less))
        .pipe(plumber())
        .pipe(less())
        .pipe(cssmin())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest(paths.cssFolder));
});

gulp.task("watch:js", function (cb) {
    return gulp.watch([paths.js, "!" + paths.minJs], ['build:js']);
});

gulp.task('watch:all', ['watch:less', 'watch:js']);