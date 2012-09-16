#!/usr/bin/env python
"""Read 'temp,humidity' lines from the Arduino and append them, timestamped.

Runs on the Raspberry Pi. The Arduino stays dumb and fast; the Pi remembers.

    python logger.py /dev/ttyACM0 readings.csv
"""
import sys
import time
import serial  # pyserial


def main():
    port = sys.argv[1] if len(sys.argv) > 1 else "/dev/ttyACM0"
    out = sys.argv[2] if len(sys.argv) > 2 else "readings.csv"
    ser = serial.Serial(port, 9600, timeout=2)
    with open(out, "a") as f:
        while True:
            line = ser.readline().strip()
            if not line or line == "ERR":
                continue
            stamp = time.strftime("%Y-%m-%d %H:%M:%S")
            f.write("%s,%s\n" % (stamp, line))
            f.flush()
            print(stamp, line)


if __name__ == "__main__":
    main()
