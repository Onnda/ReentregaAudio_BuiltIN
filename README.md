Examen de Audio Apuntes

1 - Music
Add each track to the corresponding scene so that they start playing when the scene loads and keep playing looped.

Load Screen
Unity:
Click derecho -> Audio -> Audio Source -> “Intro Music” -> Cojes la Main menu de musica y la metes en “AudioClip” en el hierarchy -> Play on awake -> Loop
Repites con cada escena


Optimize the Audio Clip sizes in memory and loading time as much as
possible.

.ogg para que la musica se optimice, para esto vas a audacity y los exportas en este formato (solo la musica que es el apartado).

Audacity
Parametros de exportacion: Ogg / Estereo / 44100Hz / 16bit / Calidad medio / Multiple archivos / pistas / y a la derecha la primera opción también

Unity:
Musica -> Load Type Streaming (solo puede haber un audio en streaming) -> pero los pasas todos

Lo dejas abajo del todo en la main scene y lo pones abajo en la esquina.
Después en las demás como son solo pantallas se ponen y ya.

Add a new group to the MainAudioMixer for music to set the Output of all music Audio Sources and adjust the overall music volume to -12 dB so it doesn’t interfere too much with the SFX.

FPS - Audio - Mixer -> 
Crear uno nuevo - > + -> “Music” -> -12dB -> A cada musica que hemos puesto se le pone el musicmixer 
Esto a cada escena otra vez
2 - SFX

UI

The IntroMenu buttons have no sound effects when clicked which isn’t quite good for feedback. You’ve got the UI Minimize.wav effect. Create a maximize effect by reversing the UI Maximize.wav one with Audacity.

Mainscene (gameplay) -> Canvas 

Audacity:
Pones el audio de la UI -> Seleccionas todo el audio -> Efecto -> Especial -> Revertir -> Esto lo exportas en .WAV 

Lo pasas a Unity.



This one will be used by the ControlsButton object. To add the sound, add an Audio Source to the scene and edit the ToggleGameObjectButton script to play the UI Maximize.wav sound. Make the Audio Source a variable accessible through the Inspector by making it public or Serializable so you’ll be able to assign the Audio Source from another object. You can ignore the active parameter.

Creas dos Audio Source -> Le quitas el Play on awake -> le metes el audio file en el AudioClip.

Pones uno para el Maximize y otro para el Minimize.

Dentro del Canvas -> Buscas el boton ControlsButton -> Para el script ToogleGameObjectButton -> Creas public AudioSource Maximize y lo mismo con Minimize -> dentro de la funcion SetGameOBjectActive ->
if (active) {Maximiza.PLay();} else{Minizime.Play();}

public void SetGameObjectActive(bool active)
{
    ObjectToToggle.SetActive(active);

    if (ResetSelectionAfterClick)
        EventSystem.current.SetSelectedGameObject(null);

    if (active) maximizeSFX.Play();
    else minimizeSFX.Play();
}

Unity:

Arrastrar los Objects a los dos Audios del script. Asegurate que el Minimize con el minimize y el maxi con el maxi.





Do the same with the CloseButton within the ControlsImage object to play the UI Minimize.wav effect when that button is pressed.

Comprovar el CloseButton que tenga bien los maxi y mini.


Do the same with the StartButton object to play the UI Select.wav effect
when that button is pressed. In this case you’ll need to play it at the
LoadSceneButton script.

Vas al script LoadSceneButton -> Creas public AudioSource LoadSceneAudio; -> en la funcion LoadTargetScene() -> LoadSceneAudio.Play(); 

   public class LoadSceneButton : MonoBehaviour
    {
        public string SceneName = "";

        [SerializeField] private AudioSource selectSFX;
	  
   //More code que no tienes que tocar	

        public void LoadTargetScene()
        {
            selectSFX.Play();
            SceneManager.LoadScene(SceneName);
        }
    }

Unity:
Audio Source -> General / Ui Select / Fuera playonawake 

StartButton -> Load Scene -> Select que acabamos de crear







Now it should be easy to copy the Audio Source to the other UI screens
(LoseScene and WinScene) and add the UI Select.wav for all the remaining buttons. If you create new Audio Sources, then make sure that their Output is connected to the General group.

Copias el AudioSource de la UI screen y lo pegas en la lose y la win. 
Menu Button -> Pones el audio source select 

PLAYER

The Footstep, Jump and Land sounds will be too soft now, amplify them 12 dB in Audacity and import them back.

