### Creando un paquete de contenido
El método para crear paquetes de contenido es como el de cualquier framework: necesitas un manifest.json y un content.json.
[Aquí se encuentra una plantilla del content.json que necesitarás.](https://github.com/misty-spring/SpousesIsland/blob/main/content_template.json).

## Contents

* [Explicación](#explicacion)

  * [Datos](#datos)

  * [Diálogo](#dialogo)

  * [Rutina](#rutina)

* [Traduciendo el mod](#traducciones)

## Explicación
Spouses' Island parcha los archivos del juego de dos maneras:
- Los diálogos siempre se agregan, sin importar el día.
- Las rutinas ("Characters/schedules") sólo se editan en "días de visita" (días donde los personajes irán a la isla).

En los días de visita, la(s) pareja(s) del personaje caminarán hacia la tienda de Willy (a las 6:20). Al llegar, entran al cuarto del fondo (de la misma manera que los NPC hacen cuando visitan la playa). Luego de esto, camina hacia la casa de la isla.
Desde ese punto, todo lo que el personaje haga es configurable por el modder (e.j., a qué hora y a dónde ir). Los personajes vuelven a la casa a las 21:50 y proceden a dormir en la cama del jugador.

### Datos
Spouses' Island's usa los siguientes datos:

nombre | descripción
-----|------------
Name | El nombre del personaje que se va a usar/modificar.
ArrivalPosition | La posición donde el personaje se pondrá cuando llegue a la isla, usa tres valores (coordenada x, coordenada y, a dónde mirar). Para más información, puedes ver [este artículo de la wiki](https://stardewvalleywiki.com/Modding:Schedule_data#Schedule_points).
ArrivalDialogue | El diálogo que dirá el personaje al terminar de caminar.
Location Name | El nombre del mapa a donde el personaje irá. __Valores permitidos:__ Cualquier mapa de la isla.
Location Time | La hora a la que el personaje _comenzará_ a moverse. __Valores permitidos:__ Entre las 10:30 y 21:40.
Location Position | La posición en la que el personaje se pondrá al llegar (similar a `ArrivalPosition`, pero para un lugar que tú indiques). __Valores permitidos:__ Cualquiera, mientras esté dentro del rango del mapa (ver [este link](https://stardewvalleywiki.com/Modding:Maps#Tile_coordinates) para más información.)
Location Dialogue | El diálogo que dirá el personaje en ese lugar.
Hay tres listas llamadas `Location<número>`: de esas, **sólo la tercera es opcional** (de lo contrario, el formato sería muy complicado.)

### Diálogo
El diálogo sigue el mismo formato que los diálogos del juego. Para más información, puedes ver [Modding Dialogue](https://stardewvalleywiki.com/Modding:Dialogue#Format) en la wiki.
El diálogo es agregado cuando el juego solicita el archivo (e.j. si el juego quiere cargar `"Characters/Dialogue/Krobus"`, se editará y luego se le pasa al juego).

Por ejemplo:
```json
"ArrivalDialogue" : "Do you think we can explore this volcano?$0#$b#Willy said we shouldn't get close..$2#$b#But I still brought my sword.$1",
```
No necesitas agregar una clave de diálogo; el mod hará eso internamente.

### Schedule
The schedule follows the same convention as the game's schedules.
To handle that for you, it uses the following information: 
- Name (of the map your spouse will go to)
- Time (which your spouse will start moving at)
- Position (where they'll stand once they arrive)
For more information on how schedules work, [see here](https://stardewvalleywiki.com/Modding:Schedule_data#Schedule_points).

## Translating your mod
Por el momento, Spouses' Island no tiene soporte para traducciones. Esto será agregado en el futuro.
Sin embargo, si (por alguna razón) no puedes esperar, puedes cambiar esto en tu paquete de contenido:
`"Spousename" : "<Nombre>.<extensión>",`
Donde <extensión> es la extensión del idioma.
Por ejemplo:
 ```json
"Spousename" : "Krobus.es-ES",
```
Si quieres ver todas las extensiones posibles, ve [aquí](https://github.com/misty-spring/SpousesIsland/blob/main/languagecodes.md) (el enlace está en inglés, pero las extensiones son las mismas sin importar el idioma que use el usuario).
 
 **Ten en cuenta que se agregará soporte para traducciones pronto, por lo cual recomendaría esperar**!
