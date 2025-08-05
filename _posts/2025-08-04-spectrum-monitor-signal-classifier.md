---
layout: post
title: "Writing a signal classifier for the spectrum monitor"
date: 2025-08-04
author: marcetux
tags: [python, signal-processing, homelab, hardware, ml]
---
The RF front-end has been collecting baseband data for a month with no analysis
beyond "there's RF here." August is the analysis project. The goal: a classifier that
labels signal types from short IQ captures — distinguishing narrowband analog carriers,
wideband digital signals, and noise floor — so the logging system can record what's on
the air without me manually reviewing FFT plots.

The features that separate the signal types are straightforward in the frequency domain:
spectral flatness distinguishes noise from signal, instantaneous bandwidth distinguishes
narrowband from wideband, and the cyclostationary properties of modulated signals are
different from continuous carriers. Computing these from a short capture doesn't require
a deep model — a gradient-boosted classifier over a small feature set gets the job done
with less overhead than anything neural.

I generated the training data the obvious way: captured known signals (FM broadcast
stations, a 433MHz remote I control), captured noise floor with the antenna disconnected,
and labeled them by hand. Fifty labeled captures was enough to train a classifier with
over 90% accuracy on held-out samples. The lesson that applies beyond signal processing:
a small well-labeled dataset and a simple model usually beats a large poorly-labeled
dataset and a complex one. Know what you have before reaching for complexity.
