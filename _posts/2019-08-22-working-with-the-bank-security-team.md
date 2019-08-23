---
layout: post
title: "Working with the bank security team instead of around them"
date: 2019-08-22
author: marcetux
tags: [banking, security, process, teams, architecture]
---
Every engineer I've known has a story about the security team blocking something. The story usually ends with the engineer finding a workaround or waiting months for a review that should have taken a week. I've had both experiences. At City National I've been trying a different approach, and it's working better than I expected.

The change is involving security early — not as a gate at the end but as a stakeholder in the design. When we scoped the mTLS project in January, I invited the security architect into the design review before we wrote a line of infrastructure code. He had opinions about the certificate lifetime, the CA trust hierarchy, and the audit log for certificate issuance. Some of those opinions shaped the design in ways I wouldn't have anticipated. By the time the implementation was complete, security already understood it and had already blessed the approach. The final review was a formality.

The pattern that's working: a one-pager threat model before significant infrastructure work. Not a formal threat model with attack trees and STRIDE categories — just a page that says "here is what we're building, here is what we're trusting, here is how we're limiting the blast radius if it goes wrong." Security gets to respond to that before the architecture is baked. They spot the scenarios the engineers didn't think of, the engineers understand the constraints the security team actually cares about, and the review at the end is two colleagues confirming what they already agreed on. Slower up front; much faster overall.
