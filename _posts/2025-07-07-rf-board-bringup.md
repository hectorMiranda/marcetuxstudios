---
layout: post
title: "Bringing up the RF front-end board"
date: 2025-07-07
author: marcetux
tags: [electronics, rf, altium, hardware, homelab]
---
The RF boards arrived last week. I did the bring-up procedure I planned in June: power
supply check, passive components only, spectrum analyzer on the input before touching
the active devices. The analog ground and RF ground are isolated properly — no
unexpected coupling on the spectrum analyzer when I toggle the digital section. Clean
enough to proceed.

Soldered the LNA and the bandpass filter Sunday. First signal sweep: the filter's 3dB
passband is about 15MHz wide centered 8MHz off from my design target. That's within the
trimming range; I adjusted the shunt capacitor value and the second sweep was inside
specification. This is exactly the iterative process that controlled-impedance routing
and a good stackup make possible — the board is close enough to spec that component
selection can close the gap, rather than the board being fundamentally broken.

The spectrum monitor now has a real front-end feeding the SDR receiver. Wide-band
reception from the roof antenna, LNA gain for the weak signals, filtered to the band
I care about, into the SDR. The software side — signal classification and anomaly
logging — is the next project. The hardware is finally not the bottleneck.
