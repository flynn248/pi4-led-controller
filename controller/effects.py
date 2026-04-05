import database as db
import rpi_ws281x as rpi
import time
import uuid

class Effects:
    def clear_strip(strip: rpi.PixelStrip, delay_ms: int = 50, with_style: bool = False):
        if with_style:
            for i in range(strip.numPixels()):
                strip.setPixelColorRGB(i, 0, 0, 0)
                time.sleep(delay_ms / 1000)
                strip.show()
        else:
            for i in range(strip.numPixels()):
                strip.setPixelColorRGB(i, 0, 0, 0)

            strip.show()

    @staticmethod
    def play_static_effect(strip: rpi.PixelStrip, effect_id: uuid):
        leds = db.Database.query_effect_led_state(effect_id)

        for led in leds:
            if led.index >= strip.numPixels():
                raise Exception("Led index is out of range")
            
            strip.setPixelColorRGB(led.index, led.red, led.green, led.blue, led.white)

        strip.show()