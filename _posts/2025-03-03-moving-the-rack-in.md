---
layout: post
title: "Moving the rack into the studio"
date: 2025-03-03
author: marcetux
tags: [studio, homelab, hardware, marcetuxstudios]
---
The 12U rack went into the studio Saturday morning. Four hours, two trips, one
bruised doorframe. The NAS, the Pi cluster, the patch panel, and the managed switch
are mounted and cabled; the rack is sitting on the rolling cart exactly as planned,
and I've already pulled it out twice to reroute cables I got wrong the first time. The
rolling-cart decision paid off immediately.

The Pi cluster runs k3s, the NAS runs TrueNAS Scale, and the whole thing is on a UPS
that gives me about 20 minutes of runtime — enough to do a clean shutdown if the
building power does something weird. Lincoln Heights isn't the most reliable block for
power; the previous tenant mentioned occasional brownouts. The UPS also conditions the
power, which matters for the test gear on the bench.

What's left: the monitor arm for the workstation, cable management on the left side of
the bench, and proper labels on every patch panel port. I've learned over several home
labs that the difference between a setup that's pleasant to use and one you dread
touching is almost entirely labeling. Three hours of upfront label-making is paid back
the first time something needs troubleshooting at 11pm.
