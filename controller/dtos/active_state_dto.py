import uuid
from dataclasses import dataclass

@dataclass(frozen = True, kw_only = True)
class ActiveStateDto:
    device_id : uuid
    led_strip_id : uuid
    scene_id : uuid