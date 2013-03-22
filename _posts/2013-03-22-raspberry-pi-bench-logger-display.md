---
layout: post
title: "Wiring the Raspberry Pi to the bench logger board"
date: 2013-03-22
author: marcetux
tags: [electronics, raspberry-pi, arduino, hardware, python]
---
The bodge-wired bench logger board reads temperature and voltage correctly, so the
obvious next step was giving it somewhere to send its data. I wired the board's UART
output to the Raspberry Pi's serial pins and wrote a thirty-line Python script that
reads lines off `/dev/ttyAMA0`, timestamps them, and appends to a CSV. Nothing clever
— but it works and it's running.

The USB-serial problem (the mirrored FT232 footprint from two weeks ago) is a non-issue
in this setup because the Pi speaks UART natively on its GPIO header, pin 8 TX, pin 10
RX. I just needed a voltage divider to drop the Arduino's 5V TX signal to 3.3V before
it hit the Pi's RX pin, or a logic-level converter if I want it cleaner. Used a
resistor divider because I had the parts. The Pi's TX to the board's RX goes direct
because the ATmega's RX pin tolerates 5V input even when running at 3.3V — checked
the datasheet before wiring, which is the lesson from the footprint mistake.

The CSV fills up at one sample per second: timestamp, voltage rail, temperature reading.
Crude, but now I can pull it into a spreadsheet and actually see what's happening on
the bench over a session. V1.1 board will have the USB-serial fixed; for now the
Pi-as-data-mule is the right workaround.
