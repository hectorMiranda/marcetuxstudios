---
layout: post
title: "Webpack replacing Gulp for the JavaScript bundle"
date: 2015-06-14
author: marcetux
tags: [webpack, gulp, frontend, tooling, javascript]
---
Gulp handled the JavaScript bundle fine when the app had a handful of files and one
entry point. Now there are three entry points — member app, admin panel, email preview
— with shared utility code across all three. The Gulp pipeline for this involved
manually specifying what's shared and writing a concat step that was always almost right.
Webpack's module graph makes this its whole job.

Webpack understands `import` and `require`. You give it an entry point, it traces the
module graph from there, and it knows exactly what each bundle needs. The shared
utilities between the three entry points land in a `CommonsChunkPlugin` output —
Webpack produces the shared chunk automatically, and each page loads the commons bundle
once and then the page-specific bundle. No manual inventory of what's shared; the graph
is the answer.

The Gulp tasks I kept are the non-JS ones: SASS compilation, image optimization, the
build timestamp injection. Webpack owns JavaScript; Gulp orchestrates the rest and calls
Webpack when it's JS time. I've seen projects throw everything into Webpack loaders and
end up with a single impenetrable configuration. Better to let each tool do what it's
good at. Webpack is genuinely good at module bundling; fighting it to do image
optimization doesn't earn you anything.
