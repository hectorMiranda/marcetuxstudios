---
layout: post
title: "KiCad footprint libraries and why you should make your own"
date: 2018-03-14
author: marcetux
tags: [electronics, kicad, pcb, hardware, hobby]
---
The pulse-oximeter board is back on track after I stopped trusting the community
footprint library and drew the MAX30102 pad by hand from the datasheet. That's the
lesson I've had to relearn twice now: community libraries are a starting point, not a
source of truth. The discrepancy that killed my last board was 0.1 mm of pad length
that made the reflow joint unreliable under any flex.

KiCad's footprint editor has gotten meaningfully better since I migrated from Eagle.
The courtyard layer is now properly enforced in the DRC, which catches the case where
I pack parts too close and they physically overlap — something that used to require a
visual sanity pass at the Gerber viewer stage. The 3D viewer helps too: I caught a
capacitor standing on its end because the footprint assumed horizontal mount and I'd
specified vertical in the BOM.

My workflow now is: pull the community footprint as a starting sketch, open the
datasheet, verify every dimension against the land pattern recommendation in the
package spec, and save it to a local project library rather than modifying the global
one. The global one will update and break you. Your local library is yours. An extra
fifteen minutes per new part to do this right; an unpleasant afternoon at the board
level if you don't.
