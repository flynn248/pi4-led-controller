import os
import time
import uuid
import rpi_ws281x as rpi
import database as db
import subprocess as sub

from dotenv import load_dotenv
from dotenv import dotenv_values
from dtos.scene_effect_dto import SceneEffectDto
from effects import Effects

def get_serial_number() -> str:
    cmd = ["cat", "/sys/firmware/devicetree/base/serial-number"]
    result = sub.run(cmd, capture_output=True, text=True)

    return result.stdout.strip('\0')
    
def initialize_strip(led_strip_id: uuid):
    deets = db.Database.query_led_strip_details(led_strip_id)

    if deets is None:
        return

    if deets.led_strip_type_id == 1:
        strip_type = rpi.ws.SK6812_STRIP_GRBW
    else:
        raise Exception("Strip type not implemented")
    

    # strip = rpi.PixelStrip(num=deets.led_count, pin=deets.gpio_pin, freq_hz=deets.frequency, dma=deets.dma_channel
    #                               , invert=deets.invert, brightness=deets.brightness, strip_type=strip_type)
    
    strip = rpi.PixelStrip(num=deets.led_count, pin=deets.gpio_pin, brightness=deets.brightness, strip_type=strip_type)
    
    strip.begin()

    return strip

def play_effect(strip: rpi.PixelStrip, scene_effect: SceneEffectDto):
    if scene_effect.name == "static color":        
        Effects.play_static_effect(strip, scene_effect.effect_id)
    else:
        raise Exception("Effect not implemented")

def main():
    serialNum = get_serial_number()
    active = db.Database.query_active_state(serialNum)

    a = active[0]

    strip = initialize_strip(a.led_strip_id)

    if strip is None:     
        return
    
    try:        
        scene_effect = db.Database.query_scene_effect(a.scene_id, a.led_strip_id)

        print(scene_effect)
        
        play_effect(strip, scene_effect)
        time.sleep(8)
    finally:
        Effects.clear_strip(strip, with_style=True)
        strip._cleanup()

if __name__ == "__main__":
    load_dotenv()
    db.Database.configure()
    main()        
