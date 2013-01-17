// Starter Gruntfile: compile LESS, lint, concat+minify JS, and watch.
module.exports = function (grunt) {
  grunt.initConfig({
    less:   { build: { files: { 'dist/app.css': 'src/less/app.less' } } },
    jshint: { all: ['src/js/**/*.js'] },
    concat: { build: { src: ['src/js/**/*.js'], dest: 'dist/app.js' } },
    uglify: { build: { files: { 'dist/app.min.js': ['dist/app.js'] } } },
    watch:  {
      less: { files: ['src/less/**/*.less'], tasks: ['less'] },
      js:   { files: ['src/js/**/*.js'], tasks: ['jshint', 'concat', 'uglify'] }
    }
  });
  ['grunt-contrib-less','grunt-contrib-jshint','grunt-contrib-concat',
   'grunt-contrib-uglify','grunt-contrib-watch'].forEach(grunt.loadNpmTasks);
  grunt.registerTask('default', ['less', 'jshint', 'concat', 'uglify']);
};
