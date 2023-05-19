---
layout: post
title: "TinyML experiments on an ESP32"
date: 2023-05-18
author: marcetux
tags: [tinyml, embedded, esp32, electronics, ai]
---
The "local model" idea has a more extreme edge than running LLaMA on a Pi: you can
run small inference models on microcontrollers with tens of kilobytes of RAM.
TensorFlow Lite for Microcontrollers lets you convert a TensorFlow model to a tiny
flatbuffer format and run it on an ESP32. I spent a weekend trying to classify
audio gestures — clap versus snap — on an ESP32-S3, which has the PSRAM to make it
feasible.

The pipeline: record a 100ms audio clip with the MEMS microphone on the dev board,
compute an MFCC (Mel-Frequency Cepstral Coefficients) feature vector in firmware, run
the TFLM interpreter on a model I trained in Python on labeled clips from my desk.
The model itself is a tiny 2-layer convolutional net; the TFLM runtime plus model fits
in 256KB of flash. Inference takes about 15ms on the ESP32-S3 at 240MHz — fast enough
to run continuously in a real-time loop.

This is obviously not LLM inference. The model has three output classes and no
language understanding. But the pattern interests me: train on a developer machine,
quantize and export, deploy to hardware with zero network dependency. If the
"intelligence" can run at the edge, close to the sensor, without a cloud round-trip,
the applications are different. A doorbell that recognizes your knock, a machine
that detects its own anomaly sounds. Worth understanding how far down the stack this
can go.
