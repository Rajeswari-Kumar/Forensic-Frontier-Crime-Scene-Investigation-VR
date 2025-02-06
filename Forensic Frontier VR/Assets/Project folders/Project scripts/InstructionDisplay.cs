using System.Collections;
using TMPro;
using UnityEngine;

public class InstructionDisplay : MonoBehaviour
{
    public string Instructions;
    public TMP_Text Instruction_text;

    private void Start()
    {
        DisplayInstructions(Instructions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DisplayInstructions(Instructions);
        }
    }

    public void DisplayInstructions(string instructions)
    {
        Instruction_text.text = Instructions.ToString();
    }
}