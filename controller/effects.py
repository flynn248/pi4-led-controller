import database as db
import rpi_ws281x as rpi
import time
import uuid
import json

class Effects:
    @staticmethod
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

    # TODO: Expand by supporting params instead to save db storage and memory. Allow "steps" to step the color?
    @staticmethod
    def play_static_effect(strip: rpi.PixelStrip, effect_id: uuid):
        num_pixels = strip.numPixels()

        leds = db.Database.query_effect_led_state(effect_id)

        for led in leds:
            if led.index >= num_pixels:
                raise Exception("Led index is out of range")
            
            strip.setPixelColorRGB(led.index, led.red, led.green, led.blue, led.white)

        strip.show()

    @staticmethod
    def play_breathe(strip: rpi.PixelStrip, effect_id: uuid, parameter_json: str):
        num_pixels = strip.numPixels()
        max_brightness = strip.getBrightness()
        # data = json.loads(parameter_json)

        # Params
        step_frequency_ms = int(parameter_json["step_frequency_ms"])
        cycle_steps = int(parameter_json["cycle_steps"])
        min_brightness = int(parameter_json["min_brightness"])
        iterations = int(parameter_json["iterations"])
        iteration_colors = list(parameter_json["iteration_colors"])

        if min_brightness > max_brightness:
            print("play_breathe - min brightness larger than max. Skipped effect")
            return

        half_cycle_steps = max(int(cycle_steps / 2), 1)
        step_brightness_increment = max(int((max_brightness - min_brightness) / cycle_steps), 1)

        def iterate_color(index: int, iteration_colors: list):
            if len(iteration_colors) <= 0:
                return
            
            index = index % len(iteration_colors)

            color = iteration_colors[index]

            # Params
            red = int(color["red"])
            green = int(color["green"])
            blue = int(color["blue"])
            white = int(color["white"])

            for i in range(num_pixels):
                strip.setPixelColorRGB(i, red, green, blue, white)
            strip.show()    

        # Initialize color and brightness
        iterate_color(0, iteration_colors)
        strip.setBrightness(min_brightness)

        # Breathe
        for i in range(iterations):
            if len(iteration_colors) > 1 and i > 0:
                iterate_color(i, iteration_colors)

            # Raise brightness            
            step_count = 0
            while step_count <= half_cycle_steps:
                current_brightness = strip.getBrightness()
                new_brightness = current_brightness + step_brightness_increment

                if new_brightness > max_brightness:
                    new_brightness = max_brightness
                    step_count = half_cycle_steps # At max brightness, max out step_count

                strip.setBrightness(new_brightness)
                strip.show()

                time.sleep(step_frequency_ms / 1000)

                step_count += 1

            # Lower brightness
            step_count = 0
            while step_count <= half_cycle_steps:
                current_brightness = strip.getBrightness()

                new_brightness = current_brightness - step_brightness_increment

                if new_brightness < min_brightness:
                    new_brightness = min_brightness
                    step_count = half_cycle_steps # At min brightness, max out step_count

                strip.setBrightness(new_brightness)
                strip.show()

                time.sleep(step_frequency_ms / 1000)

                step_count += 1

    # def play_custom_breathe(strip: rpi.PixelStrip)


        