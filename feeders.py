import paho.mqtt.client as mqtt
import RPi.GPIO as GPIO
import time
from array import array
mClient = mqtt.Client("feeders_mqtt1")
mClient.username_pw_set(username="feeders",password="feeder")
mClient.connect("ha.thesmythes.org", 1883)
mClient.publish("ha/feeders/status","ON")
#pins:
#  17 - Lucky
#  27 - Oreo
#  22 - Girl
#  23 - Feeder 4
#  05 - Feeder 5
#  06 - Feeder 6
#  13 - Feeder 7
#  Added 7/4/2025 for all the cats we now have!
#  12 - Feeder 8
#  16 - Feeder 9

pins = [27,22, 23]
pinsnew = [5, 6, 13, 12, 16]
GPIO.setmode(GPIO.BCM)

GPIO.setup(17, GPIO.OUT)
GPIO.output(17, GPIO.LOW)
time.sleep(1.4)
GPIO.output(17, GPIO.HIGH)
for pin in pins:
        GPIO.setup(pin, GPIO.OUT)
        GPIO.output(pin,GPIO.LOW)
        time.sleep(0.8)
        GPIO.output(pin,GPIO.HIGH)
for pin in pinsnew:
        GPIO.setup(pin, GPIO.OUT)
        GPIO.output(pin, GPIO.LOW)
        time.sleep(5.4)
        GPIO.output(pin, GPIO.HIGH)
GPIO.cleanup()
mClient.publish("ha/feeders/status","OFF")
