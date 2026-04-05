from dataclasses import dataclass

@dataclass(kw_only=True)
class LedStateDto:
    index : int
    red : int
    green : int
    blue : int
    white : int