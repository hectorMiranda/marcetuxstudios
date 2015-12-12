#!/usr/bin/env python3
# Poll the EC2 instance metadata endpoint for Spot termination notices.
# On notice: stop accepting new jobs, drain current job, exit cleanly.
import requests
import time
import signal
import sys

METADATA_URL = 'http://169.254.169.254/latest/meta-data/spot/termination-time'
POLL_INTERVAL = 30   # seconds between checks

terminating = False

def check_termination():
    """Returns True if AWS has issued a Spot termination notice."""
    try:
        r = requests.get(METADATA_URL, timeout=2)
        return r.status_code == 200
    except requests.RequestException:
        return False

def graceful_shutdown(signum, frame):
    global terminating
    terminating = True

signal.signal(signal.SIGTERM, graceful_shutdown)

def should_accept_job():
    """Worker calls this before pulling from SQS."""
    return not terminating

def run_termination_monitor():
    while True:
        if check_termination():
            print('Spot termination notice received — stopping job acceptance')
            global terminating
            terminating = True
            # Give current job time to finish or re-queue (max 2 min before termination)
            time.sleep(90)
            sys.exit(0)
        time.sleep(POLL_INTERVAL)

if __name__ == '__main__':
    run_termination_monitor()
