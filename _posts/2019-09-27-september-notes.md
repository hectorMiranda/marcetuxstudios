---
layout: post
title: "September notes"
date: 2019-09-27
author: marcetux
tags: [meta, retrospective]
---
September was the release month I'd been watching since January. .NET Core 3.0 and Blazor Server landing together is a meaningful moment for .NET on the server — the platform is clearly not Windows-only anymore in any meaningful sense, and the Blazor model gives .NET shops a credible full-stack story that doesn't require JavaScript expertise. Whether that's the right tool for every problem is a different question, and I tried to answer it honestly in the Blazor post.

The Worker Service migration was more satisfying than I expected, mostly because the friction points revealed actual problems in the old code rather than just migration friction. The nullable reference types analysis is ongoing — I'm going to bring it to the other services gradually rather than enabling it everywhere at once and creating a mountain of warnings to work through.

Home side: the Pi 4 is rock-solid as the energy monitor host. I've been looking at the Altium board for a version two with better analog filtering on the CT inputs — the current waveform readings have some noise that's biasing the RMS calculation at low loads. The fix is a low-pass hardware filter before the ADC input, which I want to understand properly rather than just copying a reference design. Back to the app note library this weekend.
