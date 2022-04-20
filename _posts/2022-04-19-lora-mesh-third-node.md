---
layout: post
title: "Adding a third LoRa node to the mesh"
date: 2022-04-19
author: marcetux
tags: [electronics, lora, mesh, embedded, iot]
---
The two-node LoRa setup from January was really just a long-range sensor link, not a
mesh. Adding a third node this month forced me to actually implement the mesh routing
logic I'd been putting off — specifically, how to forward a message that arrives at an
intermediate node on its way to the gateway.

The approach I landed on is a simple flooding protocol with a hop count and a seen-
message cache. Each message gets a unique ID generated at the source. When a node
receives a message with a hop count above zero and an ID it hasn't seen before, it
adds the ID to its cache, decrements the hop count, and rebroadcasts. A node that's
already seen a message ID ignores retransmissions, preventing the flood from cycling
forever. It's the simplest mesh routing that works, and for three to five nodes in a
house it's entirely adequate.

The real win is coverage. Node one is in the detached garage; node two is in the main
house; node three is in a back room that node one couldn't reach reliably. With node
two as the relay, the garage sensor data arrives at the gateway through the house node
without the two endpoints needing line of sight to each other. LoRa's range makes the
relay unnecessary in open fields; inside a concrete building with thick walls, it
earns its keep. The mesh is topology-aware even though the routing algorithm is blissfully
unaware of topology.
