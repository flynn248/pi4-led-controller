from dataclasses import dataclass

@dataclass(frozen=True, kw_only=True)
class LedStripDetailsDto:
    led_strip_type_id : int
    gpio_pin : int
    led_count : int
    frequency : int
    dma_channel : int
    brightness : int
    invert : bool
    voltage : int
    max_current_ma : int
