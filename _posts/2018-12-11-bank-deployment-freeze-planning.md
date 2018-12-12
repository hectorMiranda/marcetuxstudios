---
layout: post
title: "The bank deployment freeze and what to do with it"
date: 2018-12-11
author: marcetux
tags: [enterprise, banking, process, architecture, retrospective]
---
The bank enters a deployment freeze in mid-December through early January — no
production changes except approved emergency patches. For a startup engineer this
reads as a bureaucratic constraint; after three months in the environment I understand
it as a reasonable response to the risk surface of the holiday period. The teams that
support the transaction system go on reduced staff. Trading volume is lower. The cost
of a production incident during the holidays is not just technical — it's reputational
and, depending on the transaction, regulatory.

What to do with the freeze: it's the period when the maintenance work that never
makes it into a sprint gets done. Tech debt that doesn't have a ticket, documentation
that got deferred, architecture decisions that need writing up before the rationale
evaporates. I'm using the time to write ADRs for five decisions that were made in
verbal conversations in September and October and are currently stored in my notes
rather than the team repository.

The other use: long-horizon thinking. When you're not shipping, you can read the
things you bookmarked. The service mesh evaluation I shelved in March is back on my
reading list. I want to understand Istio's current state — 0.8 to 1.0 happened in
July — before we start the next phase of the modernization program in January. The
freeze is time the calendar gives you; the choice is whether to fill it with the
comfortable or the useful.
