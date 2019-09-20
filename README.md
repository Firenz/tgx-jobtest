# Programming test for BambooHR
This is a programming test for a Junior Backend position in C# for BambooHR.

The code is mostly written in C#, since it is the coding language which I am more comfortable.

The problem to solve how it follows (in Spanish, TODO translating it to English):

>## Descripción del problema
>El FBI a capturado algunos informes sobre la Mafia que datan desde 1985 hasta día de hoy. Quieren usar estos datos para poder mapear la organización de la mafia y así poner todos los recursos sobre los miembros mas importantes. Tenemos que seguir de cerca quien reporta a quien en la organización ya que el jefe tiene a mas de 50 personas a su cargo.
>
>Durante estos años ha habido reestructuraciones, asesinatos y encarcelamientos. Basándonos en investigaciones anteriores, sabemos como funciona la mafia cuando ocurre uno de estos eventos:
>
>* Cuando un miembro de la organización va a la cárcel, desaparece temporalmente de la organización. Todos sus subordinados directos se reubican inmediatamente y ahora trabajan para el jefe más antiguo que queda al mismo nivel que su jefe anterior. Si no existe tal jefe alternativo posible, se promociona al subordinado directo más antiguo del jefe anterior para que sea el jefe de los demás.
>
>* Cuando el miembro encarcelado sale de la cárcel, recupera inmediatamente su puesto (se le asigna el mismo jefe y se le asignan sus subordinados antiguos). 
>
>Es necesario realizar un programa donde se tenga en cuenta los siguientes puntos.
>
>* Se debe de gestionar la reubicacion de los subordinados cuando su jefe entra en la cárcel
>* Se debe gestionar cuales son los jefes (4 subordinados o mas de manera recursiva)
>* Se debe de gestionar la recuperación de los subordinados si un miembro sale de la cárcel
>
>### Pistas:
> - Se debe de tener una clase Miembro que tenga:
>   - Lista de subordinados
>   - Superior
>   -  Nombre del miembros
>   -  Antiguedad
> - Se debe de tener funciones publicas get/set que accedan a los atributos (estos deben ser privados)
> - Se debe de tener una clase Carcel que tenga los siguientes métodos:
>   - entrar: Se le pasa un miembro
>   - salir: Devuelve un miembro
> - Se debe de tener una clase principal donde se realice el siguiente flujo:
>   - Se crean el listado de los miembros a partir del JSON que se dan con esta prueba
>   - Se debe de crear una instancia de la clase cárcel, luego se debe de meter al miembro ‘Jhon’ en la cárcel y reubicar sus subordinados.
>   - Se debe de imprimir por pantalla el organigrama de los miembros después de este ingreso en prisión
>   - Se debe liberar de la cárcel a ‘Jhon’ y reorganizar sus subordinados.
>   - Se debe de imprimir por pantalla el organigrama de los miembros después de esta liberación
>   - Se debe de imprimir por pantalla al más alto cargo de la banda.
>
>### PLUS:
> - Se debe de encerrar en la cárcel al jefe de la banda
> - Se debe de sacar un nuevo jefe de los subordinados y reubicar estos en el nuevo jefe
> - Se debe liberar de la cárcel al jefe de la banda
> - Se debe reubicar sus subordinados que tenía antes de entrar en la carcel

