---
layout: post
title: "The LoRa mesh network: a year in review"
date: 2022-12-19
author: marcetux
tags: [electronics, lora, mesh, embedded, homelab]
---
The LoRa mesh network I started building at the end of 2021 and extended through 2022
is now three nodes, a gateway, and a Grafana dashboard that I check the way I used to
check my phone — habitually, and mostly because it's there. The mesh reports temperature,
humidity, CO2, and power data from the garage and both rooms it couldn't reach without
the relay. The battery life on the sleep-cycle nodes is measured in weeks. It works,
quietly, all the time.

What I learned in the year of building it: the hardware problems (radio sensitivity,
antenna placement, interference) are solved by physics and patience — move the node,
try the antenna orientation, check the RSSI. The software problems (message deduplication,
sleep-wake coordination, the flooding protocol) are solved by keeping the protocol
deliberately simple and accepting that simplicity trades off against capability. A
flooding protocol with a hop counter is not routing; it's the minimum that works for
the number of nodes I have. It will stop being sufficient at around eight to ten nodes
in a complex topology, and that's fine because I don't have eight nodes and might not
ever need them.

The energy monitor is the most practically useful addition. Knowing the house's power
draw in real time, per circuit, with historical data in Grafana, is the kind of tool
that changes how you think about appliance replacement decisions. The old chest freezer
replacement I identified in February paid back in nine months at current electricity
prices. The data infrastructure cost less than the freezer itself. Measure things.
