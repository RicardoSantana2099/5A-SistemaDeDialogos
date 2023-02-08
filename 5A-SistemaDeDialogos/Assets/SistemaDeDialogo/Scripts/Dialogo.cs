using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    public GameObject Admiracion; //El signo de admiraci�n para hscer la alerta.
    public GameObject Mouse; //El mouse que explica como activar.
    private bool isPlayerInRange;//Esta me va a indicar si el jugador esta cerca del rango.
    private bool didDialogueStart;//Nos va a indicar que el dialogo comenzo.
    private int lineIndex;//Este nos va a mostrar que linea de dialogo nos esta mostrando.
    public float typingTime = 0.05f; //Tiempo de los caracteres.

    public GameObject PanelDeDialogo; //Para tener una referencia al panel del dialogo.
    public TMP_Text TextoDeDialogo; //Para tener una referencia al texto de la ui.
    [SerializeField, TextArea(4, 6)]private string[] dialogueLines; //Aqui vamos a escribir lo que va a salir en el dialogo.

   
    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Fire1")) //El dialogo va a comenzar cuando el player este en el area y presiona la letra E
        {
            if(!didDialogueStart)
            {
                StartDialogue();
            }
            else if(TextoDeDialogo.text == dialogueLines[lineIndex]) //Si esta mostrando toda la frase completa pasamos a la siguiente linea.
            {
                NextDialogueLine();
            }
            else //Podre detener el dialogo y adelantarlo.
            {
                StopAllCoroutines();
                TextoDeDialogo.text = dialogueLines[lineIndex];
            }
        }
    }

    void StartDialogue()
    {
        didDialogueStart = true; //Le estamos diciendo que ya comenzo el dialogo.
        PanelDeDialogo.SetActive(true); //Se activa el panel para mostrar el texto.
        Admiracion.SetActive(false);//Se desactiva el signo de admiraci�n cuando esta el dialogo.
        Mouse.SetActive(false);//Se desactiva el mouse cuando esta el dialogo.
        lineIndex = 0; // Para que siempre que reactivemos el dialogo comience de 0.
        Time.timeScale = 0f; //Todo se detiene completamente mientras esta el dialogo.

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()//Para poder pasar a la siguiente linea de dialogo.
    {
        lineIndex++; //Esto me ayuda a pasar a la siguiente linea.
        if(lineIndex < dialogueLines.Length) //si este es menor quiere decir que aun no llegamos a la ultima linea.
        {
            StartCoroutine(ShowLine());
        }
        else //Si no hay mas lineas el dialogo ah terminado.
        {
            didDialogueStart = false;
            PanelDeDialogo.SetActive(false);
            Admiracion.SetActive(true);//Se activa el signo de admiraci�n.
            Mouse.SetActive(true);//Se activa el mouse.
            Time.timeScale = 1f; // Regresa el juego a la normalidad una vez acabado el dialogo.
        }
    }
    private IEnumerator ShowLine()
    {
        TextoDeDialogo.text = string.Empty; //El texto va a comenzar vacio

        foreach(char ch in dialogueLines[lineIndex])//Se va a ir escribiendo caracter por caracter.
        {
            TextoDeDialogo.text += ch;
            yield return new WaitForSecondsRealtime(typingTime); //El tiempo de letra en letra.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Cuando pasen por el trigger este se va a activar.
    {
        if(collision.gameObject.CompareTag("Player")) // Solo el que tenga el tag de player lo puede activar.
        {
            isPlayerInRange = true;
            Admiracion.SetActive(true);//Se activa el simbolo de admiraci�n al momento de estar cerca de la misi�n.
            Mouse.SetActive(true);//Se activa el mouse al momento de estar cerca de la misi�n.      
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision) //Cuando salga del trigger este se va a desactivar.
    {
        if (collision.gameObject.CompareTag("Player")) // Solo el que tenga el tag de player lo puede desactivar
        {
            isPlayerInRange = false;
            Admiracion.SetActive(false);//Se desactiva el simbolo de admiraci�n al momento de alejarse de la misi�n.
            Mouse.SetActive(false);//Se desactiva el mouse.
        }
    }
}
