---
layout: post
title: "LangChain chains vs agents, the practical difference"
date: 2023-04-13
author: marcetux
tags: [llm, langchain, ai, agents]
---
Two months into LangChain experiments and the distinction that keeps tripping people
up in the tutorials is chain vs. agent. Both connect language model calls, but the
control flow is different enough that picking the wrong one makes the code hard to
reason about.

A chain is a fixed pipeline: step A runs, its output goes to step B, that goes to C.
You compose the steps at code-write time and the execution path doesn't change based
on what the model says. For RAG — retrieve, stuff context, generate — a chain is the
right tool. The path is always the same; only the data varies. A RetrievalQA chain is
correct here and easy to debug because you can log every step.

An agent is different: the model decides at runtime which tool to call next, based on
the output of the previous step. You hand it a set of tools and a goal; it figures out
the sequence. That's powerful for problems where you don't know the path in advance
— "answer this question using search and calculator and code execution." The cost is
that agents are harder to predict and test: the same input can take different paths
depending on what the model decides. I've started using a heuristic: if I can draw the
flowchart before I write the code, it's a chain. If the flowchart depends on the model's
outputs to exist, it might be an agent.
