# FileProcessor

# Descripción

Procesa ficheros con columnas de tamaño fijo y separa la informacion en base a un campo clave en el mismo.

## Funcionamiento

El parseado del fichero se basa en un fichero xml, alli se pueden definir los tipos de registro, campos y otros detalles. Ahora solo maneja 1 fichero de [schema](schemas/CONTABIL.xml) pero esta pensado principalmente para que pueda manejar varios tipos.

Los requisitos pedian que fuera facil poder hacer modificaciones en los registros a tener en cuenta. Uno de los planteamientos fue hacerlo desde el codigo (eje: T4 templates, por injeccion, etc). Sin embargo este fue el sistema que a modo global da mas facilidad para lo que se pedia.

### Requisitos

- DotNet Core 3.1
- El resto esta todo configurado para descargarse con el proyecto
