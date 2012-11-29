---
layout: post
title: "A custom media formatter for Web API"
date: 2012-11-01
author: marcetux
tags: [csharp, webapi, rest, csv]
---
Back to content negotiation. JSON and XML come for free in Web API, but our
customers keep asking for CSV they can drop into a spreadsheet. The clean way isn't
a special `/report.csv` action — it's a `MediaTypeFormatter` that activates on
`Accept: text/csv`.

A formatter has one real job: given an object and a stream, write the bytes.
Subclass `MediaTypeFormatter`, declare the `text/csv` media type, say which types
you can serialize (any `IEnumerable<T>`), and implement `WriteToStreamAsync` to
reflect over the properties and emit rows. Register it once and *every* collection
endpoint can now hand back CSV with no per-controller code.

That's the part I like: one formatter, negotiated by the header, and the whole API
gains a format. The representation stays a transport concern, not a URL.

*Update: pulled the formatter out into a runnable file —
`examples/2012/webapi/CsvMediaFormatter.cs`. Register it with
`config.Formatters.Add(new CsvMediaFormatter())` and every collection endpoint
speaks CSV.*
