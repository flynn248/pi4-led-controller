import uuid
import psycopg as pg
import os

from psycopg.rows import class_row
from dtos.active_state_dto import ActiveStateDto
from dtos.scene_effect_dto import SceneEffectDto
from dtos.led_strip_details_dto import LedStripDetailsDto
from dtos.led_state_dto import LedStateDto


class Database:

    conn_string: str = ""

    @classmethod
    def configure(cls):
        cls.conn_string = os.getenv("DATABASE_CONNECTION", 'test')

    @classmethod
    def query_active_state(cls, serial_number: str):
        with pg.connect(cls.conn_string) as conn:
            with conn.cursor(row_factory=class_row(ActiveStateDto)) as cur:
                active_state = cur.execute("""
                                            SELECT state.led_strip_id
                                                , state.device_id
                                                , state.scene_id
                                            FROM led.device as d
                                            JOIN led.active_state as state on d.id = state.device_id
                                            WHERE d.serial_number = (%s)
                                            """, (serial_number,)).fetchall()
            
                return active_state
    
    @classmethod
    def query_scene_effect(cls, scene_id: uuid, led_strip_id: uuid):
        with pg.connect(cls.conn_string) as conn:
            with conn.cursor(row_factory=class_row(SceneEffectDto)) as cur:
                effect = cur.execute("""
                                     SELECT e.id AS effect_id
                                         , e.parameter_json_schema_version = et.schema_version AS is_param_current
                                         , e.parameter_json 
                                         , et."name"
                                     FROM led.effect e 
                                     JOIN led.effect_type et ON e.effect_type_id = et.id
                                     WHERE e.scene_id = (%s)
                                        AND e.led_strip_id = (%s)
                                     """, (scene_id, led_strip_id)).fetchone()
                
                return effect
    
    @classmethod
    def query_effect_led_state(cls, effect_id: uuid):
        with pg.connect(cls.conn_string) as conn:
            with conn.cursor(row_factory=class_row(LedStateDto)) as cur:
                leds = cur.execute("""
                                SELECT ls.led_index as index 
                                    , ls.red
                                    , ls.green 
                                    , ls.blue 
                                    , ls.white 
                                FROM led.led_state ls 
                                WHERE ls.effect_id = (%s)
                                """, (effect_id,)).fetchall()
                
                return leds
        

    @classmethod
    def query_led_strip_details(cls, led_strip_id: uuid):
        with pg.connect(cls.conn_string) as conn:
            with conn.cursor(row_factory=class_row(LedStripDetailsDto)) as cur:
                deets = cur.execute("""
                                     SELECT led_strip_type_id
                                         , gpio_pin
                                         , led_count
                                         , frequency
                                         , dma_channel
                                         , brightness
                                         , invert
                                         , voltage
                                         , max_current_ma
                                     FROM led.led_strip
                                     WHERE id = (%s)
                                     """, (led_strip_id,)).fetchone()
                
                return deets
            
 


