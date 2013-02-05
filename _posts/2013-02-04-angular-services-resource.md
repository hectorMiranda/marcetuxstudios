---
layout: post
title: "Angular services and $resource"
date: 2013-02-04
author: marcetux
tags: [javascript, angular, spa, rest, frontend]
---
Last month I decided new dashboards go Angular. This month is the going-deeper part,
and the first thing I got wrong was scattering `$http` calls across controllers. The
controller filled up with URL strings and promise plumbing, which is exactly the mess
a framework is supposed to spare you.

The fix is to push every server call into a **service**. A controller should ask for
*data*, not know how the data is fetched. Once the `$http` calls live behind a
service, the controller shrinks to "load this, bind it, react to clicks," and the
service is the one place that knows your API's shape. When the versioning change from
January lands, I edit one file.

For plain REST resources, `$resource` goes further — you describe the endpoint once
and get `get`, `query`, `save`, `delete` for free, no hand-written `$http` per verb.
It's a little magical and not right for every endpoint, but for the customer and
report resources that are textbook CRUD it deletes a pile of boilerplate. The service
I extracted is in `examples/`.
