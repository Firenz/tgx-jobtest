# Programming test for BambooHR
This is a programming test for a Junior Backend position in C# for BambooHR.

The code is mostly written in C#, since it is the coding language which I am more comfortable.

The problem to solve how it follows (in Spanish, TODO translating it to English):

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