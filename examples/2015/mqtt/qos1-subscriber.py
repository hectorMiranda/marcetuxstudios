#!/usr/bin/env python3
# MQTT subscriber at QoS 1 — guaranteed at-least-once delivery.
# Writes to InfluxDB; upsert-by-timestamp handles any duplicates.
import paho.mqtt.client as mqtt
from influxdb import InfluxDBClient
from datetime import datetime, timezone

BROKER    = '192.168.1.5'
DB_NAME   = 'home'
influx    = InfluxDBClient(host='localhost', database=DB_NAME)

def on_message(client, userdata, msg):
    topic_parts = msg.topic.split('/')   # home/room/sensor
    if len(topic_parts) != 3:
        return
    _, room, sensor = topic_parts
    try:
        value = float(msg.payload.decode())
    except ValueError:
        return

    point = {
        'measurement': sensor,
        'tags':        {'room': room},
        'time':        datetime.now(timezone.utc).isoformat(),
        'fields':      {'value': value}
    }
    influx.write_points([point])

client = mqtt.Client()
client.on_message = on_message
client.connect(BROKER, 1883)
# QoS 1: guaranteed delivery, duplicates handled at write layer
client.subscribe('home/#', qos=1)
client.loop_forever()
