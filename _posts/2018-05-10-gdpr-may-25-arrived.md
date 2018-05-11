---
layout: post
title: "GDPR day arrives and what it was actually like"
date: 2018-05-10
author: marcetux
tags: [gdpr, compliance, privacy, architecture]
---
May 25th is two weeks away and the industry chatter at every conference and Slack
workspace has reached peak-noise. At CTM the work was more measured: the legal team
had done a DPIA, engineering went through the data flows for EU business traveler
records, and by early May the major gaps were already closed. The work was not as
dramatic as the coverage suggests, which is either a sign of good advance preparation
or the evidence that the fear was proportional.

The engineering changes that actually happened: consent preferences stored explicitly
on traveler profiles and carried through the booking flow; a data subject request
handling process in the admin portal (right of access and right to erasure); a
retention policy sweep that deleted records past their defined lifetime, which nobody
had enforced since the policy was written. The retention sweep was the one that
surfaced the most data, because "we keep records for seven years" turns out to be
aspirational if nothing actually deletes them after seven years.

The honest industry observation: GDPR forced the conversation that data governance
conversations had been failing to start for a decade. The law moved faster than
engineering culture would have on its own. That's a slightly uncomfortable thing to
admit, but the audit work is clearly better for having happened.
