---
layout: post
title: "November notes, 2015"
date: 2015-11-22
author: marcetux
tags: [meta, retrospective]
---
November was the Let's Encrypt month for me — not just the announcement but the actual
doing. HTTPS on the Pi dashboard and the home Jenkins took twenty minutes, and the cron
renewal means I don't have to think about it again. The certificate authority process
that used to require time and money and attention is now a cron job. That's a real
change in what "secure by default" means for everything you run.

The React work at JibJab is maturing: PropTypes are on every component we've written,
the lifecycle hooks are being used for their intended purposes rather than as places to
stuff logic that doesn't fit elsewhere, and the multipart upload for large renders is
handling the longer holiday templates without drama. The S3 multipart threshold tuning
was the kind of decision that required looking at actual file size data rather than
picking a round number, which is the kind of decision that's easy to do correctly if
you have the logs.

Angular 2 beta is coherent in a way the alpha wasn't. The convergence with React on
component models is real and I think it's the right direction — both are better for
having pushed each other. December is going to be busy with holiday traffic; the render
pipeline will earn its observability instrumentation.
