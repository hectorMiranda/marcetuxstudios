---
layout: default
title: Home
---

# marcetux studios

Notes on software, systems, and the occasional soldering iron.

<ul>
{% for post in site.posts %}
  <li>
    <span>{{ post.date | date: "%Y-%m-%d" }}</span> —
    <a href="{{ post.url }}">{{ post.title }}</a>
  </li>
{% endfor %}
</ul>
