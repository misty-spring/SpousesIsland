using StardewModdingAPI.Events;
using System.Collections.Generic;

namespace SpousesIsland
{
    internal class SGIData
    {
        internal static void AppendFestivalData(AssetRequestedEventArgs e)
        {
            if (e.Name.StartsWith("Data/Festivals/spring", true, false))
            {
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/spring13"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string spring13_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out spring13_setup);
                            data["Set-Up_additionalCharacters"] = spring13_setup + "/Devan 25 69 up";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 25 69 up";
                        }

                        if (data.ContainsKey("MainEvent_additionalCharacters"))
                        {
                            string spring13_main;
                            data.TryGetValue("MainEvent_additionalCharacters", out spring13_main);
                            data["MainEvent_additionalCharacters"] = spring13_main + "/Devan 25 73 up";
                        }
                        else
                        {
                            data["MainEvent_additionalCharacters"] = "Devan 25 73 up";
                        }


                    });
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/spring24"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string spring24_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out spring24_setup);
                            data["Set-Up_additionalCharacters"] = spring24_setup + "/Devan 9 34 down";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 9 34 down";
                        }
                        if (data.ContainsKey("MainEvent_additionalCharacters"))
                        {
                            string spring24_main;
                            data.TryGetValue("MainEvent_additionalCharacters", out spring24_main);
                            data["MainEvent_additionalCharacters"] = spring24_main + "/Devan 8 30 up";
                        }
                        else
                        {
                            data["MainEvent_additionalCharacters"] = "Devan 8 30 up";
                        }
                    });
            }
            if(e.Name.StartsWith("Data/Festivals/summer", true, false))
            {
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/summer11"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string summer11_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out summer11_setup);
                            data["Set-Up_additionalCharacters"] = summer11_setup + "/Devan 13 9 down";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 13 9 down";
                        }
                        if (data.ContainsKey("MainEvent_additionalCharacters"))
                        {
                            string summer11_main;
                            data.TryGetValue("MainEvent_additionalCharacters", out summer11_main);
                            data["MainEvent_additionalCharacters"] = summer11_main + "/Devan 30 14 right";
                        }
                        else
                        {
                            data["MainEvent_additionalCharacters"] = "Devan 30 14 right";
                        }
                    });
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/summer28"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string summer28_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out summer28_setup);
                            data["Set-Up_additionalCharacters"] = summer28_setup + "/Devan 11 18 left";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 11 18 left";
                        }
                    });
            }
            if(e.Name.StartsWith("Data/Festivals/fall", true, false))
            {
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/fall16"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string fall16_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out fall16_setup);
                            data["Set-Up_additionalCharacters"] = fall16_setup + "/Devan 66 65 down";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 66 65 down";
                        }
                    });
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/fall27"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string fall27_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out fall27_setup);
                            data["Set-Up_additionalCharacters"] = fall27_setup + "/Devan 27 68 up";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 27 68 up";
                        }
                    });
            }
            if(e.Name.StartsWith("Data/Festivals/winter", true, false))
            {
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/winter8"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string winter8_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out winter8_setup);
                            data["Set-Up_additionalCharacters"] = winter8_setup + "/Devan 66 14 right";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 66 14 right";
                        }
                        if (data.ContainsKey("MainEvent_additionalCharacters"))
                        {
                            string winter8_main;
                            data.TryGetValue("MainEvent_additionalCharacters", out winter8_main);
                            data["MainEvent_additionalCharacters"] = winter8_main + "/Devan 69 27 down";
                        }
                        else
                        {
                            data["MainEvent_additionalCharacters"] = "Devan 69 27 down";
                        }
                    });
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/winter25"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string winter25_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out winter25_setup);
                            data["Set-Up_additionalCharacters"] = winter25_setup + "/Devan 23 74 up";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 23 74 up";
                        }
                    });
            }
        }
        
        internal static void DialoguesSpanish(AssetRequestedEventArgs e, ModConfig Config)
        {
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Abigail.es-ES") && Config.Allow_Abigail == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Ah, hola, @.$0#$b#Este clima es tan cálido..$0";
                    data["marriage_loc1"] = "Crees que podemos entrar a este volcán?$0#$b#Willy dijo quedebemos quedarnos en la playa...$2#$b#Pero aún traje mi espada.$1";
                    data["marriage_loc3"] = "¿Has luchado contra esas babas, @?";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Alex.es-ES") && Config.Allow_Alex == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Ey, @. Por fin estoy aquí.$1#$b#Ya quiero pasar todo el día contigo.$l";
                    data["marriage_loc1"] = "%Alex está haciendo ejercicio.";
                    data["marriage_loc3"] = "Phew, sienta bien un día en la playa con este calor.$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Elliott.es-ES") && Config.Allow_Elliott == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Oh, @! ¡Llegar aquí fue arduoso!$8#$b#Es un placer volver a verte.$1";
                    data["marriage_loc1"] = "%Elliott está leyendo.";
                    data["marriage_loc3"] = "Ah, la vista desde aquí es armoniosa$0.#$b#Puedo sentir una idea en camino...$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Emily.es-ES") && Config.Allow_Emily == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "¡Hola, @!#$b#¡Esta isla está llena de aves hermosas!";
                    data["marriage_loc1"] = "%Emily está concentrada pensando.";
                    data["marriage_loc3"] = "@, ¿Sientes eso?$0#$b#Estos cristales son muy poderosos.$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Haley.es-ES") && Config.Allow_Haley == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "@, ¿Podemos ver la isla hoy?$0#$b#Hay algo aquí que me hace sentir bien.";
                    data["marriage_loc1"] = "Hola, cariño.$0#$b#Amo el clima aquí.$1";
                    data["marriage_loc3"] = "%Haley está tomando fotos.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Harvey.es-ES") && Config.Allow_Harvey == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hola, cariño.$l#$b#Está bien tomar una cantidad moderada de luz solar a veces.$1";
                    data["marriage_loc1"] = "@, me quedaré adentro por un rato.$0#$b#Asegúrate de hidratarte en este clima, ¿está bien?$1";
                    data["marriage_loc3"] = "%Harvey está concentrado en un libro.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Krobus.es-ES") && Config.Allow_Krobus == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Buenos días, @.$0#$b#El clima es tan húmedo, me siento como en casa.$h#$e#Y el sol es tan fuerte...$8";
                    data["marriage_loc1"] = "@, dijiste que habían cuevas aquí... Iré a verlas más tarde.$0";
                    data["marriage_loc3"] = "¡Nunca he visto estos cristales!$0#$b#A mi gente le fascinaban.$0#$e#...$2#$b#...Es cierto. Puede que hayan otros como yo allí afuera.#$b#Gracias, @.$h";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Leah.es-ES") && Config.Allow_Leah == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Está tranquilo aquí.$0#$b#Y el clima es perfecto para un poco de vino.$1";
                    data["marriage_loc1"] = "La vista desde aquí es hermosa. Necesito dibujarla.$0";
                    data["marriage_loc3"] = "%Leah está dibujando.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Maru.es-ES") && Config.Allow_Maru == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Ey, qué tal?$0#$b#No puedo esperar a ver las estrellas de noche.$1";
                    data["marriage_loc1"] = "¡@! ¿Has visto esto?$9#$b#Al parecer, no ha habido actividad volcánica en un tiempo.";
                    data["marriage_loc3"] = "El Profesor Snail nos ha contado sobre las tradiciones en Isla Gengibre.$0#$b#Sus historias orales son asombrosas.$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Penny.es-ES") && Config.Allow_Penny == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hola @.$h#$b#Esta isla es hermosa...";
                    data["marriage_loc1"] = "Oh, estoy leyendo el libro que traje la última vez.$0#$b#Es muy interesante.$1";
                    data["marriage_loc3"] = "Cariño, el Profesor Snail tiene anécdotas maravillosas sobre la isla.$0#$b#¡No sabía que tenían tradiciones tan interesantes!$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Sam.es-ES") && Config.Allow_Sam == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hola, @.$0#$b#Es una isla grande, ¿no?$1";
                    data["marriage_loc1"] = "Oye... ¿Alguna vez has entrado?$0#$b#...$0#$b#¿En serio?$8";
                    data["marriage_loc3"] = "Ey, @. Me siento bien aquí.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Sebastian.es-ES") && Config.Allow_Sebastian == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hey, que bueno que estás aquí.$h#$b#¿Cómo has lidiado con este calor?";
                    data["marriage_loc1"] = "Hombre, se parece mucho a 'Cave Saga X'...";
                    data["marriage_loc3"] = "No soy mucho de interacción social...$0#$b#Este lugar me sienta bien.$h";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Shane.es-ES") && Config.Allow_Shane == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hola, @.$1#$b#Las plantas tropicales son hermosas aquí.$1";
                    data["marriage_loc1"] = "A Charlie le gusta este lugar.$8";
                    data["marriage_loc3"] = "Hmm...no está tan mal.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Claire.es-ES") && Config.Allow_Claire == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hola, cariño.$0#$b#¡Estoy lista para el clima de esta isla!$1";
                    data["marriage_loc1"] = "%Claire está leyendo.";
                    data["marriage_loc3"] = "Las aves de este lugar...$0#$b#Puedo escucharlas cantando.$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Lance.es-ES") && Config.Allow_Lance == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hola, mi cariño. Esta casa es tan cómoda como la de nuestra granja.";
                    data["marriage_loc1"] = "La caldera en este volcán contiene un poder inmenso.#$b#Sólo los magos más poderosos pueden usar los hechizos que requiere.";
                    data["marriage_loc3"] = "Las olas de esta playa...Me he acostumbrado a su sonido calmante.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Wizard.es-ES") && Config.Allow_Magnus == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "¡Buenos días, amor! Estoy encantado de verte.$1#$b#Este lugar es hermoso. Has hecho esta choza bastante acogedora.";
                    data["marriage_loc1"] = "Las criaturas de esta isla se han percatado de nuestra presencia. Están a la defensiva.#$b#Su poder arcano es inmenso...";
                    data["marriage_loc3"] = "Este lugar...sólo unos pocos pueden llevar a cabo los rituales de la forja.#$b#Incluso con tal experiencia, es imposible predecir sus resultados.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Olivia.es-ES") && Config.Allow_Olivia == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hola, cariño. ¿Cómo te ha tratado este clima?$0#$b#Deberíamos invitar a Victor uno de estos días.";
                    data["marriage_loc1"] = "Este lugar es único.$1#$b#Recordaste que quería una casa en esta isla...¿cierto, cariño?$4";
                    data["marriage_loc3"] = "El océano aquí es espectacular. Me encantaría pintarlo algún día.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Sophia.es-ES") && Config.Allow_Sophia == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "@, ¿Podemos quedarnos aquí hoy?#$b#Estoy disfrutando mucho este viaje.$0#$b#¿Si podemos? ¡Yay!$1";
                    data["marriage_loc1"] = "Oh, ¡@! Mira, hay unos peces aquí.$1#$b#Están comiendo algo.$1#$e#...H-hey, ten cuidado si vas al volcán de ahí arriba, ¿ok?$0";
                    data["marriage_loc3"] = "¿Esto? Es un manga que comencé a leer el otro día.$0#$b#¿Quieres verlo conmigo?$0";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Victor.es-ES") && Config.Allow_Victor == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hola, cariño. ¡Este lugar es hermoso!#$b#Comparado a Pueblo Pelícano, el clima es abrasador...";
                    data["marriage_loc1"] = "Este es el volcán que los duendes usaban como fuente de energía...#$b#Tal vez podríamos entrar un día.";
                    data["marriage_loc3"] = "¿Has visto los fósiles que hay aquí, @?#$b#Están muy bien preservados.";
                });
        }
        internal static void EventsSpanish(AssetRequestedEventArgs e)
        {
            if (e.Name.IsEquivalentTo("Data/Events/Railroad.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("11037500/F/f Devan 500/p Devan", "breezy/32 45/Devan 39 41 2 farmer 29 47 0/skippable/pause 500/move farmer 0 -6 0/move farmer 5 0 1 continue/viewport move 1 -1 3000/pause 500/emote Devan 16/pause 1000/quickQuestion @?#¿Qué estás haciendo?#¿Estás esperando a alguien?#(break)speak Devan \"Ah, ¿Esto?\"(break)speak Devan \"No, más bien a 'algo'.\"/speak Devan \"Estoy esperando que pase un tren.$0#$b#No pasan muy seguido, pero la espera vale la pena.$0\"/quickQuestion #Y ¿Has visto alguno?#¿Por qué lo haces?#Suena aburridor.(break)speak Devan \"Si, dos hasta ahora.$0#$b#El último iba con carbón, y me cayó uno cuando pasaba...$2#$b#Así que espero poder ver el siguiente con tranquilidad.\"\\friendship Devan 20(break)speak Devan \"Son máquinas increíbles. ¿Sabes? Pueden usar 4 tipos de energía: Tirado por caballos, a vapor, diesel y electricidad.$1#$b#Donde vivía antes, sólo podías llegar a través de un tren.$0\"(break)speak Devan \"¿Lo dice quien se la pasa en la granja?$5\"\\friendship Devan -50/pause 1000/emote Devan 40/speak Devan \"...De todas maneras, puedes quedarte si quieres.$0#$b#Pero te advierto, es probable que te caiga algo encima.$0#$e#Espero que tengas con qué cubrirte.$1\"/emote farmer 28/end");
                });
            if (e.Name.IsEquivalentTo("Data/Events/Forest.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("110371500/F/f Devan 1500/p Devan", "continue/34 25/Devan 34 25 2 farmer 33 25 2/skippable/pause 4000/speak Devan \"Hey, @.\"/pause 2000/speak Devan \"¿Recuerdas la pintura que le compré a Leah?#$b#Era del pueblo donde crecí.#$e#Solía trabajar en un colegio.$0#$b#Estábamos lejos de la ciudad, así que sobrevivíamos con la granja.$0#$b#...Pero las cosas cambiaron.$2\"/stopMusic/pause 500/playMusic desolate/speak Devan \"Con el tiempo, nuestra fuente de agua se secó. La gente comenzó a irse...vivir allí se volvió imposible.$2#$b#Yo me quedé. Era mi pueblo natal, y no quería abandonar a mis estudiantes.$2#$b#Pero sus familias tenían que irse, y era por su propio bien.$2#$b#El pueblo se volvió un desierto. También lo dejé, y perdí el contacto.$2#$e#Cuando llegué a la ciudad... era tan diferente. Y no podía encontrar un buen trabajo.$2\"/pause 500/speak devan \"En ese momento, escuché del valle.$7\"/pause 2000/speak Devan \"Cuando llegué, no tenía donde quedarme...de coincidencia, conocí a Gus.$7#$b#Me ofreció un cuarto en el que quedarme, a cambio de trabajar en el salón.#$b#Según él, muchas personas pasaron por lo mismo.\"/stopMusic/pause 500/playMusic spring_night_ambient/pause 2000/quickQuestion ¿Por qué viniste a este pueblo, @?#También quería escapar de la ciudad.#Era la granja de mi abuelo.#Quería dinero.(break)speak Devan \"Así que es verdad...este lugar aparece cuando más lo necesitas.$7\"\\friendship Devan 50(break)speak Devan \"¿Te dejó la granja? Tiene sentido.\"(break)speak Devan \"¿En serio? ¿Sólo por eso?$6#$b#Podrías ganar mucho más en la ciudad.\"\\friendship Devan -20(break)speak Devan \"Bueno, gracias por escucharme.\"/end");
                });
            if (e.Name.IsEquivalentTo("Data/Events/LeahHouse.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("110371000/F/f Devan 1000/p Leah", "jaunty/7 6/Leah 9 5 2 Devan 10 7 2 farmer 7 20 0/skippable/animate Leah false true 500 32 33 34/animate Devan false true 5000 20/pause 5000/speak Leah \"No te muevas. Ya casi termino.\"/speak Devan \"¿Cuánto queda? Me duelen los brazos...$2#$b#El río es tan calmante...creo que me va a adormecer.$1\"/speak Leah \"Si eso pasa, tendré que empezar de nuevo.$2\"/emote Devan 28/warp farmer 7 9/playSound doorClose/pause 500/speak Leah \"Hola, @. Disculpa... no puedo hablar ahora.$0#$b#Devan me está ayudando con una pose difícil que quería dibujar.\"/speak Devan \"Ey, sobre 'eso'...$2#$b#Lo que comisioné la semana pasada. ¿Cómo va el avance?$1\"/speak Leah \"Está listo.\"/stopAnimation Leah 35/pause 500/animate Leah false false 500 8 9 10 11/playSound pickUpItem/pause 1000/stopAnimation Leah 0/addObject 8 5 99/pause 100/stopAnimation Devan 8/pause 600/speak Devan \"...$6#$b#...Por el amor de Yoba. Es justo lo que quería. $1\"/speak Leah \"Cuando terminemos, puedes llevártelo a casa.\"/emote Devan 20/end");
                });
            if (e.Name.IsEquivalentTo("Data/Events/Saloon.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("110372000/f Devan 2000/p Devan/t 2000 2600", "Saloon1/7 20/Devan 3 19 2 Elliott 2 20 1 Gus 10 18 2 Emily 15 17 0 Clint 19 23 3 Shane 21 17 2 Willy 17 22 2 farmer 4 21 0/skippable/showFrame Devan 36/pause 3000/speak Devan \"...Y tuvimos que hacer eso.\"/pause 100/emote farmer 32/pause 500/speak Elliott \"Parece que te estás divirtiendo mucho...ese cangrejo casi me arrancó el pelo.$2#$b#Aunque tampoco te escapaste de eso.$1\"/pause 500/speak Devan \"...Bueno, no.$2#$b#Hace calor...iré afuera un rato.\"/question fork goWithDevan staywithElliott \"¿Quieres ir, @?#Iré contigo.#Me quedaré con Elliott.\"/fork goWithDevan 2000_goWithDevan/fork staywithElliott 2000_staywithElliott");

                    data.Add("2000_goWithDevan", "skippable/stopMusic/changeLocation Town/viewport 45 72/globalFadeToClear 2000/pause 500/warp farmer 45 71 2/playSound doorClose/move farmer 0 1/move farmer -2 0/faceDirection farmer 2 continue/pause 1000/warp Devan 45 71/move Devan 0 1/playSound doorClose/pause 1500/speak Devan \"Hey, @.#$e#Quería decirte algo.#$e#¿Recuerdas que te he contado del pueblo donde nací?$0#$b#Solíamos ser una comunidad unida. Las granjas ganaban dinero vendiendo sus productos, incluso a lugares lejanos.\"/pause 500/speak Devan \"Por eso, era normal ver trenes. A veces, mientras trabajaba, escuchaba su sonido.$0#$b#...La verdad, extraño trabajar en un colegio.$2#$b#Me pregunto cómo estarán mis ex alumnos, y sus familias...$7\"/pause 1000/speak Devan \"Cuando llegué, tenía mucho miedo. Ya había perdido en la ciudad, y no sabía si encajaría­. Los trenes eran mi único consuelo.$2#$b#Pero, @... últimamente, siento que soy parte de la comunidad.$7#$b#Has sido un amigo valioso para mí­.$1#$b#Amaba mi pueblo, y lo extrañaré...pero está bien disfrutar el presente. Este es mi hogar ahora.\"/emote Devan 20/speak Devan \"...Elliot se debe estar aburriendo, deberíamos volver.#$e#Oye, ¿Quieres una bebida?$6#$b#Yo invito.$1\"/end dialogue Devan \"...#$b#Aprecio mucho nuestra amistad, @.$1\"");

                    data.Add("2000_staywithElliott", "skippable/speak Devan \"Sure.#$b#I won't take too long.$1\"/pause 500/move Devan 3 0/move Devan 0 4/move Devan 8 0/move Devan 0 1/playSound doorClose/warp Devan -20 -20/pause 500/stopMusic/speak Elliott \"Presiento que tendremos tiempo para hablar.#$e#...@?$1#$b#Últimamente, me he percatado de algo intrigante.\"/pause 1000/speak Elliott \"...Al principio, Devan era bastante distante.#$b#Para ser sincero...demasiado distante.$2#$e#Excepto cuando hablaba con Leah. Tienen un gusto en común por el bosque.\"/pause 500/emote Elliott 40/pause 500/speak Elliott \"Sin embargo...\"/pause 300/speak Elliott \"Desde que te le acercaste, devan se ve más feliz.#$b#Creo que le has hecho sentirse en casa, en nuestro pequeño pueblo.\"/pause 800/speak Elliott \"Me alegro que se lleven bien.$1\"/pause 200/playSound doorClose/warp Devan 14 24/move Devan 0 -2/speak Devan \"Oigan- el viento afuera está fresquito.#$b#¿Me demoré mucho?#$e#...¿Por qué ponen esa cara?$6\"/pause 100/emote farmer 40/emote Elliott 40/pause 500/speak Elliott \"No es por nada. Estábamos hablando mientras te fuiste.#$b#Me gustaría contarle más a @ sobre la vez que intentamos atrapar cangrejos.\"/emote Devan 20/pause 200/move Devan 0 -1/move Devan -5 0/end");
                });
        }
        internal static void FesSpanish(AssetRequestedEventArgs e)
        {
            if (e.Name.IsEquivalentTo("Data/Festivals/spring13.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "¿Vas a participar en el festival?#$b#¿Yo? No. Prefiero mirar la competencia. Es más interesante.");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/spring24.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Mm...sabe exquisito.$8#$b#@, prueba un poco. De seguro te encanta.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/summer11.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "%Devan parece estar divirtiéndose con Emily.");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/summer28.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Medusas lunares... nunca había escuchado algo así.$0#$b#No puedo esperar a verlas.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/fall16.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Estas gallinas se ven muy bien cuidadas.$0#$b#Se nota que Marnie las quiere mucho.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/fall27.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Escuché que hicieron un laberinto imposible allí arriba...$0#$b#Quizás vaya a verlo más tarde.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/winter8.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "¿Lo mejor de este festival, dices...?$0#$b#Me gusta ver cómo decoran los muñecos de nieve.$0#$e#Pero especialmente, me gustan las esculturas de Leah.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/winter25.es-ES"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Ah, hola.$0#$b#¿Has probado los bastones de Gus? Le quedaron excelente.$0#$b#Se preparó desde antes para encontrar todos los ingredientes.$1");
                });
        }

        internal static void DialoguesEnglish(AssetRequestedEventArgs e, ModConfig Config)
        {
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Abigail") && Config.Allow_Abigail == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Ah, hi @.$0#$b#This weather is so warm...$0";
                    data["marriage_loc1"] = "Do you think we can explore this volcano?$0#$b#Willy said we shouldn't get close..$2#$b#But I still brought my sword.$1";
                    data["marriage_loc3"] = "Do you ever think of fighting these slimes, @?";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Alex") && Config.Allow_Alex == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Oh, hey @. I'm finally here.$0#$b#I can't wait to spend the day with you.$1";
                    data["marriage_loc1"] = "%Alex is lifting weights.";
                    data["marriage_loc3"] = "Nothing's better than the beach on a hot day like this.$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Elliott") && Config.Allow_Elliott == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Oh, @! That was quite the ride!$8#$b#It's a pleasure to see you again.$1";
                    data["marriage_loc1"] = "%Elliott is reading.";
                    data["marriage_loc3"] = "Ah, the view here is quite virtuous$0.#$b#I can feel an idea incoming...$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Emily") && Config.Allow_Emily == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hi, @!#$b#This island is full of parrots, it's lovely!";
                    data["marriage_loc1"] = "%Emily seems lost in her thoughts.";
                    data["marriage_loc3"] = "@, do you feel it?$0#$b#These crystals are full of powerful energy.$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Haley") && Config.Allow_Haley == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "@, can we go see the island today?$0#$b#There's something about this place that makes me feel at ease.";
                    data["marriage_loc1"] = "Hi, honey. I'm so glad you're here.$0#$b#I love how sunny the weather is.$1";
                    data["marriage_loc3"] = "%Haley is taking pictures.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Harvey") && Config.Allow_Harvey == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hi, honey.$l#$b#This is so much nicer than being cooped up in the clinic all day.$1";
                    data["marriage_loc1"] = "I'll stay inside for a while, @.$0#$b#Make sure you stay hydrated in this weather, alright?$1";
                    data["marriage_loc3"] = "%Harvey is engrossed in a book.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Krobus") && Config.Allow_Krobus == true)
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Good morning, @.$0#$b#The weather here is so damp, i feel right at home.$h#$e#And the sun is strong...$8";
                        data["marriage_loc1"] = "You said there were caves here...i'll visit them later.$0";
                        data["marriage_loc3"] = "I've never seen crystals like these!$0#$b#My people loved crystals, you know.$0#$e#...$2#$b#...You're right. Maybe there's others like me out there.#$b#Thank you, @.$h";
                    });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Leah") && Config.Allow_Leah == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "It's so quiet and peaceful here.$0#$b#It's the perfect weather for some wine.$1";
                    data["marriage_loc1"] = "The view here is inspiring, i just have to draw it.$0";
                    data["marriage_loc3"] = "%Leah is drawing.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Maru") && Config.Allow_Maru == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hey! What's up?$0#$b#I can't wait to see the stars at night.$1";
                    data["marriage_loc1"] = "@! Have you seen this?$9#$b#It seem there hasn't been volcanic activity in a while.";
                    data["marriage_loc3"] = "Professor Snail has been telling us about Ginger Island's traditions.$0#$b#Their oral history is amazing.$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Penny") && Config.Allow_Penny == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hi @.$h#$b#This island is so beautiful...";
                    data["marriage_loc1"] = "Oh, I'm reading the book about a sailor i told you about.$0#$b#It's very interesting.$1";
                    data["marriage_loc3"] = "Honey, Professor Snail's stories about the island are amazing.$0#$b#I didn't know they had so many traditions!$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Sam") && Config.Allow_Sam == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hi, @.$0#$b#It's a nice island... isn't it?$1";
                    data["marriage_loc1"] = "Hey... have you ever gone inside?$0#$b#...$0#$b#Really?$8";
                    data["marriage_loc3"] = "I'm feeling pretty good here.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Sebastian") && Config.Allow_Sebastian == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hey, I'm glad you're here.$h#$b#How have you been coping with this heat?";
                    data["marriage_loc1"] = "Man, this looks just like in 'Cave Saga X'...";
                    data["marriage_loc3"] = "I'm not one for crowded places.$0#$b#This place is perfect.$h";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Shane") && Config.Allow_Shane == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hey, @.$1#$b#The tropical plants here are great.$1";
                    data["marriage_loc1"] = "Charlie seems to like this place.$8";
                    data["marriage_loc3"] = "Hmm...this place isn't so bad.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Claire") && Config.Allow_Claire == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hello, dear.$0#$b#I'm ready for this island's hot weather!$1";
                    data["marriage_loc1"] = "%Claire is reading a screenplay.";
                    data["marriage_loc3"] = "The birds here...$0#$b#There's so many of them. I can hear them chirping.$1";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Lance") && Config.Allow_Lance == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hello, my dear. This house is just as cozy as our farmland's.";
                    data["marriage_loc1"] = "The caldera in this volcano holds inmense arcane power.#$b#Only the most powerful of mages can cast the spells it requires.";
                    data["marriage_loc3"] = "The gentle waves of this beach...I've become accustomed to their sound.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Wizard") && Config.Allow_Magnus == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hello, dearest! I'm overjoyed to see you.$1#$b#This place is quite wonderful. You've made the house quite cozy.";
                    data["marriage_loc1"] = "The creatures of this island are aware of our presence. It has alerted them.#$b#Their arcane power is inmense...";
                    data["marriage_loc3"] = "The forge in this place...the rituals it requires are only mastered by a handful.#$b#Even with such mastery, the results are near impossible to predict.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Olivia") && Config.Allow_Olivia == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hi, sweetie. How is this weather treating you?$0#$b#We should invite Victor here someday.";
                    data["marriage_loc1"] = "Sautine City might have some competition...this place is just unique.$1#$b#You remembered i wanted a house in this island...didn't you, dear?$4";
                    data["marriage_loc3"] = "My, the ocean in this island is so clear. I would love to paint it some day.";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Sophia") && Config.Allow_Sophia == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "@, can we stay here tonight?#$b#I'm really enjoying this trip.$0#$b#We can? Yay!$1";
                    data["marriage_loc1"] = "Oh, @! Look, there's some fishes in the water.$1#$b#They're nibbling at something.$1#$e#...H-hey, be careful if you go to that volcano over there, okay?$0";
                    data["marriage_loc3"] = "This? It's a manga i picked up the other day.$0#$b#Wanna read it together?$0";
                });
            if (e.Name.IsEquivalentTo("Characters/Dialogue/Victor") && Config.Allow_Victor == true)
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = "Hi, love. This beach is beautiful!#$b#Compared to Pelican Town, this weather is really hot...";
                    data["marriage_loc1"] = "So, this is the volcano dwarves used as an energy source...#$b#maybe we could go in someday.";
                    data["marriage_loc3"] = "Have you seen the bones here, @?#$b#These are quite ancient.";
                });
        }
        internal static void EventsEnglish(AssetRequestedEventArgs e)
        {
            if (e.Name.IsEquivalentTo("Data/Events/Railroad"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("11037500/F/f Devan 500/p Devan", "breezy/32 45/Devan 39 41 2 farmer 29 47 0/skippable/pause 500/move farmer 0 -6 0/move farmer 5 0 1 continue/viewport move 1 -1 3000/pause 500/emote Devan 16/pause 1000/quickQuestion @?#What are you doing?#Are you waiting for someone?#(break)speak Devan \"I'm waiting for a train to pass by$0.#$b#They don't show up often, but the wait is worth it.$0#$b#Like birdwatching, but with trains.\"(break)speak Devan \"No, not really.\"/speak Devan \"I'm waiting for a train to pass by$0.#$b#They don't show up often, but the wait is worth it.$0#$b#Like birdwatching, but with trains.\"/quickQuestion #Have you seen any so far?#What's the point, anyways?#That sounds boring.(break)speak Devan \"Two, up until now.$0#$b#The last train was full of coal, and some bits dropped as it passed by...$2#$b#So I hope to see the train better next time.\"\\friendship Devan 20(break)speak Devan \"These machines are amazing. Did you know? They can run on 4 types of energy: Horse-pulled, Steam, Diesel, and Electricity.$1#$b#I used to live in a location where you could only get by train.$0\"(break)speak Devan \"Well, what's the point in farming?$5\"\\friendship Devan -50/pause 1000/emote Devan 40/speak Devan \"...At any rate, you can stay if you want.$0#$b#But i'm warning you, something may fall when the train comes.$0#$e#I hope you have something to cover yourself with.$1\"/emote farmer 28/end");
                });
            if (e.Name.IsEquivalentTo("Data/Events/Forest"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("110371500/F/f Devan 1500/p Devan", "continue/34 25/Devan 34 25 2 farmer 33 25 2/skippable/pause 4000/speak Devan \"Hey, @.\"/pause 2000/speak Devan \"You know, i used to be a teacher there- at the town where i grew in. $0#$b#We were far from the big city, and sustained ourselves by farming.$0#$b#...Then things changed.$2\"/stopMusic/pause 500/playMusic desolate/speak Devan \"With time, the our water source dried up. People started moving out, because farming wasn't possible anymore...$2#$b#I stayed. I didn't want to leave my students behind, but their families needed to. And it was for their sake, too.#$b#When I moved to the city, I lost contact. It was hard to find a new job, and it all began to pile up.$2\"/pause 500/speak devan \"Then, I heard of the valley.$7\"/pause 2000/speak Devan \"When i arrived, i didn't have where to stay...but by some coincidence, i met Gus. He offered a spare room i could stay at- in exchange of working there.#$b#He said, many people had the same experience as i did.\"/stopMusic/pause 500/playMusic spring_night_ambient/pause 2000/quickQuestion Why did you move here, @?#I wanted to escape the city, too.#It was my grandfather's farm.#I wanted money.(break)speak Devan \"I see. So it's true...it's as if this place appears when you most need it.$7\"\\friendship Devan 50(break)speak Devan \"So, he left it to you? Makes sense.\"(break)speak Devan \"Really? That's it? $6#$b#You could make much more money at the city.\"\\friendship Devan -20(break)speak Devan \"Anyways, thanks for hearing my story.\"/end");
                });
            if (e.Name.IsEquivalentTo("Data/Events/LeahHouse"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("110371000/F/f Devan 1000/p Leah", "jaunty/7 6/Leah 9 5 2 Devan 10 7 2 farmer 7 20 0/skippable/animate Leah false true 500 32 33 34/animate Devan false true 5000 20/pause 5000/speak Leah \"Stay still. I'm almost done.\"/speak Devan \"How longer? My arms are starting to hurt...$2#$b#The river sounds so nice...i might fall asleep.$1\"/speak Leah \"If you do that, i'll have to start over.$2\"/emote Devan 28/warp farmer 7 9/playSound doorClose/pause 500/speak Leah \"Hello, @. Sorry... I'm busy right now.$0#$b#Devan is helping me with a difficult pose i want to draw. I was having a hard time with this one.\"/speak Devan \"Hey, so about 'that'...$2#$b#What i commissioned last week. How is progress coming along?$1\"/speak Leah \"It's done. It came out just like you asked.\"/stopAnimation Leah 35/pause 500/animate Leah false false 500 8 9 10 11/playSound pickUpItem/pause 1000/stopAnimation Leah 0/addObject 8 5 99/pause 100/stopAnimation Devan 8/pause 600/speak Devan \"...$6#$b#...I swear to Yoba, it's amazing. It looks just like what i had in mind. $1\"/speak Leah \"After we're done here, you can take it home.\"/emote Devan 20/end");
                });
            if (e.Name.IsEquivalentTo("Data/Events/Saloon"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("110372000/f Devan 2000/p Devan/t 2000 2600", "Saloon1/7 20/Devan 3 19 2 Elliott 2 20 1 Gus 10 18 2 Emily 15 17 0 Clint 19 23 3 Shane 21 17 2 Willy 17 22 2 farmer 4 21 0/skippable/showFrame Devan 36/pause 3000/speak Devan \"...Yeah, and that's what we did.\"/pause 100/emote farmer 32/pause 500/speak Elliott \"You seem a bit too amused...that crab almost tore my hair off.$2#$b#Though, you didn't escape their claws either.$1\"/speak Devan \"It's a bit hot...i'll go outside for a bit.\"/question fork goWithDevan staywithElliott \"@, do you want to come with me?#I'll go with you.#I'll stay with Elliott.\"/fork goWithDevan 2000_goWithDevan/fork staywithElliott 2000_staywithElliott");

                    data.Add("2000_goWithDevan", "skippable/stopMusic/changeLocation Town/viewport 45 72/globalFadeToClear 2000/pause 500/warp farmer 45 71 2/playSound doorClose/move farmer 0 1/move farmer -2 0/faceDirection farmer 2 continue/pause 1000/warp Devan 45 71/move Devan 0 1/playSound doorClose/pause 1500/speak Devan \"Hey, @.#$e#I wanted to tell you something.#$e#You know...the town where i grew wasnt always so lonely.$0#$b#We used to be a tight knit community. The farms made money selling their goods, and would sell them to many places in Ferngill.\"/pause 500/speak Devan \"It was normal to see trains there. Sometimes, while i'd work at the school, i'd hear its whistle as it passed by.$0#$b#...Truth be told, i miss being teacher.$2#$b#I wonder if those kids, and their families, found a better place...$7\"/pause 1000/speak Devan \"Sitting to watch for trains calmed me down. It reminded me of home. I had no idea if i'd fit here, and i was scared­.$2#$b#But, @... nowadays, i feel i'm part of the community.$7#$b#You've been a valuable friend to me­.$1#$b#I loved the town i grew in, and i'll miss it... but it's okay to move on, too. This is my home now.\"/emote Devan 20/speak Devan \"...Elliot must be getting bored. We should head back.#$e#Hey, do you want a drink?$6#$b#My treat.$1\"/end dialogue Devan \"...#$b#I really appreciate you, @.$1\"");

                    data.Add("2000_staywithElliott", "skippable/speak Devan \"Sure.#$b#I won't take too long.$1\"/pause 500/move Devan 3 0/move Devan 0 4/move Devan 8 0/move Devan 0 1/playSound doorClose/warp Devan -20 -20/pause 500/stopMusic/speak Elliott \"I get the feeling we'll have a while to talk.#$e#...@?$1#$b#Lately, i've noticed something quite intriguing.\"/pause 1000/speak Elliott \"...At first, Devan didn't talk much with our neighbors.#$b#They seemed...off, to be honest. Quite too distant.$2#$e#Except when Leah is around. Then, Devan loosens up a bit.\"/pause 500/emote Elliott 40/pause 500/speak Elliott \"However...\"/pause 300/speak Elliott \"Ever since you two became friends, Devan seems happier.#$b#You've made Devan feel at home here.\"/pause 800/speak Elliott \"It's quite refreshing.$1\"/pause 200/playSound doorClose/[make devan visible]/move Devan 0 -2/speak Devan \"Hey, the breeze outside is very refreshing.#$b#Did i take too long?#$e#...Hey, what's with that face?$6\"/pause 100/emote farmer 40/emote Elliott 40/pause 500/speak Elliott \"Not much. We were making small talk while waiting.#$b#I'd like to continue telling @ about when we tried the crab pots by ourselves.\"/emote Devan 20/pause 200/move Devan 0 -1/move Devan -5 0/end");
                });
        }
        internal static void FesEnglish(AssetRequestedEventArgs e)
        {
            if (e.Name.IsEquivalentTo("Data/Festivals/spring13"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Will you participate in the festival?#$b#Me? Not really. I prefer watching everyone compete. It's more exciting.");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/spring24"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Mm...delicious.$8#$b#@, try some. I bet you'll love it.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/summer11"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "%Devan seems to be having fun with Emily.");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/summer28"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Moonlight jellies... i'd never heard of that.$0#$b#Can't wait to see them.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/fall16"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "These chickens look very well cared for...$0#$b#It shows that Marnie loves them very much.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/fall27"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Ive heard they made an impossible labyrinth over there...$0#$b#Maybe i'll go check it out later.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/winter8"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "The best of this festival, you say...?$0#$b#I love to see everyone's snowmen.$0#$e#But, specially, i like Leah's ice sculptures.$1");
                });
            if (e.Name.IsEquivalentTo("Data/Festivals/winter25"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data.Add("Devan", "Ah, hello.$0#$b#Have you tried Gus' candy canes? They turned out great.$0#$b#He prepared for a while to find the best ingredients.$1");
                });
        }
    }
}