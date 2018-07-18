---
layout: post
title: "ML.NET preview and what it can and cannot do yet"
date: 2018-07-17
author: marcetux
tags: [mlnet, dotnet, machinelearning, csharp, backend]
---
Microsoft shipped the ML.NET 0.3 preview this month and I spent a Saturday with it
to understand where it sits. The pitch is ML pipelines authored in C#, running
on-process without Python or a model-serving sidecar. For a .NET shop that wants
to add a recommendation or anomaly-detection feature without standing up a Flask
service and managing the interop boundary, that's a real value proposition.

The API is a pipeline: `MLContext` creates a data loading step, a set of transforms,
and a trainer, chained into a model that you fit on training data and then save.
Prediction is just calling `model.Transform()` in your application code. The mental
model is familiar if you've read any scikit-learn tutorial. The thing that works well
today is binary classification and regression on tabular data — the travel industry
use case I was thinking through, predicting whether a booking is likely to cancel,
is well inside what the preview handles.

What it can't do yet: the model selection surface is thin, hyperparameter tuning is
manual, and there's no native neural network trainer — it's gradient boosted trees
and linear models. The story for images or text is thin. But for adding a simple
prediction to an existing .NET service without Python process management, it's already
useful. The API is unstable enough that I wouldn't ship it in a customer-facing service
today; I would experiment with it in a background job where I can swap the model
without a deployment ceremony.
