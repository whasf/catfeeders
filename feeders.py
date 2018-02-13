import RPi.GPIO as GPIO
import time
from array import array
#pins:
#  17 - Lucky
#  27 - Oreo
#  22 - Girl

pins = [27,22]

GPIO.setmode(GPIO.BCM)

GPIO.setup(17, GPIO.OUT)
GPIO.output(17, GPIO.LOW)
time.sleep(2)
GPIO.output(17, GPIO.HIGH)
for pin in pins:
        GPIO.setup(pin, GPIO.OUT)
        GPIO.output(pin,GPIO.LOW)
        time.sleep(1)
        GPIO.output(pin,GPIO.HIGH)
GPIO.cleanup()                       