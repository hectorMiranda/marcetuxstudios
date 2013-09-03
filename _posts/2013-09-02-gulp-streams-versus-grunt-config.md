---
layout: post
title: "Gulp streams versus Grunt configuration files"
date: 2013-09-02
author: marcetux
tags: [javascript, gulp, grunt, tooling, build, frontend]
---
I've been watching Gulp grow in the community and this week finally ported the front-end
build to it, side by side with the Grunt build, to compare them fairly. The difference
is real enough to matter.

Grunt describes a build as a configuration object: task A produces files, task B reads
them. Between steps, files get written to disk as intermediate results. The Grunt file
is big and the indirection between "what file does this step read" and "where does
this step write" is spread across the config. Gulp describes a build as a Node stream
pipeline: `gulp.src('src/**/*.ts').pipe(typescript()).pipe(uglify()).pipe(gulp.dest('dist/'))`.
The output of each step flows directly into the next as an in-memory stream. No
intermediate files on disk, which is why Gulp builds are faster for pipelines with
multiple transforms.

The concrete wins on our build: TypeScript compile → source-map generation → uglify runs
as one stream. In Grunt, each step writes to disk; in Gulp it's in memory until `dest()`.
The build goes from fourteen seconds to eight seconds, which is meaningful on the watch
loop. The `gulpfile.js` is also shorter and reads like code rather than a config DSL.
I'll migrate the full build to Gulp before the end of the month. Grunt was the right
choice when it was the only choice; the landscape moved and the tool should move with it.
