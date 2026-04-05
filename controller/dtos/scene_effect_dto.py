import uuid
from dataclasses import dataclass

@dataclass(frozen=True, kw_only=True)
class SceneEffectDto:
    effect_id : uuid
    is_param_current : bool
    parameter_json : str
    name : str