Audacity:
Pones los audios -> Seleccionas los tres -> Efecto -> Volumen y conversión -> Amplificar -> 12dB -> Aplicar

Exportas en wav a la carpeta nuevos audios.

En el player de la main scene -> buscas donde sea que tiene los sfx y los reemplazas.


There’s only a single Footstep sound. The sound is played within the
PlayerCharacterController.cs script, line 348. Randomize its volume and
pitch.

En el script del player -> en la linea que nos indica -> para randomizar:

if (m_FootstepDistanceCounter >= 1f / chosenFootstepSfxFrequency)
{
    m_FootstepDistanceCounter = 0f;

    AudioSource.pitch = Random.Range(0.8f, 1.2f);
    AudioSource.PlayOneShot(FootstepSfx, Random.Range(0.5f, 1.0f));
}

ENEMY_HoverBot

The HoverBot enemy has no impact SFX. You have been provided HoverBot Impact.wav to use it there. Open the enemy prefab. You’ll find all animations in the BasicRobot object shown in the screenshot.


HoverBot prefab -> BasicRobot -> add component -> HurtSound -> Borras el start (lo dejas vacío) -> Nuevo audio source + funcion para que se escuche


public class HurtSound : MonoBehaviour
{
    [SerializeField] private AudioSource hurtSFX;

    public void PlayHurtSound()
    {
        if (hurtSFX != null)
        {
            hurtSFX.Play();
        }
    }
}

Add component ( audio source) -> le arrastras el sonido → y le quitas el play on awake (pq es un SFX)

Animation -> HoverBot carpeta -> Animator (HurtWritable) -> doble click -> Click derecho -> Animation event -> Function -> Funcion del script “PlayHurtSound”

Enemies -> AudioSource-> poner en Output el EnemyMovement mixer

Script para cambiar Snapshot. Hay que ponerlo en el Player
using UnityEngine.Audio;

[RequireComponent(typeof(Collider))]
public class SnapshotChanger : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot m_insideSnapshot;
    [SerializeField] private AudioMixerSnapshot m_outsideSnapshot;

    [SerializeField] private float m_transitionTime = 0.5f;

    private enum SnapshotState { Inside, Outside }
    private SnapshotState m_currentState = SnapshotState.Inside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inside") && m_currentState != SnapshotState.Inside)
        {
            m_insideSnapshot.TransitionTo(m_transitionTime);
            m_currentState = SnapshotState.Inside;
        }
        else if (other.CompareTag("Outside") && m_currentState != SnapshotState.Outside)
        {
            m_outsideSnapshot.TransitionTo(m_transitionTime);
            m_currentState = SnapshotState.Outside;
        }
    }
}



















Ambience
Remove that Audio Source from the GameManager.
Remove -> Audio source del wind

Now we’ll create an environment of 3D Audio Sources using the same Wind sound. Create the first one wherever you want, assign the Output to Ambient. Once you’ve got it setup, copy it as many times as you need to make the sound noticeable near the holes and platform borders where the player could fall. We need the sound to provide this kind of warning feeling.


Añadir el Wind SFX ir poniendo por el mapa con el Spacial Blend al 1, cuanto mas te acercas mas suena y cuanto mas te alejas menos.
En sitios donde te puedas caer.

En el Output se le pone el Mixer Ambient.























Reverb

Las indoor con reverb, las outdoors no.
Audio Mixer -> Snapchot le cambias el nombre a indoor y lo duplicas y le pones outdoor.
En el indoor - Add SFX Reverb -> Parametro Room a 0 -> Hacerlo en lo que pone el enunciado.

El outdoor tiene el sfx reverb pues en el player está en -10000 es que esta bien.

Empty box collider y pones dos tags uno que sea indoor y outdoor.
El de dentro es indoor y el de fuera outdoor.
Lo juntas en un empty “Doors”

Vas al player -> script -> Quitas el start y el update -> arriba pones using UnityEngine.Audio -> public AudioMixerSnapshot inSnapshot; + public AudioMixerSnapshot outSnapshot;

Creas una funcion:





Pero queda para cuando mueras no se te queden ahi se supone que pones algo en el codigo y pues eso. ASIGNALO! 
Si en el start asignas que sea indoor, cada vez que abres se pondrá indoor automáticamente y cuando mueras, cargara la escena, ira a start y se pondrá indoor.  
Pues para asignarlo vas al player al ultimo script y pones el indoor y el outdoor.



