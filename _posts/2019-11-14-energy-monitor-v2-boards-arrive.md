---
layout: post
title: "Energy monitor v2 boards arrive and the noise is gone"
date: 2019-11-14
author: marcetux
tags: [electronics, pcb, energy, homelab, hardware]
---
The v2 boards arrived from the fab and I assembled one over the weekend. The low-pass filter and TVS diodes fit the layout cleanly; routing around the op-amp was tighter than I'd like but the DRC passed and the board looks right under a loupe. Soldering the SO-8 TLV2372 by hand with a fine-tip iron is the kind of work that rewards patience — three attempts on the pads before I was happy with the reflow.

The noise improvement is as significant as the simulation predicted. At low loads — the coffee maker's standby, a phone charger, a single LED bulb — the readings are now stable to within a watt. The v1 board would flutter ±8–12W at those loads because the ADC was sampling the noise floor as signal. With the filter, the waveform the ADC sees is clean and the RMS calculation converges to a stable value. Measuring 3W standby loads is now reasonable.

The practical finding after a week of data: the apartment's standby power — everything plugged in but idle — is about 47W. The Pi cluster and NAS account for 60W when active. The HVAC is still the dominant load in absolute terms. The coffee maker is the biggest surprise standby load at about 12W when idle; it goes on a smart plug now. This is exactly the kind of finding that requires both the hardware to be accurate enough to detect and enough data to see the pattern. V2 delivered on both.
