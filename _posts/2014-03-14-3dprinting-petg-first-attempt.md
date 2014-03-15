---
layout: post
title: "First attempt at PETG, and why it's harder than PLA"
date: 2014-03-14
author: marcetux
tags: [3dprinting, hardware, maker, materials]
---
Someone at the makerspace mentioned that PETG has better layer adhesion and temperature
resistance than PLA, which sounded useful for the parts I print that end up near a
heat source. I ordered a roll and spent a frustrating weekend learning that "better
properties" comes with harder printing.

PETG prints at a higher temperature — around 230–240°C versus PLA's 200°C — and it
absorbs moisture from the air faster, which causes bubbling and weak layers if the
filament isn't dry. The Printrbot's extruder runs fine at that temperature but my
first prints came out stringy and rough. The stringing — fine threads left between
moves — is a PETG signature and takes retraction tuning to fix. Retraction is the amount
the extruder pulls back before a travel move; too little and it strings, too much and it
grinds soft filament.

Two days of tuning later, with the filament dried in the oven at 65°C for three hours
before printing, I got clean results. Parts come out slightly translucent with a nice
finish, and the layer bonds are noticeably stronger when I try to flex a sample.
The process is harder than PLA but the output is better for anything structural.
Slicer profiles don't transfer between materials — treat every new filament as a new
machine to calibrate.
