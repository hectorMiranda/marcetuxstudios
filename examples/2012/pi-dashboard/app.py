#!/usr/bin/env python
"""A tiny Flask dashboard for the Pi: serves the latest sensor readings on the LAN.

Reads the CSV that logger.py appends to and shows the most recent rows.

    pip install flask
    python app.py            # http://<pi-ip>:5000/
"""
import csv
from flask import Flask, render_template_string

app = Flask(__name__)
READINGS = "readings.csv"

PAGE = """
<!doctype html><title>bench sensors</title>
<h1>Latest readings</h1>
<table border=1 cellpadding=6>
  <tr><th>when</th><th>temp &deg;C</th><th>humidity %</th></tr>
  {% for when, temp, hum in rows %}
  <tr><td>{{ when }}</td><td>{{ temp }}</td><td>{{ hum }}</td></tr>
  {% endfor %}
</table>
<p>{{ rows|length }} rows shown.</p>
"""


def tail(path, n=20):
    try:
        with open(path) as f:
            rows = list(csv.reader(f))
        return rows[-n:][::-1]
    except IOError:
        return []


@app.route("/")
def index():
    return render_template_string(PAGE, rows=tail(READINGS))


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
