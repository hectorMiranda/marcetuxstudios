---
layout: post
title: "First flight and what I got wrong"
date: 2018-06-19
author: marcetux
tags: [electronics, drone, hardware, hobby]
---
The drone flew for the first time on Saturday. It also crashed for the first time on
Saturday, approximately 11 seconds into the flight, when I overcorrected a yaw input
at about 8 feet and hit the concrete. One arm shattered — which is what carbon fiber
arms are supposed to do, preferentially, to protect the motors — and a standoff broke.
Twenty-minute repair. It flew again. That design decision to use replaceable arms is
not an accident.

The Betaflight setup was better than I expected and worse than the YouTube builds
make it look. Motor direction, prop orientation, and the accelerometer calibration all
went smoothly. The PID tune is where I need more flights before I can form an opinion.
The default Betaflight tune for a 5-inch quad is not bad, but it oscillates on fast
direction changes in a way that either means the rates are too high for my skill level
or the D-term needs adjusting. Probably both.

What I got right: the antenna routing, which keeps the video transmitter signal clean;
the motor to ESC wire lengths, which are short enough to avoid resonance. What I got
wrong: the capacitor I installed across the battery leads is the wrong value — I
picked 470µF when the build calls for 1000µF, and I can see the voltage spikes in the
Betaflight telemetry when I punch the throttle. That's a five-dollar fix with a
desoldering pump and the right cap.
