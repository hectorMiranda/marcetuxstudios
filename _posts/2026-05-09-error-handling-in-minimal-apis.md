---
layout: post
title: "Error handling in minimal APIs that communicates intent"
date: 2026-05-09
author: marcetux
tags: [dotnet, minimal-api, error-handling, api, aspnet]
---
The default exception behavior in a .NET minimal API is a 500 with a generic body, which is the right thing to send to untrusted callers and the wrong thing to use as your entire error strategy. Between "everything's fine" and "something crashed," there's a lot of intentional error space — validation failures, business rule violations, not-found conditions, authorization denials — and communicating those distinctions to callers via problem details is part of what makes an API legible rather than opaque.

The setup I've standardized on uses a thin result type that carries either a value or a structured error, and endpoint handlers translate that result into a `Results.Problem(...)` or the appropriate success response. The structured error has a code (for machine parsing), a message (for humans reading logs), and optionally a set of field-level validation errors for the 422 case. The key design choice is that handlers don't throw exceptions for expected failure conditions; exceptions are for genuine surprises. A user not found is not a surprise; it's a case the handler should return deliberately.

The advantage of this over exception-based error flows is that the error paths are explicit in the code. You can read a handler and see every condition it handles and what it returns for each one, without having to trace exception handlers at multiple levels above. The middleware still has an exception handler for the genuine surprises — unhandled exceptions become a 500 with correlation ID and no stack trace to the client — but that's the backstop, not the strategy.
